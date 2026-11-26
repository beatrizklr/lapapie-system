using Lapapie.DAO;
using Lapapie.Tema;
using Lapapie.Util;

namespace Lapapie.Forms.Comum
{
    /// <summary>Tipo de mensagem (define ícone e cor do diálogo).</summary>
    public enum TipoMensagem { Informacao, Sucesso, Alerta, Erro, Pergunta }

    /// <summary>
    /// Atalhos para mostrar mensagens com o visual da marca (substituem o MessageBox).
    /// Também centraliza o TRATAMENTO DE EXCEÇÕES da interface: cada tipo de erro
    /// recebe uma mensagem adequada e os inesperados são gravados em log.
    /// </summary>
    public static class Mensagem
    {
        public static void Informar(string texto, string titulo = "Informação") =>
            Mostrar(TipoMensagem.Informacao, titulo, texto, "Entendi", null);

        public static void Sucesso(string texto, string titulo = "Tudo certo!") =>
            Mostrar(TipoMensagem.Sucesso, titulo, texto, "Ok", null);

        public static void Alerta(string texto, string titulo = "Atenção") =>
            Mostrar(TipoMensagem.Alerta, titulo, texto, "Entendi", null);

        public static void Erro(string texto, string titulo = "Algo deu errado") =>
            Mostrar(TipoMensagem.Erro, titulo, texto, "Fechar", null);

        /// <summary>Pergunta sim/não. Retorna true se a pessoa confirmar.</summary>
        public static bool Confirmar(string texto, string titulo = "Confirmar", string textoSim = "Confirmar", string textoNao = "Cancelar", bool perigoso = false)
        {
            using var form = new FrmMensagem(TipoMensagem.Pergunta, titulo, texto, textoSim, textoNao, perigoso);
            return form.ShowDialog(JanelaAtiva) == DialogResult.OK;
        }

        /// <summary>Pede um texto (ex.: motivo do cancelamento). Retorna null se cancelar.</summary>
        public static string PedirTexto(string texto, string titulo, string placeholder, bool obrigatorio, string textoSim = "Confirmar", bool perigoso = false)
        {
            using var form = new FrmMensagem(TipoMensagem.Pergunta, titulo, texto, textoSim, "Voltar", perigoso);
            form.HabilitarEntrada(placeholder, obrigatorio);
            return form.ShowDialog(JanelaAtiva) == DialogResult.OK ? form.TextoDigitado : null;
        }

        /// <summary>Mostra uma lista de erros de validação.</summary>
        public static void ErrosValidacao(IEnumerable<string> erros) =>
            Alerta("Confira os campos:\n\n• " + string.Join("\n• ", erros), "Dados incompletos");

        /// <summary>Converte qualquer exceção em uma mensagem compreensível.</summary>
        public static void TratarErro(Exception ex, string contexto = null)
        {
            switch (ex)
            {
                case RegraNegocioException regra:
                    Alerta(regra.Message, "Não foi possível concluir");
                    break;
                case BancoDadosException banco when banco.Tipo == TipoErroBanco.Conexao:
                    Erro(banco.Message + "\n\nDica: no login, use \"Configurar conexão\" para ajustar servidor, usuário e senha do MySQL.",
                         "Sem conexão com o banco");
                    break;
                case BancoDadosException banco:
                    if (banco.Tipo == TipoErroBanco.Desconhecido) Log.Erro(banco, contexto);
                    Alerta(banco.Message, "Banco de dados");
                    break;
                default:
                    Log.Erro(ex, contexto);
                    Erro("Ocorreu um erro inesperado. Os detalhes foram registrados em logs/erros.log.\n\n" + ex.Message);
                    break;
            }
        }

        private static void Mostrar(TipoMensagem tipo, string titulo, string texto, string textoOk, string textoCancelar)
        {
            using var form = new FrmMensagem(tipo, titulo, texto, textoOk, textoCancelar, false);
            form.ShowDialog(JanelaAtiva);
        }

        private static IWin32Window JanelaAtiva => Form.ActiveForm;

        internal static (string Icone, Color Cor) Aparencia(TipoMensagem tipo) => tipo switch
        {
            TipoMensagem.Sucesso => (Icones.Confirmar, Tema.Tema.Salvia),
            TipoMensagem.Alerta => (Icones.Alerta, Tema.Tema.LacreDourado),
            TipoMensagem.Erro => (Icones.Erro, Tema.Tema.ArgilaRosada),
            TipoMensagem.Pergunta => (Icones.Informacao, Tema.Tema.TintaGrafite),
            _ => (Icones.Informacao, Tema.Tema.Salvia)
        };
    }
}
