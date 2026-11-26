using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Painel que desenha um "arco" (retângulo com topo em semicírculo) atrás de
    /// uma imagem — o mesmo recurso usado nas vitrines da referência visual.
    /// </summary>
    [ToolboxItem(true)]
    public class PainelArco : Panel
    {
        private Color _corArco = Color.FromArgb(240, 221, 214);
        private Image _imagem;
        private float _proporcaoLargura = 0.62f;

        public PainelArco()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Fundo;
        }

        [Category("Lapapiê")]
        public Color CorArco { get => _corArco; set { _corArco = value; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue(null)]
        public Image Imagem { get => _imagem; set { _imagem = value; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue(0.62f), Description("Largura do arco em relação à largura do painel (0 a 1).")]
        public float ProporcaoLargura { get => _proporcaoLargura; set { _proporcaoLargura = Math.Clamp(value, 0.2f, 1f); Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            T.Suavizar(g);

            float largura = Width * _proporcaoLargura;
            float x = (Width - largura) / 2f;
            float topo = Height * 0.14f;
            float base_ = Height * 0.86f;
            using (var caminho = new System.Drawing.Drawing2D.GraphicsPath())
            {
                caminho.AddArc(x, topo, largura, largura, 180, 180);
                caminho.AddLine(x + largura, topo + largura / 2f, x + largura, base_);
                caminho.AddLine(x + largura, base_, x, base_);
                caminho.CloseFigure();
                using var pincel = new SolidBrush(_corArco);
                g.FillPath(pincel, caminho);
            }

            if (_imagem != null)
            {
                float escala = Math.Min(Width * 0.86f / _imagem.Width, Height * 0.70f / _imagem.Height);
                float w = _imagem.Width * escala, h = _imagem.Height * escala;
                g.DrawImage(_imagem, (Width - w) / 2f, base_ - h + Height * 0.04f, w, h);
            }
        }
    }
}
