namespace Lapapie.Forms.Vendas
{
    partial class UcNovaVenda
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
            this.flpProdutos = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlVitrineTopo = new System.Windows.Forms.Panel();
            this.pnlCarrinho = new System.Windows.Forms.Panel();
            this.txtPesquisa = new Lapapie.Controles.CaixaPesquisa();
            this.lblDicaAtalhos = new System.Windows.Forms.Label();
            this.abasCategorias = new Lapapie.Controles.AbasPill();
            this.cardCarrinho = new Lapapie.Controles.PainelCartao();
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.pnlItemAcoes = new System.Windows.Forms.Panel();
            this.pnlTotais = new System.Windows.Forms.Panel();
            this.pnlCarrinhoTopo = new System.Windows.Forms.Panel();
            this.lblCarrinhoTitulo = new System.Windows.Forms.Label();
            this.lblQtdItens = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.btnNovoCliente = new Lapapie.Controles.BotaoPill();
            this.btnMenos = new Lapapie.Controles.BotaoPill();
            this.btnMais = new Lapapie.Controles.BotaoPill();
            this.btnRemoverItem = new Lapapie.Controles.BotaoPill();
            this.btnLimpar = new Lapapie.Controles.BotaoPill();
            this.lblSubtotalRotulo = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDescontoRotulo = new System.Windows.Forms.Label();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.lblTotalRotulo = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.abasPagamento = new Lapapie.Controles.AbasPill();
            this.pnlDinheiro = new System.Windows.Forms.Panel();
            this.txtRecebido = new Lapapie.Controles.CampoTexto();
            this.lblTroco = new System.Windows.Forms.Label();
            this.btnFinalizar = new Lapapie.Controles.BotaoPill();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            this.flpProdutos.SuspendLayout();
            this.pnlVitrineTopo.SuspendLayout();
            this.pnlCarrinho.SuspendLayout();
            this.cardCarrinho.SuspendLayout();
            this.pnlItemAcoes.SuspendLayout();
            this.pnlTotais.SuspendLayout();
            this.pnlCarrinhoTopo.SuspendLayout();
            this.pnlDinheiro.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpProdutos
            // 
            this.flpProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProdutos.AutoScroll = true;
            this.flpProdutos.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.flpProdutos.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flpProdutos.Name = "flpProdutos";
            this.flpProdutos.TabIndex = 0;
            // 
            // pnlVitrineTopo
            // 
            this.pnlVitrineTopo.Controls.Add(this.txtPesquisa);
            this.pnlVitrineTopo.Controls.Add(this.lblDicaAtalhos);
            this.pnlVitrineTopo.Controls.Add(this.abasCategorias);
            this.pnlVitrineTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVitrineTopo.Size = new System.Drawing.Size(640, 100);
            this.pnlVitrineTopo.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlVitrineTopo.Name = "pnlVitrineTopo";
            this.pnlVitrineTopo.TabIndex = 1;
            // 
            // pnlCarrinho
            // 
            this.pnlCarrinho.Controls.Add(this.cardCarrinho);
            this.pnlCarrinho.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCarrinho.Size = new System.Drawing.Size(410, 680);
            this.pnlCarrinho.BackColor = System.Drawing.Color.FromArgb(243, 236, 226);
            this.pnlCarrinho.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.pnlCarrinho.Name = "pnlCarrinho";
            this.pnlCarrinho.TabIndex = 2;
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(0, 4);
            this.txtPesquisa.Size = new System.Drawing.Size(420, 42);
            this.txtPesquisa.Placeholder = "Buscar produto ou ler o código de barras";
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.Pesquisar += new System.EventHandler(this.txtPesquisa_Pesquisar);
            // 
            // lblDicaAtalhos
            // 
            this.lblDicaAtalhos.AutoSize = true;
            this.lblDicaAtalhos.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDicaAtalhos.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDicaAtalhos.Location = new System.Drawing.Point(434, 16);
            this.lblDicaAtalhos.Text = "F3 buscar  ·  F9 finalizar";
            this.lblDicaAtalhos.Name = "lblDicaAtalhos";
            this.lblDicaAtalhos.TabIndex = 1;
            // 
            // abasCategorias
            // 
            this.abasCategorias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.abasCategorias.Location = new System.Drawing.Point(0, 56);
            this.abasCategorias.Size = new System.Drawing.Size(624, 38);
            this.abasCategorias.Name = "abasCategorias";
            this.abasCategorias.TabIndex = 2;
            this.abasCategorias.SelecaoAlterada += new System.EventHandler(this.abasCategorias_SelecaoAlterada);
            this.abasCategorias.Resize += new System.EventHandler(this.abasCategorias_Resize);
            // 
            // cardCarrinho
            // 
            this.cardCarrinho.Controls.Add(this.dgvCarrinho);
            this.cardCarrinho.Controls.Add(this.pnlItemAcoes);
            this.cardCarrinho.Controls.Add(this.pnlTotais);
            this.cardCarrinho.Controls.Add(this.pnlCarrinhoTopo);
            this.cardCarrinho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCarrinho.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.cardCarrinho.Name = "cardCarrinho";
            this.cardCarrinho.TabIndex = 0;
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.TabIndex = 0;
            this.dgvCarrinho.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarrinho_CellDoubleClick);
            // 
            // pnlItemAcoes
            // 
            this.pnlItemAcoes.Controls.Add(this.btnMenos);
            this.pnlItemAcoes.Controls.Add(this.btnMais);
            this.pnlItemAcoes.Controls.Add(this.btnRemoverItem);
            this.pnlItemAcoes.Controls.Add(this.btnLimpar);
            this.pnlItemAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlItemAcoes.Size = new System.Drawing.Size(358, 46);
            this.pnlItemAcoes.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlItemAcoes.Name = "pnlItemAcoes";
            this.pnlItemAcoes.TabIndex = 1;
            // 
            // pnlTotais
            // 
            this.pnlTotais.Controls.Add(this.lblSubtotalRotulo);
            this.pnlTotais.Controls.Add(this.lblSubtotal);
            this.pnlTotais.Controls.Add(this.lblDescontoRotulo);
            this.pnlTotais.Controls.Add(this.lblDesconto);
            this.pnlTotais.Controls.Add(this.lblTotalRotulo);
            this.pnlTotais.Controls.Add(this.lblTotal);
            this.pnlTotais.Controls.Add(this.abasPagamento);
            this.pnlTotais.Controls.Add(this.pnlDinheiro);
            this.pnlTotais.Controls.Add(this.btnFinalizar);
            this.pnlTotais.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotais.Size = new System.Drawing.Size(358, 252);
            this.pnlTotais.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlTotais.Name = "pnlTotais";
            this.pnlTotais.TabIndex = 2;
            // 
            // pnlCarrinhoTopo
            // 
            this.pnlCarrinhoTopo.Controls.Add(this.lblCarrinhoTitulo);
            this.pnlCarrinhoTopo.Controls.Add(this.lblQtdItens);
            this.pnlCarrinhoTopo.Controls.Add(this.cboCliente);
            this.pnlCarrinhoTopo.Controls.Add(this.btnNovoCliente);
            this.pnlCarrinhoTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCarrinhoTopo.Size = new System.Drawing.Size(358, 84);
            this.pnlCarrinhoTopo.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlCarrinhoTopo.Name = "pnlCarrinhoTopo";
            this.pnlCarrinhoTopo.TabIndex = 3;
            // 
            // lblCarrinhoTitulo
            // 
            this.lblCarrinhoTitulo.AutoSize = true;
            this.lblCarrinhoTitulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCarrinhoTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblCarrinhoTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblCarrinhoTitulo.Text = "CARRINHO";
            this.lblCarrinhoTitulo.Name = "lblCarrinhoTitulo";
            this.lblCarrinhoTitulo.TabIndex = 0;
            // 
            // lblQtdItens
            // 
            this.lblQtdItens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQtdItens.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblQtdItens.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblQtdItens.Location = new System.Drawing.Point(178, 4);
            this.lblQtdItens.Size = new System.Drawing.Size(180, 22);
            this.lblQtdItens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblQtdItens.Text = "vazio";
            this.lblQtdItens.Name = "lblQtdItens";
            this.lblQtdItens.TabIndex = 1;
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.Location = new System.Drawing.Point(0, 42);
            this.cboCliente.Size = new System.Drawing.Size(312, 29);
            this.cboCliente.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.TabIndex = 2;
            // 
            // btnNovoCliente
            // 
            this.btnNovoCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovoCliente.Location = new System.Drawing.Point(320, 38);
            this.btnNovoCliente.Size = new System.Drawing.Size(38, 36);
            this.btnNovoCliente.Text = "";
            this.btnNovoCliente.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnNovoCliente.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnNovoCliente.Name = "btnNovoCliente";
            this.btnNovoCliente.TabIndex = 3;
            this.btnNovoCliente.Click += new System.EventHandler(this.btnNovoCliente_Click);
            // 
            // btnMenos
            // 
            this.btnMenos.Location = new System.Drawing.Point(0, 6);
            this.btnMenos.Size = new System.Drawing.Size(40, 36);
            this.btnMenos.Text = "";
            this.btnMenos.Icone = Lapapie.Tema.Icones.Remover;
            this.btnMenos.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnMenos.Name = "btnMenos";
            this.btnMenos.TabIndex = 0;
            this.btnMenos.Click += new System.EventHandler(this.btnMenos_Click);
            // 
            // btnMais
            // 
            this.btnMais.Location = new System.Drawing.Point(46, 6);
            this.btnMais.Size = new System.Drawing.Size(40, 36);
            this.btnMais.Text = "";
            this.btnMais.Icone = Lapapie.Tema.Icones.Adicionar;
            this.btnMais.Estilo = Lapapie.Controles.EstiloBotao.Suave;
            this.btnMais.Name = "btnMais";
            this.btnMais.TabIndex = 1;
            this.btnMais.Click += new System.EventHandler(this.btnMais_Click);
            // 
            // btnRemoverItem
            // 
            this.btnRemoverItem.Location = new System.Drawing.Point(92, 6);
            this.btnRemoverItem.Size = new System.Drawing.Size(110, 36);
            this.btnRemoverItem.Text = "Remover";
            this.btnRemoverItem.Estilo = Lapapie.Controles.EstiloBotao.Perigo;
            this.btnRemoverItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.TabIndex = 2;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpar.Location = new System.Drawing.Point(258, 6);
            this.btnLimpar.Size = new System.Drawing.Size(100, 36);
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.Estilo = Lapapie.Controles.EstiloBotao.Link;
            this.btnLimpar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.TabIndex = 3;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // lblSubtotalRotulo
            // 
            this.lblSubtotalRotulo.AutoSize = true;
            this.lblSubtotalRotulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotalRotulo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblSubtotalRotulo.Location = new System.Drawing.Point(0, 8);
            this.lblSubtotalRotulo.Text = "Subtotal";
            this.lblSubtotalRotulo.Name = "lblSubtotalRotulo";
            this.lblSubtotalRotulo.TabIndex = 0;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblSubtotal.Location = new System.Drawing.Point(178, 6);
            this.lblSubtotal.Size = new System.Drawing.Size(180, 24);
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSubtotal.Text = "R$ 0,00";
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.TabIndex = 1;
            // 
            // lblDescontoRotulo
            // 
            this.lblDescontoRotulo.AutoSize = true;
            this.lblDescontoRotulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDescontoRotulo.ForeColor = System.Drawing.Color.FromArgb(107, 110, 117);
            this.lblDescontoRotulo.Location = new System.Drawing.Point(0, 32);
            this.lblDescontoRotulo.Text = "Descontos de promoção";
            this.lblDescontoRotulo.Name = "lblDescontoRotulo";
            this.lblDescontoRotulo.TabIndex = 2;
            // 
            // lblDesconto
            // 
            this.lblDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesconto.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDesconto.ForeColor = System.Drawing.Color.FromArgb(92, 105, 82);
            this.lblDesconto.Location = new System.Drawing.Point(178, 30);
            this.lblDesconto.Size = new System.Drawing.Size(180, 24);
            this.lblDesconto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDesconto.Text = "− R$ 0,00";
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.TabIndex = 3;
            // 
            // lblTotalRotulo
            // 
            this.lblTotalRotulo.AutoSize = true;
            this.lblTotalRotulo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotalRotulo.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTotalRotulo.Location = new System.Drawing.Point(0, 70);
            this.lblTotalRotulo.Text = "TOTAL";
            this.lblTotalRotulo.Name = "lblTotalRotulo";
            this.lblTotalRotulo.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Bahnschrift SemiBold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTotal.Location = new System.Drawing.Point(118, 56);
            this.lblTotal.Size = new System.Drawing.Size(240, 44);
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Text = "R$ 0,00";
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 5;
            // 
            // abasPagamento
            // 
            this.abasPagamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.abasPagamento.Location = new System.Drawing.Point(0, 106);
            this.abasPagamento.Size = new System.Drawing.Size(358, 36);
            this.abasPagamento.Opcoes = "Dinheiro|PIX|Débito|Crédito";
            this.abasPagamento.IndiceSelecionado = 1;
            this.abasPagamento.Name = "abasPagamento";
            this.abasPagamento.TabIndex = 6;
            this.abasPagamento.SelecaoAlterada += new System.EventHandler(this.abasPagamento_SelecaoAlterada);
            // 
            // pnlDinheiro
            // 
            this.pnlDinheiro.Controls.Add(this.txtRecebido);
            this.pnlDinheiro.Controls.Add(this.lblTroco);
            this.pnlDinheiro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDinheiro.Location = new System.Drawing.Point(0, 146);
            this.pnlDinheiro.Size = new System.Drawing.Size(358, 46);
            this.pnlDinheiro.BackColor = System.Drawing.Color.FromArgb(255, 253, 249);
            this.pnlDinheiro.Visible = false;
            this.pnlDinheiro.Name = "pnlDinheiro";
            this.pnlDinheiro.TabIndex = 7;
            // 
            // txtRecebido
            // 
            this.txtRecebido.Location = new System.Drawing.Point(0, 2);
            this.txtRecebido.Size = new System.Drawing.Size(150, 40);
            this.txtRecebido.Formato = Lapapie.Controles.FormatoCampo.Moeda;
            this.txtRecebido.Placeholder = "Valor recebido";
            this.txtRecebido.Name = "txtRecebido";
            this.txtRecebido.TabIndex = 0;
            this.txtRecebido.TextoAlterado += new System.EventHandler(this.txtRecebido_TextoAlterado);
            // 
            // lblTroco
            // 
            this.lblTroco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTroco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTroco.ForeColor = System.Drawing.Color.FromArgb(51, 54, 61);
            this.lblTroco.Location = new System.Drawing.Point(160, 2);
            this.lblTroco.Size = new System.Drawing.Size(198, 40);
            this.lblTroco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTroco.Text = "";
            this.lblTroco.Name = "lblTroco";
            this.lblTroco.TabIndex = 1;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizar.Location = new System.Drawing.Point(0, 198);
            this.btnFinalizar.Size = new System.Drawing.Size(358, 50);
            this.btnFinalizar.Text = "Finalizar venda";
            this.btnFinalizar.MostrarSeta = true;
            this.btnFinalizar.Estilo = Lapapie.Controles.EstiloBotao.Destaque;
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.TabIndex = 8;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // UcNovaVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1050, 680);
            this.Controls.Add(this.flpProdutos);
            this.Controls.Add(this.pnlVitrineTopo);
            this.Controls.Add(this.pnlCarrinho);
            this.Name = "UcNovaVenda";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            this.pnlDinheiro.ResumeLayout(false);
            this.pnlDinheiro.PerformLayout();
            this.pnlCarrinhoTopo.ResumeLayout(false);
            this.pnlCarrinhoTopo.PerformLayout();
            this.pnlTotais.ResumeLayout(false);
            this.pnlTotais.PerformLayout();
            this.pnlItemAcoes.ResumeLayout(false);
            this.pnlItemAcoes.PerformLayout();
            this.cardCarrinho.ResumeLayout(false);
            this.cardCarrinho.PerformLayout();
            this.pnlCarrinho.ResumeLayout(false);
            this.pnlCarrinho.PerformLayout();
            this.pnlVitrineTopo.ResumeLayout(false);
            this.pnlVitrineTopo.PerformLayout();
            this.flpProdutos.ResumeLayout(false);
            this.flpProdutos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpProdutos;
        private System.Windows.Forms.Panel pnlVitrineTopo;
        private System.Windows.Forms.Panel pnlCarrinho;
        private Lapapie.Controles.CaixaPesquisa txtPesquisa;
        private System.Windows.Forms.Label lblDicaAtalhos;
        private Lapapie.Controles.AbasPill abasCategorias;
        private Lapapie.Controles.PainelCartao cardCarrinho;
        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.Panel pnlItemAcoes;
        private System.Windows.Forms.Panel pnlTotais;
        private System.Windows.Forms.Panel pnlCarrinhoTopo;
        private System.Windows.Forms.Label lblCarrinhoTitulo;
        private System.Windows.Forms.Label lblQtdItens;
        private System.Windows.Forms.ComboBox cboCliente;
        private Lapapie.Controles.BotaoPill btnNovoCliente;
        private Lapapie.Controles.BotaoPill btnMenos;
        private Lapapie.Controles.BotaoPill btnMais;
        private Lapapie.Controles.BotaoPill btnRemoverItem;
        private Lapapie.Controles.BotaoPill btnLimpar;
        private System.Windows.Forms.Label lblSubtotalRotulo;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDescontoRotulo;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.Label lblTotalRotulo;
        private System.Windows.Forms.Label lblTotal;
        private Lapapie.Controles.AbasPill abasPagamento;
        private System.Windows.Forms.Panel pnlDinheiro;
        private Lapapie.Controles.CampoTexto txtRecebido;
        private System.Windows.Forms.Label lblTroco;
        private Lapapie.Controles.BotaoPill btnFinalizar;
    }
}
