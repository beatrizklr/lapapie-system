using System.ComponentModel;
using Lapapie.Tema;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Etiqueta em pílula com um pequeno círculo "+" (como os selos
    /// "Allergen-Free" / "Non-GMO" da referência). Ajusta a largura ao texto.
    /// </summary>
    [ToolboxItem(true)]
    public class EtiquetaChip : Control
    {
        private Color _corCirculo = Color.FromArgb(201, 139, 123);
        private bool _circuloAEsquerda;

        public EtiquetaChip()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Font = T.Texto(9f);
            BackColor = Color.White;
            ForeColor = T.TintaGrafite;
            Size = new Size(140, 32);
        }

        [Category("Lapapiê")]
        public Color CorCirculo { get => _corCirculo; set { _corCirculo = value; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue(false)]
        public bool CirculoAEsquerda { get => _circuloAEsquerda; set { _circuloAEsquerda = value; Invalidate(); } }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Width = TextRenderer.MeasureText(Text, Font).Width + Height + 22;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using (var caminho = T.RetanguloArredondado(r, r.Height / 2f))
            using (var pincel = new SolidBrush(BackColor))
                g.FillPath(pincel, caminho);

            float d = Height - 10;
            float cx = _circuloAEsquerda ? 5 : Width - d - 6;
            var circulo = new RectangleF(cx, 5, d, d);
            using (var pincel = new SolidBrush(Color.FromArgb(40, _corCirculo))) g.FillEllipse(pincel, circulo);
            TextRenderer.DrawText(g, Icones.Adicionar, T.Icones(7.5f), Rectangle.Round(circulo), _corCirculo,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            var areaTexto = _circuloAEsquerda
                ? new Rectangle((int)(d + 10), 0, Width - (int)d - 14, Height)
                : new Rectangle(12, 0, Width - (int)d - 18, Height);
            TextRenderer.DrawText(g, Text, Font, areaTexto, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }
    }
}
