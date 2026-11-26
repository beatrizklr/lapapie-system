namespace Lapapie.Forms
{
    partial class FrmLogin
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
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.pnlIlustracao = new Lapapie.Controles.PainelArco();
            this.chipCadernos = new Lapapie.Controles.EtiquetaChip();
            this.chipEscrita = new Lapapie.Controles.EtiquetaChip();
            this.chipInformatica = new Lapapie.Controles.EtiquetaChip();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblSloganApoio = new System.Windows.Forms.Label();
            this.btnFechar = new Lapapie.Controles.BotaoPill();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtLogin = new Lapapie.Controles.CampoTexto();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenha = new Lapapie.Controles.CampoTexto();
            this.lblErro = new System.Windows.Forms.Label();
            this.btnEntrar = new Lapapie.Controles.BotaoPill();
            this.btnConfigurar = new Lapapie.Controles.BotaoPill();
            this.lblStatusBanco = new System.Windows.Forms.Label();
            this.lblDica = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlFormulario.SuspendLayout();
            this.pnlIlustracao.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFormulario
            // 
            this.pnlFormulario.Controls.Add(this.btnFechar);
            this.pnlFormulario.Controls.Add(this.picLogo);
            this.pnlFormulario.Controls.Add(this.lblTitulo);
            this.pnlFormulario.Controls.Add(this.lblSubtitulo);
            this.pnlFormulario.Controls.Add(this.lblUsuario);
            this.pnlFormulario.Controls.Add(this.txtLogin);
            this.pnlFormulario.Controls.Add(this.lblSenha);
            this.pnlFormulario.Controls.Add(this.txtSenha);
            this.pnlFormulario.Controls.Add(this.lblErro);
            this.pnlFormulario.Controls.Add(this.btnEntrar);
            this.pnlFormulario.Controls.Add(this.btnConfigurar);
            this.pnlFormulario.Controls.Add(this.lblStatusBanco);
            this.pnlFormulario.Controls.Add(this.lblDica);
            this.pnlFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormulario.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.TabIndex = 0;
            this.pnlFormulario.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Arrastar_MouseDown);
            // 
            // pnlIlustracao
            // 
            this.pnlIlustracao.Controls.Add(this.chipCadernos);
            this.pnlIlustracao.Controls.Add(this.chipEscrita);
            this.pnlIlustracao.Controls.Add(this.chipInformatica);
            this.pnlIlustracao.Controls.Add(this.lblSlogan);
            this.pnlIlustracao.Controls.Add(this.lblSloganApoio);
            this.pnlIlustracao.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlIlustracao.Size = new System.Drawing.Size(500, 640);
            this.pnlIlustracao.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlIlustracao.CorArco = System.Drawing.Color.FromArgb(240, 221, 214);
            this.pnlIlustracao.ProporcaoLargura = 0.7F;
            this.pnlIlustracao.Name = "pnlIlustracao";
            this.pnlIlustracao.TabIndex = 1;
            this.pnlIlustracao.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Arrastar_MouseDown);
            // 
            // chipCadernos
            // 
            this.chipCadernos.Location = new System.Drawing.Point(40, 150);
            this.chipCadernos.Size = new System.Drawing.Size(120, 32);
            this.chipCadernos.Text = "Cadernos";
            this.chipCadernos.CorCirculo = System.Drawing.Color.FromArgb(201, 139, 123);
            this.chipCadernos.Name = "chipCadernos";
            this.chipCadernos.TabIndex = 0;
            // 
            // chipEscrita
            // 
            this.chipEscrita.Location = new System.Drawing.Point(338, 236);
            this.chipEscrita.Size = new System.Drawing.Size(136, 32);
            this.chipEscrita.Text = "Escrita & arte";
            this.chipEscrita.CorCirculo = System.Drawing.Color.FromArgb(142, 155, 131);
            this.chipEscrita.Name = "chipEscrita";
            this.chipEscrita.TabIndex = 1;
            // 
            // chipInformatica
            // 
            this.chipInformatica.Location = new System.Drawing.Point(36, 420);
            this.chipInformatica.Size = new System.Drawing.Size(132, 32);
            this.chipInformatica.Text = "Informática";
            this.chipInformatica.CorCirculo = System.Drawing.Color.FromArgb(184, 149, 90);
            this.chipInformatica.CirculoAEsquerda = true;
            this.chipInformatica.Name = "chipInformatica";
            this.chipInformatica.TabIndex = 2;
            // 
            // lblSlogan
            // 
            this.lblSlogan.AutoSize = true;
            this.lblSlogan.BackColor = System.Drawing.Color.Transparent;
            this.lblSlogan.Font = new System.Drawing.Font("Georgia", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblSlogan.Location = new System.Drawing.Point(40, 566);
            this.lblSlogan.Text = "Tudo para escrever a sua história.";
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.TabIndex = 3;
            // 
            // lblSloganApoio
            // 
            this.lblSloganApoio.AutoSize = true;
            this.lblSloganApoio.BackColor = System.Drawing.Color.Transparent;
            this.lblSloganApoio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSloganApoio.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSloganApoio.Location = new System.Drawing.Point(42, 598);
            this.lblSloganApoio.Text = "papelaria  ·  escritório  ·  informática";
            this.lblSloganApoio.Name = "lblSloganApoio";
            this.lblSloganApoio.TabIndex = 4;
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Location = new System.Drawing.Point(486, 14);
            this.btnFechar.Size = new System.Drawing.Size(40, 40);
            this.btnFechar.Text = "";
            this.btnFechar.Icone = Lapapie.Tema.Icones.Fechar;
            this.btnFechar.Estilo = Lapapie.Controles.EstiloBotao.Link;
            this.btnFechar.TabStop = false;
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.TabIndex = 0;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(60, 62);
            this.picLogo.Size = new System.Drawing.Size(240, 95);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;
            this.picLogo.Name = "picLogo";
            this.picLogo.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(56, 180);
            this.lblTitulo.Text = "BOAS-VINDAS";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 2;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(62, 234);
            this.lblSubtitulo.Text = "Entre com seu usuário para gerenciar a papelaria.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 3;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblUsuario.Location = new System.Drawing.Point(62, 284);
            this.lblUsuario.Text = "Usuário";
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 4;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(60, 306);
            this.txtLogin.Size = new System.Drawing.Size(420, 44);
            this.txtLogin.Icone = Lapapie.Tema.Icones.Contato;
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Placeholder = "seu.login";
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.TabIndex = 5;
            this.txtLogin.TeclaPressionada += new System.Windows.Forms.KeyEventHandler(this.Campos_TeclaPressionada);
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSenha.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSenha.Location = new System.Drawing.Point(62, 362);
            this.lblSenha.Text = "Senha";
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.TabIndex = 6;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(60, 384);
            this.txtSenha.Size = new System.Drawing.Size(420, 44);
            this.txtSenha.Icone = Lapapie.Tema.Icones.Cadeado;
            this.txtSenha.MaxLength = 100;
            this.txtSenha.Senha = true;
            this.txtSenha.Placeholder = "••••••••";
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.TabIndex = 7;
            this.txtSenha.TeclaPressionada += new System.Windows.Forms.KeyEventHandler(this.Campos_TeclaPressionada);
            // 
            // lblErro
            // 
            this.lblErro.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblErro.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblErro.Location = new System.Drawing.Point(62, 436);
            this.lblErro.Size = new System.Drawing.Size(420, 22);
            this.lblErro.Text = "";
            this.lblErro.Name = "lblErro";
            this.lblErro.TabIndex = 8;
            // 
            // btnEntrar
            // 
            this.btnEntrar.Location = new System.Drawing.Point(60, 464);
            this.btnEntrar.Size = new System.Drawing.Size(420, 50);
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.MostrarSeta = true;
            this.btnEntrar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.TabIndex = 9;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.Location = new System.Drawing.Point(52, 528);
            this.btnConfigurar.Size = new System.Drawing.Size(280, 34);
            this.btnConfigurar.Text = "Configurar conexão com o banco";
            this.btnConfigurar.Icone = Lapapie.Tema.Icones.Configuracao;
            this.btnConfigurar.Estilo = Lapapie.Controles.EstiloBotao.Link;
            this.btnConfigurar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.TabIndex = 10;
            this.btnConfigurar.Click += new System.EventHandler(this.btnConfigurar_Click);
            // 
            // lblStatusBanco
            // 
            this.lblStatusBanco.AutoSize = true;
            this.lblStatusBanco.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatusBanco.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblStatusBanco.Location = new System.Drawing.Point(62, 576);
            this.lblStatusBanco.Text = "● Verificando conexão...";
            this.lblStatusBanco.Name = "lblStatusBanco";
            this.lblStatusBanco.TabIndex = 11;
            // 
            // lblDica
            // 
            this.lblDica.AutoSize = true;
            this.lblDica.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDica.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDica.Location = new System.Drawing.Point(62, 600);
            this.lblDica.Text = "Demonstração: usuário admin · senha Lapapie@123";
            this.lblDica.Name = "lblDica";
            this.lblDica.TabIndex = 12;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lapapiê — Entrar";
            this.Controls.Add(this.pnlFormulario);
            this.Controls.Add(this.pnlIlustracao);
            this.Name = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlIlustracao.ResumeLayout(false);
            this.pnlIlustracao.PerformLayout();
            this.pnlFormulario.ResumeLayout(false);
            this.pnlFormulario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlFormulario;
        private Lapapie.Controles.PainelArco pnlIlustracao;
        private Lapapie.Controles.EtiquetaChip chipCadernos;
        private Lapapie.Controles.EtiquetaChip chipEscrita;
        private Lapapie.Controles.EtiquetaChip chipInformatica;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblSloganApoio;
        private Lapapie.Controles.BotaoPill btnFechar;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private Lapapie.Controles.CampoTexto txtLogin;
        private System.Windows.Forms.Label lblSenha;
        private Lapapie.Controles.CampoTexto txtSenha;
        private System.Windows.Forms.Label lblErro;
        private Lapapie.Controles.BotaoPill btnEntrar;
        private Lapapie.Controles.BotaoPill btnConfigurar;
        private System.Windows.Forms.Label lblStatusBanco;
        private System.Windows.Forms.Label lblDica;
    }
}
