namespace Lapapie.Forms.Cadastros
{
    partial class UcKits
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
            this.cardGrid = new Lapapie.Controles.PainelCartao();
            this.pnlDetalhe = new System.Windows.Forms.Panel();
            this.lblRodape = new System.Windows.Forms.Label();
            this.pnlFerramentas = new System.Windows.Forms.Panel();
            this.dgvKits = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.chkInativos = new System.Windows.Forms.CheckBox();
            this.btnEditar = new Lapapie.Controles.BotaoPill();
            this.btnNovo = new Lapapie.Controles.BotaoPill();
            this.cardDetalhe = new Lapapie.Controles.PainelCartao();
            this.dgvComponentes = new System.Windows.Forms.DataGridView();
            this.pnlCabecalhoKit = new System.Windows.Forms.Panel();
            this.lblDisponibilidade = new System.Windows.Forms.Label();
            this.picKit = new System.Windows.Forms.PictureBox();
            this.lblNomeKit = new System.Windows.Forms.Label();
            this.lblPrecoKit = new System.Windows.Forms.Label();
            this.lblEconomia = new System.Windows.Forms.Label();
            this.lblComposicao = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponentes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKit)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlDetalhe.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.cardDetalhe.SuspendLayout();
            this.pnlCabecalhoKit.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvKits);
            this.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGrid.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.cardGrid.Name = "cardGrid";
            this.cardGrid.TabIndex = 0;
            // 
            // pnlDetalhe
            // 
            this.pnlDetalhe.Controls.Add(this.cardDetalhe);
            this.pnlDetalhe.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetalhe.Size = new System.Drawing.Size(380, 500);
            this.pnlDetalhe.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.pnlDetalhe.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlDetalhe.Name = "pnlDetalhe";
            this.pnlDetalhe.TabIndex = 1;
            // 
            // lblRodape
            // 
            this.lblRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRodape.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRodape.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblRodape.Size = new System.Drawing.Size(1050, 34);
            this.lblRodape.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRodape.Text = "0 registros";
            this.lblRodape.Name = "lblRodape";
            this.lblRodape.TabIndex = 2;
            // 
            // pnlFerramentas
            // 
            this.pnlFerramentas.Controls.Add(this.txtPesquisa);
            this.pnlFerramentas.Controls.Add(this.chkInativos);
            this.pnlFerramentas.Controls.Add(this.btnEditar);
            this.pnlFerramentas.Controls.Add(this.btnNovo);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 62);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 3;
            // 
            // dgvKits
            // 
            this.dgvKits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKits.Name = "dgvKits";
            this.dgvKits.TabIndex = 0;
            this.dgvKits.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            this.dgvKits.SelectionChanged += new System.EventHandler(this.dgvKits_SelectionChanged);
            this.dgvKits.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Pesquisar kit";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // chkInativos
            // 
            this.chkInativos.AutoSize = true;
            this.chkInativos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkInativos.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkInativos.Location = new System.Drawing.Point(356, 16);
            this.chkInativos.Text = "Mostrar inativos";
            this.chkInativos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkInativos.Name = "chkInativos";
            this.chkInativos.TabIndex = 1;
            this.chkInativos.CheckedChanged += new System.EventHandler(this.chkInativos_CheckedChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.Location = new System.Drawing.Point(946, 7);
            this.btnEditar.Size = new System.Drawing.Size(104, 40);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnEditar.Icone = Lapapie.Tema.Icones.Editar;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.Location = new System.Drawing.Point(810, 7);
            this.btnNovo.Size = new System.Drawing.Size(128, 40);
            this.btnNovo.Text = "Novo kit";
            this.btnNovo.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnNovo.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.TabIndex = 3;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // cardDetalhe
            // 
            this.cardDetalhe.Controls.Add(this.dgvComponentes);
            this.cardDetalhe.Controls.Add(this.pnlCabecalhoKit);
            this.cardDetalhe.Controls.Add(this.lblDisponibilidade);
            this.cardDetalhe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDetalhe.Padding = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.cardDetalhe.Name = "cardDetalhe";
            this.cardDetalhe.TabIndex = 0;
            // 
            // dgvComponentes
            // 
            this.dgvComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComponentes.Name = "dgvComponentes";
            this.dgvComponentes.TabIndex = 0;
            // 
            // pnlCabecalhoKit
            // 
            this.pnlCabecalhoKit.Controls.Add(this.picKit);
            this.pnlCabecalhoKit.Controls.Add(this.lblNomeKit);
            this.pnlCabecalhoKit.Controls.Add(this.lblPrecoKit);
            this.pnlCabecalhoKit.Controls.Add(this.lblEconomia);
            this.pnlCabecalhoKit.Controls.Add(this.lblComposicao);
            this.pnlCabecalhoKit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecalhoKit.Size = new System.Drawing.Size(328, 226);
            this.pnlCabecalhoKit.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlCabecalhoKit.Name = "pnlCabecalhoKit";
            this.pnlCabecalhoKit.TabIndex = 1;
            // 
            // lblDisponibilidade
            // 
            this.lblDisponibilidade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDisponibilidade.Size = new System.Drawing.Size(328, 56);
            this.lblDisponibilidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDisponibilidade.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblDisponibilidade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDisponibilidade.Text = "";
            this.lblDisponibilidade.Name = "lblDisponibilidade";
            this.lblDisponibilidade.TabIndex = 2;
            // 
            // picKit
            // 
            this.picKit.Location = new System.Drawing.Point(0, 0);
            this.picKit.Size = new System.Drawing.Size(140, 140);
            this.picKit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKit.TabStop = false;
            this.picKit.Name = "picKit";
            this.picKit.TabIndex = 0;
            // 
            // lblNomeKit
            // 
            this.lblNomeKit.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNomeKit.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblNomeKit.Location = new System.Drawing.Point(152, 8);
            this.lblNomeKit.Size = new System.Drawing.Size(176, 70);
            this.lblNomeKit.Text = "Kit";
            this.lblNomeKit.Name = "lblNomeKit";
            this.lblNomeKit.TabIndex = 1;
            // 
            // lblPrecoKit
            // 
            this.lblPrecoKit.AutoSize = true;
            this.lblPrecoKit.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPrecoKit.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblPrecoKit.Location = new System.Drawing.Point(150, 84);
            this.lblPrecoKit.Text = "R$ 0,00";
            this.lblPrecoKit.Name = "lblPrecoKit";
            this.lblPrecoKit.TabIndex = 2;
            // 
            // lblEconomia
            // 
            this.lblEconomia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEconomia.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblEconomia.Location = new System.Drawing.Point(152, 118);
            this.lblEconomia.Size = new System.Drawing.Size(176, 40);
            this.lblEconomia.Text = "";
            this.lblEconomia.Name = "lblEconomia";
            this.lblEconomia.TabIndex = 3;
            // 
            // lblComposicao
            // 
            this.lblComposicao.AutoSize = true;
            this.lblComposicao.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComposicao.ForeColor = System.Drawing.Color.FromArgb(184, 149, 90);
            this.lblComposicao.Location = new System.Drawing.Point(0, 196);
            this.lblComposicao.Text = "COMPOSIÇÃO";
            this.lblComposicao.Name = "lblComposicao";
            this.lblComposicao.TabIndex = 4;
            // 
            // UcKits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.pnlDetalhe);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcKits";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponentes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKit)).EndInit();
            this.pnlCabecalhoKit.ResumeLayout(false);
            this.pnlCabecalhoKit.PerformLayout();
            this.cardDetalhe.ResumeLayout(false);
            this.cardDetalhe.PerformLayout();
            this.pnlFerramentas.ResumeLayout(false);
            this.pnlFerramentas.PerformLayout();
            this.pnlDetalhe.ResumeLayout(false);
            this.pnlDetalhe.PerformLayout();
            this.cardGrid.ResumeLayout(false);
            this.cardGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Lapapie.Controles.PainelCartao cardGrid;
        private System.Windows.Forms.Panel pnlDetalhe;
        private System.Windows.Forms.Label lblRodape;
        private System.Windows.Forms.Panel pnlFerramentas;
        private System.Windows.Forms.DataGridView dgvKits;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.CheckBox chkInativos;
        private Lapapie.Controles.BotaoPill btnEditar;
        private Lapapie.Controles.BotaoPill btnNovo;
        private Lapapie.Controles.PainelCartao cardDetalhe;
        private System.Windows.Forms.DataGridView dgvComponentes;
        private System.Windows.Forms.Panel pnlCabecalhoKit;
        private System.Windows.Forms.Label lblDisponibilidade;
        private System.Windows.Forms.PictureBox picKit;
        private System.Windows.Forms.Label lblNomeKit;
        private System.Windows.Forms.Label lblPrecoKit;
        private System.Windows.Forms.Label lblEconomia;
        private System.Windows.Forms.Label lblComposicao;
    }
}
