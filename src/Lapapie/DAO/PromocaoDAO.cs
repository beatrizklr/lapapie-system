using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Acesso às tabelas promocoes e promocao_produto (N:N).</summary>
    public class PromocaoDAO
    {
        private static Promocao Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            Nome = r.Texto("nome"),
            Descricao = r.Texto("descricao"),
            TipoDesconto = EnumBanco.DoBanco<TipoDesconto>(r.Texto("tipo_desconto")),
            ValorDesconto = r.Valor("valor_desconto"),
            DataInicio = r.Data("data_inicio"),
            DataFim = r.Data("data_fim"),
            Ativo = r.Logico("ativo"),
            CriadoEm = r.Data("criado_em")
        };

        public List<Promocao> Pesquisar(string texto)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT pr.*, (SELECT COUNT(*) FROM promocao_produto pp WHERE pp.promocao_id = pr.id) AS qtd_produtos
                            FROM promocoes pr WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (pr.nome LIKE @texto OR pr.descricao LIKE @texto)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                }
                cmd.CommandText = sql + " ORDER BY pr.data_inicio DESC";
                return DaoUtil.Listar(cmd, r =>
                {
                    var p = Mapear(r);
                    p.QuantidadeProdutos = r.Inteiro("qtd_produtos");
                    return p;
                });
            });
        }

        public Promocao ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                Promocao promocao;
                using (var cmd = DaoUtil.Comando(conexao, "SELECT * FROM promocoes WHERE id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    promocao = DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
                }
                if (promocao == null) return null;

                using (var cmd = DaoUtil.Comando(conexao, "SELECT produto_id FROM promocao_produto WHERE promocao_id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    promocao.ProdutoIds = DaoUtil.Listar(cmd, r => r.Inteiro("produto_id"));
                }
                promocao.QuantidadeProdutos = promocao.ProdutoIds.Count;
                return promocao;
            });
        }

        /// <summary>
        /// Promoções vigentes na data informada, por produto. Um produto pode estar em
        /// mais de uma promoção ao mesmo tempo; quem escolhe a melhor é o ProdutoDAO.
        /// </summary>
        public static List<(int ProdutoId, Promocao Promocao)> ListarVigentesPorProduto(MySqlConnection conexao, DateTime referencia)
        {
            using var cmd = DaoUtil.Comando(conexao,
                @"SELECT pp.produto_id, pr.*
                  FROM promocao_produto pp
                  JOIN promocoes pr ON pr.id = pp.promocao_id
                  WHERE pr.ativo = TRUE AND @agora BETWEEN pr.data_inicio AND pr.data_fim");
            cmd.Parameters.AddWithValue("@agora", referencia);
            return DaoUtil.Listar(cmd, r => (r.Inteiro("produto_id"), Mapear(r)));
        }

        /// <summary>Promoções vigentes agora (usado no dashboard).</summary>
        public List<Promocao> ListarVigentes()
        {
            return Pesquisar(null).Where(p => p.Situacao == SituacaoPromocao.Vigente).ToList();
        }

        /// <summary>Insere a promoção e seus produtos na mesma transação.</summary>
        public int Inserir(Promocao promocao)
        {
            return DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO promocoes (nome, descricao, tipo_desconto, valor_desconto, data_inicio, data_fim, ativo)
                      VALUES (@nome, @descricao, @tipo, @valor, @inicio, @fim, @ativo)", transacao);
                PreencherParametros(cmd, promocao);
                cmd.ExecuteNonQuery();
                promocao.Id = (int)cmd.LastInsertedId;

                GravarProdutos(conexao, transacao, promocao);
                return promocao.Id;
            });
        }

        public void Atualizar(Promocao promocao)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE promocoes SET nome = @nome, descricao = @descricao, tipo_desconto = @tipo, valor_desconto = @valor,
                             data_inicio = @inicio, data_fim = @fim, ativo = @ativo
                      WHERE id = @id", transacao);
                PreencherParametros(cmd, promocao);
                cmd.Parameters.AddWithValue("@id", promocao.Id);
                cmd.ExecuteNonQuery();

                GravarProdutos(conexao, transacao, promocao);
            });
        }

        /// <summary>Exclui a promoção (promocao_produto é apagada em cascata pelo banco).</summary>
        public void Excluir(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "DELETE FROM promocoes WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        private static void PreencherParametros(MySqlCommand cmd, Promocao p)
        {
            cmd.Parameters.AddWithValue("@nome", p.Nome.Trim());
            cmd.Parameters.AddWithValue("@descricao", DaoUtil.Nulo(p.Descricao));
            cmd.Parameters.AddWithValue("@tipo", EnumBanco.ParaBanco(p.TipoDesconto));
            cmd.Parameters.AddWithValue("@valor", p.ValorDesconto);
            cmd.Parameters.AddWithValue("@inicio", p.DataInicio);
            cmd.Parameters.AddWithValue("@fim", p.DataFim);
            cmd.Parameters.AddWithValue("@ativo", p.Ativo);
        }

        /// <summary>Substitui a lista de produtos participantes (apaga e insere de novo).</summary>
        private static void GravarProdutos(MySqlConnection conexao, MySqlTransaction transacao, Promocao promocao)
        {
            using (var apagar = DaoUtil.Comando(conexao, "DELETE FROM promocao_produto WHERE promocao_id = @id", transacao))
            {
                apagar.Parameters.AddWithValue("@id", promocao.Id);
                apagar.ExecuteNonQuery();
            }

            using var inserir = DaoUtil.Comando(conexao,
                "INSERT INTO promocao_produto (promocao_id, produto_id) VALUES (@promocao, @produto)", transacao);
            inserir.Parameters.AddWithValue("@promocao", promocao.Id);
            inserir.Parameters.Add("@produto", MySqlDbType.UInt32);
            foreach (var produtoId in promocao.ProdutoIds.Distinct())
            {
                inserir.Parameters["@produto"].Value = produtoId;
                inserir.ExecuteNonQuery();
            }
        }
    }
}
