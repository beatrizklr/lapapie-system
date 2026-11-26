namespace Lapapie.Forms
{
    partial class FrmConfigConexao
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
            this.lblServidor = new System.Windows.Forms.Label();
            this.txtServidor = new Lapapie.Controles.CampoTexto();
            this.lblPorta = new System.Windows.Forms.Label();
            this.txtPorta = new Lapapie.Controles.CampoTexto();
            this.lblBanco = new System.Windows.Forms.Label();
            this.txtBanco = new Lapapie.Controles.CampoTexto();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new Lapapie.Controles.CampoTexto();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenha = new Lapapie.Controles.CampoTexto();
            this.lblResultado = new System.Windows.Forms.Label();
            this.btnTestar = new Lapapie.Controles.BotaoPill();
            this.btnCancelar = new Lapapie.Controles.BotaoPill();
            this.btnSalvar = new Lapapie.Controles.BotaoPill();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTitulo.Location = new System.Drawing.Point(30, 26);
            this.lblTitulo.Text = "CONEXÃO COM O BANCO";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblSubtitulo.Location = new System.Drawing.Point(32, 62);
            this.lblSubtitulo.Size = new System.Drawing.Size(400, 40);
            this.lblSubtitulo.Text = "Os dados ficam no arquivo appsettings.json, ao lado do executável.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblServidor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblServidor.Location = new System.Drawing.Point(34, 110);
            this.lblServidor.Text = "Servidor";
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.TabIndex = 2;
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(32, 132);
            this.txtServidor.Size = new System.Drawing.Size(290, 40);
            this.txtServidor.Placeholder = "localhost";
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.TabIndex = 3;
            // 
            // lblPorta
            // 
            this.lblPorta.AutoSize = true;
            this.lblPorta.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPorta.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPorta.Location = new System.Drawing.Point(338, 110);
            this.lblPorta.Text = "Porta";
            this.lblPorta.Name = "lblPorta";
            this.lblPorta.TabIndex = 4;
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(336, 132);
            this.txtPorta.Size = new System.Drawing.Size(92, 40);
            this.txtPorta.Formato = Lapapie.Controles.FormatoCampo.Inteiro;
            this.txtPorta.MaxLength = 5;
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.TabIndex = 5;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBanco.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblBanco.Location = new System.Drawing.Point(34, 180);
            this.lblBanco.Text = "Banco de dados";
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.TabIndex = 6;
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(32, 202);
            this.txtBanco.Size = new System.Drawing.Size(396, 40);
            this.txtBanco.Placeholder = "lapapie";
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.TabIndex = 7;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblUsuario.Location = new System.Drawing.Point(34, 250);
            this.lblUsuario.Text = "Usuário do MySQL";
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 8;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(32, 272);
            this.txtUsuario.Size = new System.Drawing.Size(396, 40);
            this.txtUsuario.Placeholder = "root";
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.TabIndex = 9;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSenha.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSenha.Location = new System.Drawing.Point(34, 320);
            this.lblSenha.Text = "Senha do MySQL";
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.TabIndex = 10;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(32, 342);
            this.txtSenha.Size = new System.Drawing.Size(396, 40);
            this.txtSenha.Senha = true;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.TabIndex = 11;
            // 
            // lblResultado
            // 
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResultado.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblResultado.Location = new System.Drawing.Point(34, 390);
            this.lblResultado.Size = new System.Drawing.Size(396, 44);
            this.lblResultado.Text = "";
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.TabIndex = 12;
            // 
            // btnTestar
            // 
            this.btnTestar.Location = new System.Drawing.Point(32, 450);
            this.btnTestar.Size = new System.Drawing.Size(130, 42);
            this.btnTestar.Text = "Testar";
            this.btnTestar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnTestar.Icone = Lapapie.Tema.Icones.Sincronizar;
            this.btnTestar.Name = "btnTestar";
            this.btnTestar.TabIndex = 13;
            this.btnTestar.Click += new System.EventHandler(this.btnTestar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(178, 450);
            this.btnCancelar.Size = new System.Drawing.Size(110, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Estilo = Lapapie.Controles.EstiloBotao.Secundario;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 14;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(298, 450);
            this.btnSalvar.Size = new System.Drawing.Size(130, 42);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Icone = Lapapie.Tema.Icones.Salvar;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 15;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmConfigConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(460, 520);
            this.Text = "Conexão com o MySQL";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.lblPorta);
            this.Controls.Add(this.txtPorta);
            this.Controls.Add(this.lblBanco);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.btnTestar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Name = "FrmConfigConexao";
            this.Load += new System.EventHandler(this.FrmConfigConexao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblServidor;
        private Lapapie.Controles.CampoTexto txtServidor;
        private System.Windows.Forms.Label lblPorta;
        private Lapapie.Controles.CampoTexto txtPorta;
        private System.Windows.Forms.Label lblBanco;
        private Lapapie.Controles.CampoTexto txtBanco;
        private System.Windows.Forms.Label lblUsuario;
        private Lapapie.Controles.CampoTexto txtUsuario;
        private System.Windows.Forms.Label lblSenha;
        private Lapapie.Controles.CampoTexto txtSenha;
        private System.Windows.Forms.Label lblResultado;
        private Lapapie.Controles.BotaoPill btnTestar;
        private Lapapie.Controles.BotaoPill btnCancelar;
        private Lapapie.Controles.BotaoPill btnSalvar;
    }
}
