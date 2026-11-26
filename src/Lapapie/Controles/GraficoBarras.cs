using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Gráfico de barras verticais (períodos) ou horizontais (rankings).</summary>
    [ToolboxItem(true)]
    public class GraficoBarras : GraficoBase
    {
        private readonly List<RectangleF> _barras = new();

        [Category("Lapapiê"), DefaultValue(false), Description("Barras deitadas, com o rótulo à esquerda (bom para rankings).")]
        public bool Horizontal { get; set; }

        [Category("Lapapiê")]
        public Color CorBarra { get; set; } = Color.FromArgb(51, 54, 61);

        [Category("Lapapiê"), Description("Cor da última barra (ex.: mês atual).")]
        public Color CorDestaque { get; set; } = Color.FromArgb(201, 139, 123);

        [Category("Lapapiê"), DefaultValue(true)]
        public bool DestacarUltima { get; set; } = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            T.Suavizar(g);
            _barras.Clear();
            if (SemDados) { DesenharVazio(g); return; }
            if (Horizontal) DesenharHorizontal(g); else DesenharVertical(g);
        }

        private void DesenharVertical(Graphics g)
        {
            decimal maximo = Math.Max(1, Itens.Max(i => i.Valor));
            var area = new RectangleF(64, 18, Width - 76, Height - 48);

            // Linhas de grade com valores no eixo
            for (int i = 0; i <= 4; i++)
            {
                float y = area.Bottom - area.Height * i / 4f;
                using (var caneta = new Pen(T.Borda) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                    g.DrawLine(caneta, area.Left, y, area.Right, y);
                TextRenderer.DrawText(g, FormatarEixo(maximo * i / 4), Font, new Rectangle(0, (int)y - 8, 58, 16), T.TextoSuave,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }

            float passo = area.Width / Itens.Count;
            float larguraBarra = Math.Min(46, passo * 0.56f);
            for (int i = 0; i < Itens.Count; i++)
            {
                float h = (float)(Itens[i].Valor / maximo) * area.Height;
                float x = area.Left + passo * i + (passo - larguraBarra) / 2;
                var r = new RectangleF(x, area.Bottom - h, larguraBarra, Math.Max(h, 0.1f));
                _barras.Add(new RectangleF(area.Left + passo * i, area.Top, passo, area.Height));
                bool destaque = (DestacarUltima && i == Itens.Count - 1) || i == IndiceSobre;
                using (var caminho = T.RetanguloArredondado(new RectangleF(r.X, r.Y, r.Width, r.Height + 8), Math.Min(10, larguraBarra / 2)))
                using (var pincel = new SolidBrush(destaque ? CorDestaque : CorBarra))
                {
                    g.SetClip(new RectangleF(r.X - 1, r.Y - 1, r.Width + 2, r.Height + 1));
                    g.FillPath(pincel, caminho);
                    g.ResetClip();
                }
                int saltoRotulo = Itens.Count > 16 ? (int)Math.Ceiling(Itens.Count / 12.0) : 1;
                if (i % saltoRotulo == 0 || i == Itens.Count - 1)
                    TextRenderer.DrawText(g, Itens[i].Rotulo, Font, new Rectangle((int)(area.Left + passo * i) - 10, (int)area.Bottom + 6, (int)passo + 20, 18),
                        T.TextoSecundario, TextFormatFlags.HorizontalCenter);
            }

            if (IndiceSobre >= 0 && IndiceSobre < Itens.Count)
            {
                var item = Itens[IndiceSobre];
                float x = area.Left + passo * IndiceSobre + passo / 2;
                float y = area.Bottom - (float)(item.Valor / maximo) * area.Height;
                DesenharBalao(g, new PointF(x, y), FormatarValor(item.Valor), $"{item.Rotulo} · {item.Quantidade} venda(s)");
            }
        }

        private void DesenharHorizontal(Graphics g)
        {
            decimal maximo = Math.Max(1, Itens.Max(i => i.Valor));
            float larguraRotulo = Math.Min(Width * 0.42f, 200);
            float alturaLinha = Math.Min(34, (Height - 8f) / Itens.Count);
            float larguraValor = 92;
            float areaBarra = Width - larguraRotulo - larguraValor - 12;

            for (int i = 0; i < Itens.Count; i++)
            {
                float y = 4 + i * alturaLinha;
                float h = Math.Min(14, alturaLinha * 0.5f);
                TextRenderer.DrawText(g, Itens[i].Rotulo, T.Texto(9f), new Rectangle(0, (int)y, (int)larguraRotulo - 8, (int)alturaLinha),
                    T.TintaGrafite, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                var trilho = new RectangleF(larguraRotulo, y + (alturaLinha - h) / 2, areaBarra, h);
                using (var caminho = T.RetanguloArredondado(trilho, h / 2))
                using (var pincel = new SolidBrush(T.SuperficieAlternada))
                    g.FillPath(pincel, caminho);

                float w = Math.Max(h, (float)(Itens[i].Valor / maximo) * areaBarra);
                var barra = new RectangleF(larguraRotulo, trilho.Y, w, h);
                var cor = i == IndiceSobre ? CorDestaque : (i == 0 && DestacarUltima ? CorDestaque : CorBarra);
                using (var caminho = T.RetanguloArredondado(barra, h / 2))
                using (var pincel = new SolidBrush(cor))
                    g.FillPath(pincel, caminho);

                TextRenderer.DrawText(g, FormatarValor(Itens[i].Valor), T.Texto(9f, FontStyle.Bold),
                    new Rectangle((int)(Width - larguraValor), (int)y, (int)larguraValor - 2, (int)alturaLinha),
                    T.TintaGrafite, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                _barras.Add(new RectangleF(0, y, Width, alturaLinha));
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int indice = _barras.FindIndex(b => b.Contains(e.Location));
            if (indice != IndiceSobre) { IndiceSobre = indice; Invalidate(); }
        }
    }
}
