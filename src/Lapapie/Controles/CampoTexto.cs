using System.ComponentModel;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Máscara/validação aplicada enquanto a pessoa digita.</summary>
    public enum FormatoCampo { Livre, Cpf, Cnpj, Cep, Telefone, Inteiro, Moeda, Uf }

    /// <summary>
    /// Caixa de texto com borda arredondada, texto de exemplo (placeholder),
    /// ícone opcional e formatação automática (CPF, CNPJ, CEP, telefone, moeda...).
    /// <para>
    /// É um UserControl que "embrulha" um TextBox sem borda: o TextBox cuida da
    /// digitação e o UserControl desenha a moldura com as cores da marca.
    /// </para>
    /// </summary>
    [ToolboxItem(true), DefaultEvent(nameof(TextoAlterado))]
    public class CampoTexto : UserControl
    {
        private readonly TextBox _caixa;
        private string _icone = string.Empty;
        private FormatoCampo _formato = FormatoCampo.Livre;
        private bool _comErro;

        /// <summary>Disparado a cada alteração do texto.</summary>
        public event EventHandler TextoAlterado;

        /// <summary>Repassa as teclas da caixa interna (ex.: Enter para confirmar).</summary>
        public event KeyEventHandler TeclaPressionada;

        public CampoTexto()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            // A caixa interna precisa existir ANTES de mudar BackColor/Size/Padding:
            // essas propriedades disparam OnBackColorChanged/OnLayout, que usam _caixa.
            _caixa = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = T.Superficie,
                ForeColor = T.TintaGrafite,
                Font = T.Texto(10.5f)
            };
            _caixa.TextChanged += (s, e) => { ComErro = false; TextoAlterado?.Invoke(this, EventArgs.Empty); };
            _caixa.GotFocus += (s, e) => Invalidate();
            _caixa.LostFocus += (s, e) => { AplicarFormatoFinal(); Invalidate(); };
            _caixa.KeyPress += FiltrarTecla;
            _caixa.KeyDown += (s, e) => TeclaPressionada?.Invoke(this, e);
            Controls.Add(_caixa);
            BackColor = T.Superficie;
            Size = new Size(260, 40);
            Padding = new Padding(14, 0, 14, 0);
            Cursor = Cursors.IBeam;
        }

        /// <summary>Acesso ao TextBox interno (para autocompletar, seleção etc.).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox CaixaInterna => _caixa;

        [Category("Lapapiê"), Description("Texto digitado."), DefaultValue("")]
        public string Texto
        {
            get => _caixa.Text;
            set => _caixa.Text = value ?? string.Empty;
        }

        [Category("Lapapiê"), Description("Texto de exemplo exibido quando o campo está vazio."), DefaultValue("")]
        public string Placeholder
        {
            get => _caixa.PlaceholderText;
            set => _caixa.PlaceholderText = value ?? string.Empty;
        }

        [Category("Lapapiê"), DefaultValue(32767)]
        public int MaxLength { get => _caixa.MaxLength; set => _caixa.MaxLength = value; }

        [Category("Lapapiê"), Description("Oculta os caracteres (campo de senha)."), DefaultValue(false)]
        public bool Senha
        {
            get => _caixa.UseSystemPasswordChar;
            set => _caixa.UseSystemPasswordChar = value;
        }

        [Category("Lapapiê"), DefaultValue(false)]
        public bool SomenteLeitura
        {
            get => _caixa.ReadOnly;
            set { _caixa.ReadOnly = value; _caixa.BackColor = BackColor; Invalidate(); }
        }

        [Category("Lapapiê"), DefaultValue(false)]
        public bool Multilinha
        {
            get => _caixa.Multiline;
            set { _caixa.Multiline = value; _caixa.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None; PerformLayout(); }
        }

        [Category("Lapapiê"), Description("Glifo (Segoe MDL2 Assets) mostrado à esquerda."), DefaultValue("")]
        public string Icone { get => _icone; set { _icone = value ?? ""; PerformLayout(); Invalidate(); } }

        [Category("Lapapiê"), DefaultValue(FormatoCampo.Livre)]
        public FormatoCampo Formato
        {
            get => _formato;
            set
            {
                _formato = value;
                _caixa.TextAlign = value == FormatoCampo.Moeda || value == FormatoCampo.Inteiro
                    ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                if (value == FormatoCampo.Uf) _caixa.CharacterCasing = CharacterCasing.Upper;
            }
        }

        /// <summary>Destaca a borda em argila para indicar erro de validação.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ComErro { get => _comErro; set { if (_comErro != value) { _comErro = value; Invalidate(); } } }

        /// <summary>Texto sem pontuação (para CPF, CNPJ, CEP e telefone).</summary>
        [Browsable(false)]
        public string SomenteDigitos => Validacao.SomenteDigitos(Texto);

        /// <summary>Valor numérico do campo (formato Moeda/Inteiro). Retorna 0 se inválido.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal ValorDecimal
        {
            get => Formatacao.TentarLerDecimal(Texto, out var v) ? v : 0m;
            set => Texto = _formato == FormatoCampo.Inteiro ? value.ToString("0") : value.ToString("N2", Formatacao.PtBr);
        }

        public new void Focus() => _caixa.Focus();
        public void SelecionarTudo() => _caixa.SelectAll();

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (_caixa != null) _caixa.BackColor = BackColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _caixa.Focus();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (_caixa == null) return;
            int esquerda = Padding.Left + (string.IsNullOrEmpty(_icone) ? 0 : 24);
            int largura = Math.Max(10, Width - esquerda - Padding.Right);
            if (_caixa.Multiline)
                _caixa.SetBounds(esquerda, 9, largura, Math.Max(10, Height - 18));
            else
                _caixa.SetBounds(esquerda, (Height - _caixa.PreferredHeight) / 2 + 1, largura, _caixa.PreferredHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_caixa == null) return;
            var g = e.Graphics;
            g.Clear(Parent != null ? T.FundoEfetivo(Parent) : T.Fundo);
            T.Suavizar(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float raio = _caixa.Multiline ? 12 : r.Height / 2f;
            var corBorda = _comErro ? T.ArgilaEscura
                : _caixa.Focused ? T.ArgilaRosada
                : T.BordaForte;
            using (var caminho = T.RetanguloArredondado(r, raio))
            {
                using (var pincel = new SolidBrush(Enabled && !_caixa.ReadOnly ? BackColor : T.SuperficieAlternada))
                    g.FillPath(pincel, caminho);
                using (var caneta = new Pen(corBorda, _caixa.Focused || _comErro ? 1.6f : 1f))
                    g.DrawPath(caneta, caminho);
            }
            if (!string.IsNullOrEmpty(_icone))
            {
                var areaIcone = new Rectangle(Padding.Left - 2, _caixa.Multiline ? 8 : 0, 22, _caixa.Multiline ? 22 : Height);
                TextRenderer.DrawText(g, _icone, T.Icones(10.5f), areaIcone, T.TextoSecundario,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }

        /// <summary>Bloqueia caracteres que não fazem sentido para o formato.</summary>
        private void FiltrarTecla(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            switch (_formato)
            {
                case FormatoCampo.Cpf:
                case FormatoCampo.Cnpj:
                case FormatoCampo.Cep:
                case FormatoCampo.Inteiro:
                    if (!char.IsDigit(e.KeyChar)) e.Handled = true;
                    break;
                case FormatoCampo.Telefone:
                    if (!char.IsDigit(e.KeyChar) && "()- ".IndexOf(e.KeyChar) < 0) e.Handled = true;
                    break;
                case FormatoCampo.Moeda:
                    if (e.KeyChar == '.') e.KeyChar = ','; // ponto do teclado numérico vira vírgula
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',') e.Handled = true;
                    if (e.KeyChar == ',' && _caixa.Text.Contains(',')) e.Handled = true;
                    break;
                case FormatoCampo.Uf:
                    if (!char.IsLetter(e.KeyChar)) e.Handled = true;
                    break;
            }
        }

        /// <summary>Ao sair do campo, mostra o valor no formato brasileiro.</summary>
        private void AplicarFormatoFinal()
        {
            if (string.IsNullOrWhiteSpace(Texto)) return;
            switch (_formato)
            {
                case FormatoCampo.Cpf: Texto = Formatacao.Cpf(Texto); break;
                case FormatoCampo.Cnpj: Texto = Formatacao.Cnpj(Texto); break;
                case FormatoCampo.Cep: Texto = Formatacao.Cep(Texto); break;
                case FormatoCampo.Telefone: Texto = Formatacao.Telefone(Texto); break;
                case FormatoCampo.Moeda:
                    if (Formatacao.TentarLerDecimal(Texto, out var valor))
                        Texto = valor.ToString("N2", Formatacao.PtBr);
                    break;
            }
        }
    }
}
