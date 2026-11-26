namespace Lapapie.Forms.Inicio
{
    partial class UcDashboard
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
            this.kpiFaturamento = new Lapapie.Controles.CartaoIndicador();
            this.kpiVendas = new Lapapie.Controles.CartaoIndicador();
            this.kpiTicket = new Lapapie.Controles.CartaoIndicador();
            this.kpiEstoque = new Lapapie.Controles.CartaoIndicador();
            this.cardMensal = new Lapapie.Controles.PainelCartao();
            this.graficoMensal = new Lapapie.Controles.GraficoBarras();
            this.cardPagamentos = new Lapapie.Controles.PainelCartao();
            this.graficoPagamentos = new Lapapie.Controles.GraficoRosca();
            this.cardOferta = new Lapapie.Controles.PainelCartao();
            this.pnlOfertaConteudo = new System.Windows.Forms.Panel();
            this.faixaOferta = new Lapapie.Controles.FaixaOferta();
            this.lblOfertaNome = new System.Windows.Forms.Label();
            this.lblOfertaDescricao = new System.Windows.Forms.Label();
            this.lblOfertaValor = new System.Windows.Forms.Label();
            this.lblOfertaPeriodo = new System.Windows.Forms.Label();
            this.btnVerPromocao = new Lapapie.Controles.BotaoPill();
            this.flpOfertaProdutos = new System.Windows.Forms.FlowLayoutPanel();
            this.cardTopProdutos = new Lapapie.Controles.PainelCartao();
            this.graficoTop = new Lapapie.Controles.GraficoBarras();
            this.cardUltimas = new Lapapie.Controles.PainelCartao();
            this.dgvUltimas = new System.Windows.Forms.DataGridView();
            this.cardKits = new Lapapie.Controles.PainelCartao();
            this.dgvKits = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKits)).BeginInit();
            this.cardMensal.SuspendLayout();
            this.cardPagamentos.SuspendLayout();
            this.cardOferta.SuspendLayout();
            this.pnlOfertaConteudo.SuspendLayout();
            this.flpOfertaProdutos.SuspendLayout();
            this.cardTopProdutos.SuspendLayout();
            this.cardUltimas.SuspendLayout();
            this.cardKits.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpiFaturamento
            // 
            this.kpiFaturamento.Location = new System.Drawing.Point(0, 0);
            this.kpiFaturamento.Size = new System.Drawing.Size(250, 132);
            this.kpiFaturamento.Titulo = "Faturamento · 30 dias";
            this.kpiFaturamento.Icone = Lapapie.Tema.Icones.Cartao;
            this.kpiFaturamento.CorDestaque = System.Drawing.Color.FromArgb(201, 139, 123);
            this.kpiFaturamento.Name = "kpiFaturamento";
            this.kpiFaturamento.TabIndex = 0;
            // 
            // kpiVendas
            // 
            this.kpiVendas.Location = new System.Drawing.Point(266, 0);
            this.kpiVendas.Size = new System.Drawing.Size(250, 132);
            this.kpiVendas.Titulo = "Vendas · 30 dias";
            this.kpiVendas.Icone = Lapapie.Tema.Icones.Carrinho;
            this.kpiVendas.CorDestaque = System.Drawing.Color.FromArgb(51, 54, 61);
            this.kpiVendas.Name = "kpiVendas";
            this.kpiVendas.TabIndex = 1;
            // 
            // kpiTicket
            // 
            this.kpiTicket.Location = new System.Drawing.Point(532, 0);
            this.kpiTicket.Size = new System.Drawing.Size(250, 132);
            this.kpiTicket.Titulo = "Ticket médio";
            this.kpiTicket.Icone = Lapapie.Tema.Icones.Etiqueta;
            this.kpiTicket.CorDestaque = System.Drawing.Color.FromArgb(142, 155, 131);
            this.kpiTicket.Name = "kpiTicket";
            this.kpiTicket.TabIndex = 2;
            // 
            // kpiEstoque
            // 
            this.kpiEstoque.Location = new System.Drawing.Point(798, 0);
            this.kpiEstoque.Size = new System.Drawing.Size(250, 132);
            this.kpiEstoque.Titulo = "Estoque baixo";
            this.kpiEstoque.Icone = Lapapie.Tema.Icones.Alerta;
            this.kpiEstoque.CorDestaque = System.Drawing.Color.FromArgb(184, 149, 90);
            this.kpiEstoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kpiEstoque.Name = "kpiEstoque";
            this.kpiEstoque.TabIndex = 3;
            this.kpiEstoque.Click += new System.EventHandler(this.kpiEstoque_Click);
            // 
            // cardMensal
            // 
            this.cardMensal.Controls.Add(this.graficoMensal);
            this.cardMensal.Location = new System.Drawing.Point(0, 148);
            this.cardMensal.Size = new System.Drawing.Size(620, 320);
            this.cardMensal.Titulo = "Faturamento mensal";
            this.cardMensal.Subtitulo = "Últimos 6 meses · vendas concluídas";
            this.cardMensal.Padding = new System.Windows.Forms.Padding(12, 66, 12, 12);
            this.cardMensal.Name = "cardMensal";
            this.cardMensal.TabIndex = 4;
            // 
            // graficoMensal
            // 
            this.graficoMensal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoMensal.Name = "graficoMensal";
            this.graficoMensal.TabIndex = 0;
            // 
            // cardPagamentos
            // 
            this.cardPagamentos.Controls.Add(this.graficoPagamentos);
            this.cardPagamentos.Location = new System.Drawing.Point(636, 148);
            this.cardPagamentos.Size = new System.Drawing.Size(412, 320);
            this.cardPagamentos.Titulo = "Como pagam";
            this.cardPagamentos.Subtitulo = "Formas de pagamento · 30 dias";
            this.cardPagamentos.Padding = new System.Windows.Forms.Padding(20, 66, 20, 12);
            this.cardPagamentos.Name = "cardPagamentos";
            this.cardPagamentos.TabIndex = 5;
            // 
            // graficoPagamentos
            // 
            this.graficoPagamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoPagamentos.Name = "graficoPagamentos";
            this.graficoPagamentos.TabIndex = 0;
            // 
            // cardOferta
            // 
            this.cardOferta.Controls.Add(this.pnlOfertaConteudo);
            this.cardOferta.Controls.Add(this.faixaOferta);
            this.cardOferta.Location = new System.Drawing.Point(0, 484);
            this.cardOferta.Size = new System.Drawing.Size(620, 340);
            this.cardOferta.Padding = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this.cardOferta.Name = "cardOferta";
            this.cardOferta.TabIndex = 6;
            // 
            // pnlOfertaConteudo
            // 
            this.pnlOfertaConteudo.Controls.Add(this.lblOfertaNome);
            this.pnlOfertaConteudo.Controls.Add(this.lblOfertaDescricao);
            this.pnlOfertaConteudo.Controls.Add(this.lblOfertaValor);
            this.pnlOfertaConteudo.Controls.Add(this.lblOfertaPeriodo);
            this.pnlOfertaConteudo.Controls.Add(this.btnVerPromocao);
            this.pnlOfertaConteudo.Controls.Add(this.flpOfertaProdutos);
            this.pnlOfertaConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOfertaConteudo.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlOfertaConteudo.Name = "pnlOfertaConteudo";
            this.pnlOfertaConteudo.TabIndex = 0;
            // 
            // faixaOferta
            // 
            this.faixaOferta.Dock = System.Windows.Forms.DockStyle.Top;
            this.faixaOferta.Size = new System.Drawing.Size(620, 62);
            this.faixaOferta.Text = "OFERTA DA SEMANA";
            this.faixaOferta.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.faixaOferta.Name = "faixaOferta";
            this.faixaOferta.TabIndex = 1;
            // 
            // lblOfertaNome
            // 
            this.lblOfertaNome.AutoSize = true;
            this.lblOfertaNome.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOfertaNome.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblOfertaNome.Location = new System.Drawing.Point(24, 16);
            this.lblOfertaNome.Text = "Promoção";
            this.lblOfertaNome.Name = "lblOfertaNome";
            this.lblOfertaNome.TabIndex = 0;
            // 
            // lblOfertaDescricao
            // 
            this.lblOfertaDescricao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOfertaDescricao.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblOfertaDescricao.Location = new System.Drawing.Point(24, 46);
            this.lblOfertaDescricao.Size = new System.Drawing.Size(250, 60);
            this.lblOfertaDescricao.Text = "";
            this.lblOfertaDescricao.Name = "lblOfertaDescricao";
            this.lblOfertaDescricao.TabIndex = 1;
            // 
            // lblOfertaValor
            // 
            this.lblOfertaValor.AutoSize = true;
            this.lblOfertaValor.Font = new System.Drawing.Font("Bahnschrift SemiBold", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOfertaValor.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblOfertaValor.Location = new System.Drawing.Point(20, 108);
            this.lblOfertaValor.Text = "";
            this.lblOfertaValor.Name = "lblOfertaValor";
            this.lblOfertaValor.TabIndex = 2;
            // 
            // lblOfertaPeriodo
            // 
            this.lblOfertaPeriodo.AutoSize = true;
            this.lblOfertaPeriodo.Font = new System.Drawing.Font("Georgia", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblOfertaPeriodo.ForeColor = System.Drawing.Color.FromArgb(158, 88, 70);
            this.lblOfertaPeriodo.Location = new System.Drawing.Point(24, 164);
            this.lblOfertaPeriodo.Text = "";
            this.lblOfertaPeriodo.Name = "lblOfertaPeriodo";
            this.lblOfertaPeriodo.TabIndex = 3;
            // 
            // btnVerPromocao
            // 
            this.btnVerPromocao.Location = new System.Drawing.Point(24, 196);
            this.btnVerPromocao.Size = new System.Drawing.Size(170, 38);
            this.btnVerPromocao.Text = "Vender agora";
            this.btnVerPromocao.MostrarSeta = true;
            this.btnVerPromocao.Name = "btnVerPromocao";
            this.btnVerPromocao.TabIndex = 4;
            this.btnVerPromocao.Click += new System.EventHandler(this.btnVerPromocao_Click);
            // 
            // flpOfertaProdutos
            // 
            this.flpOfertaProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flpOfertaProdutos.Location = new System.Drawing.Point(290, 10);
            this.flpOfertaProdutos.Size = new System.Drawing.Size(320, 220);
            this.flpOfertaProdutos.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.flpOfertaProdutos.WrapContents = false;
            this.flpOfertaProdutos.Name = "flpOfertaProdutos";
            this.flpOfertaProdutos.TabIndex = 5;
            // 
            // cardTopProdutos
            // 
            this.cardTopProdutos.Controls.Add(this.graficoTop);
            this.cardTopProdutos.Location = new System.Drawing.Point(636, 484);
            this.cardTopProdutos.Size = new System.Drawing.Size(412, 340);
            this.cardTopProdutos.Titulo = "Mais vendidos";
            this.cardTopProdutos.Subtitulo = "Unidades · últimos 30 dias";
            this.cardTopProdutos.Padding = new System.Windows.Forms.Padding(20, 66, 20, 12);
            this.cardTopProdutos.Name = "cardTopProdutos";
            this.cardTopProdutos.TabIndex = 7;
            // 
            // graficoTop
            // 
            this.graficoTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoTop.Horizontal = true;
            this.graficoTop.Name = "graficoTop";
            this.graficoTop.TabIndex = 0;
            // 
            // cardUltimas
            // 
            this.cardUltimas.Controls.Add(this.dgvUltimas);
            this.cardUltimas.Location = new System.Drawing.Point(0, 840);
            this.cardUltimas.Size = new System.Drawing.Size(620, 300);
            this.cardUltimas.Titulo = "Últimas vendas";
            this.cardUltimas.Subtitulo = "As 6 vendas mais recentes";
            this.cardUltimas.Padding = new System.Windows.Forms.Padding(12, 66, 12, 12);
            this.cardUltimas.Name = "cardUltimas";
            this.cardUltimas.TabIndex = 8;
            // 
            // dgvUltimas
            // 
            this.dgvUltimas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUltimas.Name = "dgvUltimas";
            this.dgvUltimas.TabIndex = 0;
            // 
            // cardKits
            // 
            this.cardKits.Controls.Add(this.dgvKits);
            this.cardKits.Location = new System.Drawing.Point(636, 840);
            this.cardKits.Size = new System.Drawing.Size(412, 300);
            this.cardKits.Titulo = "Kits e combos";
            this.cardKits.Subtitulo = "Quantos dá para montar agora";
            this.cardKits.Padding = new System.Windows.Forms.Padding(12, 66, 12, 12);
            this.cardKits.Name = "cardKits";
            this.cardKits.TabIndex = 9;
            // 
            // dgvKits
            // 
            this.dgvKits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKits.Name = "dgvKits";
            this.dgvKits.TabIndex = 0;
            // 
            // UcDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 680);
            this.AutoScroll = true;
            this.Controls.Add(this.kpiFaturamento);
            this.Controls.Add(this.kpiVendas);
            this.Controls.Add(this.kpiTicket);
            this.Controls.Add(this.kpiEstoque);
            this.Controls.Add(this.cardMensal);
            this.Controls.Add(this.cardPagamentos);
            this.Controls.Add(this.cardOferta);
            this.Controls.Add(this.cardTopProdutos);
            this.Controls.Add(this.cardUltimas);
            this.Controls.Add(this.cardKits);
            this.Name = "UcDashboard";
            this.Resize += new System.EventHandler(this.UcDashboard_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKits)).EndInit();
            this.cardKits.ResumeLayout(false);
            this.cardKits.PerformLayout();
            this.cardUltimas.ResumeLayout(false);
            this.cardUltimas.PerformLayout();
            this.cardTopProdutos.ResumeLayout(false);
            this.cardTopProdutos.PerformLayout();
            this.flpOfertaProdutos.ResumeLayout(false);
            this.flpOfertaProdutos.PerformLayout();
            this.pnlOfertaConteudo.ResumeLayout(false);
            this.pnlOfertaConteudo.PerformLayout();
            this.cardOferta.ResumeLayout(false);
            this.cardOferta.PerformLayout();
            this.cardPagamentos.ResumeLayout(false);
            this.cardPagamentos.PerformLayout();
            this.cardMensal.ResumeLayout(false);
            this.cardMensal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Lapapie.Controles.CartaoIndicador kpiFaturamento;
        private Lapapie.Controles.CartaoIndicador kpiVendas;
        private Lapapie.Controles.CartaoIndicador kpiTicket;
        private Lapapie.Controles.CartaoIndicador kpiEstoque;
        private Lapapie.Controles.PainelCartao cardMensal;
        private Lapapie.Controles.GraficoBarras graficoMensal;
        private Lapapie.Controles.PainelCartao cardPagamentos;
        private Lapapie.Controles.GraficoRosca graficoPagamentos;
        private Lapapie.Controles.PainelCartao cardOferta;
        private System.Windows.Forms.Panel pnlOfertaConteudo;
        private Lapapie.Controles.FaixaOferta faixaOferta;
        private System.Windows.Forms.Label lblOfertaNome;
        private System.Windows.Forms.Label lblOfertaDescricao;
        private System.Windows.Forms.Label lblOfertaValor;
        private System.Windows.Forms.Label lblOfertaPeriodo;
        private Lapapie.Controles.BotaoPill btnVerPromocao;
        private System.Windows.Forms.FlowLayoutPanel flpOfertaProdutos;
        private Lapapie.Controles.PainelCartao cardTopProdutos;
        private Lapapie.Controles.GraficoBarras graficoTop;
        private Lapapie.Controles.PainelCartao cardUltimas;
        private System.Windows.Forms.DataGridView dgvUltimas;
        private Lapapie.Controles.PainelCartao cardKits;
        private System.Windows.Forms.DataGridView dgvKits;
    }
}
