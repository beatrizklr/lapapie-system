namespace Lapapie.Forms.ControleEstoque
{
    partial class FrmEstoqueParametros
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
            this.lblMinimo = new System.Windows.Forms.Label();
            this.nudMinimo = new System.Windows.Forms.NumericUpDown();
            this.lblExplicacao = new System.Windows.Forms.Label();
            this.lblLocalizacao = new System.Windows.Forms.Label();
            this.txtLocalizacao = new Lapapie.Controles.CampoTexto();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "PARÂMETROS";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(424, 22);
            this.lblSubtitulo.Text = "Produto";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblMinimo
            // 
            this.lblMinimo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMinimo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblMinimo.Location = new System.Drawing.Point(30, 100);
            this.lblMinimo.Text = "Estoque mínimo *";
            this.lblMinimo.AutoSize = true;
            this.lblMinimo.Name = "lblMinimo";
            this.lblMinimo.TabIndex = 2;
            // 
            // nudMinimo
            // 
            this.nudMinimo.Location = new System.Drawing.Point(28, 124);
            this.nudMinimo.Size = new System.Drawing.Size(160, 29);
            this.nudMinimo.Maximum = 999999;
            this.nudMinimo.Name = "nudMinimo";
            this.nudMinimo.TabIndex = 3;
            // 
            // lblExplicacao
            // 
            this.lblExplicacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblExplicacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblExplicacao.Location = new System.Drawing.Point(204, 118);
            this.lblExplicacao.Size = new System.Drawing.Size(248, 44);
            this.lblExplicacao.Text = "Abaixo ou igual a este valor o produto aparece em \"Estoque baixo\".";
            this.lblExplicacao.Name = "lblExplicacao";
            this.lblExplicacao.TabIndex = 4;
            // 
            // lblLocalizacao
            // 
            this.lblLocalizacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLocalizacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblLocalizacao.Location = new System.Drawing.Point(30, 176);
            this.lblLocalizacao.Text = "Localização na loja";
            this.lblLocalizacao.AutoSize = true;
            this.lblLocalizacao.Name = "lblLocalizacao";
            this.lblLocalizacao.TabIndex = 5;
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.Location = new System.Drawing.Point(28, 198);
            this.txtLocalizacao.Size = new System.Drawing.Size(424, 40);
            this.txtLocalizacao.MaxLength = 50;
            this.txtLocalizacao.Placeholder = "Ex.: Corredor 2 - Prateleira B";
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.TabIndex = 6;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(190, 316);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 7;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(322, 316);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmEstoqueParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(480, 380);
            this.Text = "Parâmetros de estoque";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblMinimo);
            this.Controls.Add(this.nudMinimo);
            this.Controls.Add(this.lblExplicacao);
            this.Controls.Add(this.lblLocalizacao);
            this.Controls.Add(this.txtLocalizacao);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmEstoqueParametros";
            this.Load += new System.EventHandler(this.FrmEstoqueParametros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblMinimo;
        private System.Windows.Forms.NumericUpDown nudMinimo;
        private System.Windows.Forms.Label lblExplicacao;
        private System.Windows.Forms.Label lblLocalizacao;
        private Lapapie.Controles.CampoTexto txtLocalizacao;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
