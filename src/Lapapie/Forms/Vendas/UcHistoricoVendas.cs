using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Relatorios;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Vendas
{
    /// <summary>
    /// Histórico de vendas (view vw_historico_vendas) com filtros, detalhe dos
    /// itens, comprovante em PDF e cancelamento (somente administrador).
    /// </summary>
    public partial class UcHistoricoVendas : TelaBase
    {
        private readonly VendaDAO _dao = new();
        private bool _carregandoFiltros = true;
        private Venda _vendaDetalhe;

        public UcHistoricoVendas()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Histórico de vendas";
        public override string Subtitulo => "Todas as vendas registradas, com itens, descontos e forma de pagamento.";

        private void Configurar()
        {
            T.EstilizarGrid(dgvVendas);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.VendaId), "Nº", 60, centro: true);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.DataVenda), "Data", 130, T.FormatoDataHora);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.Cliente), "Cliente", peso: 160);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.Vendedor), "Vendedor", 140);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.FormaPagamentoDescricao), "Pagamento", 130);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.TotalItens), "Itens", 60, T.FormatoInteiro);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.ValorDesconto), "Desconto", 90, T.FormatoMoeda);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.ValorTotal), "Total", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvVendas, nameof(HistoricoVenda.Status), "Situação", 110);

            T.EstilizarGrid(dgvItens);
            dgvItens.ColumnHeadersHeight = 32;
            dgvItens.RowTemplate.Height = 34;
            dgvItens.DefaultCellStyle.Font = T.Texto(9f);
            T.AdicionarColuna(dgvItens, nameof(ItemVenda.ProdutoNome), "Produto");
            T.AdicionarColuna(dgvItens, nameof(ItemVenda.Quantidade), "Qtd.", 46, T.FormatoInteiro);
            T.AdicionarColuna(dgvItens, nameof(ItemVenda.Subtotal), "Subtotal", 90, T.FormatoMoeda);

            btnCancelarVenda.Visible = Sessao.Pode(Funcionalidade.CancelarVenda);
            dtpFim.Value = DateTime.Today;
            dtpInicio.Value = DateTime.Today.AddDays(-30);
            _carregandoFiltros = false;
        }

        public override void Carregar()
        {
            if (dtpInicio.Value.Date > dtpFim.Value.Date)
            {
                lblRodape.Text = "A data inicial é maior que a final — ajuste o período.";
                return;
            }
            Executar(() =>
            {
                StatusVenda? status = abasStatus.IndiceSelecionado switch { 1 => StatusVenda.Concluida, 2 => StatusVenda.Cancelada, _ => null };
                var vendas = _dao.ListarHistorico(dtpInicio.Value, dtpFim.Value, status, txtPesquisa.Texto);
                dgvVendas.DataSource = new ListaOrdenavel<HistoricoVenda>(vendas);
                var concluidas = vendas.Where(v => v.Status == StatusVenda.Concluida).ToList();
                lblRodape.Text = Contagem(vendas.Count, "venda", "vendas") +
                                 $"  ·  faturamento: {Formatacao.Moeda(concluidas.Sum(v => v.ValorTotal))}" +
                                 $"  ·  descontos: {Formatacao.Moeda(concluidas.Sum(v => v.ValorDesconto))}" +
                                 $"  ·  canceladas: {vendas.Count - concluidas.Count}";
                MostrarDetalhe();
            });
        }

        private HistoricoVenda Selecionada => dgvVendas.CurrentRow?.DataBoundItem as HistoricoVenda;

        private void MostrarDetalhe()
        {
            var resumo = Selecionada;
            cardDetalhe.Visible = resumo != null;
            if (resumo == null) return;
            try
            {
                _vendaDetalhe = _dao.ObterPorId(resumo.VendaId);
                if (_vendaDetalhe == null) return;
                lblVendaTitulo.Text = $"VENDA Nº {_vendaDetalhe.Id}";
                lblVendaInfo.Text = $"{_vendaDetalhe.DataVenda:dd/MM/yyyy 'às' HH:mm}\n" +
                                    $"Cliente: {_vendaDetalhe.ClienteNome}\n" +
                                    $"Vendedor: {_vendaDetalhe.UsuarioNome}\n" +
                                    $"Pagamento: {_vendaDetalhe.FormaPagamento.Descricao()}" +
                                    (string.IsNullOrEmpty(_vendaDetalhe.Observacao) ? "" : $"\nObs.: {_vendaDetalhe.Observacao}");
                dgvItens.DataSource = _vendaDetalhe.Itens;
                lblVendaTotais.Text = $"Subtotal {Formatacao.Moeda(_vendaDetalhe.ValorSubtotal)}   ·   Descontos −{Formatacao.Moeda(_vendaDetalhe.ValorDesconto)}\n" +
                                      $"TOTAL {Formatacao.Moeda(_vendaDetalhe.ValorTotal)}" +
                                      (_vendaDetalhe.Status == StatusVenda.Cancelada ? "   ·   CANCELADA" : "");
                lblVendaTotais.ForeColor = _vendaDetalhe.Status == StatusVenda.Cancelada ? T.ArgilaEscura : T.TintaGrafite;
                btnCancelarVenda.Enabled = _vendaDetalhe.Status == StatusVenda.Concluida;
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Detalhe da venda");
            }
        }

        private void dgvVendas_SelectionChanged(object sender, EventArgs e) => MostrarDetalhe();
        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void Filtro_Alterado(object sender, EventArgs e)
        {
            if (!_carregandoFiltros) AgendarCarregamento();
        }

        private void btnComprovante_Click(object sender, EventArgs e)
        {
            if (_vendaDetalhe == null) return;
            var venda = _vendaDetalhe;
            Exportacao.ParaPdf($"comprovante-venda-{venda.Id}", caminho => ExportadorPdf.ExportarComprovante(caminho, venda));
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (_vendaDetalhe == null || _vendaDetalhe.Status != StatusVenda.Concluida) return;
            var motivo = Mensagem.PedirTexto(
                $"Cancelar a venda nº {_vendaDetalhe.Id} ({Formatacao.Moeda(_vendaDetalhe.ValorTotal)})?\n" +
                "Os produtos voltam para o estoque como DEVOLUÇÃO. Informe o motivo:",
                "Cancelar venda", "Motivo do cancelamento", obrigatorio: true, "Cancelar venda", perigoso: true);
            if (motivo == null) return;
            int id = _vendaDetalhe.Id;
            if (Executar(() => _dao.CancelarVenda(id, Sessao.UsuarioId, motivo), "Cancelar venda"))
            {
                Mensagem.Sucesso($"Venda nº {id} cancelada e estoque devolvido.");
                Carregar();
                Principal?.AtualizarContadorEstoqueBaixo();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "historico-vendas",
                () => Exportacao.DeGrid(dgvVendas, "Histórico de vendas", $"{dtpInicio.Value:dd/MM/yyyy} a {dtpFim.Value:dd/MM/yyyy} · {lblRodape.Text}"),
                paisagem: true);

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvVendas.Columns[e.ColumnIndex].Name != "colStatus" || e.Value is not StatusVenda s) return;
            if (s == StatusVenda.Concluida) T.DesenharChipCelula(e, "Concluída", T.SalviaClara, T.SalviaEscura);
            else T.DesenharChipCelula(e, "Cancelada", T.ArgilaClara, T.ArgilaEscura);
        }
    }
}
