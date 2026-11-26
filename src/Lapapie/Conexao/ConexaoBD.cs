using MySql.Data.MySqlClient;

namespace Lapapie.Conexao
{
    /// <summary>
    /// Ponto único de criação de conexões com o banco de dados.
    /// Todas as classes DAO pedem a conexão aqui, então se um dia o servidor
    /// mudar, só este arquivo (e o appsettings.json) precisam ser alterados.
    /// </summary>
    public static class ConexaoBD
    {
        private static ConfiguracaoConexao _configuracao;

        /// <summary>Configuração em uso (carregada do appsettings.json na primeira chamada).</summary>
        public static ConfiguracaoConexao Configuracao
        {
            get => _configuracao ??= ConfiguracaoConexao.Carregar();
            set => _configuracao = value;
        }

        /// <summary>
        /// Cria e ABRE uma conexão. Use sempre dentro de um bloco "using" para que
        /// ela seja fechada e devolvida ao pool automaticamente.
        /// </summary>
        public static MySqlConnection AbrirConexao()
        {
            var conexao = new MySqlConnection(Configuracao.MontarStringConexao());
            conexao.Open();
            return conexao;
        }

        /// <summary>Testa a conexão e devolve a mensagem de erro, se houver.</summary>
        public static bool TestarConexao(ConfiguracaoConexao configuracao, out string mensagemErro)
        {
            try
            {
                using var conexao = new MySqlConnection(configuracao.MontarStringConexao());
                conexao.Open();
                using var comando = new MySqlCommand("SELECT COUNT(*) FROM usuarios", conexao);
                comando.ExecuteScalar();
                mensagemErro = null;
                return true;
            }
            catch (MySqlException ex)
            {
                mensagemErro = DAO.BancoDadosException.Traduzir(ex).Message;
                return false;
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return false;
            }
        }
    }
}
