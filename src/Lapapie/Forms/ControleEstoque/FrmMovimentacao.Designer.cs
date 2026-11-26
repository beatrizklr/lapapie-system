namespace Lapapie.Forms.ControleEstoque
{
    partial class FrmMovimentacao
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
            this.lblProduto = new System.Windows.Forms.Label();
            this.cboProduto = new System.Windows.Forms.ComboBox();
            this.lblSaldoAtual = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.abasTipo = new Lapapie.Controles.AbasPill();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.cboMotivo = new System.Windows.Forms.ComboBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.nudQuantidade = new System.Windows.Forms.NumericUpDown();
            this.lblNovoSaldo = new System.Windows.Forms.Label();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacao = new Lapapie.Controles.CampoTexto();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "MOVIMENTAR ESTOQUE";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(484, 22);
            this.lblSubtitulo.Text = "Ajustes, devoluções e perdas. Vendas e compras geram movimentações sozinhas.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblProduto
            // 
            this.lblProduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProduto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblProduto.Location = new System.Drawing.Point(30, 96);
            this.lblProduto.Text = "Produto *";
            this.lblProduto.AutoSize = true;
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.TabIndex = 2;
            // 
            // cboProduto
            // 
            this.cboProduto.Location = new System.Drawing.Point(28, 120);
            this.cboProduto.Size = new System.Drawing.Size(484, 29);
            this.cboProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProduto.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboProduto.Name = "cboProduto";
            this.cboProduto.TabIndex = 3;
            this.cboProduto.SelectedIndexChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblSaldoAtual
            // 
            this.lblSaldoAtual.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSaldoAtual.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSaldoAtual.Location = new System.Drawing.Point(30, 156);
            this.lblSaldoAtual.Size = new System.Drawing.Size(484, 22);
            this.lblSaldoAtual.Text = "";
            this.lblSaldoAtual.Name = "lblSaldoAtual";
            this.lblSaldoAtual.TabIndex = 4;
            // 
            // lblTipo
            // 
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTipo.Location = new System.Drawing.Point(30, 188);
            this.lblTipo.Text = "Tipo *";
            this.lblTipo.AutoSize = true;
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 5;
            // 
            // abasTipo
            // 
            this.abasTipo.Location = new System.Drawing.Point(28, 212);
            this.abasTipo.Size = new System.Drawing.Size(260, 36);
            this.abasTipo.Opcoes = "Entrada|Saída";
            this.abasTipo.IndiceSelecionado = 0;
            this.abasTipo.Name = "abasTipo";
            this.abasTipo.TabIndex = 6;
            this.abasTipo.SelecaoAlterada += new System.EventHandler(this.abasTipo_SelecaoAlterada);
            // 
            // lblMotivo
            // 
            this.lblMotivo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMotivo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblMotivo.Location = new System.Drawing.Point(306, 188);
            this.lblMotivo.Text = "Motivo *";
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.TabIndex = 7;
            // 
            // cboMotivo
            // 
            this.cboMotivo.Location = new System.Drawing.Point(304, 212);
            this.cboMotivo.Size = new System.Drawing.Size(208, 29);
            this.cboMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMotivo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboMotivo.Name = "cboMotivo";
            this.cboMotivo.TabIndex = 8;
            this.cboMotivo.SelectedIndexChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblQuantidade.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblQuantidade.Location = new System.Drawing.Point(30, 262);
            this.lblQuantidade.Text = "Quantidade *";
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.TabIndex = 9;
            // 
            // nudQuantidade
            // 
            this.nudQuantidade.Location = new System.Drawing.Point(28, 286);
            this.nudQuantidade.Size = new System.Drawing.Size(160, 29);
            this.nudQuantidade.Minimum = 1;
            this.nudQuantidade.Maximum = 999999;
            this.nudQuantidade.Value = 1;
            this.nudQuantidade.Name = "nudQuantidade";
            this.nudQuantidade.TabIndex = 10;
            this.nudQuantidade.ValueChanged += new System.EventHandler(this.Valores_Alterados);
            // 
            // lblNovoSaldo
            // 
            this.lblNovoSaldo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNovoSaldo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblNovoSaldo.Location = new System.Drawing.Point(204, 286);
            this.lblNovoSaldo.Size = new System.Drawing.Size(308, 30);
            this.lblNovoSaldo.Text = "";
            this.lblNovoSaldo.Name = "lblNovoSaldo";
            this.lblNovoSaldo.TabIndex = 11;
            // 
            // lblObservacao
            // 
            this.lblObservacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblObservacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblObservacao.Location = new System.Drawing.Point(30, 332);
            this.lblObservacao.Text = "Observação (obrigatória para ajuste e perda)";
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.TabIndex = 12;
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(28, 354);
            this.txtObservacao.Size = new System.Drawing.Size(484, 90);
            this.txtObservacao.Multilinha = true;
            this.txtObservacao.MaxLength = 255;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.TabIndex = 13;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(250, 536);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 14;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(382, 536);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Registrar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 15;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmMovimentacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(540, 600);
            this.Text = "Movimentação de estoque";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.cboProduto);
            this.Controls.Add(this.lblSaldoAtual);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.abasTipo);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.cboMotivo);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.nudQuantidade);
            this.Controls.Add(this.lblNovoSaldo);
            this.Controls.Add(this.lblObservacao);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmMovimentacao";
            this.Load += new System.EventHandler(this.FrmMovimentacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.ComboBox cboProduto;
        private System.Windows.Forms.Label lblSaldoAtual;
        private System.Windows.Forms.Label lblTipo;
        private Lapapie.Controles.AbasPill abasTipo;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.ComboBox cboMotivo;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.NumericUpDown nudQuantidade;
        private System.Windows.Forms.Label lblNovoSaldo;
        private System.Windows.Forms.Label lblObservacao;
        private Lapapie.Controles.CampoTexto txtObservacao;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
