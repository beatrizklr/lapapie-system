using System.Data;
using Lapapie.Conexao;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>
    /// Funções de apoio compartilhadas por todas as classes DAO:
    /// abrir conexão + traduzir erros, controlar transações e ler valores nulos.
    /// </summary>
    internal static class DaoUtil
    {
        /// <summary>
        /// Abre uma conexão, executa a função e fecha a conexão.
        /// Qualquer MySqlException é convertida em <see cref="BancoDadosException"/>.
        /// </summary>
        public static T Executar<T>(Func<MySqlConnection, T> funcao)
        {
            try
            {
                using var conexao = ConexaoBD.AbrirConexao();
                return funcao(conexao);
            }
            catch (MySqlException ex)
            {
                throw BancoDadosException.Traduzir(ex);
            }
        }

        public static void Executar(Action<MySqlConnection> acao) =>
            Executar(conexao => { acao(conexao); return true; });

        /// <summary>
        /// Executa várias instruções dentro de UMA transação.
        /// <para>
        /// Transação = "tudo ou nada". Numa venda, por exemplo, gravamos a venda, os
        /// itens, damos baixa no estoque e registramos as movimentações. Se qualquer
        /// passo falhar (ex.: faltou estoque), o Rollback desfaz os anteriores e o
        /// banco nunca fica com uma venda pela metade.
        /// </para>
        /// </summary>
        public static T ExecutarTransacao<T>(Func<MySqlConnection, MySqlTransaction, T> funcao)
        {
            return Executar(conexao =>
            {
                // READ COMMITTED + SELECT ... FOR UPDATE: lê sempre o dado mais recente
                // e trava só as linhas que serão alteradas.
                using var transacao = conexao.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    var resultado = funcao(conexao, transacao);
                    transacao.Commit();
                    return resultado;
                }
                catch
                {
                    transacao.Rollback();
                    throw;
                }
            });
        }

        public static void ExecutarTransacao(Action<MySqlConnection, MySqlTransaction> acao) =>
            ExecutarTransacao((c, t) => { acao(c, t); return true; });

        /// <summary>Cria um comando já ligado à conexão (e à transação, se houver).</summary>
        public static MySqlCommand Comando(MySqlConnection conexao, string sql, MySqlTransaction transacao = null)
        {
            return new MySqlCommand(sql, conexao, transacao);
        }

        /// <summary>Texto vazio vira NULL no banco.</summary>
        public static object Nulo(string texto) =>
            string.IsNullOrWhiteSpace(texto) ? DBNull.Value : texto.Trim();

        public static object Nulo<T>(T? valor) where T : struct =>
            valor.HasValue ? valor.Value : DBNull.Value;

        /// <summary>
        /// Monta uma lista "@p0, @p1, @p2" para usar em "IN (...)" mantendo o comando
        /// parametrizado (nunca concatenamos valores digitados no SQL).
        /// </summary>
        public static string ParametrosIn(MySqlCommand comando, string prefixo, IEnumerable<int> valores)
        {
            var nomes = new List<string>();
            int i = 0;
            foreach (var valor in valores)
            {
                string nome = $"@{prefixo}{i++}";
                comando.Parameters.AddWithValue(nome, valor);
                nomes.Add(nome);
            }
            return nomes.Count == 0 ? "NULL" : string.Join(", ", nomes);
        }

        /// <summary>Prepara um termo de pesquisa para LIKE: escapa % e _ e adiciona curingas.</summary>
        public static string Like(string termo) =>
            "%" + (termo ?? string.Empty).Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%";

        // ---- Leitura de colunas que podem ser NULL ----

        public static string Texto(this MySqlDataReader r, string coluna)
        {
            int i = r.GetOrdinal(coluna);
            return r.IsDBNull(i) ? null : r.GetString(i);
        }

        public static int? InteiroOuNulo(this MySqlDataReader r, string coluna)
        {
            int i = r.GetOrdinal(coluna);
            return r.IsDBNull(i) ? null : Convert.ToInt32(r.GetValue(i));
        }

        public static int Inteiro(this MySqlDataReader r, string coluna) => r.InteiroOuNulo(coluna) ?? 0;

        public static decimal Valor(this MySqlDataReader r, string coluna)
        {
            int i = r.GetOrdinal(coluna);
            return r.IsDBNull(i) ? 0m : Convert.ToDecimal(r.GetValue(i));
        }

        public static DateTime? DataOuNulo(this MySqlDataReader r, string coluna)
        {
            int i = r.GetOrdinal(coluna);
            return r.IsDBNull(i) ? null : r.GetDateTime(i);
        }

        public static DateTime Data(this MySqlDataReader r, string coluna) => r.DataOuNulo(coluna) ?? DateTime.MinValue;

        public static bool Logico(this MySqlDataReader r, string coluna)
        {
            int i = r.GetOrdinal(coluna);
            return !r.IsDBNull(i) && Convert.ToBoolean(r.GetValue(i));
        }

        /// <summary>Executa um SELECT e converte cada linha com a função "mapear".</summary>
        public static List<T> Listar<T>(MySqlCommand comando, Func<MySqlDataReader, T> mapear)
        {
            var lista = new List<T>();
            using var leitor = comando.ExecuteReader();
            while (leitor.Read())
                lista.Add(mapear(leitor));
            return lista;
        }
    }
}
