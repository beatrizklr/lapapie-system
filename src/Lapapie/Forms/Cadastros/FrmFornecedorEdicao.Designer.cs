namespace Lapapie.Forms.Cadastros
{
    partial class FrmFornecedorEdicao
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
            this.lblRazaoSocial = new System.Windows.Forms.Label();
            this.txtRazaoSocial = new Lapapie.Controles.CampoTexto();
            this.lblNomeFantasia = new System.Windows.Forms.Label();
            this.txtNomeFantasia = new Lapapie.Controles.CampoTexto();
            this.lblCnpj = new System.Windows.Forms.Label();
            this.txtCnpj = new Lapapie.Controles.CampoTexto();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Lapapie.Controles.CampoTexto();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new Lapapie.Controles.CampoTexto();
            this.lblContato = new System.Windows.Forms.Label();
            this.txtContato = new Lapapie.Controles.CampoTexto();
            this.lblCidade = new System.Windows.Forms.Label();
            this.txtCidade = new Lapapie.Controles.CampoTexto();
            this.lblUf = new System.Windows.Forms.Label();
            this.txtUf = new Lapapie.Controles.CampoTexto();
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
            this.lblTitulo.Text = "NOVO FORNECEDOR";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(584, 22);
            this.lblSubtitulo.Text = "Campos com * são obrigatórios.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblRazaoSocial
            // 
            this.lblRazaoSocial.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRazaoSocial.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblRazaoSocial.Location = new System.Drawing.Point(30, 96);
            this.lblRazaoSocial.Text = "Razão social *";
            this.lblRazaoSocial.AutoSize = true;
            this.lblRazaoSocial.Name = "lblRazaoSocial";
            this.lblRazaoSocial.TabIndex = 2;
            // 
            // txtRazaoSocial
            // 
            this.txtRazaoSocial.Location = new System.Drawing.Point(28, 118);
            this.txtRazaoSocial.Size = new System.Drawing.Size(584, 40);
            this.txtRazaoSocial.MaxLength = 150;
            this.txtRazaoSocial.Name = "txtRazaoSocial";
            this.txtRazaoSocial.TabIndex = 3;
            // 
            // lblNomeFantasia
            // 
            this.lblNomeFantasia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNomeFantasia.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNomeFantasia.Location = new System.Drawing.Point(30, 166);
            this.lblNomeFantasia.Text = "Nome fantasia";
            this.lblNomeFantasia.AutoSize = true;
            this.lblNomeFantasia.Name = "lblNomeFantasia";
            this.lblNomeFantasia.TabIndex = 4;
            // 
            // txtNomeFantasia
            // 
            this.txtNomeFantasia.Location = new System.Drawing.Point(28, 188);
            this.txtNomeFantasia.Size = new System.Drawing.Size(340, 40);
            this.txtNomeFantasia.MaxLength = 100;
            this.txtNomeFantasia.Name = "txtNomeFantasia";
            this.txtNomeFantasia.TabIndex = 5;
            // 
            // lblCnpj
            // 
            this.lblCnpj.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCnpj.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCnpj.Location = new System.Drawing.Point(386, 166);
            this.lblCnpj.Text = "CNPJ *";
            this.lblCnpj.AutoSize = true;
            this.lblCnpj.Name = "lblCnpj";
            this.lblCnpj.TabIndex = 6;
            // 
            // txtCnpj
            // 
            this.txtCnpj.Location = new System.Drawing.Point(384, 188);
            this.txtCnpj.Size = new System.Drawing.Size(228, 40);
            this.txtCnpj.Formato = Lapapie.Controles.FormatoCampo.Cnpj;
            this.txtCnpj.MaxLength = 18;
            this.txtCnpj.Placeholder = "00.000.000/0000-00";
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblEmail.Location = new System.Drawing.Point(30, 236);
            this.lblEmail.Text = "E-mail";
            this.lblEmail.AutoSize = true;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(28, 258);
            this.txtEmail.Size = new System.Drawing.Size(340, 40);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 9;
            // 
            // lblTelefone
            // 
            this.lblTelefone.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTelefone.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTelefone.Location = new System.Drawing.Point(386, 236);
            this.lblTelefone.Text = "Telefone";
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.TabIndex = 10;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(384, 258);
            this.txtTelefone.Size = new System.Drawing.Size(228, 40);
            this.txtTelefone.Formato = Lapapie.Controles.FormatoCampo.Telefone;
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.TabIndex = 11;
            // 
            // lblContato
            // 
            this.lblContato.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblContato.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblContato.Location = new System.Drawing.Point(30, 306);
            this.lblContato.Text = "Pessoa de contato";
            this.lblContato.AutoSize = true;
            this.lblContato.Name = "lblContato";
            this.lblContato.TabIndex = 12;
            // 
            // txtContato
            // 
            this.txtContato.Location = new System.Drawing.Point(28, 328);
            this.txtContato.Size = new System.Drawing.Size(584, 40);
            this.txtContato.MaxLength = 100;
            this.txtContato.Name = "txtContato";
            this.txtContato.TabIndex = 13;
            // 
            // lblCidade
            // 
            this.lblCidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCidade.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCidade.Location = new System.Drawing.Point(30, 376);
            this.lblCidade.Text = "Cidade";
            this.lblCidade.AutoSize = true;
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.TabIndex = 14;
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(28, 398);
            this.txtCidade.Size = new System.Drawing.Size(440, 40);
            this.txtCidade.MaxLength = 80;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.TabIndex = 15;
            // 
            // lblUf
            // 
            this.lblUf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUf.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblUf.Location = new System.Drawing.Point(486, 376);
            this.lblUf.Text = "UF";
            this.lblUf.AutoSize = true;
            this.lblUf.Name = "lblUf";
            this.lblUf.TabIndex = 16;
            // 
            // txtUf
            // 
            this.txtUf.Location = new System.Drawing.Point(484, 398);
            this.txtUf.Size = new System.Drawing.Size(128, 40);
            this.txtUf.Formato = Lapapie.Controles.FormatoCampo.Uf;
            this.txtUf.MaxLength = 2;
            this.txtUf.Name = "txtUf";
            this.txtUf.TabIndex = 17;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkAtivo.Location = new System.Drawing.Point(30, 466);
            this.chkAtivo.Text = "Fornecedor ativo";
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 18;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(350, 456);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 19;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(482, 456);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 20;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmFornecedorEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(640, 520);
            this.Text = "Fornecedor";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblRazaoSocial);
            this.Controls.Add(this.txtRazaoSocial);
            this.Controls.Add(this.lblNomeFantasia);
            this.Controls.Add(this.txtNomeFantasia);
            this.Controls.Add(this.lblCnpj);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblContato);
            this.Controls.Add(this.txtContato);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.lblUf);
            this.Controls.Add(this.txtUf);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmFornecedorEdicao";
            this.Load += new System.EventHandler(this.FrmFornecedorEdicao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblRazaoSocial;
        private Lapapie.Controles.CampoTexto txtRazaoSocial;
        private System.Windows.Forms.Label lblNomeFantasia;
        private Lapapie.Controles.CampoTexto txtNomeFantasia;
        private System.Windows.Forms.Label lblCnpj;
        private Lapapie.Controles.CampoTexto txtCnpj;
        private System.Windows.Forms.Label lblEmail;
        private Lapapie.Controles.CampoTexto txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private Lapapie.Controles.CampoTexto txtTelefone;
        private System.Windows.Forms.Label lblContato;
        private Lapapie.Controles.CampoTexto txtContato;
        private System.Windows.Forms.Label lblCidade;
        private Lapapie.Controles.CampoTexto txtCidade;
        private System.Windows.Forms.Label lblUf;
        private Lapapie.Controles.CampoTexto txtUf;
        private System.Windows.Forms.CheckBox chkAtivo;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
