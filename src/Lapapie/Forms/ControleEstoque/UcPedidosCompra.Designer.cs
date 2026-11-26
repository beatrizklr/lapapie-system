namespace Lapapie.Forms.ControleEstoque
{
    partial class UcPedidosCompra
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
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.abasStatus = new Lapapie.Controles.AbasPill();
            this.btnNovo = new Lapapie.Controles.BotaoPill();
            this.btnEditar = new Lapapie.Controles.BotaoPill();
            this.btnReceber = new Lapapie.Controles.BotaoPill();
            this.btnCancelarPedido = new Lapapie.Controles.BotaoPill();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvPedidos);
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
            this.pnlFerramentas.Controls.Add(this.abasStatus);
            this.pnlFerramentas.Controls.Add(this.btnNovo);
            this.pnlFerramentas.Controls.Add(this.btnEditar);
            this.pnlFerramentas.Controls.Add(this.btnReceber);
            this.pnlFerramentas.Controls.Add(this.btnCancelarPedido);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 2;
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.TabIndex = 0;
            this.dgvPedidos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.dgvPedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Fornecedor, nº do pedido ou observação";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // abasStatus
            // 
            this.abasStatus.Location = new System.Drawing.Point(0, 60);
            this.abasStatus.Size = new System.Drawing.Size(480, 36);
            this.abasStatus.Opcoes = "Todos|Abertos|Recebidos|Cancelados";
            this.abasStatus.IndiceSelecionado = 0;
            this.abasStatus.Name = "abasStatus";
            this.abasStatus.TabIndex = 1;
            this.abasStatus.SelecaoAlterada += new System.EventHandler(this.Filtro_Alterado);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.Location = new System.Drawing.Point(910, 7);
            this.btnNovo.Size = new System.Drawing.Size(140, 40);
            this.btnNovo.Text = "Novo pedido";
            this.btnNovo.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnNovo.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.TabIndex = 2;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.Location = new System.Drawing.Point(806, 7);
            this.btnEditar.Size = new System.Drawing.Size(96, 40);
            this.btnEditar.Text = "Abrir";
            this.btnEditar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnEditar.Icone = Lapapie.Tema.Icones.Editar;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnReceber
            // 
            this.btnReceber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceber.Location = new System.Drawing.Point(682, 7);
            this.btnReceber.Size = new System.Drawing.Size(116, 40);
            this.btnReceber.Text = "Receber";
            this.btnReceber.Estilo = Lapapie.Controles.EstiloBotao.Destaque;
            this.btnReceber.Icone = Lapapie.Tema.Icones.Confirmar;
            this.btnReceber.Name = "btnReceber";
            this.btnReceber.TabIndex = 4;
            this.btnReceber.Click += new System.EventHandler(this.btnReceber_Click);
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarPedido.Location = new System.Drawing.Point(562, 7);
            this.btnCancelarPedido.Size = new System.Drawing.Size(112, 40);
            this.btnCancelarPedido.Text = "Cancelar";
            this.btnCancelarPedido.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnCancelarPedido.Icone = Lapapie.Tema.Icones.Fechar;
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.TabIndex = 5;
            this.btnCancelarPedido.Click += new System.EventHandler(this.btnCancelarPedido_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(932, 58);
            this.btnExportar.Size = new System.Drawing.Size(118, 40);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExportar.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.TabIndex = 6;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // UcPedidosCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcPedidosCompra";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvPedidos;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private Lapapie.Controles.AbasPill abasStatus;
        private Lapapie.Controles.BotaoPill btnNovo;
        private Lapapie.Controles.BotaoPill btnEditar;
        private Lapapie.Controles.BotaoPill btnReceber;
        private Lapapie.Controles.BotaoPill btnCancelarPedido;
        private Lapapie.Controles.BotaoPill btnExportar;
    }
}
