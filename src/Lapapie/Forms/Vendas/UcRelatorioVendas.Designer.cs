namespace Lapapie.Forms.Vendas
{
    partial class UcRelatorioVendas
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
            this.pnlRolagem = new System.Windows.Forms.Panel();
            this.pnlFerramentas = new System.Windows.Forms.Panel();
            this.abasPeriodo = new Lapapie.Controles.AbasPill();
            this.lblDe = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblAte = new System.Windows.Forms.Label();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.cboVendedor = new System.Windows.Forms.ComboBox();
            this.btnExcel = new Lapapie.Controles.BotaoPill();
            this.btnPdf = new Lapapie.Controles.BotaoPill();
            this.kpiFaturamento = new Lapapie.Controles.CartaoIndicador();
            this.kpiVendas = new Lapapie.Controles.CartaoIndicador();
            this.kpiTicket = new Lapapie.Controles.CartaoIndicador();
            this.kpiItens = new Lapapie.Controles.CartaoIndicador();
            this.kpiDescontos = new Lapapie.Controles.CartaoIndicador();
            this.cardEvolucao = new Lapapie.Controles.PainelCartao();
            this.graficoDiario = new Lapapie.Controles.GraficoLinha();
            this.cardCategorias = new Lapapie.Controles.PainelCartao();
            this.graficoCategorias = new Lapapie.Controles.GraficoBarras();
            this.cardPagamentos = new Lapapie.Controles.PainelCartao();
            this.graficoPagamentos = new Lapapie.Controles.GraficoRosca();
            this.cardVendedores = new Lapapie.Controles.PainelCartao();
            this.graficoVendedores = new Lapapie.Controles.GraficoBarras();
            this.cardProdutos = new Lapapie.Controles.PainelCartao();
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.pnlRolagem.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.cardEvolucao.SuspendLayout();
            this.cardCategorias.SuspendLayout();
            this.cardPagamentos.SuspendLayout();
            this.cardVendedores.SuspendLayout();
            this.cardProdutos.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRolagem
            // 
            this.pnlRolagem.Controls.Add(this.kpiFaturamento);
            this.pnlRolagem.Controls.Add(this.kpiVendas);
            this.pnlRolagem.Controls.Add(this.kpiTicket);
            this.pnlRolagem.Controls.Add(this.kpiItens);
            this.pnlRolagem.Controls.Add(this.kpiDescontos);
            this.pnlRolagem.Controls.Add(this.cardEvolucao);
            this.pnlRolagem.Controls.Add(this.cardCategorias);
            this.pnlRolagem.Controls.Add(this.cardPagamentos);
            this.pnlRolagem.Controls.Add(this.cardVendedores);
            this.pnlRolagem.Controls.Add(this.cardProdutos);
            this.pnlRolagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRolagem.AutoScroll = true;
            this.pnlRolagem.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlRolagem.Name = "pnlRolagem";
            this.pnlRolagem.TabIndex = 0;
            this.pnlRolagem.Resize += new System.EventHandler(this.pnlRolagem_Resize);
            // 
            // pnlFerramentas
            // 
            this.pnlFerramentas.Controls.Add(this.abasPeriodo);
            this.pnlFerramentas.Controls.Add(this.lblDe);
            this.pnlFerramentas.Controls.Add(this.dtpInicio);
            this.pnlFerramentas.Controls.Add(this.lblAte);
            this.pnlFerramentas.Controls.Add(this.dtpFim);
            this.pnlFerramentas.Controls.Add(this.lblVendedor);
            this.pnlFerramentas.Controls.Add(this.cboVendedor);
            this.pnlFerramentas.Controls.Add(this.btnExcel);
            this.pnlFerramentas.Controls.Add(this.btnPdf);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 1;
            // 
            // abasPeriodo
            // 
            this.abasPeriodo.Location = new System.Drawing.Point(0, 6);
            this.abasPeriodo.Size = new System.Drawing.Size(620, 36);
            this.abasPeriodo.Opcoes = "7 dias|30 dias|Este mês|Mês anterior|90 dias|Personalizado";
            this.abasPeriodo.IndiceSelecionado = 1;
            this.abasPeriodo.Name = "abasPeriodo";
            this.abasPeriodo.TabIndex = 0;
            this.abasPeriodo.SelecaoAlterada += new System.EventHandler(this.abasPeriodo_SelecaoAlterada);
            // 
            // lblDe
            // 
            this.lblDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDe.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDe.Location = new System.Drawing.Point(2, 70);
            this.lblDe.Text = "De";
            this.lblDe.AutoSize = true;
            this.lblDe.Name = "lblDe";
            this.lblDe.TabIndex = 1;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(26, 64);
            this.dtpInicio.Size = new System.Drawing.Size(130, 29);
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.TabIndex = 2;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.Datas_Alteradas);
            // 
            // lblAte
            // 
            this.lblAte.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAte.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblAte.Location = new System.Drawing.Point(166, 70);
            this.lblAte.Text = "até";
            this.lblAte.AutoSize = true;
            this.lblAte.Name = "lblAte";
            this.lblAte.TabIndex = 3;
            // 
            // dtpFim
            // 
            this.dtpFim.Location = new System.Drawing.Point(198, 64);
            this.dtpFim.Size = new System.Drawing.Size(130, 29);
            this.dtpFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFim.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.TabIndex = 4;
            this.dtpFim.ValueChanged += new System.EventHandler(this.Datas_Alteradas);
            // 
            // lblVendedor
            // 
            this.lblVendedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblVendedor.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblVendedor.Location = new System.Drawing.Point(348, 70);
            this.lblVendedor.Text = "Vendedor";
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.TabIndex = 5;
            // 
            // cboVendedor
            // 
            this.cboVendedor.Location = new System.Drawing.Point(418, 64);
            this.cboVendedor.Size = new System.Drawing.Size(200, 29);
            this.cboVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVendedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboVendedor.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboVendedor.Name = "cboVendedor";
            this.cboVendedor.TabIndex = 6;
            this.cboVendedor.SelectedIndexChanged += new System.EventHandler(this.Datas_Alteradas);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(814, 4);
            this.btnExcel.Size = new System.Drawing.Size(110, 40);
            this.btnExcel.Text = "Excel";
            this.btnExcel.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExcel.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPdf
            // 
            this.btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf.Location = new System.Drawing.Point(932, 4);
            this.btnPdf.Size = new System.Drawing.Size(118, 40);
            this.btnPdf.Text = "PDF";
            this.btnPdf.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnPdf.Icone = Lapapie.Tema.Icones.Imprimir;
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.TabIndex = 8;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // kpiFaturamento
            // 
            this.kpiFaturamento.Location = new System.Drawing.Point(0, 0);
            this.kpiFaturamento.Size = new System.Drawing.Size(198, 132);
            this.kpiFaturamento.Titulo = "Faturamento";
            this.kpiFaturamento.Icone = Lapapie.Tema.Icones.Cartao;
            this.kpiFaturamento.CorDestaque = System.Drawing.Color.FromArgb(201, 139, 123);
            this.kpiFaturamento.Name = "kpiFaturamento";
            this.kpiFaturamento.TabIndex = 0;
            // 
            // kpiVendas
            // 
            this.kpiVendas.Location = new System.Drawing.Point(212, 0);
            this.kpiVendas.Size = new System.Drawing.Size(198, 132);
            this.kpiVendas.Titulo = "Vendas";
            this.kpiVendas.Icone = Lapapie.Tema.Icones.Carrinho;
            this.kpiVendas.CorDestaque = System.Drawing.Color.FromArgb(51, 54, 61);
            this.kpiVendas.Name = "kpiVendas";
            this.kpiVendas.TabIndex = 1;
            // 
            // kpiTicket
            // 
            this.kpiTicket.Location = new System.Drawing.Point(424, 0);
            this.kpiTicket.Size = new System.Drawing.Size(198, 132);
            this.kpiTicket.Titulo = "Ticket médio";
            this.kpiTicket.Icone = Lapapie.Tema.Icones.Etiqueta;
            this.kpiTicket.CorDestaque = System.Drawing.Color.FromArgb(142, 155, 131);
            this.kpiTicket.Name = "kpiTicket";
            this.kpiTicket.TabIndex = 2;
            // 
            // kpiItens
            // 
            this.kpiItens.Location = new System.Drawing.Point(636, 0);
            this.kpiItens.Size = new System.Drawing.Size(198, 132);
            this.kpiItens.Titulo = "Itens vendidos";
            this.kpiItens.Icone = Lapapie.Tema.Icones.Pacote;
            this.kpiItens.CorDestaque = System.Drawing.Color.FromArgb(184, 149, 90);
            this.kpiItens.Name = "kpiItens";
            this.kpiItens.TabIndex = 3;
            // 
            // kpiDescontos
            // 
            this.kpiDescontos.Location = new System.Drawing.Point(848, 0);
            this.kpiDescontos.Size = new System.Drawing.Size(198, 132);
            this.kpiDescontos.Titulo = "Descontos";
            this.kpiDescontos.Icone = Lapapie.Tema.Icones.Cartao;
            this.kpiDescontos.CorDestaque = System.Drawing.Color.FromArgb(201, 139, 123);
            this.kpiDescontos.Name = "kpiDescontos";
            this.kpiDescontos.TabIndex = 4;
            // 
            // cardEvolucao
            // 
            this.cardEvolucao.Controls.Add(this.graficoDiario);
            this.cardEvolucao.Location = new System.Drawing.Point(0, 148);
            this.cardEvolucao.Size = new System.Drawing.Size(1030, 300);
            this.cardEvolucao.Titulo = "Faturamento por dia";
            this.cardEvolucao.Subtitulo = "Vendas concluídas no período";
            this.cardEvolucao.Padding = new System.Windows.Forms.Padding(12, 64, 12, 12);
            this.cardEvolucao.Name = "cardEvolucao";
            this.cardEvolucao.TabIndex = 5;
            // 
            // graficoDiario
            // 
            this.graficoDiario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoDiario.Name = "graficoDiario";
            this.graficoDiario.TabIndex = 0;
            // 
            // cardCategorias
            // 
            this.cardCategorias.Controls.Add(this.graficoCategorias);
            this.cardCategorias.Location = new System.Drawing.Point(0, 464);
            this.cardCategorias.Size = new System.Drawing.Size(507, 320);
            this.cardCategorias.Titulo = "Por categoria";
            this.cardCategorias.Subtitulo = "Faturamento dos itens";
            this.cardCategorias.Padding = new System.Windows.Forms.Padding(20, 66, 20, 12);
            this.cardCategorias.Name = "cardCategorias";
            this.cardCategorias.TabIndex = 6;
            // 
            // graficoCategorias
            // 
            this.graficoCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoCategorias.Horizontal = true;
            this.graficoCategorias.Name = "graficoCategorias";
            this.graficoCategorias.TabIndex = 0;
            // 
            // cardPagamentos
            // 
            this.cardPagamentos.Controls.Add(this.graficoPagamentos);
            this.cardPagamentos.Location = new System.Drawing.Point(523, 464);
            this.cardPagamentos.Size = new System.Drawing.Size(507, 320);
            this.cardPagamentos.Titulo = "Formas de pagamento";
            this.cardPagamentos.Subtitulo = "Participação no faturamento";
            this.cardPagamentos.Padding = new System.Windows.Forms.Padding(20, 66, 20, 12);
            this.cardPagamentos.Name = "cardPagamentos";
            this.cardPagamentos.TabIndex = 7;
            // 
            // graficoPagamentos
            // 
            this.graficoPagamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoPagamentos.Name = "graficoPagamentos";
            this.graficoPagamentos.TabIndex = 0;
            // 
            // cardVendedores
            // 
            this.cardVendedores.Controls.Add(this.graficoVendedores);
            this.cardVendedores.Location = new System.Drawing.Point(0, 800);
            this.cardVendedores.Size = new System.Drawing.Size(410, 360);
            this.cardVendedores.Titulo = "Por vendedor";
            this.cardVendedores.Subtitulo = "Quem mais vendeu";
            this.cardVendedores.Padding = new System.Windows.Forms.Padding(20, 66, 20, 12);
            this.cardVendedores.Name = "cardVendedores";
            this.cardVendedores.TabIndex = 8;
            // 
            // graficoVendedores
            // 
            this.graficoVendedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoVendedores.Horizontal = true;
            this.graficoVendedores.Name = "graficoVendedores";
            this.graficoVendedores.TabIndex = 0;
            // 
            // cardProdutos
            // 
            this.cardProdutos.Controls.Add(this.dgvProdutos);
            this.cardProdutos.Location = new System.Drawing.Point(426, 800);
            this.cardProdutos.Size = new System.Drawing.Size(604, 360);
            this.cardProdutos.Titulo = "Produtos mais vendidos";
            this.cardProdutos.Subtitulo = "Ranking por quantidade";
            this.cardProdutos.Padding = new System.Windows.Forms.Padding(12, 66, 12, 12);
            this.cardProdutos.Name = "cardProdutos";
            this.cardProdutos.TabIndex = 9;
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.TabIndex = 0;
            // 
            // UcRelatorioVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 680);
            this.Controls.Add(this.pnlRolagem);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcRelatorioVendas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.cardProdutos.ResumeLayout(false);
            this.cardProdutos.PerformLayout();
            this.cardVendedores.ResumeLayout(false);
            this.cardVendedores.PerformLayout();
            this.cardPagamentos.ResumeLayout(false);
            this.cardPagamentos.PerformLayout();
            this.cardCategorias.ResumeLayout(false);
            this.cardCategorias.PerformLayout();
            this.cardEvolucao.ResumeLayout(false);
            this.cardEvolucao.PerformLayout();
            this.pnlFerramentas.ResumeLayout(false);
            this.pnlFerramentas.PerformLayout();
            this.pnlRolagem.ResumeLayout(false);
            this.pnlRolagem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlRolagem;
        private System.Windows.Forms.Panel pnlFerramentas;
        private Lapapie.Controles.AbasPill abasPeriodo;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblAte;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.ComboBox cboVendedor;
        private Lapapie.Controles.BotaoPill btnExcel;
        private Lapapie.Controles.BotaoPill btnPdf;
        private Lapapie.Controles.CartaoIndicador kpiFaturamento;
        private Lapapie.Controles.CartaoIndicador kpiVendas;
        private Lapapie.Controles.CartaoIndicador kpiTicket;
        private Lapapie.Controles.CartaoIndicador kpiItens;
        private Lapapie.Controles.CartaoIndicador kpiDescontos;
        private Lapapie.Controles.PainelCartao cardEvolucao;
        private Lapapie.Controles.GraficoLinha graficoDiario;
        private Lapapie.Controles.PainelCartao cardCategorias;
        private Lapapie.Controles.GraficoBarras graficoCategorias;
        private Lapapie.Controles.PainelCartao cardPagamentos;
        private Lapapie.Controles.GraficoRosca graficoPagamentos;
        private Lapapie.Controles.PainelCartao cardVendedores;
        private Lapapie.Controles.GraficoBarras graficoVendedores;
        private Lapapie.Controles.PainelCartao cardProdutos;
        private System.Windows.Forms.DataGridView dgvProdutos;
    }
}
