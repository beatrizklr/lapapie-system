using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Registro, consulta e cancelamento de vendas.</summary>
    public class VendaDAO
    {
        /// <summary>
        /// Registra a venda em UMA transação:
        /// <list type="number">
        /// <item>calcula quanto sai de cada produto simples (kits são "desmontados" nos componentes);</item>
        /// <item>trava as linhas de estoque (SELECT ... FOR UPDATE) e confere se há saldo;</item>
        /// <item>grava a venda e os itens;</item>
        /// <item>dá baixa no estoque e registra as movimentações de SAÍDA.</item>
        /// </list>
        /// Se algo falhar, nada é gravado (rollback).
        /// </summary>
        /// <returns>Número (id) da venda gerada.</returns>
        public int RegistrarVenda(Venda venda)
        {
            var erros = venda.Validar();
            if (erros.Count > 0)
                throw new RegraNegocioException(string.Join(Environment.NewLine, erros));

            venda.RecalcularTotais();
            venda.DataVenda = DateTime.Now;

            return DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                // 1) Quantidade a baixar por produto simples
                var baixas = CalcularBaixas(conexao, transacao, venda.Itens);

                // 2) Trava e confere o saldo
                ConferirSaldos(conexao, transacao, baixas);

                // 3) Cabeçalho da venda (valor_total é coluna gerada pelo MySQL)
                using (var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO vendas (cliente_id, usuario_id, data_venda, valor_subtotal, valor_desconto, forma_pagamento, status, observacao)
                      VALUES (@cliente, @usuario, @data, @subtotal, @desconto, @forma, 'CONCLUIDA', @obs)", transacao))
                {
                    cmd.Parameters.AddWithValue("@cliente", DaoUtil.Nulo(venda.ClienteId));
                    cmd.Parameters.AddWithValue("@usuario", venda.UsuarioId);
                    cmd.Parameters.AddWithValue("@data", venda.DataVenda);
                    cmd.Parameters.AddWithValue("@subtotal", venda.ValorSubtotal);
                    cmd.Parameters.AddWithValue("@desconto", venda.ValorDesconto);
                    cmd.Parameters.AddWithValue("@forma", EnumBanco.ParaBanco(venda.FormaPagamento));
                    cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(venda.Observacao));
                    cmd.ExecuteNonQuery();
                    venda.Id = (int)cmd.LastInsertedId;
                }

                // 4) Itens (N:N venda × produto)
                using (var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO itens_venda (venda_id, produto_id, quantidade, preco_unitario, desconto)
                      VALUES (@venda, @produto, @qtd, @preco, @desconto)", transacao))
                {
                    foreach (var item in venda.Itens)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@venda", venda.Id);
                        cmd.Parameters.AddWithValue("@produto", item.ProdutoId);
                        cmd.Parameters.AddWithValue("@qtd", item.Quantidade);
                        cmd.Parameters.AddWithValue("@preco", item.PrecoUnitario);
                        cmd.Parameters.AddWithValue("@desconto", item.Desconto);
                        cmd.ExecuteNonQuery();
                        item.VendaId = venda.Id;
                    }
                }

                // 5) Baixa automática no estoque + histórico
                foreach (var (produtoId, quantidade) in baixas)
                {
                    EstoqueDAO.AlterarSaldo(conexao, transacao, produtoId, -quantidade);
                    EstoqueDAO.InserirMovimentacao(conexao, transacao, new MovimentacaoEstoque
                    {
                        ProdutoId = produtoId,
                        UsuarioId = venda.UsuarioId,
                        Tipo = TipoMovimentacao.Saida,
                        Motivo = MotivoMovimentacao.Venda,
                        Quantidade = quantidade,
                        VendaId = venda.Id,
                        Observacao = $"Venda nº {venda.Id}",
                        DataMovimentacao = venda.DataVenda
                    });
                }
                return venda.Id;
            });
        }

        /// <summary>Soma por produto simples quanto vai sair do estoque (expande os kits).</summary>
        private static Dictionary<int, int> CalcularBaixas(MySqlConnection conexao, MySqlTransaction transacao, List<ItemVenda> itens)
        {
            var baixas = new Dictionary<int, int>();
            void Somar(int produtoId, int qtd) => baixas[produtoId] = baixas.GetValueOrDefault(produtoId) + qtd;

            // Confere no banco o tipo de cada produto (não confiamos só no que veio da tela)
            var tipos = new Dictionary<int, (TipoProduto Tipo, bool Ativo, string Nome)>();
            using (var cmd = DaoUtil.Comando(conexao, "", transacao))
            {
                var lista = DaoUtil.ParametrosIn(cmd, "p", itens.Select(i => i.ProdutoId));
                cmd.CommandText = $"SELECT id, tipo, ativo, nome FROM produtos WHERE id IN ({lista})";
                foreach (var linha in DaoUtil.Listar(cmd, r => (Id: r.Inteiro("id"),
                             Tipo: EnumBanco.DoBanco<TipoProduto>(r.Texto("tipo")), Ativo: r.Logico("ativo"), Nome: r.Texto("nome"))))
                    tipos[linha.Id] = (linha.Tipo, linha.Ativo, linha.Nome);
            }

            foreach (var item in itens)
            {
                if (!tipos.TryGetValue(item.ProdutoId, out var info))
                    throw new RegraNegocioException($"O produto \"{item.ProdutoNome}\" não existe mais.");
                if (!info.Ativo)
                    throw new RegraNegocioException($"O produto \"{info.Nome}\" está inativo e não pode ser vendido.");

                if (info.Tipo == TipoProduto.Kit)
                {
                    var componentes = ProdutoDAO.ListarComponentes(conexao, item.ProdutoId, transacao);
                    if (componentes.Count == 0)
                        throw new RegraNegocioException($"O kit \"{info.Nome}\" não tem composição cadastrada.");
                    foreach (var componente in componentes)
                        Somar(componente.ProdutoId, componente.Quantidade * item.Quantidade);
                }
                else
                {
                    Somar(item.ProdutoId, item.Quantidade);
                }
            }
            return baixas;
        }

        /// <summary>Trava as linhas de estoque e verifica se há saldo para todas as baixas.</summary>
        private static void ConferirSaldos(MySqlConnection conexao, MySqlTransaction transacao, Dictionary<int, int> baixas)
        {
            using var cmd = DaoUtil.Comando(conexao, "", transacao);
            var lista = DaoUtil.ParametrosIn(cmd, "p", baixas.Keys);
            // ORDER BY produto_id: travar sempre na mesma ordem evita deadlock entre caixas.
            cmd.CommandText = $@"SELECT p.id, p.nome, COALESCE(e.quantidade, 0) AS saldo
                                 FROM produtos p LEFT JOIN estoque e ON e.produto_id = p.id
                                 WHERE p.id IN ({lista}) ORDER BY p.id FOR UPDATE";
            var saldos = DaoUtil.Listar(cmd, r => (Id: r.Inteiro("id"), Nome: r.Texto("nome"), Saldo: r.Inteiro("saldo")));

            var faltas = saldos.Where(s => s.Saldo < baixas[s.Id])
                               .Select(s => $"• {s.Nome}: precisa de {baixas[s.Id]}, há {s.Saldo}")
                               .ToList();
            if (faltas.Count > 0)
                throw new RegraNegocioException("Estoque insuficiente:" + Environment.NewLine + string.Join(Environment.NewLine, faltas));
        }

        /// <summary>
        /// Cancela a venda e devolve ao estoque exatamente o que saiu
        /// (lido das movimentações da própria venda — assim kits também voltam certo).
        /// </summary>
        public void CancelarVenda(int vendaId, int usuarioId, string motivo)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using (var cmd = DaoUtil.Comando(conexao, "SELECT status FROM vendas WHERE id = @id FOR UPDATE", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", vendaId);
                    var status = cmd.ExecuteScalar() as string;
                    if (status == null) throw new RegraNegocioException("Venda não encontrada.");
                    if (status == "CANCELADA") throw new RegraNegocioException("Esta venda já está cancelada.");
                }

                List<(int ProdutoId, int Quantidade)> saidas;
                using (var cmd = DaoUtil.Comando(conexao,
                    @"SELECT produto_id, SUM(quantidade) AS qtd FROM movimentacoes_estoque
                      WHERE venda_id = @id AND tipo = 'SAIDA' AND motivo = 'VENDA'
                      GROUP BY produto_id ORDER BY produto_id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", vendaId);
                    saidas = DaoUtil.Listar(cmd, r => (r.Inteiro("produto_id"), r.Inteiro("qtd")));
                }

                foreach (var (produtoId, quantidade) in saidas)
                {
                    EstoqueDAO.AlterarSaldo(conexao, transacao, produtoId, quantidade);
                    EstoqueDAO.InserirMovimentacao(conexao, transacao, new MovimentacaoEstoque
                    {
                        ProdutoId = produtoId,
                        UsuarioId = usuarioId,
                        Tipo = TipoMovimentacao.Entrada,
                        Motivo = MotivoMovimentacao.Devolucao,
                        Quantidade = quantidade,
                        VendaId = vendaId,
                        Observacao = $"Cancelamento da venda nº {vendaId}"
                    });
                }

                using (var cmd = DaoUtil.Comando(conexao,
                    "UPDATE vendas SET status = 'CANCELADA', observacao = @obs WHERE id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", vendaId);
                    cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(motivo?.Length > 255 ? motivo[..255] : motivo));
                    cmd.ExecuteNonQuery();
                }
            });
        }

        /// <summary>Histórico de vendas (view vw_historico_vendas) com filtros.</summary>
        public List<HistoricoVenda> ListarHistorico(DateTime inicio, DateTime fim, StatusVenda? status, string texto)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT h.*, v.observacao
                            FROM vw_historico_vendas h
                            JOIN vendas v ON v.id = h.venda_id
                            WHERE h.data_venda >= @inicio AND h.data_venda < @fim";
                cmd.Parameters.AddWithValue("@inicio", inicio.Date);
                cmd.Parameters.AddWithValue("@fim", fim.Date.AddDays(1));
                if (status.HasValue)
                {
                    sql += " AND h.status = @status";
                    cmd.Parameters.AddWithValue("@status", EnumBanco.ParaBanco(status.Value));
                }
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (h.cliente LIKE @texto OR h.vendedor LIKE @texto OR h.venda_id = @numero)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    cmd.Parameters.AddWithValue("@numero", int.TryParse(texto.Trim().TrimStart('#'), out var n) ? n : -1);
                }
                cmd.CommandText = sql + " ORDER BY h.data_venda DESC";
                return DaoUtil.Listar(cmd, MapearHistorico);
            });
        }

        internal static HistoricoVenda MapearHistorico(MySqlDataReader r) => new()
        {
            VendaId = r.Inteiro("venda_id"),
            DataVenda = r.Data("data_venda"),
            Cliente = r.Texto("cliente"),
            Vendedor = r.Texto("vendedor"),
            FormaPagamento = EnumBanco.DoBanco<FormaPagamento>(r.Texto("forma_pagamento")),
            Status = EnumBanco.DoBanco<StatusVenda>(r.Texto("status")),
            TotalItens = r.Inteiro("total_itens"),
            ValorSubtotal = r.Valor("valor_subtotal"),
            ValorDesconto = r.Valor("valor_desconto"),
            ValorTotal = r.Valor("valor_total"),
            Observacao = r.Texto("observacao")
        };

        /// <summary>Venda completa com itens (para detalhe e comprovante).</summary>
        public Venda ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                Venda venda;
                using (var cmd = DaoUtil.Comando(conexao,
                    @"SELECT v.*, COALESCE(c.nome, 'Consumidor não identificado') AS cliente_nome, u.nome AS usuario_nome
                      FROM vendas v
                      JOIN usuarios u ON u.id = v.usuario_id
                      LEFT JOIN clientes c ON c.id = v.cliente_id
                      WHERE v.id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    venda = DaoUtil.Listar(cmd, r => new Venda
                    {
                        Id = r.Inteiro("id"),
                        ClienteId = r.InteiroOuNulo("cliente_id"),
                        UsuarioId = r.Inteiro("usuario_id"),
                        DataVenda = r.Data("data_venda"),
                        ValorSubtotal = r.Valor("valor_subtotal"),
                        ValorDesconto = r.Valor("valor_desconto"),
                        FormaPagamento = EnumBanco.DoBanco<FormaPagamento>(r.Texto("forma_pagamento")),
                        Status = EnumBanco.DoBanco<StatusVenda>(r.Texto("status")),
                        Observacao = r.Texto("observacao"),
                        ClienteNome = r.Texto("cliente_nome"),
                        UsuarioNome = r.Texto("usuario_nome")
                    }).FirstOrDefault();
                }
                if (venda == null) return null;

                using (var cmd = DaoUtil.Comando(conexao,
                    @"SELECT iv.*, p.nome, p.tipo FROM itens_venda iv
                      JOIN produtos p ON p.id = iv.produto_id
                      WHERE iv.venda_id = @id ORDER BY p.nome"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    venda.Itens = DaoUtil.Listar(cmd, r => new ItemVenda
                    {
                        VendaId = r.Inteiro("venda_id"),
                        ProdutoId = r.Inteiro("produto_id"),
                        Quantidade = r.Inteiro("quantidade"),
                        PrecoUnitario = r.Valor("preco_unitario"),
                        Desconto = r.Valor("desconto"),
                        ProdutoNome = r.Texto("nome"),
                        TipoProduto = EnumBanco.DoBanco<TipoProduto>(r.Texto("tipo"))
                    });
                }
                return venda;
            });
        }

        /// <summary>Últimas vendas (dashboard).</summary>
        public List<HistoricoVenda> ListarUltimas(int quantidade)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"SELECT h.*, NULL AS observacao FROM vw_historico_vendas h
                      ORDER BY h.data_venda DESC LIMIT @limite");
                cmd.Parameters.AddWithValue("@limite", quantidade);
                return DaoUtil.Listar(cmd, MapearHistorico);
            });
        }
    }
}
