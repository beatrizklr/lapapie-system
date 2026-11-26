namespace Lapapie.Forms.Cadastros
{
    partial class FrmUsuarioEdicao
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
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new Lapapie.Controles.CampoTexto();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.cboPerfil = new System.Windows.Forms.ComboBox();
            this.lblDescricaoPerfil = new System.Windows.Forms.Label();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenha = new Lapapie.Controles.CampoTexto();
            this.lblConfirmacao = new System.Windows.Forms.Label();
            this.txtConfirmacao = new Lapapie.Controles.CampoTexto();
            this.lblDicaSenha = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(26, 24);
            this.lblTitulo.Text = "NOVO USUÁRIO";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(484, 22);
            this.lblSubtitulo.Text = "O perfil define quais telas a pessoa pode acessar.";
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
            this.txtNome.Size = new System.Drawing.Size(484, 40);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.TabIndex = 3;
            // 
            // lblLogin
            // 
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblLogin.Location = new System.Drawing.Point(30, 166);
            this.lblLogin.Text = "Login *";
            this.lblLogin.AutoSize = true;
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.TabIndex = 4;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(28, 188);
            this.txtLogin.Size = new System.Drawing.Size(230, 40);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.TabIndex = 5;
            // 
            // lblPerfil
            // 
            this.lblPerfil.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPerfil.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPerfil.Location = new System.Drawing.Point(276, 166);
            this.lblPerfil.Text = "Perfil *";
            this.lblPerfil.AutoSize = true;
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.TabIndex = 6;
            // 
            // cboPerfil
            // 
            this.cboPerfil.Location = new System.Drawing.Point(274, 190);
            this.cboPerfil.Size = new System.Drawing.Size(238, 29);
            this.cboPerfil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPerfil.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboPerfil.Name = "cboPerfil";
            this.cboPerfil.TabIndex = 7;
            this.cboPerfil.SelectedIndexChanged += new System.EventHandler(this.cboPerfil_SelectedIndexChanged);
            // 
            // lblDescricaoPerfil
            // 
            this.lblDescricaoPerfil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDescricaoPerfil.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDescricaoPerfil.Location = new System.Drawing.Point(28, 234);
            this.lblDescricaoPerfil.Size = new System.Drawing.Size(484, 40);
            this.lblDescricaoPerfil.Text = "";
            this.lblDescricaoPerfil.Name = "lblDescricaoPerfil";
            this.lblDescricaoPerfil.TabIndex = 8;
            // 
            // lblSenha
            // 
            this.lblSenha.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSenha.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSenha.Location = new System.Drawing.Point(30, 280);
            this.lblSenha.Text = "Senha *";
            this.lblSenha.AutoSize = true;
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.TabIndex = 9;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(28, 302);
            this.txtSenha.Size = new System.Drawing.Size(230, 40);
            this.txtSenha.Senha = true;
            this.txtSenha.MaxLength = 100;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.TabIndex = 10;
            // 
            // lblConfirmacao
            // 
            this.lblConfirmacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConfirmacao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblConfirmacao.Location = new System.Drawing.Point(276, 280);
            this.lblConfirmacao.Text = "Confirmar senha *";
            this.lblConfirmacao.AutoSize = true;
            this.lblConfirmacao.Name = "lblConfirmacao";
            this.lblConfirmacao.TabIndex = 11;
            // 
            // txtConfirmacao
            // 
            this.txtConfirmacao.Location = new System.Drawing.Point(274, 302);
            this.txtConfirmacao.Size = new System.Drawing.Size(238, 40);
            this.txtConfirmacao.Senha = true;
            this.txtConfirmacao.MaxLength = 100;
            this.txtConfirmacao.Name = "txtConfirmacao";
            this.txtConfirmacao.TabIndex = 12;
            // 
            // lblDicaSenha
            // 
            this.lblDicaSenha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDicaSenha.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDicaSenha.Location = new System.Drawing.Point(28, 350);
            this.lblDicaSenha.Size = new System.Drawing.Size(484, 40);
            this.lblDicaSenha.Text = "Mínimo de 6 caracteres, com letras e números. A senha é gravada com hash PBKDF2.";
            this.lblDicaSenha.Name = "lblDicaSenha";
            this.lblDicaSenha.TabIndex = 13;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkAtivo.Location = new System.Drawing.Point(30, 404);
            this.chkAtivo.Text = "Usuário ativo (pode entrar no sistema)";
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 14;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(250, 496);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 15;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(382, 496);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 16;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmUsuarioEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(540, 560);
            this.Text = "Usuário";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblPerfil);
            this.Controls.Add(this.cboPerfil);
            this.Controls.Add(this.lblDescricaoPerfil);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblConfirmacao);
            this.Controls.Add(this.txtConfirmacao);
            this.Controls.Add(this.lblDicaSenha);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmUsuarioEdicao";
            this.Load += new System.EventHandler(this.FrmUsuarioEdicao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblNome;
        private Lapapie.Controles.CampoTexto txtNome;
        private System.Windows.Forms.Label lblLogin;
        private Lapapie.Controles.CampoTexto txtLogin;
        private System.Windows.Forms.Label lblPerfil;
        private System.Windows.Forms.ComboBox cboPerfil;
        private System.Windows.Forms.Label lblDescricaoPerfil;
        private System.Windows.Forms.Label lblSenha;
        private Lapapie.Controles.CampoTexto txtSenha;
        private System.Windows.Forms.Label lblConfirmacao;
        private Lapapie.Controles.CampoTexto txtConfirmacao;
        private System.Windows.Forms.Label lblDicaSenha;
        private System.Windows.Forms.CheckBox chkAtivo;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
