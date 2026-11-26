using Lapapie.Controles;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Comum
{
    /// <summary>
    /// Diálogo de mensagem com o visual da marca. Use pela classe <see cref="Mensagem"/>.
    /// </summary>
    public partial class FrmMensagem : Form
    {
        private readonly Color _corIcone;
        private bool _entradaObrigatoria;
        private bool _temEntrada;

        /// <summary>Construtor usado pelo designer do Visual Studio.</summary>
        public FrmMensagem() : this(TipoMensagem.Informacao, "Título", "Mensagem", "Ok", null, false) { }

        public FrmMensagem(TipoMensagem tipo, string titulo, string texto, string textoOk, string textoCancelar, bool perigoso)
        {
            InitializeComponent();
            var (icone, cor) = Mensagem.Aparencia(tipo);
            _corIcone = cor;
            lblIcone.Text = icone;
            lblIcone.ForeColor = cor;
            lblTitulo.Text = titulo;
            lblTexto.Text = texto;
            btnOk.Text = textoOk;
            btnOk.Estilo = perigoso ? EstiloBotao.Destaque : EstiloBotao.Primario;
            // Atenção: antes de o formulário aparecer, Control.Visible sempre devolve false.
            // Por isso a decisão usa o parâmetro, e não btnCancelar.Visible.
            bool temCancelar = !string.IsNullOrEmpty(textoCancelar);
            btnCancelar.Visible = temCancelar;
            btnCancelar.Text = textoCancelar ?? "";
            AcceptButton = btnOk;
            CancelButton = temCancelar ? btnCancelar : btnOk; // Esc = cancelar (ou fechar, se só houver "Ok")
            AjustarTamanho();
        }

        /// <summary>Texto digitado quando o diálogo pede uma entrada.</summary>
        public string TextoDigitado => txtEntrada.Texto.Trim();

        public void HabilitarEntrada(string placeholder, bool obrigatorio)
        {
            _temEntrada = true;
            txtEntrada.Visible = true;
            txtEntrada.Placeholder = placeholder;
            _entradaObrigatoria = obrigatorio;
            AjustarTamanho();
            Shown += (s, e) => txtEntrada.Focus();
        }

        /// <summary>Calcula a altura do diálogo conforme o tamanho da mensagem.</summary>
        private void AjustarTamanho()
        {
            var tamanho = TextRenderer.MeasureText(lblTexto.Text, lblTexto.Font, new Size(lblTexto.Width, 2000), TextFormatFlags.WordBreak);
            lblTexto.Height = Math.Max(24, tamanho.Height + 4);
            int y = lblTexto.Bottom + 16;
            if (_temEntrada)
            {
                txtEntrada.Top = y;
                y = txtEntrada.Bottom + 18;
            }
            ClientSize = new Size(ClientSize.Width, Math.Max(180, y + 64));
            btnOk.Top = btnCancelar.Top = ClientSize.Height - 60;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            T.Suavizar(g);
            using (var pincel = new SolidBrush(Color.FromArgb(40, _corIcone)))
                g.FillEllipse(pincel, lblIcone.Bounds);
            using (var caneta = new Pen(T.BordaForte))
                g.DrawRectangle(caneta, 0, 0, Width - 1, Height - 1);
            using (var pincel = new SolidBrush(_corIcone))
                g.FillRectangle(pincel, 0, 0, Width, 4);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_temEntrada && _entradaObrigatoria && string.IsNullOrWhiteSpace(txtEntrada.Texto))
            {
                txtEntrada.ComErro = true;
                txtEntrada.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                Clipboard.SetText($"{lblTitulo.Text}\n{lblTexto.Text}"); // copia a mensagem (útil para relatar erros)
        }

        // Permite arrastar o diálogo sem barra de título.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left) MoverJanela.Iniciar(this);
        }
    }
}
