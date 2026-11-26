using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Acesso à tabela usuarios (login e CRUD de usuários).</summary>
    public class UsuarioDAO
    {
        private const string Colunas = "id, nome, login, senha, perfil, ativo, ultimo_login, criado_em";

        private static Usuario Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            Nome = r.Texto("nome"),
            Login = r.Texto("login"),
            Senha = r.Texto("senha"),
            Perfil = EnumBanco.DoBanco<PerfilUsuario>(r.Texto("perfil")),
            Ativo = r.Logico("ativo"),
            UltimoLogin = r.DataOuNulo("ultimo_login"),
            CriadoEm = r.Data("criado_em")
        };

        /// <summary>
        /// Confere login e senha. Devolve o usuário autenticado ou null se os dados
        /// estiverem errados. Usuário inativo gera <see cref="RegraNegocioException"/>.
        /// </summary>
        public Usuario Autenticar(string login, string senha)
        {
            return DaoUtil.Executar(conexao =>
            {
                Usuario usuario;
                using (var cmd = DaoUtil.Comando(conexao, $"SELECT {Colunas} FROM usuarios WHERE login = @login"))
                {
                    cmd.Parameters.AddWithValue("@login", login.Trim());
                    usuario = DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
                }

                if (usuario == null || !Senha.Conferir(senha, usuario.Senha))
                    return null; // mesma resposta para login ou senha errados (não revela qual)

                if (!usuario.Ativo)
                    throw new RegraNegocioException("Este usuário está inativo. Procure o administrador do sistema.");

                // Registra o acesso e, se a senha ainda estava em texto puro, grava o hash.
                string sql = "UPDATE usuarios SET ultimo_login = NOW()" +
                             (Senha.EstaProtegida(usuario.Senha) ? "" : ", senha = @hash") +
                             " WHERE id = @id";
                using (var cmd = DaoUtil.Comando(conexao, sql))
                {
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    if (!Senha.EstaProtegida(usuario.Senha))
                        cmd.Parameters.AddWithValue("@hash", Senha.GerarHash(senha));
                    cmd.ExecuteNonQuery();
                }
                usuario.UltimoLogin = DateTime.Now;
                usuario.Senha = null; // não mantemos a senha em memória
                return usuario;
            });
        }

        public List<Usuario> Pesquisar(string texto, bool somenteAtivos)
        {
            return DaoUtil.Executar(conexao =>
            {
                var sql = $"SELECT {Colunas} FROM usuarios WHERE 1 = 1";
                using var cmd = DaoUtil.Comando(conexao, "");
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (nome LIKE @texto OR login LIKE @texto)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                }
                if (somenteAtivos) sql += " AND ativo = TRUE";
                cmd.CommandText = sql + " ORDER BY nome";
                var lista = DaoUtil.Listar(cmd, Mapear);
                lista.ForEach(u => u.Senha = null);
                return lista;
            });
        }

        public Usuario ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, $"SELECT {Colunas} FROM usuarios WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                var usuario = DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
                if (usuario != null) usuario.Senha = null;
                return usuario;
            });
        }

        public int Inserir(Usuario usuario, string senha)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO usuarios (nome, login, senha, perfil, ativo)
                      VALUES (@nome, @login, @senha, @perfil, @ativo)");
                cmd.Parameters.AddWithValue("@nome", usuario.Nome.Trim());
                cmd.Parameters.AddWithValue("@login", usuario.Login.Trim().ToLowerInvariant());
                cmd.Parameters.AddWithValue("@senha", Senha.GerarHash(senha));
                cmd.Parameters.AddWithValue("@perfil", EnumBanco.ParaBanco(usuario.Perfil));
                cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);
                cmd.ExecuteNonQuery();
                usuario.Id = (int)cmd.LastInsertedId;
                return usuario.Id;
            });
        }

        /// <summary>Atualiza os dados. Se "novaSenha" vier vazia, a senha atual é mantida.</summary>
        public void Atualizar(Usuario usuario, string novaSenha)
        {
            DaoUtil.Executar(conexao =>
            {
                GarantirOutroAdministrador(conexao, usuario.Id, usuario.Perfil == PerfilUsuario.Admin && usuario.Ativo);

                bool trocarSenha = !string.IsNullOrEmpty(novaSenha);
                using var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE usuarios SET nome = @nome, login = @login, perfil = @perfil, ativo = @ativo" +
                    (trocarSenha ? ", senha = @senha" : "") + " WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome.Trim());
                cmd.Parameters.AddWithValue("@login", usuario.Login.Trim().ToLowerInvariant());
                cmd.Parameters.AddWithValue("@perfil", EnumBanco.ParaBanco(usuario.Perfil));
                cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);
                if (trocarSenha) cmd.Parameters.AddWithValue("@senha", Senha.GerarHash(novaSenha));
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Exclui de vez (só funciona se o usuário nunca registrou vendas/movimentações).</summary>
        public void Excluir(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                GarantirOutroAdministrador(conexao, id, continuaAdmin: false);
                using var cmd = DaoUtil.Comando(conexao, "DELETE FROM usuarios WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Desativa o acesso sem apagar o histórico ligado ao usuário.</summary>
        public void Inativar(int id)
        {
            DaoUtil.Executar(conexao =>
            {
                GarantirOutroAdministrador(conexao, id, continuaAdmin: false);
                using var cmd = DaoUtil.Comando(conexao, "UPDATE usuarios SET ativo = FALSE WHERE id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>Impede que o sistema fique sem nenhum administrador ativo.</summary>
        private static void GarantirOutroAdministrador(MySqlConnection conexao, int usuarioId, bool continuaAdmin)
        {
            if (continuaAdmin) return;
            using var cmd = DaoUtil.Comando(conexao,
                "SELECT COUNT(*) FROM usuarios WHERE perfil = 'ADMIN' AND ativo = TRUE AND id <> @id");
            cmd.Parameters.AddWithValue("@id", usuarioId);
            long outrosAdmins = Convert.ToInt64(cmd.ExecuteScalar());

            using var cmdEle = DaoUtil.Comando(conexao,
                "SELECT COUNT(*) FROM usuarios WHERE perfil = 'ADMIN' AND ativo = TRUE AND id = @id");
            cmdEle.Parameters.AddWithValue("@id", usuarioId);
            bool eleEhAdmin = Convert.ToInt64(cmdEle.ExecuteScalar()) > 0;

            if (eleEhAdmin && outrosAdmins == 0)
                throw new RegraNegocioException("Este é o único administrador ativo. Cadastre outro administrador antes de alterá-lo.");
        }
    }
}
