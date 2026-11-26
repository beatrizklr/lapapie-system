using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Círculo com as iniciais do usuário logado.</summary>
    [ToolboxItem(true)]
    public class Avatar : Control
    {
        public Avatar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Salvia;
            ForeColor = Color.White;
            Font = T.Titulo(11f);
            Size = new Size(42, 42);
        }

        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);
            using (var pincel = new SolidBrush(BackColor))
                g.FillEllipse(pincel, 0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }
}
