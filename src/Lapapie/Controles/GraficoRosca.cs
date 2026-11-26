using System.ComponentModel;
using System.Drawing.Drawing2D;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Gráfico de rosca (donut) com legenda e total no centro.</summary>
    [ToolboxItem(true)]
    public class GraficoRosca : GraficoBase
    {
        private RectangleF _circulo;
        private readonly List<(float Inicio, float Varredura)> _fatias = new();

        [Category("Lapapiê"), DefaultValue("Total")]
        public string RotuloCentro { get; set; } = "Total";

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            T.Suavizar(g);
            _fatias.Clear();
            if (SemDados) { DesenharVazio(g); return; }

            decimal total = Itens.Sum(i => i.Valor);
            float diametro = Math.Min(Height - 16, Width * 0.46f);
            _circulo = new RectangleF(8, (Height - diametro) / 2, diametro, diametro);
            float espessura = diametro * 0.22f;

            float angulo = -90;
            for (int i = 0; i < Itens.Count; i++)
            {
                float varredura = (float)(Itens[i].Valor / total) * 360f;
                var cor = T.CoresGraficos[i % T.CoresGraficos.Length];
                var r = _circulo;
                if (i == IndiceSobre) r.Inflate(4, 4);
                using (var caneta = new Pen(cor, espessura) { StartCap = LineCap.Flat, EndCap = LineCap.Flat })
                {
                    var interno = RectangleF.Inflate(r, -espessura / 2, -espessura / 2);
                    if (varredura > 0.5f)
                        g.DrawArc(caneta, interno, angulo + 0.6f, Math.Max(0.1f, varredura - 1.2f));
                }
                _fatias.Add((angulo, varredura));
                angulo += varredura;
            }

            // Centro: total ou valor da fatia sob o mouse
            var centro = RectangleF.Inflate(_circulo, -espessura - 4, -espessura - 4);
            string rotulo = IndiceSobre >= 0 ? Itens[IndiceSobre].Rotulo : RotuloCentro;
            string valor = IndiceSobre >= 0 ? FormatarValor(Itens[IndiceSobre].Valor) : FormatarValor(total);
            TextRenderer.DrawText(g, rotulo, T.Texto(8.5f), new Rectangle((int)centro.X, (int)(centro.Y + centro.Height / 2 - 22), (int)centro.Width, 18),
                T.TextoSecundario, TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(g, valor, T.Titulo(centro.Width > 110 ? 13f : 10f), new Rectangle((int)centro.X - 6, (int)(centro.Y + centro.Height / 2 - 4), (int)centro.Width + 12, 26),
                T.TintaGrafite, TextFormatFlags.HorizontalCenter);

            // Legenda
            float xLegenda = _circulo.Right + 22;
            float alturaLinha = 26;
            float yLegenda = Math.Max(6, (Height - Itens.Count * alturaLinha) / 2);
            for (int i = 0; i < Itens.Count; i++)
            {
                float y = yLegenda + i * alturaLinha;
                var cor = T.CoresGraficos[i % T.CoresGraficos.Length];
                using (var pincel = new SolidBrush(cor)) g.FillEllipse(pincel, xLegenda, y + 7, 10, 10);
                decimal percentual = total == 0 ? 0 : Itens[i].Valor / total * 100;
                var fonte = i == IndiceSobre ? T.Texto(9f, FontStyle.Bold) : T.Texto(9f);
                TextRenderer.DrawText(g, Itens[i].Rotulo, fonte, new Rectangle((int)xLegenda + 16, (int)y, (int)(Width - xLegenda - 70), 24),
                    T.TintaGrafite, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                TextRenderer.DrawText(g, percentual.ToString("0", Formatacao.PtBr) + "%", T.Texto(9f, FontStyle.Bold),
                    new Rectangle(Width - 54, (int)y, 50, 24), T.TextoSecundario, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int indice = -1;
            var centro = new PointF(_circulo.X + _circulo.Width / 2, _circulo.Y + _circulo.Height / 2);
            float dx = e.X - centro.X, dy = e.Y - centro.Y;
            float distancia = (float)Math.Sqrt(dx * dx + dy * dy);
            if (distancia <= _circulo.Width / 2 + 4 && distancia >= _circulo.Width * 0.25f)
            {
                float angulo = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
                if (angulo < -90) angulo += 360;
                indice = _fatias.FindIndex(f => angulo >= f.Inicio && angulo < f.Inicio + f.Varredura);
            }
            if (indice != IndiceSobre) { IndiceSobre = indice; Invalidate(); }
        }
    }
}
