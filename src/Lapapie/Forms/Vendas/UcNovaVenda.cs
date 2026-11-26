using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Cadastros;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Relatorios;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Vendas
{
    /// <summary>
    /// Caixa (registro de venda): vitrine de produtos à esquerda e carrinho à direita.
    /// O total é recalculado a cada alteração; ao finalizar, a venda é gravada em
    /// transação e o estoque recebe baixa automática (ver VendaDAO.RegistrarVenda).
    /// </summary>
    public partial class UcNovaVenda : TelaBase
    {
        private readonly ProdutoDAO _produtoDAO = new();
        private readonly VendaDAO _vendaDAO = new();
        private List<Produto> _produtos = new();
        private readonly Dictionary<int, CartaoProduto> _cartoes = new();
        private Venda _venda = NovaVenda();
        /// <summary>Composição de cada kit (kitId → componentes), para conferir o estoque compartilhado.</summary>
        private Dictionary<int, List<KitItem>> _composicaoKits = new();
        /// <summary>Último filtro aplicado à vitrine (evita recriar os cartões sem necessidade).</summary>
        private (string Texto, int Categoria)? _filtroVitrine;

        private static Venda NovaVenda() => new() { UsuarioId = Sessao.UsuarioLogado?.Id ?? 0 };

        public UcNovaVenda()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Nova venda";
        public override string Subtitulo => "Clique nos produtos para montar o carrinho. Promoções vigentes entram sozinhas.";

        /// <summary>Usado pela janela principal para avisar antes de sair.</summary>
        public bool TemItens => _venda.Itens.Count > 0;

        public override bool PodeSair() =>
            !TemItens || Mensagem.Confirmar("Há uma venda em andamento e o carrinho será perdido. Sair mesmo assim?",
                                            "Venda em andamento", "Sair", "Continuar vendendo", perigoso: true);

        private void Configurar()
        {
            T.EstilizarGrid(dgvCarrinho);
            dgvCarrinho.ColumnHeadersHeight = 34;
            dgvCarrinho.RowTemplate.Height = 40;
            dgvCarrinho.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCarrinho.DefaultCellStyle.Font = T.Texto(9f);
            T.AdicionarColuna(dgvCarrinho, nameof(ItemVenda.ProdutoNome), "Produto");
            T.AdicionarColuna(dgvCarrinho, nameof(ItemVenda.Quantidade), "Qtd.", 48, T.FormatoInteiro, centro: true);
            T.AdicionarColuna(dgvCarrinho, nameof(ItemVenda.Subtotal), "Subtotal", 96, T.FormatoMoeda);
            dgvCarrinho.CellFormatting += DgvCarrinho_CellFormatting;
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                _produtos = _produtoDAO.Pesquisar(null, somenteAtivos: true, comPromocoes: true);
                _composicaoKits = _produtos.Where(p => p.EhKit).ToDictionary(p => p.Id, p => _produtoDAO.ListarComponentes(p.Id));

                var categorias = _produtos.GroupBy(p => (p.CategoriaId, p.CategoriaNome)).OrderBy(g => g.Key.CategoriaNome)
                                          .Select(g => (g.Key.CategoriaNome, (object)g.Key.CategoriaId)).ToList();
                categorias.Insert(0, ("Todos", 0));
                if (_produtos.Any(p => p.PromocaoVigente != null)) categorias.Insert(1, ("Em promoção", -1));
                abasCategorias.DefinirItens(categorias, 0);

                CarregarClientes();
                MontarVitrine(forcar: true);
                AtualizarCarrinho();
            });
            BeginInvoke(new Action(() => txtPesquisa.Focus()));
        }

        private void CarregarClientes()
        {
            var clientes = new ClienteDAO().Pesquisar(null, somenteAtivos: true);
            var itens = new List<KeyValuePair<int?, string>> { new(null, "Consumidor não identificado") };
            itens.AddRange(clientes.Select(c => new KeyValuePair<int?, string>(c.Id,
                string.IsNullOrEmpty(c.Cpf) ? c.Nome : $"{c.Nome} — {Formatacao.Cpf(c.Cpf)}")));
            cboCliente.DisplayMember = "Value";
            cboCliente.ValueMember = "Key";
            cboCliente.DataSource = itens;
            cboCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // ---------------- Vitrine ----------------

        /// <summary>Recria os cartões conforme a busca e a categoria selecionada.</summary>
        private void MontarVitrine(bool forcar = false)
        {
            string texto = txtPesquisa.Texto.Trim();
            int categoria = abasCategorias.ValorSelecionado is int c ? c : 0;
            if (!forcar && _filtroVitrine == (texto, categoria)) return;
            _filtroVitrine = (texto, categoria);
            var visiveis = _produtos.Where(p =>
                    (categoria == 0 || (categoria == -1 ? p.PromocaoVigente != null : p.CategoriaId == categoria)) &&
                    (texto.Length == 0 || p.Nome.Contains(texto, StringComparison.CurrentCultureIgnoreCase)
                                       || (p.Marca?.Contains(texto, StringComparison.CurrentCultureIgnoreCase) ?? false)
                                       || p.CodigoBarras == texto))
                .ToList();

            flpProdutos.SuspendLayout();
            foreach (var cartao in _cartoes.Values) cartao.Dispose();
            _cartoes.Clear();
            flpProdutos.Controls.Clear();
            foreach (var produto in visiveis)
            {
                var cartao = new CartaoProduto
                {
                    Produto = produto,
                    Imagem = ImagensProduto.Obter(produto.Id, produto.CategoriaId, 168),
                    NoCarrinho = _venda.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id)?.Quantidade ?? 0
                };
                cartao.AdicionarClicado += (s, e) => Adicionar(((CartaoProduto)s).Produto);
                _cartoes[produto.Id] = cartao;
                flpProdutos.Controls.Add(cartao);
            }
            if (visiveis.Count == 0)
                flpProdutos.Controls.Add(new Label
                {
                    Text = "Nenhum produto encontrado.",
                    AutoSize = true,
                    Font = T.Serifa(11f),
                    ForeColor = T.TextoSecundario,
                    Margin = new Padding(8, 20, 0, 0)
                });
            flpProdutos.ResumeLayout();
        }

        private void txtPesquisa_Pesquisar(object sender, EventArgs e)
        {
            // Leitor de código de barras: digita os números e envia Enter.
            var texto = txtPesquisa.Texto.Trim();
            if (texto.Length >= 8 && texto.All(char.IsDigit))
            {
                var produto = _produtos.FirstOrDefault(p => p.CodigoBarras == texto);
                if (produto != null)
                {
                    Adicionar(produto);
                    txtPesquisa.Texto = string.Empty;
                    return;
                }
            }
            MontarVitrine();
        }

        private void abasCategorias_SelecaoAlterada(object sender, EventArgs e) => MontarVitrine();

        private void abasCategorias_Resize(object sender, EventArgs e) =>
            pnlVitrineTopo.Height = abasCategorias.Bottom + 10;

        // ---------------- Carrinho ----------------

        private void Adicionar(Produto produto, int quantidade = 1)
        {
            var item = _venda.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
            int jaNoCarrinho = item?.Quantidade ?? 0;
            if (jaNoCarrinho + quantidade > produto.QuantidadeDisponivel)
            {
                Mensagem.Alerta($"Só há {produto.QuantidadeDisponivel} unidade(s) de \"{produto.Nome}\" disponível(is).", "Estoque insuficiente");
                return;
            }
            var falta = ConferirEstoqueCompartilhado(produto.Id, quantidade);
            if (falta != null)
            {
                Mensagem.Alerta(falta, "Estoque insuficiente");
                return;
            }
            if (item == null)
            {
                item = new ItemVenda
                {
                    ProdutoId = produto.Id,
                    ProdutoNome = produto.Nome,
                    TipoProduto = produto.Tipo,
                    PrecoUnitario = produto.PrecoVenda,
                    Promocao = produto.PromocaoVigente,
                    PromocaoNome = produto.PromocaoVigente?.Nome
                };
                _venda.Itens.Add(item);
            }
            item.Quantidade += quantidade;
            item.RecalcularDesconto();
            AtualizarCarrinho(item.ProdutoId);
        }

        /// <summary>
        /// Um mesmo produto pode estar no carrinho avulso e dentro de um kit (ex.: caneta azul).
        /// Aqui somamos tudo o que sairia de cada produto simples, como o VendaDAO fará,
        /// e avisamos antes de a pessoa chegar ao "Finalizar".
        /// </summary>
        private string ConferirEstoqueCompartilhado(int produtoAdicionado, int quantidadeAdicionada)
        {
            var necessidade = new Dictionary<int, int>();
            void Somar(int produtoId, int qtd)
            {
                if (_composicaoKits.TryGetValue(produtoId, out var componentes))
                    foreach (var c in componentes) necessidade[c.ProdutoId] = necessidade.GetValueOrDefault(c.ProdutoId) + c.Quantidade * qtd;
                else
                    necessidade[produtoId] = necessidade.GetValueOrDefault(produtoId) + qtd;
            }
            foreach (var i in _venda.Itens) Somar(i.ProdutoId, i.Quantidade);
            Somar(produtoAdicionado, quantidadeAdicionada);

            foreach (var (produtoId, qtd) in necessidade)
            {
                var simples = _produtos.FirstOrDefault(p => p.Id == produtoId);
                int saldo = simples?.QuantidadeDisponivel ?? 0;
                if (qtd > saldo)
                    return $"Não há \"{simples?.Nome ?? "componente"}\" suficiente: o carrinho (contando os kits) precisaria de {qtd} e há {saldo}.";
            }
            return null;
        }

        private ItemVenda ItemSelecionado => dgvCarrinho.CurrentRow?.DataBoundItem as ItemVenda;

        private void AlterarQuantidade(ItemVenda item, int delta)
        {
            if (item == null) return;
            if (delta > 0)
            {
                var produto = _produtos.FirstOrDefault(p => p.Id == item.ProdutoId);
                if (produto != null) Adicionar(produto, delta);
                return;
            }
            item.Quantidade += delta;
            if (item.Quantidade <= 0) _venda.Itens.Remove(item);
            else item.RecalcularDesconto();
            AtualizarCarrinho(item.ProdutoId);
        }

        private void btnMais_Click(object sender, EventArgs e) => AlterarQuantidade(ItemSelecionado, +1);
        private void btnMenos_Click(object sender, EventArgs e) => AlterarQuantidade(ItemSelecionado, -1);

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            var item = ItemSelecionado;
            if (item == null) return;
            _venda.Itens.Remove(item);
            AtualizarCarrinho(item.ProdutoId);
        }

        private void dgvCarrinho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = ItemSelecionado;
            if (item == null) return;
            var texto = Mensagem.PedirTexto($"Quantidade de \"{item.ProdutoNome}\":", "Alterar quantidade", item.Quantidade.ToString(), true);
            if (texto == null) return;
            if (!int.TryParse(texto, out var nova) || nova < 0) { Mensagem.Alerta("Informe um número inteiro."); return; }
            AlterarQuantidade(item, nova - item.Quantidade);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            if (_venda.Itens.Count == 0) return;
            if (!Mensagem.Confirmar("Esvaziar o carrinho?", "Limpar carrinho", "Esvaziar", "Manter")) return;
            Reiniciar();
        }

        private void Reiniciar()
        {
            _venda = NovaVenda();
            txtRecebido.Texto = string.Empty;
            if (cboCliente.Items.Count > 0) cboCliente.SelectedIndex = 0;
            foreach (var cartao in _cartoes.Values) { cartao.NoCarrinho = 0; cartao.Invalidate(); }
            AtualizarCarrinho();
        }

        /// <summary>Recalcula os totais e atualiza a lista do carrinho (cálculo automático).</summary>
        private void AtualizarCarrinho(int? produtoAlterado = null)
        {
            _venda.RecalcularTotais();
            var selecionado = produtoAlterado ?? ItemSelecionado?.ProdutoId;
            dgvCarrinho.DataSource = null;
            dgvCarrinho.DataSource = _venda.Itens.ToList();
            foreach (DataGridViewRow linha in dgvCarrinho.Rows)
                if (linha.DataBoundItem is ItemVenda i && i.ProdutoId == selecionado)
                    dgvCarrinho.CurrentCell = linha.Cells[0];

            int unidades = _venda.TotalItens;
            lblQtdItens.Text = unidades == 0 ? "vazio" : $"{unidades} {(unidades == 1 ? "item" : "itens")}";
            lblSubtotal.Text = Formatacao.Moeda(_venda.ValorSubtotal);
            lblDesconto.Text = "− " + Formatacao.Moeda(_venda.ValorDesconto);
            lblTotal.Text = Formatacao.Moeda(_venda.ValorTotal);
            btnFinalizar.Enabled = _venda.Itens.Count > 0;
            AtualizarTroco();

            if (produtoAlterado.HasValue && _cartoes.TryGetValue(produtoAlterado.Value, out var cartao))
            {
                cartao.NoCarrinho = _venda.Itens.FirstOrDefault(i => i.ProdutoId == produtoAlterado)?.Quantidade ?? 0;
                cartao.Invalidate();
            }
        }

        private void DgvCarrinho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Mostra a promoção aplicada logo abaixo do nome do produto.
            if (e.RowIndex < 0 || dgvCarrinho.Columns[e.ColumnIndex].Name != "colProdutoNome") return;
            if (dgvCarrinho.Rows[e.RowIndex].DataBoundItem is ItemVenda item && item.Desconto > 0)
            {
                e.Value = $"{item.ProdutoNome}\n−{Formatacao.Moeda(item.Desconto)} · {item.PromocaoNome}";
                e.FormattingApplied = true;
            }
        }

        // ---------------- Pagamento ----------------

        private FormaPagamento FormaSelecionada => abasPagamento.IndiceSelecionado switch
        {
            0 => FormaPagamento.Dinheiro,
            2 => FormaPagamento.CartaoDebito,
            3 => FormaPagamento.CartaoCredito,
            _ => FormaPagamento.Pix
        };

        private void abasPagamento_SelecaoAlterada(object sender, EventArgs e)
        {
            pnlDinheiro.Visible = FormaSelecionada == FormaPagamento.Dinheiro;
            AtualizarTroco();
        }

        private void txtRecebido_TextoAlterado(object sender, EventArgs e) => AtualizarTroco();

        private void AtualizarTroco()
        {
            if (!pnlDinheiro.Visible || !Formatacao.TentarLerDecimal(txtRecebido.Texto, out var recebido))
            {
                lblTroco.Text = string.Empty;
                return;
            }
            decimal troco = recebido - _venda.ValorTotal;
            lblTroco.Text = troco >= 0 ? $"Troco: {Formatacao.Moeda(troco)}" : $"Faltam {Formatacao.Moeda(-troco)}";
            lblTroco.ForeColor = troco >= 0 ? T.SalviaEscura : T.ArgilaEscura;
        }

        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            using var form = new FrmClienteEdicao(null);
            if (form.ShowDialog(this) != DialogResult.OK) return;
            Executar(() =>
            {
                CarregarClientes();
                // seleciona o cliente recém-criado (maior id)
                var lista = (List<KeyValuePair<int?, string>>)cboCliente.DataSource;
                var novo = lista.Where(x => x.Key.HasValue).OrderByDescending(x => x.Key).First();
                cboCliente.SelectedValue = novo.Key;
            });
        }

        // ---------------- Finalização ----------------

        private void btnFinalizar_Click(object sender, EventArgs e) => Finalizar();

        private void Finalizar()
        {
            if (_venda.Itens.Count == 0) return;
            if (cboCliente.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(cboCliente.Text))
            {
                Mensagem.Alerta("Cliente não encontrado. Escolha um cliente da lista ou cadastre um novo.");
                return;
            }
            _venda.ClienteId = cboCliente.SelectedValue as int?;
            _venda.FormaPagamento = FormaSelecionada;
            _venda.UsuarioId = Sessao.UsuarioId;
            _venda.RecalcularTotais();

            if (_venda.FormaPagamento == FormaPagamento.Dinheiro && Formatacao.TentarLerDecimal(txtRecebido.Texto, out var recebido)
                && recebido < _venda.ValorTotal)
            {
                Mensagem.Alerta("O valor recebido é menor que o total da venda.");
                return;
            }

            var erros = _venda.Validar();
            if (erros.Count > 0) { Mensagem.ErrosValidacao(erros); return; }

            if (!Mensagem.Confirmar($"{_venda.TotalItens} item(ns) · {_venda.FormaPagamento.Descricao()}\n" +
                                    $"Cliente: {cboCliente.Text}\n\nTotal: {Formatacao.Moeda(_venda.ValorTotal)}",
                                    "Finalizar venda", "Confirmar venda", "Voltar"))
                return;

            int numero = 0;
            if (!Executar(() => numero = _vendaDAO.RegistrarVenda(_venda), "Registrar venda"))
            {
                Carregar(); // recarrega saldos: o erro pode ter sido estoque insuficiente
                return;
            }

            if (Mensagem.Confirmar($"Venda nº {numero} registrada e estoque atualizado.\nDeseja gerar o comprovante em PDF?",
                                   "Venda concluída!", "Gerar comprovante", "Agora não"))
            {
                Exportacao.ParaPdf($"comprovante-venda-{numero}", caminho =>
                    ExportadorPdf.ExportarComprovante(caminho, _vendaDAO.ObterPorId(numero)));
            }

            Reiniciar();
            Carregar();
            Principal?.AtualizarContadorEstoqueBaixo();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F9:
                    Finalizar();
                    return true;
                case Keys.F3:
                    txtPesquisa.Focus();
                    txtPesquisa.SelecionarTudo();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
