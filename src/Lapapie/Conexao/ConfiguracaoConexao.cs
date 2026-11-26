using System.Text.Json;
using MySql.Data.MySqlClient;

namespace Lapapie.Conexao
{
    /// <summary>
    /// Dados de acesso ao MySQL, lidos do arquivo appsettings.json que fica
    /// na mesma pasta do executável. Assim a senha do banco não fica "chumbada"
    /// no código e cada pessoa pode apontar para o seu próprio servidor.
    /// </summary>
    public class ConfiguracaoConexao
    {
        private const string NomeArquivo = "appsettings.json";

        public string Servidor { get; set; } = "localhost";
        public int Porta { get; set; } = 3306;
        public string Banco { get; set; } = "lapapie";
        public string Usuario { get; set; } = "root";
        public string Senha { get; set; } = "";

        /// <summary>Caminho completo do arquivo de configuração.</summary>
        public static string CaminhoArquivo => Path.Combine(AppContext.BaseDirectory, NomeArquivo);

        /// <summary>Monta a string de conexão a partir dos campos.</summary>
        public string MontarStringConexao()
        {
            // O builder evita erros de digitação e escapa caracteres especiais da senha.
            var builder = new MySqlConnectionStringBuilder
            {
                Server = Servidor,
                Port = (uint)Porta,
                Database = Banco,
                UserID = Usuario,
                Password = Senha,
                CharacterSet = "utf8mb4",
                // Necessário no MySQL 8 quando o usuário usa caching_sha2_password sem SSL.
                AllowPublicKeyRetrieval = true,
                ConnectionTimeout = 8
            };
            return builder.ConnectionString;
        }

        /// <summary>Lê o appsettings.json. Se não existir, devolve os valores padrão.</summary>
        public static ConfiguracaoConexao Carregar()
        {
            try
            {
                if (!File.Exists(CaminhoArquivo))
                    return new ConfiguracaoConexao();

                using var documento = JsonDocument.Parse(File.ReadAllText(CaminhoArquivo));
                if (!documento.RootElement.TryGetProperty("Conexao", out var secao))
                    return new ConfiguracaoConexao();

                return JsonSerializer.Deserialize<ConfiguracaoConexao>(secao.GetRawText())
                       ?? new ConfiguracaoConexao();
            }
            catch (JsonException)
            {
                // Arquivo corrompido: usa o padrão em vez de impedir o sistema de abrir.
                return new ConfiguracaoConexao();
            }
        }

        /// <summary>Grava os dados no appsettings.json.</summary>
        public void Salvar()
        {
            var conteudo = new { Conexao = this };
            var json = JsonSerializer.Serialize(conteudo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CaminhoArquivo, json);
        }
    }
}
