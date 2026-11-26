using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Grupo de opções em pílulas (como "Vitamins | Dietary supplements" da referência).
    /// A opção selecionada recebe fundo argila claro. Quebra linha se não couber.
    /// </summary>
    [ToolboxItem(true), DefaultEvent(nameof(SelecaoAlterada))]
    public class AbasPill : Control
    {
        private readonly List<(string Texto, object Valor)> _itens = new();
        private readonly List<RectangleF> _areas = new();
        private int _selecionado = -1;
        private int _sobre = -1;
        private bool _ajustarAltura = true;

        public event EventHandler SelecaoAlterada;

        public AbasPill()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.Selectable, true);
            Font = T.Texto(9.75f, FontStyle.Bold);
            Size = new Size(400, 38);
            Cursor = Cursors.Hand;
        }

        [Category("Lapapiê"), DefaultValue(true), Description("Aumenta a altura quando as opções quebram linha.")]
        public bool AjustarAltura { get => _ajustarAltura; set { _ajustarAltura = value; Recalcular(); } }

        /// <summary>Itens definidos no designer (separados por "|"). Ex.: "Todos|Ativos|Inativos".</summary>
        [Category("Lapapiê"), DefaultValue("")]
        public string Opcoes
        {
            get => string.Join("|", _itens.Select(i => i.Texto));
            set
            {
                var textos = (value ?? "").Split('|', StringSplitOptions.RemoveEmptyEntries);
                // Mantém o índice já definido (o designer do VS pode gravar IndiceSelecionado antes de Opcoes).
                DefinirItens(textos.Select(t => (t, (object)t)), Math.Max(0, _selecionado));
            }
        }

        /// <summary>Define as opções com um valor associado a cada uma (ex.: id da categoria).</summary>
        public void DefinirItens(IEnumerable<(string Texto, object Valor)> itens, int selecionado = 0)
        {
            _itens.Clear();
            _itens.AddRange(itens);
            _selecionado = _itens.Count == 0 ? -1 : Math.Clamp(selecionado, 0, _itens.Count - 1);
            Recalcular();
        }

        [Category("Lapapiê"), DefaultValue(-1)]
        public int IndiceSelecionado
        {
            get => _selecionado;
            set
            {
                if (_itens.Count == 0)
                {
                    _selecionado = value; // guardado até as opções serem definidas
                    return;
                }
                if (value < -1 || value >= _itens.Count || value == _selecionado) return;
                _selecionado = value;
                Invalidate();
                SelecaoAlterada?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(false)]
        public object ValorSelecionado => _selecionado >= 0 ? _itens[_selecionado].Valor : null;

        [Browsable(false)]
        public string TextoSelecionado => _selecionado >= 0 ? _itens[_selecionado].Texto : null;

        protected override void OnResize(EventArgs e) { base.OnResize(e); Recalcular(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); Recalcular(); }

        private void Recalcular()
        {
            _areas.Clear();
            float x = 0, y = 0, altura = 34, espaco = 6;
            foreach (var item in _itens)
            {
                float largura = TextRenderer.MeasureText(item.Texto, Font).Width + 26;
                if (x > 0 && x + largura > Width)
                {
                    x = 0;
                    y += altura + espaco;
                }
                _areas.Add(new RectangleF(x, y, largura, altura));
                x += largura + espaco;
            }
            if (_ajustarAltura && _areas.Count > 0)
            {
                int nova = (int)(_areas[^1].Bottom + 2);
                if (nova != Height) Height = nova;
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int indice = _areas.FindIndex(a => a.Contains(e.Location));
            if (indice != _sobre) { _sobre = indice; Invalidate(); }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e) { _sobre = -1; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int indice = _areas.FindIndex(a => a.Contains(e.Location));
            if (indice >= 0) { Focus(); IndiceSelecionado = indice; }
            base.OnMouseClick(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && _selecionado < _itens.Count - 1) IndiceSelecionado = _selecionado + 1;
            if (e.KeyCode == Keys.Left && _selecionado > 0) IndiceSelecionado = _selecionado - 1;
            base.OnKeyDown(e);
        }

        protected override bool IsInputKey(Keys tecla) => tecla is Keys.Left or Keys.Right || base.IsInputKey(tecla);

        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);
            for (int i = 0; i < _itens.Count && i < _areas.Count; i++)
            {
                var r = _areas[i];
                r.Width -= 1; r.Height -= 1;
                bool sel = i == _selecionado;
                if (sel || i == _sobre)
                {
                    using var caminho = T.RetanguloArredondado(r, r.Height / 2f);
                    using var pincel = new SolidBrush(sel ? T.ArgilaClara : Color.FromArgb(120, T.Borda));
                    g.FillPath(pincel, caminho);
                    if (sel && Focused)
                        using (var caneta = new Pen(T.ArgilaRosada, 1.2f)) g.DrawPath(caneta, caminho);
                }
                TextRenderer.DrawText(g, _itens[i].Texto, Font, Rectangle.Round(r), sel ? T.TintaGrafite : T.TextoSecundario,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }
    }
}
