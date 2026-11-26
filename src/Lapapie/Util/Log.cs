namespace Lapapie.Util
{
    /// <summary>
    /// Grava erros inesperados em logs/erros.log (na pasta do executável).
    /// A pessoa vê uma mensagem amigável; o detalhe técnico fica no arquivo
    /// para quem for investigar o problema.
    /// </summary>
    public static class Log
    {
        private static readonly object Trava = new();

        public static void Erro(Exception ex, string contexto = null)
        {
            try
            {
                var pasta = Path.Combine(AppContext.BaseDirectory, "logs");
                Directory.CreateDirectory(pasta);
                var linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {contexto}{Environment.NewLine}{ex}{Environment.NewLine}{new string('-', 80)}{Environment.NewLine}";
                lock (Trava) File.AppendAllText(Path.Combine(pasta, "erros.log"), linha);
            }
            catch (Exception)
            {
                // Se nem o log puder ser gravado (pasta sem permissão), não há o que fazer.
            }
        }
    }
}
