using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>
    /// Controle de estoque: saldos (tabela estoque), histórico (movimentacoes_estoque)
    /// e as consultas prontas (views) de estoque baixo e disponibilidade de kits.
    /// </summary>
    public class EstoqueDAO
    {
        private static Estoque MapearSaldo(MySqlDataReader r) => new()
        {
            ProdutoId = r.Inteiro("produto_id"),
            Quantidade = r.Inteiro("quantidade"),
            EstoqueMinimo = r.Inteiro("estoque_minimo"),
            Localizacao = r.Texto("localizacao"),
            AtualizadoEm = r.Data("atualizado_em"),
            ProdutoNome = r.Texto("produto_nome"),
            CodigoBarras = r.Texto("codigo_barras"),
            CategoriaNome = r.Texto("categoria_nome"),
            UnidadeMedida = r.Texto("unidade_medida")
        };

        /// <summary>Saldo atual dos produtos SIMPLES ativos.</summary>
        public List<Estoque> ListarSaldos(string texto, int? categoriaId, SituacaoEstoque? situacao)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT e.*, p.nome AS produto_nome, p.codigo_barras, p.unidade_medida, c.nome AS categoria_nome
                            FROM estoque e
                            JOIN produtos p   ON p.id = e.produto_id
                            JOIN categorias c ON c.id = p.categoria_id
                            WHERE p.ativo = TRUE AND p.tipo = 'SIMPLES'";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (p.nome LIKE @texto OR p.codigo_barras = @codigo OR e.localizacao LIKE @texto)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    cmd.Parameters.AddWithValue("@codigo", texto.Trim());
                }
                if (categoriaId.HasValue)
                {
                    sql += " AND p.categoria_id = @categoria";
                    cmd.Parameters.AddWithValue("@categoria", categoriaId.Value);
                }
                if (situacao == SituacaoEstoque.Zerado) sql += " AND e.quantidade = 0";
                else if (situacao == SituacaoEstoque.Baixo) sql += " AND e.quantidade <= e.estoque_minimo";
                else if (situacao == SituacaoEstoque.Normal) sql += " AND e.quantidade > e.estoque_minimo";

                cmd.CommandText = sql + " ORDER BY p.nome";
                return DaoUtil.Listar(cmd, MapearSaldo);
            });
        }

        /// <summary>Altera estoque mínimo e localização (não mexe no saldo).</summary>
        public void AtualizarParametros(int produtoId, int estoqueMinimo, string localizacao)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    "UPDATE estoque SET estoque_minimo = @minimo, localizacao = @local WHERE produto_id = @id");
                cmd.Parameters.AddWithValue("@id", produtoId);
                cmd.Parameters.AddWithValue("@minimo", estoqueMinimo);
                cmd.Parameters.AddWithValue("@local", DaoUtil.Nulo(localizacao));
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>
        /// Registra uma entrada ou saída MANUAL (ajuste, devolução, perda).
        /// Saldo e histórico são alterados juntos, em transação.
        /// </summary>
        public void RegistrarMovimentacao(MovimentacaoEstoque movimentacao)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                // Trava a linha do produto: se duas pessoas movimentarem ao mesmo tempo,
                // a segunda espera a primeira terminar e lê o saldo já atualizado.
                int? saldoAtual;
                using (var cmd = DaoUtil.Comando(conexao,
                    "SELECT quantidade FROM estoque WHERE produto_id = @id FOR UPDATE", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", movimentacao.ProdutoId);
                    var valor = cmd.ExecuteScalar();
                    saldoAtual = valor == null ? null : Convert.ToInt32(valor);
                }

                if (saldoAtual == null)
                    throw new RegraNegocioException("Este produto não controla estoque (kits usam o saldo dos componentes).");

                int delta = movimentacao.QuantidadeComSinal;
                if (saldoAtual + delta < 0)
                    throw new RegraNegocioException(
                        $"Saldo insuficiente: há {saldoAtual} unidade(s) e a saída é de {movimentacao.Quantidade}.");

                AlterarSaldo(conexao, transacao, movimentacao.ProdutoId, delta);
                InserirMovimentacao(conexao, transacao, movimentacao);
            });
        }

        /// <summary>Soma (ou subtrai, se negativo) "delta" ao saldo do produto.</summary>
        internal static void AlterarSaldo(MySqlConnection conexao, MySqlTransaction transacao, int produtoId, int delta)
        {
            using var cmd = DaoUtil.Comando(conexao,
                "UPDATE estoque SET quantidade = quantidade + @delta WHERE produto_id = @id", transacao);
            cmd.Parameters.AddWithValue("@delta", delta);
            cmd.Parameters.AddWithValue("@id", produtoId);
            if (cmd.ExecuteNonQuery() == 0)
            {
                // Produto simples sem linha de estoque (cadastro antigo): cria a linha.
                using var inserir = DaoUtil.Comando(conexao,
                    "INSERT INTO estoque (produto_id, quantidade) VALUES (@id, @qtd)", transacao);
                inserir.Parameters.AddWithValue("@id", produtoId);
                inserir.Parameters.AddWithValue("@qtd", delta);
                inserir.ExecuteNonQuery();
            }
        }

        /// <summary>Grava uma linha no histórico de movimentações (sempre dentro de uma transação).</summary>
        internal static void InserirMovimentacao(MySqlConnection conexao, MySqlTransaction transacao, MovimentacaoEstoque m)
        {
            using var cmd = DaoUtil.Comando(conexao,
                @"INSERT INTO movimentacoes_estoque
                      (produto_id, usuario_id, tipo, motivo, quantidade, venda_id, pedido_compra_id, observacao, data_movimentacao)
                  VALUES (@produto, @usuario, @tipo, @motivo, @qtd, @venda, @pedido, @obs, @data)", transacao);
            cmd.Parameters.AddWithValue("@produto", m.ProdutoId);
            cmd.Parameters.AddWithValue("@usuario", m.UsuarioId);
            cmd.Parameters.AddWithValue("@tipo", EnumBanco.ParaBanco(m.Tipo));
            cmd.Parameters.AddWithValue("@motivo", EnumBanco.ParaBanco(m.Motivo));
            cmd.Parameters.AddWithValue("@qtd", m.Quantidade);
            cmd.Parameters.AddWithValue("@venda", DaoUtil.Nulo(m.VendaId));
            cmd.Parameters.AddWithValue("@pedido", DaoUtil.Nulo(m.PedidoCompraId));
            cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(m.Observacao));
            cmd.Parameters.AddWithValue("@data", m.DataMovimentacao);
            cmd.ExecuteNonQuery();
            m.Id = cmd.LastInsertedId;
        }

        /// <summary>Histórico de movimentações com filtros.</summary>
        public List<MovimentacaoEstoque> ListarMovimentacoes(DateTime inicio, DateTime fim, string textoProduto,
                                                              TipoMovimentacao? tipo, MotivoMovimentacao? motivo)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT m.*, p.nome AS produto_nome, u.nome AS usuario_nome
                            FROM movimentacoes_estoque m
                            JOIN produtos p ON p.id = m.produto_id
                            JOIN usuarios u ON u.id = m.usuario_id
                            WHERE m.data_movimentacao >= @inicio AND m.data_movimentacao < @fim";
                cmd.Parameters.AddWithValue("@inicio", inicio.Date);
                cmd.Parameters.AddWithValue("@fim", fim.Date.AddDays(1));
                if (!Validacao.Vazio(textoProduto))
                {
                    sql += " AND (p.nome LIKE @texto OR p.codigo_barras = @codigo OR m.observacao LIKE @texto)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(textoProduto));
                    cmd.Parameters.AddWithValue("@codigo", textoProduto.Trim());
                }
                if (tipo.HasValue)
                {
                    sql += " AND m.tipo = @tipo";
                    cmd.Parameters.AddWithValue("@tipo", EnumBanco.ParaBanco(tipo.Value));
                }
                if (motivo.HasValue)
                {
                    sql += " AND m.motivo = @motivo";
                    cmd.Parameters.AddWithValue("@motivo", EnumBanco.ParaBanco(motivo.Value));
                }
                // O índice idx_mov_produto_data ajuda nas consultas por produto e período.
                cmd.CommandText = sql + " ORDER BY m.data_movimentacao DESC, m.id DESC";
                return DaoUtil.Listar(cmd, r => new MovimentacaoEstoque
                {
                    Id = Convert.ToInt64(r["id"]),
                    ProdutoId = r.Inteiro("produto_id"),
                    UsuarioId = r.Inteiro("usuario_id"),
                    Tipo = EnumBanco.DoBanco<TipoMovimentacao>(r.Texto("tipo")),
                    Motivo = EnumBanco.DoBanco<MotivoMovimentacao>(r.Texto("motivo")),
                    Quantidade = r.Inteiro("quantidade"),
                    VendaId = r.InteiroOuNulo("venda_id"),
                    PedidoCompraId = r.InteiroOuNulo("pedido_compra_id"),
                    Observacao = r.Texto("observacao"),
                    DataMovimentacao = r.Data("data_movimentacao"),
                    ProdutoNome = r.Texto("produto_nome"),
                    UsuarioNome = r.Texto("usuario_nome")
                });
            });
        }

        /// <summary>Consulta a view vw_produtos_estoque_baixo.</summary>
        public List<ProdutoEstoqueBaixo> ListarEstoqueBaixo()
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"SELECT v.*, e.localizacao
                      FROM vw_produtos_estoque_baixo v
                      JOIN estoque e ON e.produto_id = v.produto_id
                      ORDER BY (v.quantidade = 0) DESC, v.deficit DESC, v.produto");
                return DaoUtil.Listar(cmd, r => new ProdutoEstoqueBaixo
                {
                    ProdutoId = r.Inteiro("produto_id"),
                    Produto = r.Texto("produto"),
                    Categoria = r.Texto("categoria"),
                    Quantidade = r.Inteiro("quantidade"),
                    EstoqueMinimo = r.Inteiro("estoque_minimo"),
                    Deficit = r.Inteiro("deficit"),
                    Localizacao = r.Texto("localizacao")
                });
            });
        }

        public int ContarEstoqueBaixo()
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "SELECT COUNT(*) FROM vw_produtos_estoque_baixo");
                return Convert.ToInt32(cmd.ExecuteScalar());
            });
        }

        /// <summary>
        /// Todos os kits com a disponibilidade calculada pela view vw_disponibilidade_kits,
        /// mais o valor que os componentes custariam avulsos (para mostrar a economia).
        /// </summary>
        public List<DisponibilidadeKit> ListarKits(bool somenteAtivos)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"SELECT k.id, k.nome, k.preco_venda, k.ativo,
                             COALESCE(dk.quantidade_disponivel, 0) AS disponivel,
                             COALESCE(SUM(ki.quantidade * c.preco_venda), 0) AS valor_avulso,
                             COUNT(ki.produto_id) AS componentes
                      FROM produtos k
                      LEFT JOIN kit_itens ki ON ki.kit_id = k.id
                      LEFT JOIN produtos c   ON c.id = ki.produto_id
                      LEFT JOIN vw_disponibilidade_kits dk ON dk.kit_id = k.id
                      WHERE k.tipo = 'KIT'" + (somenteAtivos ? " AND k.ativo = TRUE" : "") + @"
                      GROUP BY k.id, k.nome, k.preco_venda, k.ativo, dk.quantidade_disponivel
                      ORDER BY k.nome");
                return DaoUtil.Listar(cmd, r => new DisponibilidadeKit
                {
                    KitId = r.Inteiro("id"),
                    Kit = r.Texto("nome"),
                    PrecoVenda = r.Valor("preco_venda"),
                    Ativo = r.Logico("ativo"),
                    QuantidadeDisponivel = r.Inteiro("disponivel"),
                    ValorAvulso = r.Valor("valor_avulso"),
                    QuantidadeComponentes = r.Inteiro("componentes")
                });
            });
        }
    }
}
