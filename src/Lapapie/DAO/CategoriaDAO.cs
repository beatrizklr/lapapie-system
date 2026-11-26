using Lapapie.Models;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Acesso à tabela categorias.</summary>
    public class CategoriaDAO
    {
        private static Categoria Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            Nome = r.Texto("nome"),
            Descricao = r.Texto("descricao"),
            Ativo = r.Logico("ativo"),
            QuantidadeProdutos = r.Inteiro("qtd_produtos")
        };

        public List<Categoria> Pesquisar(string texto, bool somenteAtivas)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = @"SELECT c.id, c.nome, c.descricao, c.ativo, COUNT(p.id) AS qtd_produtos
                            FROM categorias c
                            LEFT JOIN produtos p ON p.categoria_id = c.id AND p.ativo = TRUE
                            WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (c.nome LIKE @texto OR c.descricao LIKE @texto)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                }
                if (somenteAtivas) sql += " AND c.ativo = TRUE";
                cmd.CommandText = sql + " GROUP BY c.id, c.nome, c.descricao, c.ativo ORDER BY c.nome";
                return DaoUtil.Listar(cmd, Mapear);
            });
        }

        /// <summary>Categorias ativas, para preencher ComboBox e filtros.</summary>
        public List<Categoria> ListarAtivas() => Pesquisar(null, somenteAtivas: true);

        public int Inserir(Categoria categoria)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    "INSERT INTO categorias (nome, descricao, ativo) VALUES (@nome, @descricao, @ativo)");
                cmd.Parameters.AddWithValue("@nome", categoria.Nome.Trim());
                cmd.Parameters.AddWithValue("@descricao", DaoUtil.Nulo(categoria.Descricao));
                cmd.Parameters.AddWithValue("@ativo", categoria.Ativo);
                cmd.ExecuteNonQuery();
                categoria.Id = (int)cmd.LastInsertedId;
                return categoria.Id;
            });
        }

        public void Atualizar(Categoria categoria)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    "UPDATE categorias SET nome = @nome, descricao = @descricao, ativo = @ativo WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", categoria.Id);
                cmd.Parameters.AddWithValue("@nome", categoria.Nome.Trim());
                cmd.Parameters.AddWithValue("@descricao", DaoUtil.Nulo(categoria.Descricao));
                cmd.Parameters.AddWithValue("@ativo", categoria.Ativo);
                cmd.ExecuteNonQuery();
            });
        }

        public void Excluir(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "DELETE FROM categorias WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        public void Inativar(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "UPDATE categorias SET ativo = FALSE WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }
    }
}
