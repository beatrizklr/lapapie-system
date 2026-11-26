using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Faixa com texto grande repetido e rolando ("OFERTA · OFERTA · OFERTA"),
    /// releitura do letreiro "GREAT OFFER" da referência visual.
    /// </summary>
    [ToolboxItem(true)]
    public class FaixaOferta : Control
    {
        private readonly System.Windows.Forms.Timer _animacao = new() { Interval = 30 };
        private float _deslocamento;
        private bool _animar = true;

        public FaixaOferta()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Font = T.Titulo(30f);
            ForeColor = T.TintaGrafite;
            BackColor = T.Superficie;
            Text = "OFERTA DA SEMANA";
            Size = new Size(600, 60);
            _animacao.Tick += (s, e) => { _deslocamento += 0.8f; Invalidate(); };
        }

        [Category("Lapapiê"), DefaultValue(true)]
        public bool Animar
        {
            get => _animar;
            set { _animar = value; AtualizarAnimacao(); }
        }

        private bool EmDesign => LicenseManager.UsageMode == LicenseUsageMode.Designtime || (Site?.DesignMode ?? false);

        private void AtualizarAnimacao()
        {
            if (_animar && Visible && !EmDesign && IsHandleCreated) _animacao.Start();
            else _animacao.Stop();
        }

        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); AtualizarAnimacao(); }
        protected override void OnVisibleChanged(EventArgs e) { base.OnVisibleChanged(e); AtualizarAnimacao(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            T.Suavizar(g);
            string bloco = (Text ?? "").ToUpperInvariant() + "  ·  ";
            float largura = Math.Max(1, TextRenderer.MeasureText(g, bloco, Font, Size.Empty, TextFormatFlags.NoPadding).Width);
            float inicio = -(_deslocamento % largura);
            using var pincel = new SolidBrush(ForeColor);
            for (float x = inicio; x < Width; x += largura)
                TextRenderer.DrawText(g, bloco, Font, new Point((int)x, (Height - Font.Height) / 2), ForeColor, TextFormatFlags.NoPadding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _animacao.Dispose();
            base.Dispose(disposing);
        }
    }
}
