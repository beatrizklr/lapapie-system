namespace Lapapie.Forms.ControleEstoque
{
    partial class UcEstoque
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
            this.lblRodape = new System.Windows.Forms.Label();
            this.pnlFerramentas = new System.Windows.Forms.Panel();
            this.dgvEstoque = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.abasSituacao = new Lapapie.Controles.AbasPill();
            this.btnEntrada = new Lapapie.Controles.BotaoPill();
            this.btnSaida = new Lapapie.Controles.BotaoPill();
            this.btnParametros = new Lapapie.Controles.BotaoPill();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvEstoque);
            this.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGrid.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.cardGrid.Name = "cardGrid";
            this.cardGrid.TabIndex = 0;
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
            this.lblRodape.TabIndex = 1;
            // 
            // pnlFerramentas
            // 
            this.pnlFerramentas.Controls.Add(this.txtPesquisa);
            this.pnlFerramentas.Controls.Add(this.cboCategoria);
            this.pnlFerramentas.Controls.Add(this.abasSituacao);
            this.pnlFerramentas.Controls.Add(this.btnEntrada);
            this.pnlFerramentas.Controls.Add(this.btnSaida);
            this.pnlFerramentas.Controls.Add(this.btnParametros);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 2;
            // 
            // dgvEstoque
            // 
            this.dgvEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstoque.Name = "dgvEstoque";
            this.dgvEstoque.TabIndex = 0;
            this.dgvEstoque.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.dgvEstoque.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Produto, código ou localização";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // cboCategoria
            // 
            this.cboCategoria.Location = new System.Drawing.Point(352, 12);
            this.cboCategoria.Size = new System.Drawing.Size(220, 29);
            this.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategoria.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.TabIndex = 1;
            this.cboCategoria.SelectedIndexChanged += new System.EventHandler(this.Filtro_Alterado);
            // 
            // abasSituacao
            // 
            this.abasSituacao.Location = new System.Drawing.Point(0, 60);
            this.abasSituacao.Size = new System.Drawing.Size(520, 36);
            this.abasSituacao.Opcoes = "Todos|Estoque baixo|Zerados|Normais";
            this.abasSituacao.IndiceSelecionado = 0;
            this.abasSituacao.Name = "abasSituacao";
            this.abasSituacao.TabIndex = 2;
            this.abasSituacao.SelecaoAlterada += new System.EventHandler(this.Filtro_Alterado);
            // 
            // btnEntrada
            // 
            this.btnEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntrada.Location = new System.Drawing.Point(938, 7);
            this.btnEntrada.Size = new System.Drawing.Size(112, 40);
            this.btnEntrada.Text = "Entrada";
            this.btnEntrada.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnEntrada.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.TabIndex = 3;
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // btnSaida
            // 
            this.btnSaida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaida.Location = new System.Drawing.Point(830, 7);
            this.btnSaida.Size = new System.Drawing.Size(100, 40);
            this.btnSaida.Text = "Saída";
            this.btnSaida.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnSaida.Icone = Lapapie.Tema.Icones.Remover;
            this.btnSaida.Name = "btnSaida";
            this.btnSaida.TabIndex = 4;
            this.btnSaida.Click += new System.EventHandler(this.btnSaida_Click);
            // 
            // btnParametros
            // 
            this.btnParametros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParametros.Location = new System.Drawing.Point(688, 7);
            this.btnParametros.Size = new System.Drawing.Size(134, 40);
            this.btnParametros.Text = "Parâmetros";
            this.btnParametros.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnParametros.Icone = Lapapie.Tema.Icones.Configuracao;
            this.btnParametros.Name = "btnParametros";
            this.btnParametros.TabIndex = 5;
            this.btnParametros.Click += new System.EventHandler(this.btnParametros_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(562, 7);
            this.btnExportar.Size = new System.Drawing.Size(118, 40);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExportar.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.TabIndex = 6;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // UcEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcEstoque";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).EndInit();
            this.pnlFerramentas.ResumeLayout(false);
            this.pnlFerramentas.PerformLayout();
            this.cardGrid.ResumeLayout(false);
            this.cardGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Lapapie.Controles.PainelCartao cardGrid;
        private System.Windows.Forms.Label lblRodape;
        private System.Windows.Forms.Panel pnlFerramentas;
        private System.Windows.Forms.DataGridView dgvEstoque;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.ComboBox cboCategoria;
        private Lapapie.Controles.AbasPill abasSituacao;
        private Lapapie.Controles.BotaoPill btnEntrada;
        private Lapapie.Controles.BotaoPill btnSaida;
        private Lapapie.Controles.BotaoPill btnParametros;
        private Lapapie.Controles.BotaoPill btnExportar;
    }
}
