using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>Registro manual de entrada ou saída de estoque (ajuste, devolução ou perda).</summary>
    public partial class FrmMovimentacao : FrmDialogoBase
    {
        private readonly EstoqueDAO _dao = new();
        private readonly int _produtoInicial;
        private readonly TipoMovimentacao _tipoInicial;
        private List<Estoque> _saldos = new();

        public FrmMovimentacao() : this(0, TipoMovimentacao.Entrada) { }

        public FrmMovimentacao(int produtoId, TipoMovimentacao tipo)
        {
            InitializeComponent();
            _produtoInicial = produtoId;
            _tipoInicial = tipo;
        }

        private TipoMovimentacao Tipo => abasTipo.IndiceSelecionado == 1 ? TipoMovimentacao.Saida : TipoMovimentacao.Entrada;

        private void FrmMovimentacao_Load(object sender, EventArgs e)
        {
            if (EmDesign) return;
            try
            {
                _saldos = _dao.ListarSaldos(null, null, null);
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Carregar produtos");
                Close();
                return;
            }
            cboProduto.DisplayMember = nameof(Estoque.ProdutoNome);
            cboProduto.ValueMember = nameof(Estoque.ProdutoId);
            cboProduto.DataSource = _saldos;
            // Permite digitar parte do nome para encontrar o produto
            cboProduto.DropDownStyle = ComboBoxStyle.DropDown;
            cboProduto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProduto.AutoCompleteSource = AutoCompleteSource.ListItems;
            if (_produtoInicial > 0) cboProduto.SelectedValue = _produtoInicial;

            abasTipo.IndiceSelecionado = _tipoInicial == TipoMovimentacao.Saida ? 1 : 0;
            CarregarMotivos();
        }

        /// <summary>
        /// Motivos possíveis para cada tipo. VENDA e COMPRA não aparecem: são
        /// gerados automaticamente pelas telas de venda e de pedido de compra.
        /// </summary>
        private void CarregarMotivos()
        {
            var motivos = Tipo == TipoMovimentacao.Entrada
                ? new[] { MotivoMovimentacao.Ajuste, MotivoMovimentacao.Devolucao }
                : new[] { MotivoMovimentacao.Ajuste, MotivoMovimentacao.Perda, MotivoMovimentacao.Devolucao };
            cboMotivo.DisplayMember = "Value";
            cboMotivo.ValueMember = "Key";
            cboMotivo.DataSource = motivos.Select(m => new KeyValuePair<MotivoMovimentacao, string>(m, m.Descricao())).ToList();
            btnSalvar.Estilo = Tipo == TipoMovimentacao.Entrada ? Controles.EstiloBotao.Primario : Controles.EstiloBotao.Destaque;
            AtualizarPrevia();
        }

        private void abasTipo_SelecaoAlterada(object sender, EventArgs e) => CarregarMotivos();

        private void Valores_Alterados(object sender, EventArgs e) => AtualizarPrevia();

        /// <summary>Mostra como o saldo vai ficar depois da movimentação.</summary>
        private void AtualizarPrevia()
        {
            if (cboProduto.SelectedItem is not Estoque estoque)
            {
                lblSaldoAtual.Text = "";
                lblNovoSaldo.Text = "";
                return;
            }
            lblSaldoAtual.Text = $"Saldo atual: {estoque.Quantidade} {estoque.UnidadeMedida}  ·  mínimo {estoque.EstoqueMinimo}  ·  {estoque.Localizacao}";
            int novo = estoque.Quantidade + (Tipo == TipoMovimentacao.Entrada ? 1 : -1) * (int)nudQuantidade.Value;
            lblNovoSaldo.Text = $"→ novo saldo: {novo} {estoque.UnidadeMedida}";
            lblNovoSaldo.ForeColor = novo < 0 ? T.ArgilaEscura : novo <= estoque.EstoqueMinimo ? T.DouradoEscuro : T.SalviaEscura;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cboProduto.SelectedItem is not Estoque estoque)
            {
                Mensagem.Alerta("Selecione um produto da lista.");
                return;
            }
            var movimentacao = new MovimentacaoEstoque
            {
                ProdutoId = estoque.ProdutoId,
                UsuarioId = Sessao.UsuarioId,
                Tipo = Tipo,
                Motivo = (MotivoMovimentacao)cboMotivo.SelectedValue,
                Quantidade = (int)nudQuantidade.Value,
                Observacao = txtObservacao.Texto.Trim()
            };
            if (!ValidarOuAvisar(movimentacao.Validar(), (txtObservacao, "observação"))) return;

            string verbo = Tipo == TipoMovimentacao.Entrada ? "entrada" : "saída";
            if (!Mensagem.Confirmar($"Registrar {verbo} de {movimentacao.Quantidade} {estoque.UnidadeMedida} de \"{estoque.ProdutoNome}\"?", "Confirmar movimentação", "Registrar"))
                return;
            try
            {
                _dao.RegistrarMovimentacao(movimentacao);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Registrar movimentação");
            }
        }
    }
}
