namespace Lapapie.Forms.Comum
{
    partial class FrmMensagem
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
            this.lblIcone = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.txtEntrada = new Lapapie.Controles.CampoTexto();
            this.btnOk = new Lapapie.Controles.BotaoPill();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.SuspendLayout();
            // 
            // lblIcone
            // 
            this.lblIcone.BackColor = System.Drawing.Color.Transparent;
            this.lblIcone.Font = new System.Drawing.Font("Segoe MDL2 Assets", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIcone.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblIcone.Location = new System.Drawing.Point(28, 30);
            this.lblIcone.Size = new System.Drawing.Size(52, 52);
            this.lblIcone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIcone.Name = "lblIcone";
            this.lblIcone.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(96, 30);
            this.lblTitulo.Size = new System.Drawing.Size(356, 28);
            this.lblTitulo.Text = "Título";
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 1;
            // 
            // lblTexto
            // 
            this.lblTexto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTexto.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTexto.Location = new System.Drawing.Point(96, 62);
            this.lblTexto.Size = new System.Drawing.Size(356, 70);
            this.lblTexto.Text = "Mensagem";
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.TabIndex = 2;
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(96, 140);
            this.txtEntrada.Size = new System.Drawing.Size(356, 40);
            this.txtEntrada.Visible = false;
            this.txtEntrada.MaxLength = 255;
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(312, 180);
            this.btnOk.Size = new System.Drawing.Size(140, 40);
            this.btnOk.Text = "Ok";
            this.btnOk.Name = "btnOk";
            this.btnOk.TabIndex = 4;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(166, 180);
            this.btnCancelar.Size = new System.Drawing.Size(136, 40);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 5;
            // 
            // FrmMensagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.ClientSize = new System.Drawing.Size(480, 240);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lapapiê";
            this.Controls.Add(this.lblIcone);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblTexto);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancelar);
            this.Name = "FrmMensagem";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMensagem_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblIcone;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTexto;
        private Lapapie.Controles.CampoTexto txtEntrada;
        private Lapapie.Controles.BotaoPill btnOk;
        private Lapapie.Controles.BotaoPill btnCancelar;
    }
}
