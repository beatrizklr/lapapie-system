using System.ComponentModel;
using Lapapie.Models.Consultas;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Base dos gráficos desenhados com GDI+ (sem bibliotecas externas).
    /// Guarda a série de dados, a formatação dos valores e o item sob o mouse.
    /// </summary>
    public abstract class GraficoBase : Control
    {
        protected List<ItemSerie> Itens = new();
        protected int IndiceSobre = -1;

        protected GraficoBase()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Superficie;
            Font = T.Texto(8.5f);
            Size = new Size(420, 240);
        }

        /// <summary>Formata os valores exibidos (padrão: moeda).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<decimal, string> FormatarValor { get; set; } = v => Formatacao.Moeda(v);

        /// <summary>Formato curto para eixos (ex.: "R$ 1,2 mil").</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<decimal, string> FormatarEixo { get; set; } = ValorCurto;

        [Category("Lapapiê"), DefaultValue("Sem dados no período.")]
        public string MensagemVazio { get; set; } = "Sem dados no período.";

        public void DefinirDados(IEnumerable<ItemSerie> itens)
        {
            Itens = itens?.ToList() ?? new List<ItemSerie>();
            IndiceSobre = -1;
            Invalidate();
        }

        public static string ValorCurto(decimal v)
        {
            if (Math.Abs(v) >= 1000) return "R$ " + (v / 1000m).ToString("0.#", Formatacao.PtBr) + " mil";
            return "R$ " + v.ToString("0", Formatacao.PtBr);
        }

        protected bool SemDados => Itens.Count == 0 || Itens.All(i => i.Valor == 0);

        protected void DesenharVazio(Graphics g)
        {
            TextRenderer.DrawText(g, MensagemVazio, T.Serifa(10f), ClientRectangle, T.TextoSuave,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        /// <summary>Balão com o valor do item sob o mouse.</summary>
        protected void DesenharBalao(Graphics g, PointF ancora, string linha1, string linha2)
        {
            var f1 = T.Texto(8.5f, FontStyle.Bold);
            var f2 = T.Texto(8.5f);
            var t1 = TextRenderer.MeasureText(linha1, f1);
            var t2 = TextRenderer.MeasureText(linha2 ?? "", f2);
            float w = Math.Max(t1.Width, t2.Width) + 18, h = string.IsNullOrEmpty(linha2) ? 28 : 44;
            float x = Math.Clamp(ancora.X - w / 2, 2, Width - w - 2);
            float y = Math.Max(2, ancora.Y - h - 8);
            var r = new RectangleF(x, y, w, h);
            using (var caminho = T.RetanguloArredondado(r, 8))
            using (var pincel = new SolidBrush(T.TintaGrafite))
                g.FillPath(pincel, caminho);
            TextRenderer.DrawText(g, linha1, f1, new Point((int)x + 9, (int)y + 6), T.PapelAlgodao);
            if (!string.IsNullOrEmpty(linha2))
                TextRenderer.DrawText(g, linha2, f2, new Point((int)x + 9, (int)y + 23), T.CremeSobreGrafite);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (IndiceSobre != -1) { IndiceSobre = -1; Invalidate(); }
        }

        /// <summary>Gera um PNG do gráfico (usado na exportação para PDF).</summary>
        public byte[] GerarPng(int largura, int altura)
        {
            // Desenha o gráfico num tamanho fixo, independente do tamanho na tela.
            // O Dock é desligado temporariamente para o layout não "desfazer" o novo tamanho.
            var dockOriginal = Dock;
            var limitesOriginais = Bounds;
            int sobreOriginal = IndiceSobre;
            Parent?.SuspendLayout();
            IndiceSobre = -1;
            using var bitmap = new Bitmap(largura, altura);
            try
            {
                Dock = DockStyle.None;
                Size = new Size(largura, altura);
                DrawToBitmap(bitmap, new Rectangle(0, 0, largura, altura));
            }
            finally
            {
                Bounds = limitesOriginais;
                Dock = dockOriginal;
                IndiceSobre = sobreOriginal;
                Parent?.ResumeLayout();
            }
            using var memoria = new MemoryStream();
            bitmap.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
            return memoria.ToArray();
        }
    }
}
