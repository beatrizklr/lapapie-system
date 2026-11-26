namespace Lapapie.Forms
{
    partial class FrmPrincipal
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
            this.pnlConteudo = new System.Windows.Forms.Panel();
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlUsuario = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.avatarUsuario = new Lapapie.Controles.Avatar();
            this.lblNomeUsuario = new System.Windows.Forms.Label();
            this.lblPerfilUsuario = new System.Windows.Forms.Label();
            this.btnSair = new Lapapie.Controles.BotaoPill();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.btnNovaVendaTopo = new Lapapie.Controles.BotaoPill();
            this.lblData = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlConteudo.SuspendLayout();
            this.pnlTopo.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConteudo.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlConteudo.Padding = new System.Windows.Forms.Padding(32, 4, 32, 24);
            this.pnlConteudo.Name = "pnlConteudo";
            this.pnlConteudo.TabIndex = 0;
            // 
            // pnlTopo
            // 
            this.pnlTopo.Controls.Add(this.lblTitulo);
            this.pnlTopo.Controls.Add(this.lblSubtitulo);
            this.pnlTopo.Controls.Add(this.btnNovaVendaTopo);
            this.pnlTopo.Controls.Add(this.lblData);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlTopo.Size = new System.Drawing.Size(1114, 96);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.TabIndex = 1;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.flpMenu);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Controls.Add(this.pnlUsuario);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.pnlMenu.Size = new System.Drawing.Size(252, 768);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.TabIndex = 2;
            // 
            // flpMenu
            // 
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.AutoScroll = true;
            this.flpMenu.BackColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.WrapContents = false;
            this.flpMenu.Padding = new System.Windows.Forms.Padding(20, 8, 8, 8);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.TabIndex = 0;
            // 
            // pnlLogo
            // 
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.pnlLogo.Size = new System.Drawing.Size(252, 104);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.TabIndex = 1;
            // 
            // pnlUsuario
            // 
            this.pnlUsuario.Controls.Add(this.avatarUsuario);
            this.pnlUsuario.Controls.Add(this.lblNomeUsuario);
            this.pnlUsuario.Controls.Add(this.lblPerfilUsuario);
            this.pnlUsuario.Controls.Add(this.btnSair);
            this.pnlUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUsuario.BackColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.pnlUsuario.Size = new System.Drawing.Size(252, 86);
            this.pnlUsuario.Name = "pnlUsuario";
            this.pnlUsuario.TabIndex = 2;
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(26, 22);
            this.picLogo.Size = new System.Drawing.Size(190, 66);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Name = "picLogo";
            this.picLogo.TabIndex = 0;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // avatarUsuario
            // 
            this.avatarUsuario.Location = new System.Drawing.Point(24, 22);
            this.avatarUsuario.Size = new System.Drawing.Size(42, 42);
            this.avatarUsuario.Text = "AD";
            this.avatarUsuario.Name = "avatarUsuario";
            this.avatarUsuario.TabIndex = 0;
            // 
            // lblNomeUsuario
            // 
            this.lblNomeUsuario.AutoEllipsis = true;
            this.lblNomeUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNomeUsuario.ForeColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.lblNomeUsuario.Location = new System.Drawing.Point(76, 22);
            this.lblNomeUsuario.Size = new System.Drawing.Size(120, 20);
            this.lblNomeUsuario.Text = "Usuário";
            this.lblNomeUsuario.Name = "lblNomeUsuario";
            this.lblNomeUsuario.TabIndex = 1;
            // 
            // lblPerfilUsuario
            // 
            this.lblPerfilUsuario.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPerfilUsuario.ForeColor = System.Drawing.Color.FromArgb(214, 208, 199);
            this.lblPerfilUsuario.Location = new System.Drawing.Point(76, 42);
            this.lblPerfilUsuario.Size = new System.Drawing.Size(120, 18);
            this.lblPerfilUsuario.Text = "Perfil";
            this.lblPerfilUsuario.Name = "lblPerfilUsuario";
            this.lblPerfilUsuario.TabIndex = 2;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(200, 24);
            this.btnSair.Size = new System.Drawing.Size(38, 38);
            this.btnSair.Text = "";
            this.btnSair.Icone = Lapapie.Tema.Icones.Sair;
            this.btnSair.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnSair.Name = "btnSair";
            this.btnSair.TabIndex = 3;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(28, 14);
            this.lblTitulo.Text = "INÍCIO";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 11.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(32, 60);
            this.lblSubtitulo.Text = "Subtítulo";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // btnNovaVendaTopo
            // 
            this.btnNovaVendaTopo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovaVendaTopo.Location = new System.Drawing.Point(924, 26);
            this.btnNovaVendaTopo.Size = new System.Drawing.Size(162, 44);
            this.btnNovaVendaTopo.Text = "Nova venda";
            this.btnNovaVendaTopo.MostrarSeta = true;
            this.btnNovaVendaTopo.Name = "btnNovaVendaTopo";
            this.btnNovaVendaTopo.TabIndex = 2;
            this.btnNovaVendaTopo.Click += new System.EventHandler(this.btnNovaVendaTopo_Click);
            // 
            // lblData
            // 
            this.lblData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblData.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblData.Location = new System.Drawing.Point(624, 36);
            this.lblData.Size = new System.Drawing.Size(286, 24);
            this.lblData.Text = "data";
            this.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblData.Name = "lblData";
            this.lblData.TabIndex = 3;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.KeyPreview = true;
            this.Text = "Lapapiê — Sistema Gerenciador de Papelaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.pnlConteudo);
            this.Controls.Add(this.pnlTopo);
            this.Controls.Add(this.pnlMenu);
            this.Name = "FrmPrincipal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPrincipal_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlUsuario.ResumeLayout(false);
            this.pnlUsuario.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.flpMenu.ResumeLayout(false);
            this.flpMenu.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.pnlTopo.ResumeLayout(false);
            this.pnlTopo.PerformLayout();
            this.pnlConteudo.ResumeLayout(false);
            this.pnlConteudo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlConteudo;
        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Panel pnlUsuario;
        private System.Windows.Forms.PictureBox picLogo;
        private Lapapie.Controles.Avatar avatarUsuario;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.Label lblPerfilUsuario;
        private Lapapie.Controles.BotaoPill btnSair;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private Lapapie.Controles.BotaoPill btnNovaVendaTopo;
        private System.Windows.Forms.Label lblData;
    }
}
