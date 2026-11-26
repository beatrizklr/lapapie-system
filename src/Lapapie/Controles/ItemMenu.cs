using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Item do menu lateral (ícone + texto). O item ativo vira uma pílula creme.</summary>
    [ToolboxItem(true)]
    public class ItemMenu : Control
    {
        private string _icone = string.Empty;
        private bool _selecionado;
        private bool _sobre;

        public ItemMenu()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Font = T.Texto(10f);
            Size = new Size(212, 40);
            Cursor = Cursors.Hand;
            BackColor = T.TintaGrafite;
        }

        [Category("Lapapiê"), DefaultValue("")]
        public string Icone { get => _icone; set { _icone = value ?? ""; Invalidate(); } }

        [Category("Lapapiê"), DefaultValue(false)]
        public bool Selecionado { get => _selecionado; set { _selecionado = value; Invalidate(); } }

        /// <summary>Atalho de teclado exibido à direita (ex.: "F2").</summary>
        [Category("Lapapiê"), DefaultValue("")]
        public string Atalho { get; set; } = "";

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _sobre = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            T.Suavizar(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            Color corTexto = T.CremeSobreGrafite;
            if (_selecionado)
            {
                using var caminho = T.RetanguloArredondado(r, r.Height / 2f);
                using var pincel = new SolidBrush(T.PapelAlgodao);
                g.FillPath(pincel, caminho);
                corTexto = T.TintaGrafite;
            }
            else if (_sobre)
            {
                using var caminho = T.RetanguloArredondado(r, r.Height / 2f);
                using var pincel = new SolidBrush(T.GrafiteHover);
                g.FillPath(pincel, caminho);
                corTexto = T.PapelAlgodao;
            }

            TextRenderer.DrawText(g, _icone, T.Icones(11f), new Rectangle(14, 0, 22, Height), _selecionado ? T.ArgilaRosada : corTexto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, Text, _selecionado ? T.Texto(10f, FontStyle.Bold) : Font,
                new Rectangle(46, 0, Width - 90, Height), corTexto, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            if (!string.IsNullOrEmpty(Atalho))
                TextRenderer.DrawText(g, Atalho, T.Texto(8f), new Rectangle(Width - 44, 0, 34, Height),
                    _selecionado ? T.TextoSecundario : Color.FromArgb(150, T.CremeSobreGrafite), TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
