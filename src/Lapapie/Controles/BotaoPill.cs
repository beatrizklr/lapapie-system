using System.ComponentModel;
using Lapapie.Tema;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Controles
{
    /// <summary>Variações visuais do botão.</summary>
    public enum EstiloBotao
    {
        /// <summary>Grafite com texto creme — ação principal da tela.</summary>
        Primario,
        /// <summary>Contorno grafite — ações secundárias.</summary>
        Secundario,
        /// <summary>Argila rosada — destaque afetivo (ex.: finalizar venda).</summary>
        Destaque,
        /// <summary>Fundo argila claro com texto escuro — ações destrutivas.</summary>
        Perigo,
        /// <summary>Somente texto sublinhado, como um link.</summary>
        Link,
        /// <summary>Fundo suave da superfície — ações discretas.</summary>
        Suave
    }

    /// <summary>
    /// Botão em formato de "pílula" (cantos totalmente arredondados), inspirado
    /// nos botões "Shop now →" da referência visual. Herda de Button para manter
    /// o comportamento padrão (AcceptButton, DialogResult, teclado, acessibilidade).
    /// </summary>
    [ToolboxItem(true)]
    public class BotaoPill : Button
    {
        private EstiloBotao _estilo = EstiloBotao.Primario;
        private string _icone = string.Empty;
        private bool _mostrarSeta;
        private bool _sobre;
        private bool _pressionado;

        public BotaoPill()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Font = T.Texto(10f, FontStyle.Bold);
            Size = new Size(150, 42);
            UseVisualStyleBackColor = false;
        }

        [Category("Lapapiê"), Description("Variação de cor do botão."), DefaultValue(EstiloBotao.Primario)]
        public EstiloBotao Estilo
        {
            get => _estilo;
            set { _estilo = value; Invalidate(); }
        }

        [Category("Lapapiê"), Description("Glifo da fonte Segoe MDL2 Assets exibido antes do texto."), DefaultValue("")]
        public string Icone
        {
            get => _icone;
            set { _icone = value ?? string.Empty; Invalidate(); }
        }

        [Category("Lapapiê"), Description("Mostra um círculo com seta à direita (estilo \"Shop now →\")."), DefaultValue(false)]
        public bool MostrarSeta
        {
            get => _mostrarSeta;
            set { _mostrarSeta = value; Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _sobre = false; _pressionado = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _pressionado = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _pressionado = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        private (Color Fundo, Color Texto, Color Borda) Cores()
        {
            return _estilo switch
            {
                EstiloBotao.Primario => (_sobre ? T.GrafiteHover : T.TintaGrafite, T.PapelAlgodao, Color.Empty),
                EstiloBotao.Destaque => (_sobre ? T.Misturar(T.ArgilaRosada, T.TintaGrafite, 0.15f) : T.ArgilaRosada, Color.White, Color.Empty),
                EstiloBotao.Secundario => (_sobre ? T.ArgilaClara : Color.Transparent, T.TintaGrafite, T.TintaGrafite),
                EstiloBotao.Perigo => (_sobre ? T.Misturar(T.ArgilaClara, T.ArgilaRosada, 0.35f) : T.ArgilaClara, T.ArgilaEscura, Color.Empty),
                EstiloBotao.Suave => (_sobre ? T.Borda : T.SuperficieAlternada, T.TintaGrafite, T.Borda),
                _ => (Color.Transparent, _sobre ? T.ArgilaEscura : T.TintaGrafite, Color.Empty)
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(T.FundoEfetivo(Parent ?? this));
            T.Suavizar(g);

            var (fundo, texto, borda) = Cores();
            if (!Enabled)
            {
                fundo = fundo.A == 0 ? fundo : T.Misturar(fundo, T.FundoEfetivo(Parent ?? this), 0.55f);
                texto = T.Misturar(texto, T.FundoEfetivo(Parent ?? this), 0.45f);
            }

            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            if (_pressionado) r.Inflate(-1, -1);
            float raio = r.Height / 2f;

            if (_estilo != EstiloBotao.Link)
            {
                using var caminho = T.RetanguloArredondado(r, raio);
                if (fundo.A > 0)
                    using (var pincel = new SolidBrush(fundo)) g.FillPath(pincel, caminho);
                if (!borda.IsEmpty)
                    using (var caneta = new Pen(Enabled ? borda : T.Borda, 1.2f)) g.DrawPath(caneta, caminho);
            }

            // Área do texto (deixa espaço para a seta, se houver)
            var areaTexto = Rectangle.Round(r);
            if (_mostrarSeta)
            {
                float d = r.Height - 10;
                var circulo = new RectangleF(r.Right - d - 5, r.Y + 5, d, d);
                var corCirculo = _estilo == EstiloBotao.Secundario || _estilo == EstiloBotao.Suave ? T.TintaGrafite : T.PapelAlgodao;
                var corSeta = _estilo == EstiloBotao.Secundario || _estilo == EstiloBotao.Suave ? T.PapelAlgodao : fundo;
                using (var pincel = new SolidBrush(corCirculo)) g.FillEllipse(pincel, circulo);
                TextRenderer.DrawText(g, Icones.SetaDireita, T.Icones(9f), Rectangle.Round(circulo), corSeta,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                areaTexto.Width -= (int)d + 6;
                areaTexto.X += 6;
            }

            var fonteTexto = Font;
            var tamanhoTexto = TextRenderer.MeasureText(g, Text, fonteTexto, Size.Empty, TextFormatFlags.NoPadding);
            int larguraIcone = string.IsNullOrEmpty(_icone) ? 0 : 22;
            int larguraTotal = tamanhoTexto.Width + larguraIcone;
            int x = areaTexto.X + (areaTexto.Width - larguraTotal) / 2;
            if (_mostrarSeta) x = Math.Max(areaTexto.X + 14, x);

            if (larguraIcone > 0)
            {
                var areaIcone = new Rectangle(x, areaTexto.Y, 18, areaTexto.Height);
                TextRenderer.DrawText(g, _icone, T.Icones(Font.Size + 0.5f), areaIcone, texto,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
            var areaFinal = new Rectangle(x + larguraIcone, areaTexto.Y, tamanhoTexto.Width + 2, areaTexto.Height);
            var fonteFinal = _estilo == EstiloBotao.Link && _sobre ? T.Fonte(fonteTexto.FontFamily.Name, fonteTexto.Size, fonteTexto.Style | FontStyle.Underline) : fonteTexto;
            TextRenderer.DrawText(g, Text, fonteFinal, areaFinal, texto,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis);

            if (Focused && ShowFocusCues && _estilo != EstiloBotao.Link)
            {
                var foco = r;
                foco.Inflate(-3, -3);
                using var caminhoFoco = T.RetanguloArredondado(foco, foco.Height / 2f);
                using var caneta = new Pen(Color.FromArgb(120, T.ArgilaRosada), 1.5f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
                g.DrawPath(caneta, caminhoFoco);
            }
        }
    }
}
