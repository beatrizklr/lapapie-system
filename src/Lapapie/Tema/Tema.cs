using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Lapapie.Tema
{
    /// <summary>
    /// Identidade visual Lapapiê aplicada ao Windows Forms: paleta, fontes e
    /// estilos reutilizados. Ter tudo em um lugar só garante consistência entre
    /// as telas e permite trocar o tema mudando apenas este arquivo.
    /// </summary>
    public static class Tema
    {
        // ---------------- Paleta oficial (manual da marca) ----------------
        public static readonly Color PapelAlgodao = Color.FromArgb(243, 236, 226); // fundos e respiro
        public static readonly Color TintaGrafite = Color.FromArgb(51, 54, 61);    // texto e traço
        public static readonly Color ArgilaRosada = Color.FromArgb(201, 139, 123); // destaque afetivo
        public static readonly Color Salvia = Color.FromArgb(142, 155, 131);       // apoio, natureza
        public static readonly Color LacreDourado = Color.FromArgb(184, 149, 90);  // detalhes pontuais

        // ---------------- Tons derivados (tints/shades da paleta) ----------------
        public static readonly Color Fundo = PapelAlgodao;
        public static readonly Color Superficie = Color.FromArgb(255, 253, 249);
        public static readonly Color SuperficieAlternada = Color.FromArgb(250, 246, 239);
        public static readonly Color Borda = Color.FromArgb(230, 220, 205);
        public static readonly Color BordaForte = Color.FromArgb(214, 200, 181);
        public static readonly Color TextoSecundario = Color.FromArgb(107, 110, 117);
        public static readonly Color TextoSuave = Color.FromArgb(150, 152, 158);
        public static readonly Color GrafiteHover = Color.FromArgb(70, 74, 83);
        public static readonly Color ArgilaClara = Color.FromArgb(240, 221, 214);
        public static readonly Color ArgilaEscura = Color.FromArgb(158, 88, 70);   // texto de erro (contraste AA)
        public static readonly Color SalviaClara = Color.FromArgb(223, 229, 217);
        public static readonly Color SalviaEscura = Color.FromArgb(92, 105, 82);   // texto de sucesso
        public static readonly Color DouradoClaro = Color.FromArgb(241, 230, 208);
        public static readonly Color DouradoEscuro = Color.FromArgb(128, 98, 47);
        public static readonly Color CremeSobreGrafite = Color.FromArgb(214, 208, 199);

        /// <summary>Sequência de cores para séries de gráficos.</summary>
        public static readonly Color[] CoresGraficos =
        {
            TintaGrafite, ArgilaRosada, Salvia, LacreDourado,
            Color.FromArgb(122, 126, 135), Color.FromArgb(222, 183, 172), Color.FromArgb(189, 199, 180), Color.FromArgb(214, 192, 152)
        };

        // ---------------- Tipografia ----------------
        // Bahnschrift (Windows 10+) lembra as grotescas dos títulos da referência;
        // Georgia itálico conversa com o logotipo em serifa itálica.
        public const string FamiliaTitulo = "Bahnschrift SemiBold";
        public const string FamiliaTituloRegular = "Bahnschrift";
        public const string FamiliaTexto = "Segoe UI";
        public const string FamiliaSerifa = "Georgia";
        public const string FamiliaIcones = "Segoe MDL2 Assets";

        private static readonly Dictionary<string, Font> CacheFontes = new();

        /// <summary>Fontes são objetos GDI: guardamos em cache para não criar uma por pintura.</summary>
        public static Font Fonte(string familia, float tamanho, FontStyle estilo = FontStyle.Regular)
        {
            string chave = $"{familia}|{tamanho}|{estilo}";
            lock (CacheFontes)
            {
                if (!CacheFontes.TryGetValue(chave, out var fonte))
                {
                    fonte = new Font(familia, tamanho, estilo, GraphicsUnit.Point);
                    CacheFontes[chave] = fonte;
                }
                return fonte;
            }
        }

        public static Font Titulo(float tamanho) => Fonte(FamiliaTitulo, tamanho);
        public static Font Texto(float tamanho = 10f, FontStyle estilo = FontStyle.Regular) => Fonte(FamiliaTexto, tamanho, estilo);
        public static Font Serifa(float tamanho) => Fonte(FamiliaSerifa, tamanho, FontStyle.Italic);
        public static Font Icones(float tamanho) => Fonte(FamiliaIcones, tamanho);

        // ---------------- Desenho ----------------

        /// <summary>Caminho de retângulo com cantos arredondados.</summary>
        public static GraphicsPath RetanguloArredondado(RectangleF r, float raio)
        {
            var caminho = new GraphicsPath();
            float d = Math.Min(raio * 2, Math.Min(r.Width, r.Height));
            if (d <= 0.5f)
            {
                caminho.AddRectangle(r);
                return caminho;
            }
            caminho.AddArc(r.X, r.Y, d, d, 180, 90);
            caminho.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            caminho.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            caminho.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            caminho.CloseFigure();
            return caminho;
        }

        /// <summary>Ativa antisserrilhado (bordas suaves) no desenho.</summary>
        public static void Suavizar(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        /// <summary>Mistura duas cores (t = 0 → a, t = 1 → b).</summary>
        public static Color Misturar(Color a, Color b, float t) => Color.FromArgb(
            (int)(a.R + (b.R - a.R) * t), (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));

        /// <summary>Cor de fundo efetiva de um controle (sobe até achar uma cor não transparente).</summary>
        public static Color FundoEfetivo(Control controle)
        {
            for (var c = controle; c != null; c = c.Parent)
                if (c.BackColor.A == 255) return c.BackColor;
            return Fundo;
        }

        // ---------------- Grade de dados ----------------

        /// <summary>Aplica o visual da marca a um DataGridView.</summary>
        public static void EstilizarGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Superficie;
            grid.GridColor = Borda;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoGenerateColumns = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 44;
            grid.RowTemplate.Height = 42;

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Superficie,
                ForeColor = TextoSecundario,
                SelectionBackColor = Superficie,
                SelectionForeColor = TextoSecundario,
                Font = Fonte(FamiliaTitulo, 9f),
                Padding = new Padding(10, 0, 10, 0),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Superficie,
                ForeColor = TintaGrafite,
                SelectionBackColor = ArgilaClara,
                SelectionForeColor = TintaGrafite,
                Font = Texto(9.75f),
                Padding = new Padding(10, 0, 10, 0),
                WrapMode = DataGridViewTriState.False
            };
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = SuperficieAlternada };

            // DoubleBuffered é protegido: habilitamos por reflexão para a rolagem não piscar.
            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.SetValue(grid, true);
        }

        /// <summary>Formatos de coluna usados em <see cref="AdicionarColuna"/>.</summary>
        public const string FormatoMoeda = "C2";
        public const string FormatoInteiro = "N0";
        public const string FormatoData = "dd/MM/yyyy";
        public const string FormatoDataHora = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Cria uma coluna ligada a uma propriedade do Model.
        /// largura = 0 → coluna preenche o espaço restante (Fill), com o peso informado.
        /// </summary>
        public static DataGridViewTextBoxColumn AdicionarColuna(DataGridView grid, string propriedade, string cabecalho,
            int largura = 0, string formato = null, bool direita = false, float peso = 100f, bool centro = false)
        {
            var coluna = new DataGridViewTextBoxColumn
            {
                Name = "col" + propriedade,
                DataPropertyName = propriedade,
                HeaderText = cabecalho,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            };
            if (largura > 0)
            {
                coluna.Width = largura;
                coluna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            else
            {
                coluna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                coluna.FillWeight = peso;
                coluna.MinimumWidth = 80;
            }
            if (formato != null)
            {
                coluna.DefaultCellStyle.Format = formato;
                coluna.DefaultCellStyle.FormatProvider = Util.Formatacao.PtBr;
            }
            if (direita || formato == FormatoMoeda || formato == FormatoInteiro)
            {
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                coluna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (centro)
            {
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                coluna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            grid.Columns.Add(coluna);
            return coluna;
        }

        /// <summary>
        /// Desenha uma "etiqueta" (chip arredondado) dentro de uma célula do grid.
        /// Usado em CellPainting para mostrar status (Ativo, Cancelada, Estoque baixo...).
        /// </summary>
        public static void DesenharChipCelula(DataGridViewCellPaintingEventArgs e, string texto, Color fundo, Color corTexto)
        {
            e.PaintBackground(e.ClipBounds, (e.State & DataGridViewElementStates.Selected) != 0);
            var g = e.Graphics;
            Suavizar(g);
            var fonte = Texto(8.5f, FontStyle.Bold);
            var tamanho = TextRenderer.MeasureText(texto, fonte);
            var largura = tamanho.Width + 14;
            var altura = 24;
            var alinhamento = e.CellStyle.Alignment;
            float x = alinhamento == DataGridViewContentAlignment.MiddleCenter
                ? e.CellBounds.X + (e.CellBounds.Width - largura) / 2f
                : e.CellBounds.X + 10;
            var r = new RectangleF(x, e.CellBounds.Y + (e.CellBounds.Height - altura) / 2f, largura, altura);
            using (var caminho = RetanguloArredondado(r, altura / 2f))
            using (var pincel = new SolidBrush(fundo))
                g.FillPath(pincel, caminho);
            TextRenderer.DrawText(g, texto, fonte, Rectangle.Round(r), corTexto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            e.Handled = true;
        }

        // ---------------- Janela ----------------

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamanho);

        /// <summary>
        /// No Windows 11, pinta a barra de título com as cores da marca.
        /// Em versões anteriores a chamada simplesmente não tem efeito.
        /// </summary>
        public static void AplicarBarraTitulo(Form formulario)
        {
            try
            {
                const int DWMWA_BORDER_COLOR = 34, DWMWA_CAPTION_COLOR = 35, DWMWA_TEXT_COLOR = 36;
                int legenda = ColorTranslator.ToWin32(PapelAlgodao);
                int texto = ColorTranslator.ToWin32(TintaGrafite);
                int borda = ColorTranslator.ToWin32(BordaForte);
                DwmSetWindowAttribute(formulario.Handle, DWMWA_CAPTION_COLOR, ref legenda, sizeof(int));
                DwmSetWindowAttribute(formulario.Handle, DWMWA_TEXT_COLOR, ref texto, sizeof(int));
                DwmSetWindowAttribute(formulario.Handle, DWMWA_BORDER_COLOR, ref borda, sizeof(int));
            }
            catch (Exception)
            {
                // Recurso visual opcional: se o Windows não suportar, seguimos sem ele.
            }
        }

        /// <summary>Estiliza ComboBox, DateTimePicker, NumericUpDown e CheckBox de um formulário inteiro.</summary>
        public static void EstilizarControlesPadrao(Control raiz)
        {
            foreach (Control c in raiz.Controls)
            {
                switch (c)
                {
                    case ComboBox combo:
                        combo.FlatStyle = FlatStyle.Flat;
                        combo.BackColor = Superficie;
                        combo.ForeColor = TintaGrafite;
                        combo.Font = Texto(10.5f);
                        break;
                    case NumericUpDown numero:
                        numero.BorderStyle = BorderStyle.FixedSingle;
                        numero.BackColor = Superficie;
                        numero.ForeColor = TintaGrafite;
                        numero.Font = Texto(10.5f);
                        break;
                    case DateTimePicker data:
                        data.Font = Texto(10.5f);
                        data.CalendarTitleBackColor = TintaGrafite;
                        data.CalendarTitleForeColor = PapelAlgodao;
                        break;
                    case CheckBox check:
                        check.FlatStyle = FlatStyle.Flat;
                        check.FlatAppearance.BorderColor = BordaForte;
                        check.FlatAppearance.CheckedBackColor = ArgilaClara;
                        check.ForeColor = TintaGrafite;
                        break;
                    case CheckedListBox lista:
                        lista.BorderStyle = BorderStyle.None;
                        lista.BackColor = Superficie;
                        lista.ForeColor = TintaGrafite;
                        break;
                    case TabControl abas:
                        abas.Font = Texto(10f, FontStyle.Bold);
                        break;
                }
                if (c.HasChildren) EstilizarControlesPadrao(c);
            }
        }
    }
}
