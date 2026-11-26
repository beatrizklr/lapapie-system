using System.ComponentModel;
using Lapapie.Models;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Cartão de produto da vitrine do caixa (imagem, nome, preço e botão
    /// "Adicionar"), inspirado nos cards "Add to cart" da referência visual.
    /// Tudo é desenhado em um único controle — bem mais leve do que montar
    /// dezenas de Labels/PictureBoxes para cada um dos produtos.
    /// </summary>
    [ToolboxItem(true)]
    public class CartaoProduto : Control
    {
        private Produto _produto;
        private Image _imagem;
        private bool _sobre;
        private bool _sobreBotao;
        private Rectangle _botao;

        /// <summary>Clique no botão "Adicionar".</summary>
        public event EventHandler AdicionarClicado;

        public CartaoProduto()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = T.Superficie;
            Size = new Size(192, 300);
            Margin = new Padding(0, 0, 14, 14);
            Cursor = Cursors.Hand;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Produto Produto { get => _produto; set { _produto = value; Invalidate(); } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Imagem { get => _imagem; set { _imagem = value; Invalidate(); } }

        /// <summary>Quantidade deste produto que já está no carrinho (reduz o disponível exibido).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NoCarrinho { get; set; }

        private int Disponivel => (_produto?.QuantidadeDisponivel ?? 0) - NoCarrinho;

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _sobre = false; _sobreBotao = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool sobre = _botao.Contains(e.Location);
            if (sobre != _sobreBotao) { _sobreBotao = sobre; Invalidate(); }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            // O cartão inteiro adiciona ao carrinho; o botão é o atalho visual.
            if (e.Button == MouseButtons.Left && Disponivel > 0)
                AdicionarClicado?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);

            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using (var caminho = T.RetanguloArredondado(r, 18))
            {
                using (var pincel = new SolidBrush(BackColor)) g.FillPath(pincel, caminho);
                using (var caneta = new Pen(_sobre ? T.ArgilaRosada : T.Borda, _sobre ? 1.4f : 1f)) g.DrawPath(caneta, caminho);
            }
            if (_produto == null) return;

            // Imagem
            int lado = Width - 24;
            if (_imagem != null)
                g.DrawImage(_imagem, new Rectangle(12, 10, lado, lado));

            bool esgotado = Disponivel <= 0;

            // Selo de promoção (canto superior esquerdo)
            if (_produto.PromocaoVigente != null && !esgotado)
            {
                string selo = "−" + _produto.PromocaoVigente.DescricaoDesconto.Replace("/un", "");
                DesenharSelo(g, selo, new Point(14, 14), T.ArgilaRosada, Color.White);
            }

            // Selo de disponibilidade (canto superior direito)
            string textoEstoque = esgotado ? "Esgotado" : _produto.EhKit ? $"{Disponivel} kits" : $"{Disponivel} {(_produto.UnidadeMedida ?? "un").ToLowerInvariant()}";
            var corFundo = esgotado ? T.ArgilaClara : Disponivel <= _produto.EstoqueMinimo && !_produto.EhKit ? T.DouradoClaro : T.SalviaClara;
            var corTexto = esgotado ? T.ArgilaEscura : Disponivel <= _produto.EstoqueMinimo && !_produto.EhKit ? T.DouradoEscuro : T.SalviaEscura;
            var tamSelo = TextRenderer.MeasureText(textoEstoque, T.Texto(8f, FontStyle.Bold));
            DesenharSelo(g, textoEstoque, new Point(Width - tamSelo.Width - 30, 14), corFundo, corTexto);

            // Nome (até 2 linhas) e marca
            int yTexto = 10 + lado + 4;
            TextRenderer.DrawText(g, _produto.Nome, T.Texto(9.5f, FontStyle.Bold), new Rectangle(14, yTexto, Width - 28, 38),
                T.TintaGrafite, TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, _produto.Marca ?? _produto.CategoriaNome, T.Texto(8.5f), new Rectangle(14, yTexto + 40, Width - 28, 16),
                T.TextoSecundario, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

            // Preço e botão
            int yBase = Height - 46;
            if (_produto.PromocaoVigente != null)
            {
                TextRenderer.DrawText(g, Formatacao.Moeda(_produto.PrecoVenda), T.Fonte(T.FamiliaTexto, 8f, FontStyle.Strikeout),
                    new Point(14, yBase - 2), T.TextoSuave);
                TextRenderer.DrawText(g, Formatacao.Moeda(_produto.PrecoPromocional), T.Titulo(12f), new Point(12, yBase + 12), T.ArgilaEscura);
            }
            else
            {
                TextRenderer.DrawText(g, Formatacao.Moeda(_produto.PrecoVenda), T.Titulo(12f), new Point(12, yBase + 5), T.TintaGrafite);
            }

            _botao = new Rectangle(Width - 46, yBase + 2, 34, 34);
            var corBotao = esgotado ? T.Borda : _sobreBotao ? T.ArgilaRosada : T.TintaGrafite;
            using (var pincel = new SolidBrush(corBotao)) g.FillEllipse(pincel, _botao);
            TextRenderer.DrawText(g, Tema.Icones.Adicionar, T.Icones(10f), _botao, esgotado ? T.TextoSuave : T.PapelAlgodao,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            if (esgotado)
            {
                using var veu = new SolidBrush(Color.FromArgb(90, T.Superficie));
                using var caminho = T.RetanguloArredondado(r, 18);
                g.FillPath(veu, caminho);
            }
        }

        private static void DesenharSelo(Graphics g, string texto, Point posicao, Color fundo, Color corTexto)
        {
            var fonte = T.Texto(8f, FontStyle.Bold);
            var tamanho = TextRenderer.MeasureText(texto, fonte);
            var r = new RectangleF(posicao.X, posicao.Y, tamanho.Width + 12, 22);
            using (var caminho = T.RetanguloArredondado(r, 11))
            using (var pincel = new SolidBrush(fundo))
                g.FillPath(pincel, caminho);
            TextRenderer.DrawText(g, texto, fonte, Rectangle.Round(r), corTexto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }
}
