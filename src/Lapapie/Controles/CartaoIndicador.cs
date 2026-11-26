using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Tendência exibida na linha de detalhe do indicador.</summary>
    public enum Tendencia { Neutra, Alta, Baixa, Alerta }

    /// <summary>Cartão de indicador do dashboard (título, valor grande, detalhe e ícone).</summary>
    [ToolboxItem(true)]
    public class CartaoIndicador : Control
    {
        private string _titulo = "Indicador";
        private string _valor = "0";
        private string _detalhe = string.Empty;
        private string _icone = string.Empty;
        private Color _corDestaque = Color.FromArgb(201, 139, 123);
        private Tendencia _tendencia = Tendencia.Neutra;

        public CartaoIndicador()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Superficie;
            Size = new Size(260, 132);
        }

        [Category("Lapapiê")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [Category("Lapapiê")] public string Valor { get => _valor; set { _valor = value; Invalidate(); } }
        [Category("Lapapiê")] public string Detalhe { get => _detalhe; set { _detalhe = value; Invalidate(); } }
        [Category("Lapapiê")] public string Icone { get => _icone; set { _icone = value; Invalidate(); } }
        [Category("Lapapiê")] public Color CorDestaque { get => _corDestaque; set { _corDestaque = value; Invalidate(); } }
        [Category("Lapapiê"), DefaultValue(Tendencia.Neutra)] public Tendencia Tendencia { get => _tendencia; set { _tendencia = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using (var caminho = T.RetanguloArredondado(r, 18))
            {
                using (var pincel = new SolidBrush(BackColor)) g.FillPath(pincel, caminho);
                using (var caneta = new Pen(T.Borda)) g.DrawPath(caneta, caminho);
            }

            // Círculo do ícone (canto superior direito)
            var circulo = new RectangleF(Width - 58, 18, 40, 40);
            using (var pincel = new SolidBrush(Color.FromArgb(46, _corDestaque))) g.FillEllipse(pincel, circulo);
            TextRenderer.DrawText(g, _icone, T.Icones(13f), Rectangle.Round(circulo), _corDestaque,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            TextRenderer.DrawText(g, (_titulo ?? "").ToUpperInvariant(), T.Titulo(9f), new Rectangle(20, 20, Width - 84, 20),
                T.TextoSecundario, TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(g, _valor, T.Titulo(22f), new Rectangle(18, 44, Width - 36, 44),
                T.TintaGrafite, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (!string.IsNullOrEmpty(_detalhe))
            {
                Color cor = _tendencia switch
                {
                    Tendencia.Alta => T.SalviaEscura,
                    Tendencia.Baixa => T.ArgilaEscura,
                    Tendencia.Alerta => T.DouradoEscuro,
                    _ => T.TextoSecundario
                };
                string seta = _tendencia switch { Tendencia.Alta => "▲ ", Tendencia.Baixa => "▼ ", Tendencia.Alerta => "● ", _ => "" };
                TextRenderer.DrawText(g, seta + _detalhe, T.Texto(9f), new Rectangle(20, Height - 36, Width - 40, 20),
                    cor, TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
