using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>Pedidos de compra a fornecedores: criar, receber (entrada no estoque) e cancelar.</summary>
    public partial class UcPedidosCompra : TelaBase
    {
        private readonly PedidoCompraDAO _dao = new();

        public UcPedidosCompra()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Pedidos de compra";
        public override string Subtitulo => "Compras feitas aos fornecedores. Receber um pedido dá entrada no estoque.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvPedidos);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.Id), "Nº", 60, centro: true);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.DataPedido), "Data do pedido", 140, T.FormatoDataHora);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.FornecedorNome), "Fornecedor", peso: 160);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.DataPrevistaEntrega), "Previsão", 100, T.FormatoData);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.DataRecebimento), "Recebido em", 140, T.FormatoDataHora);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.QuantidadeItens), "Itens", 60, T.FormatoInteiro);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.ValorTotal), "Valor", 110, T.FormatoMoeda);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.Status), "Situação", 110);
            T.AdicionarColuna(dgvPedidos, nameof(PedidoCompra.Observacao), "Observação", peso: 140);
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                StatusPedidoCompra? status = abasStatus.IndiceSelecionado switch
                {
                    1 => StatusPedidoCompra.Aberto,
                    2 => StatusPedidoCompra.Recebido,
                    3 => StatusPedidoCompra.Cancelado,
                    _ => null
                };
                var pedidos = _dao.Pesquisar(txtPesquisa.Texto, status);
                dgvPedidos.DataSource = new ListaOrdenavel<PedidoCompra>(pedidos);
                var abertos = pedidos.Where(p => p.Status == StatusPedidoCompra.Aberto).ToList();
                lblRodape.Text = Contagem(pedidos.Count, "pedido", "pedidos") +
                                 $"  ·  {abertos.Count} em aberto ({Formatacao.Moeda(abertos.Sum(p => p.ValorTotal))} a receber)";
            });
        }

        private PedidoCompra Selecionado => dgvPedidos.CurrentRow?.DataBoundItem as PedidoCompra;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void Filtro_Alterado(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmPedidoCompraEdicao(0, null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um pedido na lista."); return; }
            using var form = new FrmPedidoCompraEdicao(Selecionado.Id, null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            var pedido = Selecionado;
            if (pedido == null) { Mensagem.Informar("Selecione um pedido na lista."); return; }
            if (pedido.Status != StatusPedidoCompra.Aberto) { Mensagem.Alerta("Só pedidos em aberto podem ser recebidos."); return; }
            if (!Mensagem.Confirmar($"Confirmar o recebimento do pedido nº {pedido.Id} ({pedido.FornecedorNome})?\n\n" +
                                    $"{pedido.QuantidadeItens} produto(s) darão entrada no estoque.", "Receber mercadoria", "Receber"))
                return;
            if (Executar(() => _dao.Receber(pedido.Id, Sessao.UsuarioId)))
            {
                Mensagem.Sucesso("Mercadoria recebida e estoque atualizado.");
                Carregar();
                Principal?.AtualizarContadorEstoqueBaixo();
            }
        }

        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            var pedido = Selecionado;
            if (pedido == null) { Mensagem.Informar("Selecione um pedido na lista."); return; }
            if (pedido.Status != StatusPedidoCompra.Aberto) { Mensagem.Alerta("Só pedidos em aberto podem ser cancelados."); return; }
            var motivo = Mensagem.PedirTexto($"Cancelar o pedido nº {pedido.Id}? Informe o motivo:", "Cancelar pedido", "Motivo do cancelamento",
                                             obrigatorio: true, "Cancelar pedido", perigoso: true);
            if (motivo == null) return;
            if (Executar(() => _dao.Cancelar(pedido.Id, motivo))) Carregar();
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "pedidos-compra", () => Exportacao.DeGrid(dgvPedidos, "Pedidos de compra", lblRodape.Text), paisagem: true);

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPedidos.Columns[e.ColumnIndex].Name != "colStatus" || e.Value is not StatusPedidoCompra s) return;
            switch (s)
            {
                case StatusPedidoCompra.Aberto: T.DesenharChipCelula(e, "Aberto", T.DouradoClaro, T.DouradoEscuro); break;
                case StatusPedidoCompra.Recebido: T.DesenharChipCelula(e, "Recebido", T.SalviaClara, T.SalviaEscura); break;
                default: T.DesenharChipCelula(e, "Cancelado", T.ArgilaClara, T.ArgilaEscura); break;
            }
        }
    }
}
