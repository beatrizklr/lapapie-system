using Lapapie.Models;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Acesso à tabela clientes.</summary>
    public class ClienteDAO
    {
        private const string SelectBase =
            @"SELECT c.*,
                     (SELECT COUNT(*) FROM vendas v WHERE v.cliente_id = c.id AND v.status = 'CONCLUIDA') AS qtd_compras,
                     (SELECT MAX(v.data_venda) FROM vendas v WHERE v.cliente_id = c.id AND v.status = 'CONCLUIDA') AS ultima_compra
              FROM clientes c";

        private static Cliente Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            Nome = r.Texto("nome"),
            Cpf = r.Texto("cpf"),
            Email = r.Texto("email"),
            Telefone = r.Texto("telefone"),
            Cep = r.Texto("cep"),
            Logradouro = r.Texto("logradouro"),
            Numero = r.Texto("numero"),
            Complemento = r.Texto("complemento"),
            Bairro = r.Texto("bairro"),
            Cidade = r.Texto("cidade"),
            Uf = r.Texto("uf"),
            Ativo = r.Logico("ativo"),
            CriadoEm = r.Data("criado_em"),
            QuantidadeCompras = r.Inteiro("qtd_compras"),
            UltimaCompra = r.DataOuNulo("ultima_compra")
        };

        /// <summary>Pesquisa por nome, CPF, e-mail ou telefone.</summary>
        public List<Cliente> Pesquisar(string texto, bool somenteAtivos)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = SelectBase + " WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (c.nome LIKE @texto OR c.email LIKE @texto OR c.telefone LIKE @texto";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    var digitos = Validacao.SomenteDigitos(texto);
                    if (digitos.Length >= 3)
                    {
                        sql += " OR c.cpf LIKE @cpf";
                        cmd.Parameters.AddWithValue("@cpf", DaoUtil.Like(digitos));
                    }
                    sql += ")";
                }
                if (somenteAtivos) sql += " AND c.ativo = TRUE";
                cmd.CommandText = sql + " ORDER BY c.nome";
                return DaoUtil.Listar(cmd, Mapear);
            });
        }

        public Cliente ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, SelectBase + " WHERE c.id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                return DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
            });
        }

        private static void PreencherParametros(MySqlCommand cmd, Cliente c)
        {
            cmd.Parameters.AddWithValue("@nome", c.Nome.Trim());
            cmd.Parameters.AddWithValue("@cpf", DaoUtil.Nulo(Validacao.SomenteDigitos(c.Cpf)));
            cmd.Parameters.AddWithValue("@email", DaoUtil.Nulo(c.Email));
            cmd.Parameters.AddWithValue("@telefone", DaoUtil.Nulo(c.Telefone));
            cmd.Parameters.AddWithValue("@cep", DaoUtil.Nulo(Validacao.SomenteDigitos(c.Cep)));
            cmd.Parameters.AddWithValue("@logradouro", DaoUtil.Nulo(c.Logradouro));
            cmd.Parameters.AddWithValue("@numero", DaoUtil.Nulo(c.Numero));
            cmd.Parameters.AddWithValue("@complemento", DaoUtil.Nulo(c.Complemento));
            cmd.Parameters.AddWithValue("@bairro", DaoUtil.Nulo(c.Bairro));
            cmd.Parameters.AddWithValue("@cidade", DaoUtil.Nulo(c.Cidade));
            cmd.Parameters.AddWithValue("@uf", DaoUtil.Nulo(c.Uf?.ToUpperInvariant()));
            cmd.Parameters.AddWithValue("@ativo", c.Ativo);
        }

        public int Inserir(Cliente cliente)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO clientes (nome, cpf, email, telefone, cep, logradouro, numero, complemento, bairro, cidade, uf, ativo)
                      VALUES (@nome, @cpf, @email, @telefone, @cep, @logradouro, @numero, @complemento, @bairro, @cidade, @uf, @ativo)");
                PreencherParametros(cmd, cliente);
                cmd.ExecuteNonQuery();
                cliente.Id = (int)cmd.LastInsertedId;
                return cliente.Id;
            });
        }

        public void Atualizar(Cliente cliente)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE clientes SET nome = @nome, cpf = @cpf, email = @email, telefone = @telefone, cep = @cep,
                             logradouro = @logradouro, numero = @numero, complemento = @complemento, bairro = @bairro,
                             cidade = @cidade, uf = @uf, ativo = @ativo
                      WHERE id = @id");
                PreencherParametros(cmd, cliente);
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Exclusão física. Falha (RegistroEmUso) se o cliente já comprou.</summary>
        public void Excluir(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "DELETE FROM clientes WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Exclusão lógica: o cliente some das telas, mas o histórico de vendas continua.</summary>
        public void Inativar(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "UPDATE clientes SET ativo = FALSE WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }
    }
}
