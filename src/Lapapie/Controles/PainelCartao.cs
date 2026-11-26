using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Painel com cantos arredondados (o "cartão" branco das referências).
    /// Opcionalmente desenha um título e um subtítulo no topo — nesse caso,
    /// use Padding.Top para afastar os controles filhos do título.
    /// </summary>
    [ToolboxItem(true)]
    public class PainelCartao : Panel
    {
        private int _raio = 18;
        private Color _corBorda = Color.FromArgb(230, 220, 205);
        private string _titulo = string.Empty;
        private string _subtitulo = string.Empty;

        public PainelCartao()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Superficie;
            Padding = new Padding(16);
        }

        [Category("Lapapiê"), DefaultValue(18)]
        public int Raio { get => _raio; set { _raio = value; Invalidate(); } }

        [Category("Lapapiê")]
        public Color CorBorda { get => _corBorda; set { _corBorda = value; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue("")]
        public string Titulo { get => _titulo; set { _titulo = value ?? ""; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue("")]
        public string Subtitulo { get => _subtitulo; set { _subtitulo = value ?? ""; Invalidate(); } }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent != null ? T.FundoEfetivo(Parent) : T.Fundo);
            T.Suavizar(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using var caminho = T.RetanguloArredondado(r, _raio);
            using (var pincel = new SolidBrush(BackColor)) g.FillPath(pincel, caminho);
            if (_corBorda.A > 0)
                using (var caneta = new Pen(_corBorda, 1f)) g.DrawPath(caneta, caminho);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (string.IsNullOrEmpty(_titulo)) return;
            var g = e.Graphics;
            TextRenderer.DrawText(g, _titulo.ToUpperInvariant(), T.Titulo(12f), new Point(20, 16), T.TintaGrafite);
            if (!string.IsNullOrEmpty(_subtitulo))
                TextRenderer.DrawText(g, _subtitulo, T.Serifa(9.5f), new Point(20, 40), T.ArgilaEscura);
        }
    }
}
