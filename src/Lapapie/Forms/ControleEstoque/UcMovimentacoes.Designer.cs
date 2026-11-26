namespace Lapapie.Forms.ControleEstoque
{
    partial class UcMovimentacoes
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
            this.dgvMovimentacoes = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblAte = new System.Windows.Forms.Label();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.abasTipo = new Lapapie.Controles.AbasPill();
            this.cboMotivo = new System.Windows.Forms.ComboBox();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvMovimentacoes);
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
            this.pnlFerramentas.Controls.Add(this.lblPeriodo);
            this.pnlFerramentas.Controls.Add(this.dtpInicio);
            this.pnlFerramentas.Controls.Add(this.lblAte);
            this.pnlFerramentas.Controls.Add(this.dtpFim);
            this.pnlFerramentas.Controls.Add(this.abasTipo);
            this.pnlFerramentas.Controls.Add(this.cboMotivo);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 2;
            // 
            // dgvMovimentacoes
            // 
            this.dgvMovimentacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovimentacoes.Name = "dgvMovimentacoes";
            this.dgvMovimentacoes.TabIndex = 0;
            this.dgvMovimentacoes.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Produto, código ou observação";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPeriodo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblPeriodo.Location = new System.Drawing.Point(354, 18);
            this.lblPeriodo.Text = "De";
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.TabIndex = 1;
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
            // abasTipo
            // 
            this.abasTipo.Location = new System.Drawing.Point(0, 60);
            this.abasTipo.Size = new System.Drawing.Size(330, 36);
            this.abasTipo.Opcoes = "Todas|Entradas|Saídas";
            this.abasTipo.IndiceSelecionado = 0;
            this.abasTipo.Name = "abasTipo";
            this.abasTipo.TabIndex = 5;
            this.abasTipo.SelecaoAlterada += new System.EventHandler(this.Filtro_Alterado);
            // 
            // cboMotivo
            // 
            this.cboMotivo.Location = new System.Drawing.Point(352, 64);
            this.cboMotivo.Size = new System.Drawing.Size(220, 29);
            this.cboMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMotivo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboMotivo.Name = "cboMotivo";
            this.cboMotivo.TabIndex = 6;
            this.cboMotivo.SelectedIndexChanged += new System.EventHandler(this.Filtro_Alterado);
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
            this.btnExportar.TabIndex = 7;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // UcMovimentacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcMovimentacoes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvMovimentacoes;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblAte;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private Lapapie.Controles.AbasPill abasTipo;
        private System.Windows.Forms.ComboBox cboMotivo;
        private Lapapie.Controles.BotaoPill btnExportar;
    }
}
