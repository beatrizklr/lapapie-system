using System.ComponentModel;
using System.Drawing.Drawing2D;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Gráfico de linha com área preenchida (evolução diária do faturamento).</summary>
    [ToolboxItem(true)]
    public class GraficoLinha : GraficoBase
    {
        private RectangleF _area;

        [Category("Lapapiê")]
        public Color CorLinha { get; set; } = Color.FromArgb(201, 139, 123);

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            T.Suavizar(g);
            if (SemDados) { DesenharVazio(g); return; }

            decimal maximo = Math.Max(1, Itens.Max(i => i.Valor));
            _area = new RectangleF(64, 18, Width - 80, Height - 48);
            for (int i = 0; i <= 4; i++)
            {
                float y = _area.Bottom - _area.Height * i / 4f;
                using (var caneta = new Pen(T.Borda) { DashStyle = DashStyle.Dash })
                    g.DrawLine(caneta, _area.Left, y, _area.Right, y);
                TextRenderer.DrawText(g, FormatarEixo(maximo * i / 4), Font, new Rectangle(0, (int)y - 8, 58, 16), T.TextoSuave,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }

            var pontos = new PointF[Itens.Count];
            float passo = Itens.Count > 1 ? _area.Width / (Itens.Count - 1) : 0;
            for (int i = 0; i < Itens.Count; i++)
                pontos[i] = new PointF(_area.Left + passo * i, _area.Bottom - (float)(Itens[i].Valor / maximo) * _area.Height);

            if (pontos.Length > 1)
            {
                using (var caminhoArea = new GraphicsPath())
                {
                    caminhoArea.AddLine(_area.Left, _area.Bottom, pontos[0].X, pontos[0].Y);
                    caminhoArea.AddLines(pontos);
                    caminhoArea.AddLine(pontos[^1].X, pontos[^1].Y, pontos[^1].X, _area.Bottom);
                    caminhoArea.CloseFigure();
                    using var gradiente = new LinearGradientBrush(new RectangleF(0, _area.Top - 1, 1, _area.Height + 2),
                        Color.FromArgb(110, CorLinha), Color.FromArgb(8, CorLinha), LinearGradientMode.Vertical);
                    g.FillPath(gradiente, caminhoArea);
                }
                using var caneta = new Pen(CorLinha, 2.4f) { LineJoin = LineJoin.Round };
                g.DrawLines(caneta, pontos);
            }

            int salto = Math.Max(1, (int)Math.Ceiling(Itens.Count / 10.0));
            for (int i = 0; i < Itens.Count; i += salto)
                TextRenderer.DrawText(g, Itens[i].Rotulo, Font, new Rectangle((int)pontos[i].X - 30, (int)_area.Bottom + 6, 60, 18),
                    T.TextoSecundario, TextFormatFlags.HorizontalCenter);

            if (IndiceSobre >= 0 && IndiceSobre < Itens.Count)
            {
                var p = pontos[IndiceSobre];
                using (var caneta = new Pen(T.BordaForte)) g.DrawLine(caneta, p.X, _area.Top, p.X, _area.Bottom);
                using (var pincel = new SolidBrush(T.Superficie)) g.FillEllipse(pincel, p.X - 5, p.Y - 5, 10, 10);
                using (var caneta = new Pen(T.TintaGrafite, 2)) g.DrawEllipse(caneta, p.X - 5, p.Y - 5, 10, 10);
                var item = Itens[IndiceSobre];
                DesenharBalao(g, p, FormatarValor(item.Valor), $"{item.Rotulo} · {item.Quantidade} venda(s)");
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Itens.Count < 2 || _area.Width <= 0) return;
            float passo = _area.Width / (Itens.Count - 1);
            int indice = (int)Math.Round((e.X - _area.Left) / passo);
            indice = e.X < _area.Left - 10 || e.X > _area.Right + 10 ? -1 : Math.Clamp(indice, 0, Itens.Count - 1);
            if (indice != IndiceSobre) { IndiceSobre = indice; Invalidate(); }
        }
    }
}
