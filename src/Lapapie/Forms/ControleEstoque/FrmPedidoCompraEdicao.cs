using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>
    /// Cadastro/visualização de pedido de compra. O custo de cada item é sugerido
    /// a partir da tabela fornecedor_produto (preço que aquele fornecedor cobra).
    /// </summary>
    public partial class FrmPedidoCompraEdicao : FrmDialogoBase
    {
        private readonly PedidoCompraDAO _dao = new();
        private readonly FornecedorDAO _fornecedorDAO = new();
        private readonly int _pedidoId;
        private readonly List<ItemPedidoCompra> _itensSugeridos;
        private PedidoCompra _pedido;
        private List<FornecedorProduto> _custosFornecedor = new();
        private bool _carregando = true;

        public FrmPedidoCompraEdicao() : this(0, null) { }

        /// <param name="pedidoId">0 para novo pedido.</param>
        /// <param name="itensSugeridos">Itens pré-preenchidos (vindos da tela de estoque baixo).</param>
        public FrmPedidoCompraEdicao(int pedidoId, List<ItemPedidoCompra> itensSugeridos)
        {
            InitializeComponent();
            _pedidoId = pedidoId;
            _itensSugeridos = itensSugeridos;
        }

        private bool SomenteLeitura => _pedido != null && !_pedido.PodeSerAlterado;

        private void FrmPedidoCompraEdicao_Load(object sender, EventArgs e)
        {
            if (EmDesign) return;
            T.EstilizarGrid(dgvItens);
            dgvItens.RowTemplate.Height = 36;
            T.AdicionarColuna(dgvItens, nameof(ItemPedidoCompra.ProdutoNome), "Produto");
            T.AdicionarColuna(dgvItens, nameof(ItemPedidoCompra.Quantidade), "Qtd.", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvItens, nameof(ItemPedidoCompra.CustoUnitario), "Custo un.", 110, T.FormatoMoeda);
            T.AdicionarColuna(dgvItens, nameof(ItemPedidoCompra.Subtotal), "Subtotal", 120, T.FormatoMoeda);

            try
            {
                var fornecedores = _fornecedorDAO.ListarAtivos();
                _pedido = _pedidoId == 0
                    ? new PedidoCompra { UsuarioId = Sessao.UsuarioId, DataPrevistaEntrega = DateTime.Today.AddDays(7) }
                    : _dao.ObterPorId(_pedidoId);
                if (_pedido == null) throw new RegraNegocioException("Pedido não encontrado.");

                // Pedido antigo de fornecedor inativo: inclui o fornecedor na lista para exibir.
                if (_pedido.FornecedorId > 0 && fornecedores.All(f => f.Id != _pedido.FornecedorId))
                    fornecedores.Add(new Fornecedor { Id = _pedido.FornecedorId, RazaoSocial = _pedido.FornecedorNome });

                cboFornecedor.DisplayMember = nameof(Fornecedor.NomeExibicao);
                cboFornecedor.ValueMember = nameof(Fornecedor.Id);
                cboFornecedor.DataSource = fornecedores;

                cboProduto.DisplayMember = nameof(Produto.Nome);
                cboProduto.ValueMember = nameof(Produto.Id);
                cboProduto.DataSource = new ProdutoDAO().Pesquisar(null, tipo: TipoProduto.Simples);
                cboProduto.DropDownStyle = ComboBoxStyle.DropDown;
                cboProduto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboProduto.AutoCompleteSource = AutoCompleteSource.ListItems;

                if (_itensSugeridos != null)
                {
                    _pedido.Itens.AddRange(_itensSugeridos);
                    _pedido.Observacao = "Reposição de estoque baixo";
                    SugerirFornecedor(fornecedores);
                }
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Abrir pedido de compra");
                Close();
                return;
            }

            if (_pedido.FornecedorId > 0) cboFornecedor.SelectedValue = _pedido.FornecedorId;
            dtpPrevisao.Checked = _pedido.DataPrevistaEntrega.HasValue;
            if (_pedido.DataPrevistaEntrega.HasValue) dtpPrevisao.Value = _pedido.DataPrevistaEntrega.Value;
            txtObservacao.Texto = _pedido.Observacao;

            if (_pedidoId > 0)
            {
                lblTitulo.Text = $"PEDIDO DE COMPRA Nº {_pedido.Id}";
                lblStatus.Text = _pedido.Status.Descricao().ToUpperInvariant() +
                                 (_pedido.DataRecebimento.HasValue ? $" em {_pedido.DataRecebimento:dd/MM/yyyy}" : "");
            }
            if (SomenteLeitura)
            {
                lblSubtitulo.Text = "Pedido encerrado — apenas consulta.";
                cboFornecedor.Enabled = dtpPrevisao.Enabled = txtObservacao.Enabled = false;
                cardItem.Visible = btnRemoverItem.Visible = btnSalvar.Visible = false;
                btnCancelar.Text = "Fechar";
            }
            _carregando = false;
            CarregarCustosFornecedor();
            AtualizarItens();
        }

        /// <summary>Escolhe o fornecedor que vende mais itens do pedido sugerido (e mais barato).</summary>
        private void SugerirFornecedor(List<Fornecedor> fornecedores)
        {
            var ids = _pedido.Itens.Select(i => i.ProdutoId).ToHashSet();
            var melhor = fornecedores
                .Select(f => new { f.Id, Produtos = _fornecedorDAO.ListarProdutos(f.Id).Where(p => ids.Contains(p.ProdutoId)).ToList() })
                .OrderByDescending(x => x.Produtos.Count).ThenBy(x => x.Produtos.Sum(p => p.PrecoCusto))
                .FirstOrDefault();
            if (melhor != null && melhor.Produtos.Count > 0) _pedido.FornecedorId = melhor.Id;
        }

        private void cboFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_carregando) return;
            CarregarCustosFornecedor();
            AtualizarItens();
        }

        /// <summary>Busca os custos do fornecedor e preenche os itens que ainda estão sem custo.</summary>
        private void CarregarCustosFornecedor()
        {
            if (cboFornecedor.SelectedValue is not int fornecedorId) return;
            try
            {
                _custosFornecedor = _fornecedorDAO.ListarProdutos(fornecedorId);
                if (!SomenteLeitura)
                    foreach (var item in _pedido.Itens.Where(i => i.CustoUnitario == 0))
                        item.CustoUnitario = _custosFornecedor.FirstOrDefault(c => c.ProdutoId == item.ProdutoId)?.PrecoCusto ?? 0;
                cboProduto_SelectedIndexChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Custos do fornecedor");
            }
        }

        private void cboProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_carregando || cboProduto.SelectedItem is not Produto produto) return;
            var custo = _custosFornecedor.FirstOrDefault(c => c.ProdutoId == produto.Id);
            txtCusto.Texto = custo == null ? "" : custo.PrecoCusto.ToString("N2", Formatacao.PtBr);
            txtCusto.Placeholder = custo == null ? "não cadastrado" : "";
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (cboProduto.SelectedItem is not Produto produto)
            {
                Mensagem.Alerta("Selecione um produto da lista.");
                return;
            }
            if (!Formatacao.TentarLerDecimal(txtCusto.Texto, out var custo) || custo < 0)
            {
                txtCusto.ComErro = true;
                Mensagem.Alerta("Informe o custo unitário.");
                return;
            }
            var item = _pedido.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
            if (item == null)
            {
                item = new ItemPedidoCompra { ProdutoId = produto.Id, ProdutoNome = produto.Nome };
                _pedido.Itens.Add(item);
            }
            item.Quantidade = (int)nudQuantidade.Value;
            item.CustoUnitario = custo;
            AtualizarItens();
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dgvItens.CurrentRow?.DataBoundItem is ItemPedidoCompra item)
            {
                _pedido.Itens.Remove(item);
                AtualizarItens();
            }
        }

        private void AtualizarItens()
        {
            _pedido.RecalcularTotal();
            dgvItens.DataSource = null;
            dgvItens.DataSource = _pedido.Itens.ToList();
            int semCusto = _pedido.Itens.Count(i => i.CustoUnitario == 0);
            lblTotal.Text = $"{_pedido.Itens.Count} item(ns)  ·  Total: {Formatacao.Moeda(_pedido.ValorTotal)}";
            lblSubtitulo.Text = semCusto > 0 && !SomenteLeitura
                ? $"Atenção: {semCusto} item(ns) sem custo para este fornecedor. Ajuste antes de salvar."
                : SomenteLeitura ? lblSubtitulo.Text : "Ao receber a mercadoria, o estoque é atualizado automaticamente.";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _pedido.FornecedorId = cboFornecedor.SelectedValue is int f ? f : 0;
            _pedido.DataPrevistaEntrega = dtpPrevisao.Checked ? dtpPrevisao.Value.Date : null;
            _pedido.Observacao = txtObservacao.Texto.Trim();
            var erros = _pedido.Validar();
            if (_pedido.Itens.Any(i => i.CustoUnitario == 0) &&
                !Mensagem.Confirmar("Há itens com custo zero. Deseja salvar mesmo assim?", "Custo zerado", "Salvar", "Revisar"))
                return;
            if (!ValidarOuAvisar(erros, (txtObservacao, "observação"))) return;
            try
            {
                if (_pedidoId == 0) _dao.Inserir(_pedido); else _dao.Atualizar(_pedido);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Salvar pedido de compra");
            }
        }
    }
}
