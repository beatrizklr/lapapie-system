namespace Lapapie.Forms.ControleEstoque
{
    partial class UcEstoqueBaixo
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
            this.pnlResumo = new System.Windows.Forms.Panel();
            this.lblRodape = new System.Windows.Forms.Label();
            this.pnlFerramentas = new System.Windows.Forms.Panel();
            this.dgvEstoqueBaixo = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.btnGerarPedido = new Lapapie.Controles.BotaoPill();
            this.btnMovimentar = new Lapapie.Controles.BotaoPill();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            this.kpiZerados = new Lapapie.Controles.CartaoIndicador();
            this.kpiAbaixo = new Lapapie.Controles.CartaoIndicador();
            this.kpiKits = new Lapapie.Controles.CartaoIndicador();
            this.lblDicaSelecao = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoqueBaixo)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlResumo.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvEstoqueBaixo);
            this.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGrid.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.cardGrid.Name = "cardGrid";
            this.cardGrid.TabIndex = 0;
            // 
            // pnlResumo
            // 
            this.pnlResumo.Controls.Add(this.kpiZerados);
            this.pnlResumo.Controls.Add(this.kpiAbaixo);
            this.pnlResumo.Controls.Add(this.kpiKits);
            this.pnlResumo.Controls.Add(this.lblDicaSelecao);
            this.pnlResumo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResumo.Size = new System.Drawing.Size(1050, 150);
            this.pnlResumo.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlResumo.Name = "pnlResumo";
            this.pnlResumo.TabIndex = 1;
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
            this.pnlFerramentas.Controls.Add(this.btnGerarPedido);
            this.pnlFerramentas.Controls.Add(this.btnMovimentar);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 62);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 3;
            // 
            // dgvEstoqueBaixo
            // 
            this.dgvEstoqueBaixo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstoqueBaixo.Name = "dgvEstoqueBaixo";
            this.dgvEstoqueBaixo.TabIndex = 0;
            this.dgvEstoqueBaixo.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Filtrar produto";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // btnGerarPedido
            // 
            this.btnGerarPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarPedido.Location = new System.Drawing.Point(838, 7);
            this.btnGerarPedido.Size = new System.Drawing.Size(212, 40);
            this.btnGerarPedido.Text = "Gerar pedido de compra";
            this.btnGerarPedido.Estilo = Lapapie.Controles.EstiloBotao.Destaque;
            this.btnGerarPedido.Icone = Lapapie.Tema.Icones.Documento;
            this.btnGerarPedido.Name = "btnGerarPedido";
            this.btnGerarPedido.TabIndex = 1;
            this.btnGerarPedido.Click += new System.EventHandler(this.btnGerarPedido_Click);
            // 
            // btnMovimentar
            // 
            this.btnMovimentar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMovimentar.Location = new System.Drawing.Point(662, 7);
            this.btnMovimentar.Size = new System.Drawing.Size(168, 40);
            this.btnMovimentar.Text = "Registrar entrada";
            this.btnMovimentar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnMovimentar.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnMovimentar.Name = "btnMovimentar";
            this.btnMovimentar.TabIndex = 2;
            this.btnMovimentar.Click += new System.EventHandler(this.btnMovimentar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(536, 7);
            this.btnExportar.Size = new System.Drawing.Size(118, 40);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExportar.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.TabIndex = 3;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // kpiZerados
            // 
            this.kpiZerados.Location = new System.Drawing.Point(0, 8);
            this.kpiZerados.Size = new System.Drawing.Size(250, 130);
            this.kpiZerados.Titulo = "Zerados";
            this.kpiZerados.Valor = "0";
            this.kpiZerados.Icone = Lapapie.Tema.Icones.Erro;
            this.kpiZerados.CorDestaque = System.Drawing.Color.FromArgb(201, 139, 123);
            this.kpiZerados.Name = "kpiZerados";
            this.kpiZerados.TabIndex = 0;
            // 
            // kpiAbaixo
            // 
            this.kpiAbaixo.Location = new System.Drawing.Point(266, 8);
            this.kpiAbaixo.Size = new System.Drawing.Size(250, 130);
            this.kpiAbaixo.Titulo = "Abaixo do mínimo";
            this.kpiAbaixo.Valor = "0";
            this.kpiAbaixo.Icone = Lapapie.Tema.Icones.Alerta;
            this.kpiAbaixo.CorDestaque = System.Drawing.Color.FromArgb(184, 149, 90);
            this.kpiAbaixo.Name = "kpiAbaixo";
            this.kpiAbaixo.TabIndex = 1;
            // 
            // kpiKits
            // 
            this.kpiKits.Location = new System.Drawing.Point(532, 8);
            this.kpiKits.Size = new System.Drawing.Size(250, 130);
            this.kpiKits.Titulo = "Kits indisponíveis";
            this.kpiKits.Valor = "0";
            this.kpiKits.Icone = Lapapie.Tema.Icones.Loja;
            this.kpiKits.CorDestaque = System.Drawing.Color.FromArgb(142, 155, 131);
            this.kpiKits.Name = "kpiKits";
            this.kpiKits.TabIndex = 2;
            // 
            // lblDicaSelecao
            // 
            this.lblDicaSelecao.Location = new System.Drawing.Point(800, 20);
            this.lblDicaSelecao.Size = new System.Drawing.Size(250, 110);
            this.lblDicaSelecao.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblDicaSelecao.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblDicaSelecao.Text = "Dica: selecione linhas (Ctrl+clique) antes de gerar o pedido para incluir só esses produtos.";
            this.lblDicaSelecao.Name = "lblDicaSelecao";
            this.lblDicaSelecao.TabIndex = 3;
            // 
            // UcEstoqueBaixo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.pnlResumo);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcEstoqueBaixo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoqueBaixo)).EndInit();
            this.pnlFerramentas.ResumeLayout(false);
            this.pnlFerramentas.PerformLayout();
            this.pnlResumo.ResumeLayout(false);
            this.pnlResumo.PerformLayout();
            this.cardGrid.ResumeLayout(false);
            this.cardGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Lapapie.Controles.PainelCartao cardGrid;
        private System.Windows.Forms.Panel pnlResumo;
        private System.Windows.Forms.Label lblRodape;
        private System.Windows.Forms.Panel pnlFerramentas;
        private System.Windows.Forms.DataGridView dgvEstoqueBaixo;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private Lapapie.Controles.BotaoPill btnGerarPedido;
        private Lapapie.Controles.BotaoPill btnMovimentar;
        private Lapapie.Controles.BotaoPill btnExportar;
        private Lapapie.Controles.CartaoIndicador kpiZerados;
        private Lapapie.Controles.CartaoIndicador kpiAbaixo;
        private Lapapie.Controles.CartaoIndicador kpiKits;
        private System.Windows.Forms.Label lblDicaSelecao;
    }
}
