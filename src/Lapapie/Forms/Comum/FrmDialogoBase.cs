using Lapapie.Controles;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Comum
{
    /// <summary>
    /// Base dos formulários de cadastro/edição (janelas modais).
    /// Aplica o tema, fecha com Esc e oferece ajuda para mostrar erros de validação.
    /// </summary>
    public class FrmDialogoBase : Form
    {
        public FrmDialogoBase()
        {
            BackColor = T.Superficie;
            Font = T.Texto(10f);
            ForeColor = T.TintaGrafite;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            KeyPreview = true;
        }

        protected bool EmDesign => System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (EmDesign) return;
            try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch (Exception) { /* ícone é opcional */ }
            T.AplicarBarraTitulo(this);
            T.EstilizarControlesPadrao(this);
            // Esc = "Cancelar". Usar CancelButton (em vez de capturar a tecla) respeita
            // controles que usam o Esc, como uma lista de ComboBox aberta.
            if (CancelButton == null && Controls.Find("btnCancelar", true).FirstOrDefault() is IButtonControl cancelar)
                CancelButton = cancelar;
        }

        /// <summary>Mostra os erros de validação. Retorna true se NÃO houver erros.</summary>
        protected bool ValidarOuAvisar(List<string> erros, params (CampoTexto Campo, string Palavra)[] campos)
        {
            foreach (var (campo, palavra) in campos)
                campo.ComErro = erros.Any(e => e.Contains(palavra, StringComparison.OrdinalIgnoreCase));
            if (erros.Count == 0) return true;
            Mensagem.ErrosValidacao(erros);
            campos.FirstOrDefault(c => c.Campo.ComErro).Campo?.Focus();
            return false;
        }

        /// <summary>Desenha a faixa argila no topo, marca registrada dos diálogos do sistema.</summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pincel = new SolidBrush(T.ArgilaRosada);
            e.Graphics.FillRectangle(pincel, 0, 0, ClientSize.Width, 4);
        }
    }
}
