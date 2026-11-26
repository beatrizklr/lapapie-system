using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>
    /// Consulta de produtos com estoque baixo (view vw_produtos_estoque_baixo),
    /// com atalho para gerar um pedido de compra de reposição.
    /// </summary>
    public partial class UcEstoqueBaixo : TelaBase
    {
        private readonly EstoqueDAO _dao = new();
        private List<ProdutoEstoqueBaixo> _itens = new();

        public UcEstoqueBaixo()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Estoque baixo";
        public override string Subtitulo => "Produtos no limite ou abaixo do estoque mínimo — hora de repor.";

        private void Configurar()
        {
            T.EstilizarGrid(dgvEstoqueBaixo);
            dgvEstoqueBaixo.MultiSelect = true;
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.Produto), "Produto", peso: 220);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.Categoria), "Categoria", 140);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.Localizacao), "Localização", peso: 120);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.Quantidade), "Saldo", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.EstoqueMinimo), "Mínimo", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.Deficit), "Déficit", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvEstoqueBaixo, nameof(ProdutoEstoqueBaixo.SugestaoCompra), "Sugestão de compra", 150, T.FormatoInteiro);

            btnGerarPedido.Visible = Sessao.Pode(Funcionalidade.PedidosCompra);
            btnMovimentar.Visible = Sessao.Pode(Funcionalidade.MovimentarEstoque);
            lblDicaSelecao.Visible = btnGerarPedido.Visible;
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                _itens = _dao.ListarEstoqueBaixo();
                var filtro = txtPesquisa.Texto.Trim();
                var visiveis = _itens.Where(i => filtro.Length == 0 || i.Produto.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)).ToList();
                dgvEstoqueBaixo.DataSource = new ListaOrdenavel<ProdutoEstoqueBaixo>(visiveis);
                dgvEstoqueBaixo.ClearSelection();

                int zerados = _itens.Count(i => i.Quantidade == 0);
                kpiZerados.Valor = zerados.ToString();
                kpiZerados.Detalhe = zerados == 0 ? "Nenhum produto em falta" : "Sem nenhuma unidade";
                kpiZerados.Tendencia = zerados > 0 ? Tendencia.Baixa : Tendencia.Alta;
                kpiAbaixo.Valor = (_itens.Count - zerados).ToString();
                kpiAbaixo.Detalhe = $"Faltam {Formatacao.Numero(_itens.Sum(i => i.Deficit))} un. para o mínimo";
                kpiAbaixo.Tendencia = Tendencia.Alerta;
                var kits = _dao.ListarKits(somenteAtivos: true);
                int kitsIndisponiveis = kits.Count(k => k.QuantidadeDisponivel == 0);
                kpiKits.Valor = $"{kitsIndisponiveis} de {kits.Count}";
                kpiKits.Detalhe = kitsIndisponiveis == 0 ? "Todos os kits podem ser montados" : "Faltam componentes";
                kpiKits.Tendencia = kitsIndisponiveis == 0 ? Tendencia.Alta : Tendencia.Baixa;

                lblRodape.Text = Contagem(visiveis.Count, "produto", "produtos") + " precisando de reposição";
            });
        }

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void btnGerarPedido_Click(object sender, EventArgs e)
        {
            var selecionados = dgvEstoqueBaixo.SelectedRows.Cast<DataGridViewRow>()
                                              .Select(r => r.DataBoundItem as ProdutoEstoqueBaixo).Where(x => x != null).ToList();
            var origem = selecionados.Count > 0 ? selecionados : _itens;
            if (origem.Count == 0) { Mensagem.Informar("Não há produtos com estoque baixo. 🎉"); return; }

            var itens = origem.Select(i => new ItemPedidoCompra
            {
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.Produto,
                Quantidade = i.SugestaoCompra
            }).ToList();

            using var form = new FrmPedidoCompraEdicao(0, itens);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Mensagem.Sucesso("Pedido de compra criado. Quando a mercadoria chegar, use \"Receber\" em Pedidos de compra para atualizar o estoque.");
                Carregar();
            }
        }

        private void btnMovimentar_Click(object sender, EventArgs e)
        {
            var item = dgvEstoqueBaixo.CurrentRow?.DataBoundItem as ProdutoEstoqueBaixo;
            using var form = new FrmMovimentacao(item?.ProdutoId ?? 0, TipoMovimentacao.Entrada);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Carregar();
                Principal?.AtualizarContadorEstoqueBaixo();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "estoque-baixo", () => Exportacao.DeGrid(dgvEstoqueBaixo, "Produtos com estoque baixo", lblRodape.Text), paisagem: true);

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvEstoqueBaixo.Columns[e.ColumnIndex].Name != "colQuantidade" || e.Value is not int saldo) return;
            if (saldo == 0) T.DesenharChipCelula(e, "0 · zerado", T.ArgilaClara, T.ArgilaEscura);
            else T.DesenharChipCelula(e, saldo.ToString(), T.DouradoClaro, T.DouradoEscuro);
        }
    }
}
