namespace Lapapie.Forms.Vendas
{
    partial class UcHistoricoVendas
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
            this.dgvVendas = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.lblDe = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblAte = new System.Windows.Forms.Label();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.abasStatus = new Lapapie.Controles.AbasPill();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            this.cardDetalhe = new Lapapie.Controles.PainelCartao();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.pnlDetalheRodape = new System.Windows.Forms.Panel();
            this.pnlDetalheTopo = new System.Windows.Forms.Panel();
            this.lblVendaTitulo = new System.Windows.Forms.Label();
            this.lblVendaInfo = new System.Windows.Forms.Label();
            this.lblVendaTotais = new System.Windows.Forms.Label();
            this.btnComprovante = new Lapapie.Controles.BotaoPill();
            this.btnCancelarVenda = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlDetalhe.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.cardDetalhe.SuspendLayout();
            this.pnlDetalheRodape.SuspendLayout();
            this.pnlDetalheTopo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvVendas);
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
            this.pnlFerramentas.Controls.Add(this.lblDe);
            this.pnlFerramentas.Controls.Add(this.dtpInicio);
            this.pnlFerramentas.Controls.Add(this.lblAte);
            this.pnlFerramentas.Controls.Add(this.dtpFim);
            this.pnlFerramentas.Controls.Add(this.abasStatus);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 3;
            // 
            // dgvVendas
            // 
            this.dgvVendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVendas.Name = "dgvVendas";
            this.dgvVendas.TabIndex = 0;
            this.dgvVendas.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.dgvVendas.SelectionChanged += new System.EventHandler(this.dgvVendas_SelectionChanged);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Cliente, vendedor ou nº da venda";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // lblDe
            // 
            this.lblDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDe.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDe.Location = new System.Drawing.Point(354, 18);
            this.lblDe.Text = "De";
            this.lblDe.AutoSize = true;
            this.lblDe.Name = "lblDe";
            this.lblDe.TabIndex = 1;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(376, 12);
            this.dtpInicio.Size = new System.Drawing.Size(130, 29);
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.TabIndex = 2;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.Filtro_Alterado);
            // 
            // lblAte
            // 
            this.lblAte.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAte.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblAte.Location = new System.Drawing.Point(516, 18);
            this.lblAte.Text = "até";
            this.lblAte.AutoSize = true;
            this.lblAte.Name = "lblAte";
            this.lblAte.TabIndex = 3;
            // 
            // dtpFim
            // 
            this.dtpFim.Location = new System.Drawing.Point(548, 12);
            this.dtpFim.Size = new System.Drawing.Size(130, 29);
            this.dtpFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFim.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.TabIndex = 4;
            this.dtpFim.ValueChanged += new System.EventHandler(this.Filtro_Alterado);
            // 
            // abasStatus
            // 
            this.abasStatus.Location = new System.Drawing.Point(0, 60);
            this.abasStatus.Size = new System.Drawing.Size(420, 36);
            this.abasStatus.Opcoes = "Todas|Concluídas|Canceladas";
            this.abasStatus.IndiceSelecionado = 0;
            this.abasStatus.Name = "abasStatus";
            this.abasStatus.TabIndex = 5;
            this.abasStatus.SelecaoAlterada += new System.EventHandler(this.Filtro_Alterado);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(932, 7);
            this.btnExportar.Size = new System.Drawing.Size(118, 40);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExportar.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.TabIndex = 6;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // cardDetalhe
            // 
            this.cardDetalhe.Controls.Add(this.dgvItens);
            this.cardDetalhe.Controls.Add(this.pnlDetalheRodape);
            this.cardDetalhe.Controls.Add(this.pnlDetalheTopo);
            this.cardDetalhe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDetalhe.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.cardDetalhe.Name = "cardDetalhe";
            this.cardDetalhe.TabIndex = 0;
            // 
            // dgvItens
            // 
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.TabIndex = 0;
            // 
            // pnlDetalheRodape
            // 
            this.pnlDetalheRodape.Controls.Add(this.lblVendaTotais);
            this.pnlDetalheRodape.Controls.Add(this.btnComprovante);
            this.pnlDetalheRodape.Controls.Add(this.btnCancelarVenda);
            this.pnlDetalheRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetalheRodape.Size = new System.Drawing.Size(328, 150);
            this.pnlDetalheRodape.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlDetalheRodape.Name = "pnlDetalheRodape";
            this.pnlDetalheRodape.TabIndex = 1;
            // 
            // pnlDetalheTopo
            // 
            this.pnlDetalheTopo.Controls.Add(this.lblVendaTitulo);
            this.pnlDetalheTopo.Controls.Add(this.lblVendaInfo);
            this.pnlDetalheTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetalheTopo.Size = new System.Drawing.Size(328, 150);
            this.pnlDetalheTopo.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlDetalheTopo.Name = "pnlDetalheTopo";
            this.pnlDetalheTopo.TabIndex = 2;
            // 
            // lblVendaTitulo
            // 
            this.lblVendaTitulo.AutoSize = true;
            this.lblVendaTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVendaTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblVendaTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblVendaTitulo.Text = "VENDA";
            this.lblVendaTitulo.Name = "lblVendaTitulo";
            this.lblVendaTitulo.TabIndex = 0;
            // 
            // lblVendaInfo
            // 
            this.lblVendaInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVendaInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVendaInfo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblVendaInfo.Location = new System.Drawing.Point(0, 34);
            this.lblVendaInfo.Size = new System.Drawing.Size(328, 110);
            this.lblVendaInfo.Text = "";
            this.lblVendaInfo.Name = "lblVendaInfo";
            this.lblVendaInfo.TabIndex = 1;
            // 
            // lblVendaTotais
            // 
            this.lblVendaTotais.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVendaTotais.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVendaTotais.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblVendaTotais.Location = new System.Drawing.Point(0, 6);
            this.lblVendaTotais.Size = new System.Drawing.Size(328, 62);
            this.lblVendaTotais.Text = "";
            this.lblVendaTotais.Name = "lblVendaTotais";
            this.lblVendaTotais.TabIndex = 0;
            // 
            // btnComprovante
            // 
            this.btnComprovante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnComprovante.Location = new System.Drawing.Point(0, 92);
            this.btnComprovante.Size = new System.Drawing.Size(156, 40);
            this.btnComprovante.Text = "Comprovante";
            this.btnComprovante.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnComprovante.Icone = Lapapie.Tema.Icones.Imprimir;
            this.btnComprovante.Name = "btnComprovante";
            this.btnComprovante.TabIndex = 1;
            this.btnComprovante.Click += new System.EventHandler(this.btnComprovante_Click);
            // 
            // btnCancelarVenda
            // 
            this.btnCancelarVenda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarVenda.Location = new System.Drawing.Point(168, 92);
            this.btnCancelarVenda.Size = new System.Drawing.Size(160, 40);
            this.btnCancelarVenda.Text = "Cancelar venda";
            this.btnCancelarVenda.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnCancelarVenda.Icone = Lapapie.Tema.Icones.Fechar;
            this.btnCancelarVenda.Name = "btnCancelarVenda";
            this.btnCancelarVenda.TabIndex = 2;
            this.btnCancelarVenda.Click += new System.EventHandler(this.btnCancelarVenda_Click);
            // 
            // UcHistoricoVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.pnlDetalhe);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcHistoricoVendas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.pnlDetalheTopo.ResumeLayout(false);
            this.pnlDetalheTopo.PerformLayout();
            this.pnlDetalheRodape.ResumeLayout(false);
            this.pnlDetalheRodape.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvVendas;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblAte;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private Lapapie.Controles.AbasPill abasStatus;
        private Lapapie.Controles.BotaoPill btnExportar;
        private Lapapie.Controles.PainelCartao cardDetalhe;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.Panel pnlDetalheRodape;
        private System.Windows.Forms.Panel pnlDetalheTopo;
        private System.Windows.Forms.Label lblVendaTitulo;
        private System.Windows.Forms.Label lblVendaInfo;
        private System.Windows.Forms.Label lblVendaTotais;
        private Lapapie.Controles.BotaoPill btnComprovante;
        private Lapapie.Controles.BotaoPill btnCancelarVenda;
    }
}
