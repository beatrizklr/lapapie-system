namespace Lapapie.Forms.Cadastros
{
    partial class FrmClienteEdicao
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
            this.lblCpf = new System.Windows.Forms.Label();
            this.txtCpf = new Lapapie.Controles.CampoTexto();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new Lapapie.Controles.CampoTexto();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Lapapie.Controles.CampoTexto();
            this.lblSecaoEndereco = new System.Windows.Forms.Label();
            this.lblCep = new System.Windows.Forms.Label();
            this.txtCep = new Lapapie.Controles.CampoTexto();
            this.lblLogradouro = new System.Windows.Forms.Label();
            this.txtLogradouro = new Lapapie.Controles.CampoTexto();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new Lapapie.Controles.CampoTexto();
            this.lblComplemento = new System.Windows.Forms.Label();
            this.txtComplemento = new Lapapie.Controles.CampoTexto();
            this.lblBairro = new System.Windows.Forms.Label();
            this.txtBairro = new Lapapie.Controles.CampoTexto();
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
            this.lblTitulo.Text = "NOVO CLIENTE";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Size = new System.Drawing.Size(584, 22);
            this.lblSubtitulo.Text = "Campos com * são obrigatórios. CPF e endereço são opcionais.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblNome
            // 
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNome.Location = new System.Drawing.Point(30, 96);
            this.lblNome.Text = "Nome completo *";
            this.lblNome.AutoSize = true;
            this.lblNome.Name = "lblNome";
            this.lblNome.TabIndex = 2;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(28, 118);
            this.txtNome.Size = new System.Drawing.Size(584, 40);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.TabIndex = 3;
            // 
            // lblCpf
            // 
            this.lblCpf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCpf.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCpf.Location = new System.Drawing.Point(30, 166);
            this.lblCpf.Text = "CPF";
            this.lblCpf.AutoSize = true;
            this.lblCpf.Name = "lblCpf";
            this.lblCpf.TabIndex = 4;
            // 
            // txtCpf
            // 
            this.txtCpf.Location = new System.Drawing.Point(28, 188);
            this.txtCpf.Size = new System.Drawing.Size(280, 40);
            this.txtCpf.Formato = Lapapie.Controles.FormatoCampo.Cpf;
            this.txtCpf.MaxLength = 14;
            this.txtCpf.Placeholder = "000.000.000-00";
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.TabIndex = 5;
            // 
            // lblTelefone
            // 
            this.lblTelefone.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTelefone.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblTelefone.Location = new System.Drawing.Point(334, 166);
            this.lblTelefone.Text = "Telefone";
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.TabIndex = 6;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(332, 188);
            this.txtTelefone.Size = new System.Drawing.Size(280, 40);
            this.txtTelefone.Formato = Lapapie.Controles.FormatoCampo.Telefone;
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.Placeholder = "(11) 99999-9999";
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.TabIndex = 7;
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
            this.txtEmail.Size = new System.Drawing.Size(584, 40);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Placeholder = "nome@exemplo.com";
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 9;
            // 
            // lblSecaoEndereco
            // 
            this.lblSecaoEndereco.AutoSize = true;
            this.lblSecaoEndereco.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSecaoEndereco.ForeColor = System.Drawing.Color.FromArgb(184, 149, 90);
            this.lblSecaoEndereco.Location = new System.Drawing.Point(28, 316);
            this.lblSecaoEndereco.Text = "ENDEREÇO";
            this.lblSecaoEndereco.Name = "lblSecaoEndereco";
            this.lblSecaoEndereco.TabIndex = 10;
            // 
            // lblCep
            // 
            this.lblCep.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCep.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCep.Location = new System.Drawing.Point(30, 340);
            this.lblCep.Text = "CEP";
            this.lblCep.AutoSize = true;
            this.lblCep.Name = "lblCep";
            this.lblCep.TabIndex = 11;
            // 
            // txtCep
            // 
            this.txtCep.Location = new System.Drawing.Point(28, 362);
            this.txtCep.Size = new System.Drawing.Size(170, 40);
            this.txtCep.Formato = Lapapie.Controles.FormatoCampo.Cep;
            this.txtCep.MaxLength = 9;
            this.txtCep.Placeholder = "00000-000";
            this.txtCep.Name = "txtCep";
            this.txtCep.TabIndex = 12;
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogradouro.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblLogradouro.Location = new System.Drawing.Point(216, 340);
            this.lblLogradouro.Text = "Logradouro";
            this.lblLogradouro.AutoSize = true;
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.TabIndex = 13;
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Location = new System.Drawing.Point(214, 362);
            this.txtLogradouro.Size = new System.Drawing.Size(398, 40);
            this.txtLogradouro.MaxLength = 150;
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.TabIndex = 14;
            // 
            // lblNumero
            // 
            this.lblNumero.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNumero.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblNumero.Location = new System.Drawing.Point(30, 410);
            this.lblNumero.Text = "Número";
            this.lblNumero.AutoSize = true;
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.TabIndex = 15;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(28, 432);
            this.txtNumero.Size = new System.Drawing.Size(120, 40);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.TabIndex = 16;
            // 
            // lblComplemento
            // 
            this.lblComplemento.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblComplemento.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblComplemento.Location = new System.Drawing.Point(166, 410);
            this.lblComplemento.Text = "Complemento";
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.TabIndex = 17;
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(164, 432);
            this.txtComplemento.Size = new System.Drawing.Size(200, 40);
            this.txtComplemento.MaxLength = 50;
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.TabIndex = 18;
            // 
            // lblBairro
            // 
            this.lblBairro.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBairro.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblBairro.Location = new System.Drawing.Point(382, 410);
            this.lblBairro.Text = "Bairro";
            this.lblBairro.AutoSize = true;
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.TabIndex = 19;
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(380, 432);
            this.txtBairro.Size = new System.Drawing.Size(232, 40);
            this.txtBairro.MaxLength = 80;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.TabIndex = 20;
            // 
            // lblCidade
            // 
            this.lblCidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCidade.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblCidade.Location = new System.Drawing.Point(30, 480);
            this.lblCidade.Text = "Cidade";
            this.lblCidade.AutoSize = true;
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.TabIndex = 21;
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(28, 502);
            this.txtCidade.Size = new System.Drawing.Size(440, 40);
            this.txtCidade.MaxLength = 80;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.TabIndex = 22;
            // 
            // lblUf
            // 
            this.lblUf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUf.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblUf.Location = new System.Drawing.Point(486, 480);
            this.lblUf.Text = "UF";
            this.lblUf.AutoSize = true;
            this.lblUf.Name = "lblUf";
            this.lblUf.TabIndex = 23;
            // 
            // txtUf
            // 
            this.txtUf.Location = new System.Drawing.Point(484, 502);
            this.txtUf.Size = new System.Drawing.Size(128, 40);
            this.txtUf.Formato = Lapapie.Controles.FormatoCampo.Uf;
            this.txtUf.MaxLength = 2;
            this.txtUf.Name = "txtUf";
            this.txtUf.TabIndex = 24;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkAtivo.Location = new System.Drawing.Point(30, 566);
            this.chkAtivo.Text = "Cliente ativo";
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 25;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(350, 556);
            this.btnCancelar.Size = new System.Drawing.Size(120, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 26;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(482, 556);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 27;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmClienteEdicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(640, 620);
            this.Text = "Cliente";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblCpf);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblSecaoEndereco);
            this.Controls.Add(this.lblCep);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.lblLogradouro);
            this.Controls.Add(this.txtLogradouro);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblComplemento);
            this.Controls.Add(this.txtComplemento);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.lblUf);
            this.Controls.Add(this.txtUf);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmClienteEdicao";
            this.Load += new System.EventHandler(this.FrmClienteEdicao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblNome;
        private Lapapie.Controles.CampoTexto txtNome;
        private System.Windows.Forms.Label lblCpf;
        private Lapapie.Controles.CampoTexto txtCpf;
        private System.Windows.Forms.Label lblTelefone;
        private Lapapie.Controles.CampoTexto txtTelefone;
        private System.Windows.Forms.Label lblEmail;
        private Lapapie.Controles.CampoTexto txtEmail;
        private System.Windows.Forms.Label lblSecaoEndereco;
        private System.Windows.Forms.Label lblCep;
        private Lapapie.Controles.CampoTexto txtCep;
        private System.Windows.Forms.Label lblLogradouro;
        private Lapapie.Controles.CampoTexto txtLogradouro;
        private System.Windows.Forms.Label lblNumero;
        private Lapapie.Controles.CampoTexto txtNumero;
        private System.Windows.Forms.Label lblComplemento;
        private Lapapie.Controles.CampoTexto txtComplemento;
        private System.Windows.Forms.Label lblBairro;
        private Lapapie.Controles.CampoTexto txtBairro;
        private System.Windows.Forms.Label lblCidade;
        private Lapapie.Controles.CampoTexto txtCidade;
        private System.Windows.Forms.Label lblUf;
        private Lapapie.Controles.CampoTexto txtUf;
        private System.Windows.Forms.CheckBox chkAtivo;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
