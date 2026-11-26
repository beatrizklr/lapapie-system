using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>
    /// Acesso à tabela produtos e às tabelas que dependem dela no cadastro:
    /// estoque (1:1), kit_itens (composição dos kits) e fornecedor_produto (N:N).
    /// </summary>
    public class ProdutoDAO
    {
        // Saldo: para SIMPLES vem da tabela estoque; para KIT vem da view de disponibilidade.
        private const string SelectBase =
            @"SELECT p.*, c.nome AS categoria_nome,
                     e.quantidade AS estoque_quantidade, e.estoque_minimo, e.localizacao, e.atualizado_em AS estoque_atualizado_em,
                     dk.quantidade_disponivel AS kit_disponivel
              FROM produtos p
              JOIN categorias c ON c.id = p.categoria_id
              LEFT JOIN estoque e ON e.produto_id = p.id
              LEFT JOIN vw_disponibilidade_kits dk ON dk.kit_id = p.id";

        private static Produto Mapear(MySqlDataReader r)
        {
            var produto = new Produto
            {
                Id = r.Inteiro("id"),
                CategoriaId = r.Inteiro("categoria_id"),
                CategoriaNome = r.Texto("categoria_nome"),
                CodigoBarras = r.Texto("codigo_barras"),
                Nome = r.Texto("nome"),
                Descricao = r.Texto("descricao"),
                Marca = r.Texto("marca"),
                Tipo = EnumBanco.DoBanco<TipoProduto>(r.Texto("tipo")),
                UnidadeMedida = r.Texto("unidade_medida"),
                PrecoVenda = r.Valor("preco_venda"),
                Ativo = r.Logico("ativo"),
                CriadoEm = r.Data("criado_em"),
                AtualizadoEm = r.Data("atualizado_em"),
                EstoqueMinimo = r.Inteiro("estoque_minimo")
            };
            produto.QuantidadeDisponivel = produto.EhKit ? r.Inteiro("kit_disponivel") : r.Inteiro("estoque_quantidade");

            if (!produto.EhKit && r.InteiroOuNulo("estoque_quantidade").HasValue)
            {
                produto.Estoque = new Estoque
                {
                    ProdutoId = produto.Id,
                    Quantidade = r.Inteiro("estoque_quantidade"),
                    EstoqueMinimo = r.Inteiro("estoque_minimo"),
                    Localizacao = r.Texto("localizacao"),
                    AtualizadoEm = r.Data("estoque_atualizado_em")
                };
            }
            return produto;
        }

        internal static FornecedorProduto MapearFornecedorProduto(MySqlDataReader r) => new()
        {
            FornecedorId = r.Inteiro("fornecedor_id"),
            ProdutoId = r.Inteiro("produto_id"),
            CodigoNoFornecedor = r.Texto("codigo_no_fornecedor"),
            PrecoCusto = r.Valor("preco_custo"),
            PrazoEntregaDias = r.InteiroOuNulo("prazo_entrega_dias"),
            FornecedorNome = r.Texto("fornecedor_nome"),
            ProdutoNome = r.Texto("produto_nome")
        };

        /// <summary>
        /// Pesquisa de produtos com filtros opcionais.
        /// </summary>
        /// <param name="texto">Parte do nome, marca ou código de barras.</param>
        /// <param name="categoriaId">Filtro por categoria (null = todas).</param>
        /// <param name="tipo">SIMPLES, KIT ou null para ambos.</param>
        /// <param name="somenteAtivos">Oculta produtos inativos.</param>
        /// <param name="comPromocoes">Também carrega a melhor promoção vigente de cada produto.</param>
        public List<Produto> Pesquisar(string texto, int? categoriaId = null, TipoProduto? tipo = null,
                                       bool somenteAtivos = true, bool comPromocoes = false)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = SelectBase + " WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (p.nome LIKE @texto OR p.marca LIKE @texto OR p.codigo_barras = @codigo)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    cmd.Parameters.AddWithValue("@codigo", texto.Trim());
                }
                if (categoriaId.HasValue)
                {
                    sql += " AND p.categoria_id = @categoria";
                    cmd.Parameters.AddWithValue("@categoria", categoriaId.Value);
                }
                if (tipo.HasValue)
                {
                    sql += " AND p.tipo = @tipo";
                    cmd.Parameters.AddWithValue("@tipo", EnumBanco.ParaBanco(tipo.Value));
                }
                if (somenteAtivos) sql += " AND p.ativo = TRUE";
                cmd.CommandText = sql + " ORDER BY p.nome";

                var produtos = DaoUtil.Listar(cmd, Mapear);
                if (comPromocoes)
                    AplicarPromocoes(conexao, produtos);
                return produtos;
            });
        }

        /// <summary>Busca exata pelo código de barras (leitor no caixa).</summary>
        public Produto ObterPorCodigoBarras(string codigo)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, SelectBase + " WHERE p.codigo_barras = @codigo AND p.ativo = TRUE");
                cmd.Parameters.AddWithValue("@codigo", codigo.Trim());
                var produtos = DaoUtil.Listar(cmd, Mapear);
                AplicarPromocoes(conexao, produtos);
                return produtos.FirstOrDefault();
            });
        }

        /// <summary>Carrega o produto completo: dados, estoque, composição do kit e fornecedores.</summary>
        public Produto ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                Produto produto;
                using (var cmd = DaoUtil.Comando(conexao, SelectBase + " WHERE p.id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    produto = DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
                }
                if (produto == null) return null;

                produto.ItensKit = ListarComponentes(conexao, id);
                using (var cmd = DaoUtil.Comando(conexao,
                    @"SELECT fp.*, p.nome AS produto_nome, COALESCE(f.nome_fantasia, f.razao_social) AS fornecedor_nome
                      FROM fornecedor_produto fp
                      JOIN fornecedores f ON f.id = fp.fornecedor_id
                      JOIN produtos p     ON p.id = fp.produto_id
                      WHERE fp.produto_id = @id
                      ORDER BY fp.preco_custo"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    produto.Fornecedores = DaoUtil.Listar(cmd, MapearFornecedorProduto);
                }
                AplicarPromocoes(conexao, new List<Produto> { produto });
                return produto;
            });
        }

        /// <summary>Componentes de um kit, com preço e saldo de cada um.</summary>
        public List<KitItem> ListarComponentes(int kitId) => DaoUtil.Executar(c => ListarComponentes(c, kitId));

        internal static List<KitItem> ListarComponentes(MySqlConnection conexao, int kitId, MySqlTransaction transacao = null)
        {
            using var cmd = DaoUtil.Comando(conexao,
                @"SELECT ki.kit_id, ki.produto_id, ki.quantidade, p.nome, p.preco_venda, COALESCE(e.quantidade, 0) AS saldo
                  FROM kit_itens ki
                  JOIN produtos p ON p.id = ki.produto_id
                  LEFT JOIN estoque e ON e.produto_id = ki.produto_id
                  WHERE ki.kit_id = @kit
                  ORDER BY p.nome", transacao);
            cmd.Parameters.AddWithValue("@kit", kitId);
            return DaoUtil.Listar(cmd, r => new KitItem
            {
                KitId = r.Inteiro("kit_id"),
                ProdutoId = r.Inteiro("produto_id"),
                Quantidade = r.Inteiro("quantidade"),
                ProdutoNome = r.Texto("nome"),
                PrecoUnitario = r.Valor("preco_venda"),
                EstoqueDisponivel = r.Inteiro("saldo")
            });
        }

        /// <summary>
        /// Escolhe, para cada produto, a promoção vigente que dá o MAIOR desconto
        /// (caso o produto participe de mais de uma ao mesmo tempo).
        /// </summary>
        private static void AplicarPromocoes(MySqlConnection conexao, List<Produto> produtos)
        {
            if (produtos.Count == 0) return;
            var vigentes = PromocaoDAO.ListarVigentesPorProduto(conexao, DateTime.Now)
                                      .ToLookup(v => v.ProdutoId, v => v.Promocao);
            foreach (var produto in produtos)
            {
                produto.PromocaoVigente = vigentes[produto.Id]
                    .OrderByDescending(p => p.CalcularDesconto(produto.PrecoVenda, 1))
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Cadastra o produto. Em uma única transação grava: produto, linha de estoque
        /// (se SIMPLES), saldo inicial como movimentação, composição do kit e fornecedores.
        /// </summary>
        public int Inserir(Produto produto, int estoqueInicial, int usuarioId)
        {
            return DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using (var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO produtos (categoria_id, codigo_barras, nome, descricao, marca, tipo, unidade_medida, preco_venda, ativo)
                      VALUES (@categoria, @codigo, @nome, @descricao, @marca, @tipo, @unidade, @preco, @ativo)", transacao))
                {
                    PreencherParametros(cmd, produto);
                    cmd.Parameters.AddWithValue("@tipo", EnumBanco.ParaBanco(produto.Tipo));
                    cmd.ExecuteNonQuery();
                    produto.Id = (int)cmd.LastInsertedId;
                }

                if (!produto.EhKit)
                {
                    var estoque = produto.Estoque ?? new Estoque();
                    using (var cmd = DaoUtil.Comando(conexao,
                        "INSERT INTO estoque (produto_id, quantidade, estoque_minimo, localizacao) VALUES (@id, @qtd, @minimo, @local)", transacao))
                    {
                        cmd.Parameters.AddWithValue("@id", produto.Id);
                        cmd.Parameters.AddWithValue("@qtd", Math.Max(0, estoqueInicial));
                        cmd.Parameters.AddWithValue("@minimo", estoque.EstoqueMinimo);
                        cmd.Parameters.AddWithValue("@local", DaoUtil.Nulo(estoque.Localizacao));
                        cmd.ExecuteNonQuery();
                    }

                    // O saldo inicial também entra no histórico: estoque = soma das movimentações.
                    if (estoqueInicial > 0)
                    {
                        EstoqueDAO.InserirMovimentacao(conexao, transacao, new MovimentacaoEstoque
                        {
                            ProdutoId = produto.Id,
                            UsuarioId = usuarioId,
                            Tipo = TipoMovimentacao.Entrada,
                            Motivo = MotivoMovimentacao.Ajuste,
                            Quantidade = estoqueInicial,
                            Observacao = "Saldo inicial no cadastro do produto"
                        });
                    }
                }

                GravarComposicaoKit(conexao, transacao, produto);
                GravarFornecedores(conexao, transacao, produto);
                return produto.Id;
            });
        }

        /// <summary>Atualiza o produto e substitui composição do kit e fornecedores.</summary>
        public void Atualizar(Produto produto)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using (var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE produtos SET categoria_id = @categoria, codigo_barras = @codigo, nome = @nome, descricao = @descricao,
                             marca = @marca, unidade_medida = @unidade, preco_venda = @preco, ativo = @ativo
                      WHERE id = @id", transacao))
                {
                    PreencherParametros(cmd, produto);
                    cmd.Parameters.AddWithValue("@id", produto.Id);
                    cmd.ExecuteNonQuery();
                }

                if (!produto.EhKit && produto.Estoque != null)
                {
                    // Só os parâmetros mudam aqui. O SALDO só muda por movimentação.
                    using var cmd = DaoUtil.Comando(conexao,
                        @"INSERT INTO estoque (produto_id, quantidade, estoque_minimo, localizacao) VALUES (@id, 0, @minimo, @local)
                          ON DUPLICATE KEY UPDATE estoque_minimo = @minimo, localizacao = @local", transacao);
                    cmd.Parameters.AddWithValue("@id", produto.Id);
                    cmd.Parameters.AddWithValue("@minimo", produto.Estoque.EstoqueMinimo);
                    cmd.Parameters.AddWithValue("@local", DaoUtil.Nulo(produto.Estoque.Localizacao));
                    cmd.ExecuteNonQuery();
                }

                GravarComposicaoKit(conexao, transacao, produto);
                GravarFornecedores(conexao, transacao, produto);
            });
        }

        private static void PreencherParametros(MySqlCommand cmd, Produto p)
        {
            cmd.Parameters.AddWithValue("@categoria", p.CategoriaId);
            cmd.Parameters.AddWithValue("@codigo", DaoUtil.Nulo(p.CodigoBarras));
            cmd.Parameters.AddWithValue("@nome", p.Nome.Trim());
            cmd.Parameters.AddWithValue("@descricao", DaoUtil.Nulo(p.Descricao));
            cmd.Parameters.AddWithValue("@marca", DaoUtil.Nulo(p.Marca));
            cmd.Parameters.AddWithValue("@unidade", p.UnidadeMedida.Trim().ToUpperInvariant());
            cmd.Parameters.AddWithValue("@preco", p.PrecoVenda);
            cmd.Parameters.AddWithValue("@ativo", p.Ativo);
        }

        private static void GravarComposicaoKit(MySqlConnection conexao, MySqlTransaction transacao, Produto produto)
        {
            using (var apagar = DaoUtil.Comando(conexao, "DELETE FROM kit_itens WHERE kit_id = @id", transacao))
            {
                apagar.Parameters.AddWithValue("@id", produto.Id);
                apagar.ExecuteNonQuery();
            }
            if (!produto.EhKit) return;

            using var inserir = DaoUtil.Comando(conexao,
                "INSERT INTO kit_itens (kit_id, produto_id, quantidade) VALUES (@kit, @produto, @qtd)", transacao);
            inserir.Parameters.AddWithValue("@kit", produto.Id);
            inserir.Parameters.Add("@produto", MySqlDbType.UInt32);
            inserir.Parameters.Add("@qtd", MySqlDbType.UInt32);
            foreach (var item in produto.ItensKit)
            {
                inserir.Parameters["@produto"].Value = item.ProdutoId;
                inserir.Parameters["@qtd"].Value = item.Quantidade;
                inserir.ExecuteNonQuery();
            }
        }

        private static void GravarFornecedores(MySqlConnection conexao, MySqlTransaction transacao, Produto produto)
        {
            using (var apagar = DaoUtil.Comando(conexao, "DELETE FROM fornecedor_produto WHERE produto_id = @id", transacao))
            {
                apagar.Parameters.AddWithValue("@id", produto.Id);
                apagar.ExecuteNonQuery();
            }

            using var inserir = DaoUtil.Comando(conexao,
                @"INSERT INTO fornecedor_produto (fornecedor_id, produto_id, codigo_no_fornecedor, preco_custo, prazo_entrega_dias)
                  VALUES (@fornecedor, @produto, @codigo, @custo, @prazo)", transacao);
            foreach (var fp in produto.Fornecedores)
            {
                inserir.Parameters.Clear();
                inserir.Parameters.AddWithValue("@fornecedor", fp.FornecedorId);
                inserir.Parameters.AddWithValue("@produto", produto.Id);
                inserir.Parameters.AddWithValue("@codigo", DaoUtil.Nulo(fp.CodigoNoFornecedor));
                inserir.Parameters.AddWithValue("@custo", fp.PrecoCusto);
                inserir.Parameters.AddWithValue("@prazo", DaoUtil.Nulo(fp.PrazoEntregaDias));
                inserir.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Exclusão física. Só é possível para produtos sem histórico
        /// (vendas, compras ou movimentações); caso contrário use <see cref="Inativar"/>.
        /// </summary>
        public void Excluir(int id)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                // Composição do kit não tem ON DELETE CASCADE, então removemos antes.
                using (var cmd = DaoUtil.Comando(conexao, "DELETE FROM kit_itens WHERE kit_id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = DaoUtil.Comando(conexao, "DELETE FROM produtos WHERE id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public void Inativar(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "UPDATE produtos SET ativo = FALSE WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Gera um código de barras interno (prefixo 789 + sequência) ainda não usado.</summary>
        public string SugerirCodigoBarras()
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    "SELECT MAX(CAST(codigo_barras AS UNSIGNED)) FROM produtos WHERE codigo_barras LIKE '7891%' AND LENGTH(codigo_barras) = 13");
                var maior = cmd.ExecuteScalar();
                long baseNumero = maior == DBNull.Value || maior == null ? 789100000000L : Convert.ToInt64(maior) / 10 + 1;
                string doze = baseNumero.ToString().PadLeft(12, '0')[..12];
                // Dígito verificador EAN-13
                int soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += (doze[i] - '0') * (i % 2 == 0 ? 1 : 3);
                int dv = (10 - soma % 10) % 10;
                return doze + dv;
            });
        }
    }
}
