using Lapapie.Forms;
using Lapapie.Forms.Comum;
using Lapapie.Util;

namespace Lapapie
{
    internal static class Program
    {
        /// <summary>Ponto de entrada do programa.</summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Iniciar();
            }
            catch (Exception ex)
            {
                // Erro antes de qualquer janela abrir: sem este bloco o programa
                // simplesmente fecharia em silêncio, sem dizer o motivo.
                Log.Erro(ex, "Erro ao iniciar o sistema");
                MessageBox.Show("O Lapapiê não conseguiu iniciar.\n\n" + ex, "Lapapiê — erro ao iniciar",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fluxo: tela de login → janela principal.
        /// Ao clicar em "Sair", a pessoa volta para o login (troca de usuário).
        /// </summary>
        private static void Iniciar()
        {
            ApplicationConfiguration.Initialize();
            Thread.CurrentThread.CurrentCulture = Formatacao.PtBr;
            Thread.CurrentThread.CurrentUICulture = Formatacao.PtBr;

            // Rede de segurança: qualquer exceção não tratada vira uma mensagem amigável + log.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Mensagem.TratarErro(e.Exception, "Exceção não tratada");
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex) Log.Erro(ex, "Erro fatal");
            };

            while (true)
            {
                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        break;
                }

                using var principal = new FrmPrincipal();
                principal.ShowDialog();
                Sessao.Encerrar();
                if (!principal.VoltarParaLogin)
                    break;
            }
        }
    }
}
