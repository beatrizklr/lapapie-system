namespace Lapapie.Forms.Cadastros
{
    partial class FrmProdutoEdicao
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Formulários do Windows

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.picImagem = new System.Windows.Forms.PictureBox();
            this.btnEscolherImagem = new Lapapie.Controles.BotaoPill();
            this.lblInfoImagem = new System.Windows.Forms.Label();
            this.cardResumo = new Lapapie.Controles.PainelCartao();
            this.lblResumo = new System.Windows.Forms.Label();
            this.abasSecoes = new Lapapie.Controles.AbasPill();
            this.pnlDados = new System.Windows.Forms.Panel();
            this.pnlEstoque = new System.Windows.Forms.Panel();
            this.pnlFornecedores = new System.Windows.Forms.Panel();
            this.pnlKit = new System.Windows.Forms.Panel();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new Lapapie.Controles.CampoTexto();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new Lapapie.Controles.CampoTexto();
            this.btnGerarCodigo = new Lapapie.Controles.BotaoPill();
            this.lblMarca = new System.Windows.Forms.Label();
            this.txtMarca = new Lapapie.Controles.CampoTexto();
            this.lblUnidade = new System.Windows.Forms.Label();
            this.cboUnidade = new System.Windows.Forms.ComboBox();
            this.lblPreco = new System.Windows.Forms.Label();
            this.txtPreco = new Lapapie.Controles.CampoTexto();
            this.lblPromocao = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new Lapapie.Controles.CampoTexto();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.lblSaldoTitulo = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblEstoqueInicial = new System.Windows.Forms.Label();
            this.nudEstoqueInicial = new System.Windows.Forms.NumericUpDown();
            this.lblMinimo = new System.Windows.Forms.Label();
            this.nudMinimo = new System.Windows.Forms.NumericUpDown();
            this.lblLocalizacao = new System.Windows.Forms.Label();
            this.txtLocalizacao = new Lapapie.Controles.CampoTexto();
            this.lblNotaEstoque = new System.Windows.Forms.Label();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.cboFornecedor = new System.Windows.Forms.ComboBox();
            this.lblCodigoFornecedor = new System.Windows.Forms.Label();
            this.txtCodigoFornecedor = new Lapapie.Controles.CampoTexto();
            this.lblCusto = new System.Windows.Forms.Label();
            this.txtCusto = new Lapapie.Controles.CampoTexto();
            this.lblPrazo = new System.Windows.Forms.Label();
            this.nudPrazo = new System.Windows.Forms.NumericUpDown();
            this.btnAdicionarFornecedor = new Lapapie.Controles.BotaoPill();
            this.btnRemoverFornecedor = new Lapapie.Controles.BotaoPill();
            this.dgvFornecedores = new System.Windows.Forms.DataGridView();
            this.lblMargem = new System.Windows.Forms.Label();
            this.lblComponente = new System.Windows.Forms.Label();
            this.cboComponente = new System.Windows.Forms.ComboBox();
            this.lblQtdComponente = new System.Windows.Forms.Label();
            this.nudQtdComponente = new System.Windows.Forms.NumericUpDown();
            this.btnAdicionarComponente = new Lapapie.Controles.BotaoPill();
            this.btnRemoverComponente = new Lapapie.Controles.BotaoPill();
            this.dgvComponentes = new System.Windows.Forms.DataGridView();
            this.lblResumoKit = new System.Windows.Forms.Label();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.picImagem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEstoqueInicial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrazo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtdComponente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponentes)).BeginInit();
            this.cardResumo.SuspendLayout();
            this.pnlDados.SuspendLayout();
            this.pnlEstoque.SuspendLayout();
            this.pnlFornecedores.SuspendLayout();
            this.pnlKit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "NOVO PRODUTO";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(824, 22);
            this.lblSubtitulo.Text = "Campos com * são obrigatórios.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // picImagem
            // 
            this.picImagem.Location = new System.Drawing.Point(28, 96);
            this.picImagem.Size = new System.Drawing.Size(200, 200);
            this.picImagem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImagem.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.picImagem.TabStop = false;
            this.picImagem.Name = "picImagem";
            this.picImagem.TabIndex = 2;
            // 
            // btnEscolherImagem
            // 
            this.btnEscolherImagem.Location = new System.Drawing.Point(28, 306);
            this.btnEscolherImagem.Size = new System.Drawing.Size(200, 38);
            this.btnEscolherImagem.Text = "Escolher imagem";
            this.btnEscolherImagem.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnEscolherImagem.Icone = Lapapie.Tema.Icones.Editar;
            this.btnEscolherImagem.Name = "btnEscolherImagem";
            this.btnEscolherImagem.TabIndex = 3;
            this.btnEscolherImagem.Click += new System.EventHandler(this.btnEscolherImagem_Click);
            // 
            // lblInfoImagem
            // 
            this.lblInfoImagem.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInfoImagem.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblInfoImagem.Location = new System.Drawing.Point(28, 350);
            this.lblInfoImagem.Size = new System.Drawing.Size(200, 54);
            this.lblInfoImagem.Text = "PNG ou JPG. Sem imagem própria, o produto usa a ilustração da categoria.";
            this.lblInfoImagem.Name = "lblInfoImagem";
            this.lblInfoImagem.TabIndex = 4;
            // 
            // cardResumo
            // 
            this.cardResumo.Controls.Add(this.lblResumo);
            this.cardResumo.Location = new System.Drawing.Point(28, 412);
            this.cardResumo.Size = new System.Drawing.Size(200, 144);
            this.cardResumo.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.cardResumo.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.cardResumo.Name = "cardResumo";
            this.cardResumo.TabIndex = 5;
            // 
            // lblResumo
            // 
            this.lblResumo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResumo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResumo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblResumo.Text = "Resumo";
            this.lblResumo.Name = "lblResumo";
            this.lblResumo.TabIndex = 0;
            // 
            // abasSecoes
            // 
            this.abasSecoes.Location = new System.Drawing.Point(256, 96);
            this.abasSecoes.Size = new System.Drawing.Size(596, 38);
            this.abasSecoes.Opcoes = "Dados gerais|Estoque|Fornecedores|Composição do kit";
            this.abasSecoes.IndiceSelecionado = 0;
            this.abasSecoes.Name = "abasSecoes";
            this.abasSecoes.TabIndex = 6;
            this.abasSecoes.SelecaoAlterada += new System.EventHandler(this.abasSecoes_SelecaoAlterada);
            // 
            // pnlDados
            // 
            this.pnlDados.Controls.Add(this.lblNome);
            this.pnlDados.Controls.Add(this.txtNome);
            this.pnlDados.Controls.Add(this.lblCategoria);
            this.pnlDados.Controls.Add(this.cboCategoria);
            this.pnlDados.Controls.Add(this.lblTipo);
            this.pnlDados.Controls.Add(this.cboTipo);
            this.pnlDados.Controls.Add(this.lblCodigoBarras);
            this.pnlDados.Controls.Add(this.txtCodigoBarras);
            this.pnlDados.Controls.Add(this.btnGerarCodigo);
            this.pnlDados.Controls.Add(this.lblMarca);
            this.pnlDados.Controls.Add(this.txtMarca);
            this.pnlDados.Controls.Add(this.lblUnidade);
            this.pnlDados.Controls.Add(this.cboUnidade);
            this.pnlDados.Controls.Add(this.lblPreco);
            this.pnlDados.Controls.Add(this.txtPreco);
            this.pnlDados.Controls.Add(this.lblPromocao);
            this.pnlDados.Controls.Add(this.lblDescricao);
            this.pnlDados.Controls.Add(this.txtDescricao);
            this.pnlDados.Controls.Add(this.chkAtivo);
            this.pnlDados.Location = new System.Drawing.Point(256, 146);
            this.pnlDados.Size = new System.Drawing.Size(596, 410);
            this.pnlDados.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlDados.Name = "pnlDados";
            this.pnlDados.TabIndex = 7;
            // 
            // pnlEstoque
            // 
            this.pnlEstoque.Controls.Add(this.lblSaldoTitulo);
            this.pnlEstoque.Controls.Add(this.lblSaldo);
            this.pnlEstoque.Controls.Add(this.lblEstoqueInicial);
            this.pnlEstoque.Controls.Add(this.nudEstoqueInicial);
            this.pnlEstoque.Controls.Add(this.lblMinimo);
            this.pnlEstoque.Controls.Add(this.nudMinimo);
            this.pnlEstoque.Controls.Add(this.lblLocalizacao);
            this.pnlEstoque.Controls.Add(this.txtLocalizacao);
            this.pnlEstoque.Controls.Add(this.lblNotaEstoque);
            this.pnlEstoque.Location = new System.Drawing.Point(256, 146);
            this.pnlEstoque.Size = new System.Drawing.Size(596, 410);
            this.pnlEstoque.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlEstoque.Name = "pnlEstoque";
            this.pnlEstoque.TabIndex = 8;
            // 
            // pnlFornecedores
            // 
            this.pnlFornecedores.Controls.Add(this.lblFornecedor);
            this.pnlFornecedores.Controls.Add(this.cboFornecedor);
            this.pnlFornecedores.Controls.Add(this.lblCodigoFornecedor);
            this.pnlFornecedores.Controls.Add(this.txtCodigoFornecedor);
            this.pnlFornecedores.Controls.Add(this.lblCusto);
            this.pnlFornecedores.Controls.Add(this.txtCusto);
            this.pnlFornecedores.Controls.Add(this.lblPrazo);
            this.pnlFornecedores.Controls.Add(this.nudPrazo);
            this.pnlFornecedores.Controls.Add(this.btnAdicionarFornecedor);
            this.pnlFornecedores.Controls.Add(this.btnRemoverFornecedor);
            this.pnlFornecedores.Controls.Add(this.dgvFornecedores);
            this.pnlFornecedores.Controls.Add(this.lblMargem);
            this.pnlFornecedores.Location = new System.Drawing.Point(256, 146);
            this.pnlFornecedores.Size = new System.Drawing.Size(596, 410);
            this.pnlFornecedores.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlFornecedores.Name = "pnlFornecedores";
            this.pnlFornecedores.TabIndex = 9;
            // 
            // pnlKit
            // 
            this.pnlKit.Controls.Add(this.lblComponente);
            this.pnlKit.Controls.Add(this.cboComponente);
            this.pnlKit.Controls.Add(this.lblQtdComponente);
            this.pnlKit.Controls.Add(this.nudQtdComponente);
            this.pnlKit.Controls.Add(this.btnAdicionarComponente);
            this.pnlKit.Controls.Add(this.btnRemoverComponente);
            this.pnlKit.Controls.Add(this.dgvComponentes);
            this.pnlKit.Controls.Add(this.lblResumoKit);
            this.pnlKit.Location = new System.Drawing.Point(256, 146);
            this.pnlKit.Size = new System.Drawing.Size(596, 410);
            this.pnlKit.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlKit.Name = "pnlKit";
            this.pnlKit.TabIndex = 10;
            // 
            // lblNome
            // 
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNome.Location = new System.Drawing.Point(2, 0);
            this.lblNome.Text = "Nome *";
            this.lblNome.AutoSize = true;
            this.lblNome.Name = "lblNome";
            this.lblNome.TabIndex = 0;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(0, 22);
            this.txtNome.Size = new System.Drawing.Size(596, 40);
            this.txtNome.MaxLength = 120;
            this.txtNome.Name = "txtNome";
            this.txtNome.TabIndex = 1;
            // 
            // lblCategoria
            // 
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCategoria.Location = new System.Drawing.Point(2, 70);
            this.lblCategoria.Text = "Categoria *";
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.TabIndex = 2;
            // 
            // cboCategoria
            // 
            this.cboCategoria.Location = new System.Drawing.Point(0, 94);
            this.cboCategoria.Size = new System.Drawing.Size(290, 29);
            this.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategoria.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.TabIndex = 3;
            // 
            // lblTipo
            // 
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTipo.Location = new System.Drawing.Point(308, 70);
            this.lblTipo.Text = "Tipo";
            this.lblTipo.AutoSize = true;
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 4;
            // 
            // cboTipo
            // 
            this.cboTipo.Location = new System.Drawing.Point(306, 94);
            this.cboTipo.Size = new System.Drawing.Size(290, 29);
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTipo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.TabIndex = 5;
            this.cboTipo.SelectedIndexChanged += new System.EventHandler(this.cboTipo_SelectedIndexChanged);
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCodigoBarras.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCodigoBarras.Location = new System.Drawing.Point(2, 140);
            this.lblCodigoBarras.Text = "Código de barras";
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.TabIndex = 6;
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(0, 162);
            this.txtCodigoBarras.Size = new System.Drawing.Size(220, 40);
            this.txtCodigoBarras.Formato = Lapapie.Controles.FormatoCampo.Inteiro;
            this.txtCodigoBarras.MaxLength = 14;
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.TabIndex = 7;
            // 
            // btnGerarCodigo
            // 
            this.btnGerarCodigo.Location = new System.Drawing.Point(226, 162);
            this.btnGerarCodigo.Size = new System.Drawing.Size(66, 40);
            this.btnGerarCodigo.Text = "Gerar";
            this.btnGerarCodigo.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnGerarCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGerarCodigo.Name = "btnGerarCodigo";
            this.btnGerarCodigo.TabIndex = 8;
            this.btnGerarCodigo.Click += new System.EventHandler(this.btnGerarCodigo_Click);
            // 
            // lblMarca
            // 
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblMarca.Location = new System.Drawing.Point(308, 140);
            this.lblMarca.Text = "Marca";
            this.lblMarca.AutoSize = true;
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.TabIndex = 9;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(306, 162);
            this.txtMarca.Size = new System.Drawing.Size(290, 40);
            this.txtMarca.MaxLength = 60;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.TabIndex = 10;
            // 
            // lblUnidade
            // 
            this.lblUnidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUnidade.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblUnidade.Location = new System.Drawing.Point(2, 210);
            this.lblUnidade.Text = "Unidade *";
            this.lblUnidade.AutoSize = true;
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.TabIndex = 11;
            // 
            // cboUnidade
            // 
            this.cboUnidade.Location = new System.Drawing.Point(0, 234);
            this.cboUnidade.Size = new System.Drawing.Size(140, 29);
            this.cboUnidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboUnidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUnidade.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboUnidade.MaxLength = 10;
            this.cboUnidade.Name = "cboUnidade";
            this.cboUnidade.TabIndex = 12;
            // 
            // lblPreco
            // 
            this.lblPreco.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPreco.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPreco.Location = new System.Drawing.Point(158, 210);
            this.lblPreco.Text = "Preço de venda (R$) *";
            this.lblPreco.AutoSize = true;
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.TabIndex = 13;
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(156, 232);
            this.txtPreco.Size = new System.Drawing.Size(134, 40);
            this.txtPreco.Formato = Lapapie.Controles.FormatoCampo.Moeda;
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.TabIndex = 14;
            this.txtPreco.TextoAlterado += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblPromocao
            // 
            this.lblPromocao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPromocao.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblPromocao.Location = new System.Drawing.Point(306, 234);
            this.lblPromocao.Size = new System.Drawing.Size(290, 40);
            this.lblPromocao.Text = "";
            this.lblPromocao.Name = "lblPromocao";
            this.lblPromocao.TabIndex = 15;
            // 
            // lblDescricao
            // 
            this.lblDescricao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDescricao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDescricao.Location = new System.Drawing.Point(2, 280);
            this.lblDescricao.Text = "Descrição";
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.TabIndex = 16;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(0, 302);
            this.txtDescricao.Size = new System.Drawing.Size(596, 76);
            this.txtDescricao.Multilinha = true;
            this.txtDescricao.MaxLength = 500;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.TabIndex = 17;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkAtivo.Location = new System.Drawing.Point(2, 382);
            this.chkAtivo.Text = "Produto ativo (aparece no caixa)";
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 18;
            // 
            // lblSaldoTitulo
            // 
            this.lblSaldoTitulo.AutoSize = true;
            this.lblSaldoTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSaldoTitulo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSaldoTitulo.Location = new System.Drawing.Point(2, 0);
            this.lblSaldoTitulo.Text = "SALDO ATUAL";
            this.lblSaldoTitulo.Name = "lblSaldoTitulo";
            this.lblSaldoTitulo.TabIndex = 0;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSaldo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblSaldo.Location = new System.Drawing.Point(0, 18);
            this.lblSaldo.Text = "0 UN";
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.TabIndex = 1;
            // 
            // lblEstoqueInicial
            // 
            this.lblEstoqueInicial.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEstoqueInicial.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblEstoqueInicial.Location = new System.Drawing.Point(2, 90);
            this.lblEstoqueInicial.Text = "Estoque inicial";
            this.lblEstoqueInicial.AutoSize = true;
            this.lblEstoqueInicial.Name = "lblEstoqueInicial";
            this.lblEstoqueInicial.TabIndex = 2;
            // 
            // nudEstoqueInicial
            // 
            this.nudEstoqueInicial.Location = new System.Drawing.Point(0, 114);
            this.nudEstoqueInicial.Size = new System.Drawing.Size(160, 29);
            this.nudEstoqueInicial.Maximum = 999999;
            this.nudEstoqueInicial.Name = "nudEstoqueInicial";
            this.nudEstoqueInicial.TabIndex = 3;
            // 
            // lblMinimo
            // 
            this.lblMinimo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMinimo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblMinimo.Location = new System.Drawing.Point(192, 90);
            this.lblMinimo.Text = "Estoque mínimo *";
            this.lblMinimo.AutoSize = true;
            this.lblMinimo.Name = "lblMinimo";
            this.lblMinimo.TabIndex = 4;
            // 
            // nudMinimo
            // 
            this.nudMinimo.Location = new System.Drawing.Point(190, 114);
            this.nudMinimo.Size = new System.Drawing.Size(160, 29);
            this.nudMinimo.Maximum = 999999;
            this.nudMinimo.Value = 5;
            this.nudMinimo.Name = "nudMinimo";
            this.nudMinimo.TabIndex = 5;
            // 
            // lblLocalizacao
            // 
            this.lblLocalizacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLocalizacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblLocalizacao.Location = new System.Drawing.Point(2, 166);
            this.lblLocalizacao.Text = "Localização na loja";
            this.lblLocalizacao.AutoSize = true;
            this.lblLocalizacao.Name = "lblLocalizacao";
            this.lblLocalizacao.TabIndex = 6;
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.Location = new System.Drawing.Point(0, 188);
            this.txtLocalizacao.Size = new System.Drawing.Size(596, 40);
            this.txtLocalizacao.MaxLength = 50;
            this.txtLocalizacao.Placeholder = "Ex.: Corredor 2 - Prateleira B";
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.TabIndex = 7;
            // 
            // lblNotaEstoque
            // 
            this.lblNotaEstoque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNotaEstoque.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNotaEstoque.Location = new System.Drawing.Point(0, 244);
            this.lblNotaEstoque.Size = new System.Drawing.Size(596, 90);
            this.lblNotaEstoque.Text = "O saldo não é digitado aqui: ele só muda por movimentações (vendas, recebimento de compras e ajustes), para que todo o histórico fique registrado.";
            this.lblNotaEstoque.Name = "lblNotaEstoque";
            this.lblNotaEstoque.TabIndex = 8;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFornecedor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblFornecedor.Location = new System.Drawing.Point(2, 0);
            this.lblFornecedor.Text = "Fornecedor";
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.TabIndex = 0;
            // 
            // cboFornecedor
            // 
            this.cboFornecedor.Location = new System.Drawing.Point(0, 24);
            this.cboFornecedor.Size = new System.Drawing.Size(230, 29);
            this.cboFornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFornecedor.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFornecedor.Name = "cboFornecedor";
            this.cboFornecedor.TabIndex = 1;
            // 
            // lblCodigoFornecedor
            // 
            this.lblCodigoFornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCodigoFornecedor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCodigoFornecedor.Location = new System.Drawing.Point(244, 0);
            this.lblCodigoFornecedor.Text = "Cód. no fornecedor";
            this.lblCodigoFornecedor.AutoSize = true;
            this.lblCodigoFornecedor.Name = "lblCodigoFornecedor";
            this.lblCodigoFornecedor.TabIndex = 2;
            // 
            // txtCodigoFornecedor
            // 
            this.txtCodigoFornecedor.Location = new System.Drawing.Point(242, 22);
            this.txtCodigoFornecedor.Size = new System.Drawing.Size(110, 40);
            this.txtCodigoFornecedor.MaxLength = 30;
            this.txtCodigoFornecedor.Name = "txtCodigoFornecedor";
            this.txtCodigoFornecedor.TabIndex = 3;
            // 
            // lblCusto
            // 
            this.lblCusto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCusto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCusto.Location = new System.Drawing.Point(366, 0);
            this.lblCusto.Text = "Custo (R$)";
            this.lblCusto.AutoSize = true;
            this.lblCusto.Name = "lblCusto";
            this.lblCusto.TabIndex = 4;
            // 
            // txtCusto
            // 
            this.txtCusto.Location = new System.Drawing.Point(364, 22);
            this.txtCusto.Size = new System.Drawing.Size(110, 40);
            this.txtCusto.Formato = Lapapie.Controles.FormatoCampo.Moeda;
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.TabIndex = 5;
            // 
            // lblPrazo
            // 
            this.lblPrazo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrazo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPrazo.Location = new System.Drawing.Point(488, 0);
            this.lblPrazo.Text = "Prazo (dias)";
            this.lblPrazo.AutoSize = true;
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.TabIndex = 6;
            // 
            // nudPrazo
            // 
            this.nudPrazo.Location = new System.Drawing.Point(486, 27);
            this.nudPrazo.Size = new System.Drawing.Size(110, 29);
            this.nudPrazo.Maximum = 365;
            this.nudPrazo.Value = 3;
            this.nudPrazo.Name = "nudPrazo";
            this.nudPrazo.TabIndex = 7;
            // 
            // btnAdicionarFornecedor
            // 
            this.btnAdicionarFornecedor.Location = new System.Drawing.Point(0, 74);
            this.btnAdicionarFornecedor.Size = new System.Drawing.Size(170, 38);
            this.btnAdicionarFornecedor.Text = "Adicionar / atualizar";
            this.btnAdicionarFornecedor.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnAdicionarFornecedor.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnAdicionarFornecedor.Name = "btnAdicionarFornecedor";
            this.btnAdicionarFornecedor.TabIndex = 8;
            this.btnAdicionarFornecedor.Click += new System.EventHandler(this.btnAdicionarFornecedor_Click);
            // 
            // btnRemoverFornecedor
            // 
            this.btnRemoverFornecedor.Location = new System.Drawing.Point(178, 74);
            this.btnRemoverFornecedor.Size = new System.Drawing.Size(120, 38);
            this.btnRemoverFornecedor.Text = "Remover";
            this.btnRemoverFornecedor.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnRemoverFornecedor.Icone = Lapapie.Tema.Icones.Remover;
            this.btnRemoverFornecedor.Name = "btnRemoverFornecedor";
            this.btnRemoverFornecedor.TabIndex = 9;
            this.btnRemoverFornecedor.Click += new System.EventHandler(this.btnRemoverFornecedor_Click);
            // 
            // dgvFornecedores
            // 
            this.dgvFornecedores.Location = new System.Drawing.Point(0, 122);
            this.dgvFornecedores.Size = new System.Drawing.Size(596, 232);
            this.dgvFornecedores.Name = "dgvFornecedores";
            this.dgvFornecedores.TabIndex = 10;
            // 
            // lblMargem
            // 
            this.lblMargem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMargem.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblMargem.Location = new System.Drawing.Point(0, 362);
            this.lblMargem.Size = new System.Drawing.Size(596, 40);
            this.lblMargem.Text = "";
            this.lblMargem.Name = "lblMargem";
            this.lblMargem.TabIndex = 11;
            // 
            // lblComponente
            // 
            this.lblComponente.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblComponente.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblComponente.Location = new System.Drawing.Point(2, 0);
            this.lblComponente.Text = "Produto do kit";
            this.lblComponente.AutoSize = true;
            this.lblComponente.Name = "lblComponente";
            this.lblComponente.TabIndex = 0;
            // 
            // cboComponente
            // 
            this.cboComponente.Location = new System.Drawing.Point(0, 24);
            this.cboComponente.Size = new System.Drawing.Size(420, 29);
            this.cboComponente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComponente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboComponente.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboComponente.Name = "cboComponente";
            this.cboComponente.TabIndex = 1;
            // 
            // lblQtdComponente
            // 
            this.lblQtdComponente.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblQtdComponente.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblQtdComponente.Location = new System.Drawing.Point(438, 0);
            this.lblQtdComponente.Text = "Quantidade";
            this.lblQtdComponente.AutoSize = true;
            this.lblQtdComponente.Name = "lblQtdComponente";
            this.lblQtdComponente.TabIndex = 2;
            // 
            // nudQtdComponente
            // 
            this.nudQtdComponente.Location = new System.Drawing.Point(436, 24);
            this.nudQtdComponente.Size = new System.Drawing.Size(160, 29);
            this.nudQtdComponente.Minimum = 1;
            this.nudQtdComponente.Maximum = 999;
            this.nudQtdComponente.Value = 1;
            this.nudQtdComponente.Name = "nudQtdComponente";
            this.nudQtdComponente.TabIndex = 3;
            // 
            // btnAdicionarComponente
            // 
            this.btnAdicionarComponente.Location = new System.Drawing.Point(0, 74);
            this.btnAdicionarComponente.Size = new System.Drawing.Size(170, 38);
            this.btnAdicionarComponente.Text = "Adicionar / atualizar";
            this.btnAdicionarComponente.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnAdicionarComponente.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnAdicionarComponente.Name = "btnAdicionarComponente";
            this.btnAdicionarComponente.TabIndex = 4;
            this.btnAdicionarComponente.Click += new System.EventHandler(this.btnAdicionarComponente_Click);
            // 
            // btnRemoverComponente
            // 
            this.btnRemoverComponente.Location = new System.Drawing.Point(178, 74);
            this.btnRemoverComponente.Size = new System.Drawing.Size(120, 38);
            this.btnRemoverComponente.Text = "Remover";
            this.btnRemoverComponente.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnRemoverComponente.Icone = Lapapie.Tema.Icones.Remover;
            this.btnRemoverComponente.Name = "btnRemoverComponente";
            this.btnRemoverComponente.TabIndex = 5;
            this.btnRemoverComponente.Click += new System.EventHandler(this.btnRemoverComponente_Click);
            // 
            // dgvComponentes
            // 
            this.dgvComponentes.Location = new System.Drawing.Point(0, 122);
            this.dgvComponentes.Size = new System.Drawing.Size(596, 232);
            this.dgvComponentes.Name = "dgvComponentes";
            this.dgvComponentes.TabIndex = 6;
            // 
            // lblResumoKit
            // 
            this.lblResumoKit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResumoKit.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblResumoKit.Location = new System.Drawing.Point(0, 362);
            this.lblResumoKit.Size = new System.Drawing.Size(596, 40);
            this.lblResumoKit.Text = "";
            this.lblResumoKit.Name = "lblResumoKit";
            this.lblResumoKit.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(590, 576);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 11;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(722, 576);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 12;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmProdutoEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(880, 640);
            this.Text = "Produto";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.picImagem);
            this.Controls.Add(this.btnEscolherImagem);
            this.Controls.Add(this.lblInfoImagem);
            this.Controls.Add(this.cardResumo);
            this.Controls.Add(this.abasSecoes);
            this.Controls.Add(this.pnlDados);
            this.Controls.Add(this.pnlEstoque);
            this.Controls.Add(this.pnlFornecedores);
            this.Controls.Add(this.pnlKit);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmProdutoEdicao";
            this.Load += new System.EventHandler(this.FrmProdutoEdicao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImagem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEstoqueInicial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrazo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtdComponente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponentes)).EndInit();
            this.pnlKit.ResumeLayout(false);
            this.pnlKit.PerformLayout();
            this.pnlFornecedores.ResumeLayout(false);
            this.pnlFornecedores.PerformLayout();
            this.pnlEstoque.ResumeLayout(false);
            this.pnlEstoque.PerformLayout();
            this.pnlDados.ResumeLayout(false);
            this.pnlDados.PerformLayout();
            this.cardResumo.ResumeLayout(false);
            this.cardResumo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.PictureBox picImagem;
        private Lapapie.Controles.BotaoPill btnEscolherImagem;
        private System.Windows.Forms.Label lblInfoImagem;
        private Lapapie.Controles.PainelCartao cardResumo;
        private System.Windows.Forms.Label lblResumo;
        private Lapapie.Controles.AbasPill abasSecoes;
        private System.Windows.Forms.Panel pnlDados;
        private System.Windows.Forms.Panel pnlEstoque;
        private System.Windows.Forms.Panel pnlFornecedores;
        private System.Windows.Forms.Panel pnlKit;
        private System.Windows.Forms.Label lblNome;
        private Lapapie.Controles.CampoTexto txtNome;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label lblCodigoBarras;
        private Lapapie.Controles.CampoTexto txtCodigoBarras;
        private Lapapie.Controles.BotaoPill btnGerarCodigo;
        private System.Windows.Forms.Label lblMarca;
        private Lapapie.Controles.CampoTexto txtMarca;
        private System.Windows.Forms.Label lblUnidade;
        private System.Windows.Forms.ComboBox cboUnidade;
        private System.Windows.Forms.Label lblPreco;
        private Lapapie.Controles.CampoTexto txtPreco;
        private System.Windows.Forms.Label lblPromocao;
        private System.Windows.Forms.Label lblDescricao;
        private Lapapie.Controles.CampoTexto txtDescricao;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label lblSaldoTitulo;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblEstoqueInicial;
        private System.Windows.Forms.NumericUpDown nudEstoqueInicial;
        private System.Windows.Forms.Label lblMinimo;
        private System.Windows.Forms.NumericUpDown nudMinimo;
        private System.Windows.Forms.Label lblLocalizacao;
        private Lapapie.Controles.CampoTexto txtLocalizacao;
        private System.Windows.Forms.Label lblNotaEstoque;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.ComboBox cboFornecedor;
        private System.Windows.Forms.Label lblCodigoFornecedor;
        private Lapapie.Controles.CampoTexto txtCodigoFornecedor;
        private System.Windows.Forms.Label lblCusto;
        private Lapapie.Controles.CampoTexto txtCusto;
        private System.Windows.Forms.Label lblPrazo;
        private System.Windows.Forms.NumericUpDown nudPrazo;
        private Lapapie.Controles.BotaoPill btnAdicionarFornecedor;
        private Lapapie.Controles.BotaoPill btnRemoverFornecedor;
        private System.Windows.Forms.DataGridView dgvFornecedores;
        private System.Windows.Forms.Label lblMargem;
        private System.Windows.Forms.Label lblComponente;
        private System.Windows.Forms.ComboBox cboComponente;
        private System.Windows.Forms.Label lblQtdComponente;
        private System.Windows.Forms.NumericUpDown nudQtdComponente;
        private Lapapie.Controles.BotaoPill btnAdicionarComponente;
        private Lapapie.Controles.BotaoPill btnRemoverComponente;
        private System.Windows.Forms.DataGridView dgvComponentes;
        private System.Windows.Forms.Label lblResumoKit;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
