namespace Lapapie.Forms.ControleEstoque
{
    partial class FrmPedidoCompraEdicao
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
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.cboFornecedor = new System.Windows.Forms.ComboBox();
            this.lblPrevisao = new System.Windows.Forms.Label();
            this.dtpPrevisao = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacao = new Lapapie.Controles.CampoTexto();
            this.cardItem = new Lapapie.Controles.PainelCartao();
            this.lblProduto = new System.Windows.Forms.Label();
            this.cboProduto = new System.Windows.Forms.ComboBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.nudQuantidade = new System.Windows.Forms.NumericUpDown();
            this.lblCusto = new System.Windows.Forms.Label();
            this.txtCusto = new Lapapie.Controles.CampoTexto();
            this.btnAdicionarItem = new Lapapie.Controles.BotaoPill();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.btnRemoverItem = new Lapapie.Controles.BotaoPill();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.cardItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "NOVO PEDIDO DE COMPRA";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(824, 22);
            this.lblSubtitulo.Text = "Ao receber a mercadoria, o estoque é atualizado automaticamente.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFornecedor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblFornecedor.Location = new System.Drawing.Point(30, 96);
            this.lblFornecedor.Text = "Fornecedor *";
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.TabIndex = 2;
            // 
            // cboFornecedor
            // 
            this.cboFornecedor.Location = new System.Drawing.Point(28, 120);
            this.cboFornecedor.Size = new System.Drawing.Size(420, 29);
            this.cboFornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFornecedor.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFornecedor.Name = "cboFornecedor";
            this.cboFornecedor.TabIndex = 3;
            this.cboFornecedor.SelectedIndexChanged += new System.EventHandler(this.cboFornecedor_SelectedIndexChanged);
            // 
            // lblPrevisao
            // 
            this.lblPrevisao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrevisao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPrevisao.Location = new System.Drawing.Point(468, 96);
            this.lblPrevisao.Text = "Previsão de entrega";
            this.lblPrevisao.AutoSize = true;
            this.lblPrevisao.Name = "lblPrevisao";
            this.lblPrevisao.TabIndex = 4;
            // 
            // dtpPrevisao
            // 
            this.dtpPrevisao.Location = new System.Drawing.Point(466, 120);
            this.dtpPrevisao.Size = new System.Drawing.Size(180, 29);
            this.dtpPrevisao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPrevisao.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpPrevisao.ShowCheckBox = true;
            this.dtpPrevisao.Name = "dtpPrevisao";
            this.dtpPrevisao.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(184, 149, 90);
            this.lblStatus.Location = new System.Drawing.Point(664, 124);
            this.lblStatus.Size = new System.Drawing.Size(188, 24);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatus.Text = "";
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 6;
            // 
            // lblObservacao
            // 
            this.lblObservacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblObservacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblObservacao.Location = new System.Drawing.Point(30, 160);
            this.lblObservacao.Text = "Observação";
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.TabIndex = 7;
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(28, 182);
            this.txtObservacao.Size = new System.Drawing.Size(824, 40);
            this.txtObservacao.MaxLength = 255;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.TabIndex = 8;
            // 
            // cardItem
            // 
            this.cardItem.Controls.Add(this.lblProduto);
            this.cardItem.Controls.Add(this.cboProduto);
            this.cardItem.Controls.Add(this.lblQuantidade);
            this.cardItem.Controls.Add(this.nudQuantidade);
            this.cardItem.Controls.Add(this.lblCusto);
            this.cardItem.Controls.Add(this.txtCusto);
            this.cardItem.Controls.Add(this.btnAdicionarItem);
            this.cardItem.Location = new System.Drawing.Point(28, 232);
            this.cardItem.Size = new System.Drawing.Size(824, 92);
            this.cardItem.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.cardItem.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.cardItem.Name = "cardItem";
            this.cardItem.TabIndex = 9;
            // 
            // lblProduto
            // 
            this.lblProduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProduto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblProduto.Location = new System.Drawing.Point(18, 8);
            this.lblProduto.Text = "Produto";
            this.lblProduto.AutoSize = true;
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.TabIndex = 0;
            // 
            // cboProduto
            // 
            this.cboProduto.Location = new System.Drawing.Point(16, 32);
            this.cboProduto.Size = new System.Drawing.Size(380, 29);
            this.cboProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProduto.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboProduto.Name = "cboProduto";
            this.cboProduto.TabIndex = 1;
            this.cboProduto.SelectedIndexChanged += new System.EventHandler(this.cboProduto_SelectedIndexChanged);
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblQuantidade.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblQuantidade.Location = new System.Drawing.Point(414, 8);
            this.lblQuantidade.Text = "Quantidade";
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.TabIndex = 2;
            // 
            // nudQuantidade
            // 
            this.nudQuantidade.Location = new System.Drawing.Point(412, 32);
            this.nudQuantidade.Size = new System.Drawing.Size(110, 29);
            this.nudQuantidade.Minimum = 1;
            this.nudQuantidade.Maximum = 999999;
            this.nudQuantidade.Value = 10;
            this.nudQuantidade.Name = "nudQuantidade";
            this.nudQuantidade.TabIndex = 3;
            // 
            // lblCusto
            // 
            this.lblCusto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCusto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCusto.Location = new System.Drawing.Point(540, 8);
            this.lblCusto.Text = "Custo unitário";
            this.lblCusto.AutoSize = true;
            this.lblCusto.Name = "lblCusto";
            this.lblCusto.TabIndex = 4;
            // 
            // txtCusto
            // 
            this.txtCusto.Location = new System.Drawing.Point(538, 30);
            this.txtCusto.Size = new System.Drawing.Size(130, 40);
            this.txtCusto.Formato = Lapapie.Controles.FormatoCampo.Moeda;
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.TabIndex = 5;
            // 
            // btnAdicionarItem
            // 
            this.btnAdicionarItem.Location = new System.Drawing.Point(684, 28);
            this.btnAdicionarItem.Size = new System.Drawing.Size(124, 40);
            this.btnAdicionarItem.Text = "Adicionar";
            this.btnAdicionarItem.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnAdicionarItem.Name = "btnAdicionarItem";
            this.btnAdicionarItem.TabIndex = 6;
            this.btnAdicionarItem.Click += new System.EventHandler(this.btnAdicionarItem_Click);
            // 
            // dgvItens
            // 
            this.dgvItens.Location = new System.Drawing.Point(28, 336);
            this.dgvItens.Size = new System.Drawing.Size(824, 178);
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.TabIndex = 10;
            // 
            // btnRemoverItem
            // 
            this.btnRemoverItem.Location = new System.Drawing.Point(28, 524);
            this.btnRemoverItem.Size = new System.Drawing.Size(150, 38);
            this.btnRemoverItem.Text = "Remover item";
            this.btnRemoverItem.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnRemoverItem.Icone = Lapapie.Tema.Icones.Remover;
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.TabIndex = 11;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTotal.Location = new System.Drawing.Point(452, 526);
            this.lblTotal.Size = new System.Drawing.Size(400, 34);
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Text = "Total: R$ 0,00";
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 12;
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
            this.btnCancelar.TabIndex = 13;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(722, 576);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 14;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmPedidoCompraEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(880, 640);
            this.Text = "Pedido de compra";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.cboFornecedor);
            this.Controls.Add(this.lblPrevisao);
            this.Controls.Add(this.dtpPrevisao);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblObservacao);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.cardItem);
            this.Controls.Add(this.dgvItens);
            this.Controls.Add(this.btnRemoverItem);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmPedidoCompraEdicao";
            this.Load += new System.EventHandler(this.FrmPedidoCompraEdicao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.cardItem.ResumeLayout(false);
            this.cardItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.ComboBox cboFornecedor;
        private System.Windows.Forms.Label lblPrevisao;
        private System.Windows.Forms.DateTimePicker dtpPrevisao;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblObservacao;
        private Lapapie.Controles.CampoTexto txtObservacao;
        private Lapapie.Controles.PainelCartao cardItem;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.ComboBox cboProduto;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.NumericUpDown nudQuantidade;
        private System.Windows.Forms.Label lblCusto;
        private Lapapie.Controles.CampoTexto txtCusto;
        private Lapapie.Controles.BotaoPill btnAdicionarItem;
        private System.Windows.Forms.DataGridView dgvItens;
        private Lapapie.Controles.BotaoPill btnRemoverItem;
        private System.Windows.Forms.Label lblTotal;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
