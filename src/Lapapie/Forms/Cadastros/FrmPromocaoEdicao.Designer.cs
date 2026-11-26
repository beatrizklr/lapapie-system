namespace Lapapie.Forms.Cadastros
{
    partial class FrmPromocaoEdicao
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new Lapapie.Controles.CampoTexto();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new Lapapie.Controles.CampoTexto();
            this.lblTipoDesconto = new System.Windows.Forms.Label();
            this.cboTipoDesconto = new System.Windows.Forms.ComboBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new Lapapie.Controles.CampoTexto();
            this.lblInicio = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFim = new System.Windows.Forms.Label();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.cardPreview = new Lapapie.Controles.PainelCartao();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lblProdutos = new System.Windows.Forms.Label();
            this.cboFiltroCategoria = new System.Windows.Forms.ComboBox();
            this.txtFiltroProduto = new Lapapie.Controles.CaixaPesquisa();
            this.clbProdutos = new System.Windows.Forms.CheckedListBox();
            this.lblSelecionados = new System.Windows.Forms.Label();
            this.btnMarcarVisiveis = new Lapapie.Controles.BotaoPill();
            this.btnDesmarcarVisiveis = new Lapapie.Controles.BotaoPill();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            this.cardPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "NOVA PROMOÇÃO";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(844, 22);
            this.lblSubtitulo.Text = "Defina o desconto, o período e os produtos participantes.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblNome
            // 
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNome.Location = new System.Drawing.Point(30, 96);
            this.lblNome.Text = "Nome *";
            this.lblNome.AutoSize = true;
            this.lblNome.Name = "lblNome";
            this.lblNome.TabIndex = 2;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(28, 118);
            this.txtNome.Size = new System.Drawing.Size(380, 40);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.TabIndex = 3;
            // 
            // lblDescricao
            // 
            this.lblDescricao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDescricao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDescricao.Location = new System.Drawing.Point(30, 166);
            this.lblDescricao.Text = "Descrição";
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.TabIndex = 4;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(28, 188);
            this.txtDescricao.Size = new System.Drawing.Size(380, 64);
            this.txtDescricao.Multilinha = true;
            this.txtDescricao.MaxLength = 255;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.TabIndex = 5;
            // 
            // lblTipoDesconto
            // 
            this.lblTipoDesconto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTipoDesconto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTipoDesconto.Location = new System.Drawing.Point(30, 250);
            this.lblTipoDesconto.Text = "Tipo de desconto";
            this.lblTipoDesconto.AutoSize = true;
            this.lblTipoDesconto.Name = "lblTipoDesconto";
            this.lblTipoDesconto.TabIndex = 6;
            // 
            // cboTipoDesconto
            // 
            this.cboTipoDesconto.Location = new System.Drawing.Point(28, 274);
            this.cboTipoDesconto.Size = new System.Drawing.Size(186, 29);
            this.cboTipoDesconto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDesconto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTipoDesconto.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboTipoDesconto.Name = "cboTipoDesconto";
            this.cboTipoDesconto.TabIndex = 7;
            this.cboTipoDesconto.SelectedIndexChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblValor
            // 
            this.lblValor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblValor.Location = new System.Drawing.Point(232, 250);
            this.lblValor.Text = "Valor do desconto *";
            this.lblValor.AutoSize = true;
            this.lblValor.Name = "lblValor";
            this.lblValor.TabIndex = 8;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(230, 272);
            this.txtValor.Size = new System.Drawing.Size(178, 40);
            this.txtValor.Formato = Lapapie.Controles.FormatoCampo.Moeda;
            this.txtValor.Name = "txtValor";
            this.txtValor.TabIndex = 9;
            this.txtValor.TextoAlterado += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblInicio
            // 
            this.lblInicio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInicio.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblInicio.Location = new System.Drawing.Point(30, 320);
            this.lblInicio.Text = "Início *";
            this.lblInicio.AutoSize = true;
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.TabIndex = 10;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(28, 344);
            this.dtpInicio.Size = new System.Drawing.Size(186, 29);
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.TabIndex = 11;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblFim
            // 
            this.lblFim.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFim.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblFim.Location = new System.Drawing.Point(232, 320);
            this.lblFim.Text = "Fim *";
            this.lblFim.AutoSize = true;
            this.lblFim.Name = "lblFim";
            this.lblFim.TabIndex = 12;
            // 
            // dtpFim
            // 
            this.dtpFim.Location = new System.Drawing.Point(230, 344);
            this.dtpFim.Size = new System.Drawing.Size(178, 29);
            this.dtpFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFim.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.TabIndex = 13;
            this.dtpFim.ValueChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkAtivo.Location = new System.Drawing.Point(30, 392);
            this.chkAtivo.Text = "Promoção ativa";
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 14;
            this.chkAtivo.CheckedChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // cardPreview
            // 
            this.cardPreview.Controls.Add(this.lblPreview);
            this.cardPreview.Location = new System.Drawing.Point(28, 428);
            this.cardPreview.Size = new System.Drawing.Size(380, 120);
            this.cardPreview.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.cardPreview.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.cardPreview.Name = "cardPreview";
            this.cardPreview.TabIndex = 15;
            // 
            // lblPreview
            // 
            this.lblPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblPreview.Text = "";
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.TabIndex = 0;
            // 
            // lblProdutos
            // 
            this.lblProdutos.AutoSize = true;
            this.lblProdutos.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblProdutos.ForeColor = System.Drawing.Color.FromArgb(184, 149, 90);
            this.lblProdutos.Location = new System.Drawing.Point(436, 96);
            this.lblProdutos.Text = "PRODUTOS PARTICIPANTES";
            this.lblProdutos.Name = "lblProdutos";
            this.lblProdutos.TabIndex = 16;
            // 
            // cboFiltroCategoria
            // 
            this.cboFiltroCategoria.Location = new System.Drawing.Point(436, 124);
            this.cboFiltroCategoria.Size = new System.Drawing.Size(200, 29);
            this.cboFiltroCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltroCategoria.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFiltroCategoria.Name = "cboFiltroCategoria";
            this.cboFiltroCategoria.TabIndex = 17;
            this.cboFiltroCategoria.SelectedIndexChanged += new System.EventHandler(this.FiltroProdutos_Alterado);
            // 
            // txtFiltroProduto
            // 
            this.txtFiltroProduto.Location = new System.Drawing.Point(648, 118);
            this.txtFiltroProduto.Size = new System.Drawing.Size(224, 40);
            this.txtFiltroProduto.Placeholder = "Filtrar produtos";
            this.txtFiltroProduto.Name = "txtFiltroProduto";
            this.txtFiltroProduto.TabIndex = 18;
            this.txtFiltroProduto.Pesquisar += new System.EventHandler(this.FiltroProdutos_Alterado);
            // 
            // clbProdutos
            // 
            this.clbProdutos.Location = new System.Drawing.Point(436, 170);
            this.clbProdutos.Size = new System.Drawing.Size(436, 340);
            this.clbProdutos.CheckOnClick = true;
            this.clbProdutos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clbProdutos.IntegralHeight = false;
            this.clbProdutos.Name = "clbProdutos";
            this.clbProdutos.TabIndex = 19;
            this.clbProdutos.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbProdutos_ItemCheck);
            // 
            // lblSelecionados
            // 
            this.lblSelecionados.AutoSize = true;
            this.lblSelecionados.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelecionados.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblSelecionados.Location = new System.Drawing.Point(436, 522);
            this.lblSelecionados.Text = "0 produtos selecionados";
            this.lblSelecionados.Name = "lblSelecionados";
            this.lblSelecionados.TabIndex = 20;
            // 
            // btnMarcarVisiveis
            // 
            this.btnMarcarVisiveis.Location = new System.Drawing.Point(636, 514);
            this.btnMarcarVisiveis.Size = new System.Drawing.Size(118, 32);
            this.btnMarcarVisiveis.Text = "Marcar todos";
            this.btnMarcarVisiveis.Estilo = Lapapie.Controles.EstiloBotao.Link;
            this.btnMarcarVisiveis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMarcarVisiveis.Name = "btnMarcarVisiveis";
            this.btnMarcarVisiveis.TabIndex = 21;
            this.btnMarcarVisiveis.Click += new System.EventHandler(this.btnMarcarVisiveis_Click);
            // 
            // btnDesmarcarVisiveis
            // 
            this.btnDesmarcarVisiveis.Location = new System.Drawing.Point(756, 514);
            this.btnDesmarcarVisiveis.Size = new System.Drawing.Size(118, 32);
            this.btnDesmarcarVisiveis.Text = "Desmarcar";
            this.btnDesmarcarVisiveis.Estilo = Lapapie.Controles.EstiloBotao.Link;
            this.btnDesmarcarVisiveis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDesmarcarVisiveis.Name = "btnDesmarcarVisiveis";
            this.btnDesmarcarVisiveis.TabIndex = 22;
            this.btnDesmarcarVisiveis.Click += new System.EventHandler(this.btnDesmarcarVisiveis_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(610, 556);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 23;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(742, 556);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 24;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmPromocaoEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Text = "Promoção";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblTipoDesconto);
            this.Controls.Add(this.cboTipoDesconto);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.lblFim);
            this.Controls.Add(this.dtpFim);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.cardPreview);
            this.Controls.Add(this.lblProdutos);
            this.Controls.Add(this.cboFiltroCategoria);
            this.Controls.Add(this.txtFiltroProduto);
            this.Controls.Add(this.clbProdutos);
            this.Controls.Add(this.lblSelecionados);
            this.Controls.Add(this.btnMarcarVisiveis);
            this.Controls.Add(this.btnDesmarcarVisiveis);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmPromocaoEdicao";
            this.Load += new System.EventHandler(this.FrmPromocaoEdicao_Load);
            this.cardPreview.ResumeLayout(false);
            this.cardPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblNome;
        private Lapapie.Controles.CampoTexto txtNome;
        private System.Windows.Forms.Label lblDescricao;
        private Lapapie.Controles.CampoTexto txtDescricao;
        private System.Windows.Forms.Label lblTipoDesconto;
        private System.Windows.Forms.ComboBox cboTipoDesconto;
        private System.Windows.Forms.Label lblValor;
        private Lapapie.Controles.CampoTexto txtValor;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblFim;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.CheckBox chkAtivo;
        private Lapapie.Controles.PainelCartao cardPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Label lblProdutos;
        private System.Windows.Forms.ComboBox cboFiltroCategoria;
        private Lapapie.Controles.CaixaPesquisa txtFiltroProduto;
        private System.Windows.Forms.CheckedListBox clbProdutos;
        private System.Windows.Forms.Label lblSelecionados;
        private Lapapie.Controles.BotaoPill btnMarcarVisiveis;
        private Lapapie.Controles.BotaoPill btnDesmarcarVisiveis;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
