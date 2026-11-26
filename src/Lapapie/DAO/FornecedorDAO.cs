using Lapapie.Models;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Acesso às tabelas fornecedores e fornecedor_produto.</summary>
    public class FornecedorDAO
    {
        private static Fornecedor Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            RazaoSocial = r.Texto("razao_social"),
            NomeFantasia = r.Texto("nome_fantasia"),
            Cnpj = r.Texto("cnpj"),
            Email = r.Texto("email"),
            Telefone = r.Texto("telefone"),
            NomeContato = r.Texto("nome_contato"),
            Cidade = r.Texto("cidade"),
            Uf = r.Texto("uf"),
            Ativo = r.Logico("ativo"),
            CriadoEm = r.Data("criado_em"),
            QuantidadeProdutos = r.Inteiro("qtd_produtos")
        };

        public List<Fornecedor> Pesquisar(string texto, bool somenteAtivos)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT f.*, (SELECT COUNT(*) FROM fornecedor_produto fp WHERE fp.fornecedor_id = f.id) AS qtd_produtos
                            FROM fornecedores f WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (f.razao_social LIKE @texto OR f.nome_fantasia LIKE @texto OR f.nome_contato LIKE @texto OR f.cidade LIKE @texto";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    var digitos = Validacao.SomenteDigitos(texto);
                    if (digitos.Length >= 3)
                    {
                        sql += " OR f.cnpj LIKE @cnpj";
                        cmd.Parameters.AddWithValue("@cnpj", DaoUtil.Like(digitos));
                    }
                    sql += ")";
                }
                if (somenteAtivos) sql += " AND f.ativo = TRUE";
                cmd.CommandText = sql + " ORDER BY COALESCE(f.nome_fantasia, f.razao_social)";
                return DaoUtil.Listar(cmd, Mapear);
            });
        }

        public List<Fornecedor> ListarAtivos() => Pesquisar(null, somenteAtivos: true);

        private static void PreencherParametros(MySqlCommand cmd, Fornecedor f)
        {
            cmd.Parameters.AddWithValue("@razao", f.RazaoSocial.Trim());
            cmd.Parameters.AddWithValue("@fantasia", DaoUtil.Nulo(f.NomeFantasia));
            cmd.Parameters.AddWithValue("@cnpj", Validacao.SomenteDigitos(f.Cnpj));
            cmd.Parameters.AddWithValue("@email", DaoUtil.Nulo(f.Email));
            cmd.Parameters.AddWithValue("@telefone", DaoUtil.Nulo(f.Telefone));
            cmd.Parameters.AddWithValue("@contato", DaoUtil.Nulo(f.NomeContato));
            cmd.Parameters.AddWithValue("@cidade", DaoUtil.Nulo(f.Cidade));
            cmd.Parameters.AddWithValue("@uf", DaoUtil.Nulo(f.Uf?.ToUpperInvariant()));
            cmd.Parameters.AddWithValue("@ativo", f.Ativo);
        }

        public int Inserir(Fornecedor fornecedor)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO fornecedores (razao_social, nome_fantasia, cnpj, email, telefone, nome_contato, cidade, uf, ativo)
                      VALUES (@razao, @fantasia, @cnpj, @email, @telefone, @contato, @cidade, @uf, @ativo)");
                PreencherParametros(cmd, fornecedor);
                cmd.ExecuteNonQuery();
                fornecedor.Id = (int)cmd.LastInsertedId;
                return fornecedor.Id;
            });
        }

        public void Atualizar(Fornecedor fornecedor)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE fornecedores SET razao_social = @razao, nome_fantasia = @fantasia, cnpj = @cnpj, email = @email,
                             telefone = @telefone, nome_contato = @contato, cidade = @cidade, uf = @uf, ativo = @ativo
                      WHERE id = @id");
                PreencherParametros(cmd, fornecedor);
                cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                cmd.ExecuteNonQuery();
            });
        }

        public void Excluir(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "DELETE FROM fornecedores WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        public void Inativar(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "UPDATE fornecedores SET ativo = FALSE WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Produtos que o fornecedor vende, com custo e prazo (N:N fornecedor_produto).</summary>
        public List<FornecedorProduto> ListarProdutos(int fornecedorId)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"SELECT fp.*, p.nome AS produto_nome, COALESCE(f.nome_fantasia, f.razao_social) AS fornecedor_nome
                      FROM fornecedor_produto fp
                      JOIN produtos p     ON p.id = fp.produto_id
                      JOIN fornecedores f ON f.id = fp.fornecedor_id
                      WHERE fp.fornecedor_id = @id
                      ORDER BY p.nome");
                cmd.Parameters.AddWithValue("@id", fornecedorId);
                return DaoUtil.Listar(cmd, ProdutoDAO.MapearFornecedorProduto);
            });
        }
    }
}
