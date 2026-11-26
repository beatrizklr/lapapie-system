namespace Lapapie.Forms.Cadastros
{
    partial class UcProdutos
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
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.abasTipo = new Lapapie.Controles.AbasPill();
            this.abasVisualizacao = new Lapapie.Controles.AbasPill();
            this.chkInativos = new System.Windows.Forms.CheckBox();
            this.btnNovo = new Lapapie.Controles.BotaoPill();
            this.btnEditar = new Lapapie.Controles.BotaoPill();
            this.btnExcluir = new Lapapie.Controles.BotaoPill();
            this.btnExportar = new Lapapie.Controles.BotaoPill();
            this.flpVitrine = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.cardGrid.SuspendLayout();
            this.pnlFerramentas.SuspendLayout();
            this.flpVitrine.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGrid
            // 
            this.cardGrid.Controls.Add(this.dgvProdutos);
            this.cardGrid.Controls.Add(this.flpVitrine);
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
            this.pnlFerramentas.Controls.Add(this.abasTipo);
            this.pnlFerramentas.Controls.Add(this.abasVisualizacao);
            this.pnlFerramentas.Controls.Add(this.chkInativos);
            this.pnlFerramentas.Controls.Add(this.btnNovo);
            this.pnlFerramentas.Controls.Add(this.btnEditar);
            this.pnlFerramentas.Controls.Add(this.btnExcluir);
            this.pnlFerramentas.Controls.Add(this.btnExportar);
            this.pnlFerramentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFerramentas.Size = new System.Drawing.Size(1050, 108);
            this.pnlFerramentas.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlFerramentas.Name = "pnlFerramentas";
            this.pnlFerramentas.TabIndex = 2;
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.TabIndex = 0;
            this.dgvProdutos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            this.dgvProdutos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.dgvProdutos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProdutos_CellFormatting);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 6);
            this.txtPesquisa.Size = new System.Drawing.Size(340, 42);
            this.txtPesquisa.Placeholder = "Nome, marca ou código de barras";
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
            // abasTipo
            // 
            this.abasTipo.Location = new System.Drawing.Point(0, 60);
            this.abasTipo.Size = new System.Drawing.Size(300, 36);
            this.abasTipo.Opcoes = "Todos|Simples|Kits";
            this.abasTipo.IndiceSelecionado = 0;
            this.abasTipo.Name = "abasTipo";
            this.abasTipo.TabIndex = 2;
            this.abasTipo.SelecaoAlterada += new System.EventHandler(this.Filtro_Alterado);
            // 
            // abasVisualizacao
            // 
            this.abasVisualizacao.Location = new System.Drawing.Point(316, 60);
            this.abasVisualizacao.Size = new System.Drawing.Size(200, 36);
            this.abasVisualizacao.Opcoes = "Lista|Vitrine";
            this.abasVisualizacao.IndiceSelecionado = 0;
            this.abasVisualizacao.Name = "abasVisualizacao";
            this.abasVisualizacao.TabIndex = 3;
            this.abasVisualizacao.SelecaoAlterada += new System.EventHandler(this.abasVisualizacao_SelecaoAlterada);
            // 
            // chkInativos
            // 
            this.chkInativos.AutoSize = true;
            this.chkInativos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkInativos.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.chkInativos.Location = new System.Drawing.Point(536, 67);
            this.chkInativos.Text = "Mostrar inativos";
            this.chkInativos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkInativos.Name = "chkInativos";
            this.chkInativos.TabIndex = 4;
            this.chkInativos.CheckedChanged += new System.EventHandler(this.Filtro_Alterado);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.Location = new System.Drawing.Point(932, 7);
            this.btnNovo.Size = new System.Drawing.Size(118, 40);
            this.btnNovo.Text = "Novo";
            this.btnNovo.Estilo = Lapapie.Controles.EstiloBotao.Primario;
            this.btnNovo.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.TabIndex = 5;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.Location = new System.Drawing.Point(820, 7);
            this.btnEditar.Size = new System.Drawing.Size(104, 40);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnEditar.Icone = Lapapie.Tema.Icones.Editar;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluir.Location = new System.Drawing.Point(704, 7);
            this.btnExcluir.Size = new System.Drawing.Size(108, 40);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnExcluir.Icone = Lapapie.Tema.Icones.Excluir;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.TabIndex = 7;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(578, 7);
            this.btnExportar.Size = new System.Drawing.Size(118, 40);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnExportar.Icone = Lapapie.Tema.Icones.Exportar;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // flpVitrine
            // 
            this.flpVitrine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpVitrine.AutoScroll = true;
            this.flpVitrine.Visible = false;
            this.flpVitrine.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.flpVitrine.Name = "flpVitrine";
            this.flpVitrine.TabIndex = 1;
            // 
            // UcProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.pnlFerramentas);
            this.Name = "UcProdutos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.flpVitrine.ResumeLayout(false);
            this.flpVitrine.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvProdutos;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.ComboBox cboCategoria;
        private Lapapie.Controles.AbasPill abasTipo;
        private Lapapie.Controles.AbasPill abasVisualizacao;
        private System.Windows.Forms.CheckBox chkInativos;
        private Lapapie.Controles.BotaoPill btnNovo;
        private Lapapie.Controles.BotaoPill btnEditar;
        private Lapapie.Controles.BotaoPill btnExcluir;
        private Lapapie.Controles.BotaoPill btnExportar;
        private System.Windows.Forms.FlowLayoutPanel flpVitrine;
    }
}
