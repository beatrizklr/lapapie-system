using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>Histórico de entradas e saídas de estoque, com filtros.</summary>
    public partial class UcMovimentacoes : TelaBase
    {
        private readonly EstoqueDAO _dao = new();
        private bool _carregandoFiltros = true;

        public UcMovimentacoes()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Movimentações";
        public override string Subtitulo => "O caminho de cada unidade: vendas, compras, ajustes, devoluções e perdas.";

        private void Configurar()
        {
            T.EstilizarGrid(dgvMovimentacoes);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.DataMovimentacao), "Data", 140, T.FormatoDataHora);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.ProdutoNome), "Produto", peso: 200);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.Tipo), "Tipo", 100);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.MotivoDescricao), "Motivo", 150);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.QuantidadeComSinal), "Qtd.", 70, "+#,##0;-#,##0", direita: true);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.Origem), "Origem", 120);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.UsuarioNome), "Responsável", 150);
            T.AdicionarColuna(dgvMovimentacoes, nameof(MovimentacaoEstoque.Observacao), "Observação", peso: 160);

            dtpFim.Value = DateTime.Today;
            dtpInicio.Value = DateTime.Today.AddDays(-30);
            var motivos = new List<KeyValuePair<MotivoMovimentacao?, string>> { new(null, "Todos os motivos") };
            motivos.AddRange(Enum.GetValues<MotivoMovimentacao>().Select(m => new KeyValuePair<MotivoMovimentacao?, string>(m, m.Descricao())));
            cboMotivo.DisplayMember = "Value";
            cboMotivo.ValueMember = "Key";
            cboMotivo.DataSource = motivos;
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
                TipoMovimentacao? tipo = abasTipo.IndiceSelecionado switch { 1 => TipoMovimentacao.Entrada, 2 => TipoMovimentacao.Saida, _ => null };
                var lista = _dao.ListarMovimentacoes(dtpInicio.Value, dtpFim.Value, txtPesquisa.Texto, tipo, cboMotivo.SelectedValue as MotivoMovimentacao?);
                dgvMovimentacoes.DataSource = new ListaOrdenavel<MovimentacaoEstoque>(lista);
                int entradas = lista.Where(m => m.Tipo == TipoMovimentacao.Entrada).Sum(m => m.Quantidade);
                int saidas = lista.Where(m => m.Tipo == TipoMovimentacao.Saida).Sum(m => m.Quantidade);
                lblRodape.Text = Contagem(lista.Count, "movimentação", "movimentações") +
                                 $"  ·  entradas: {Formatacao.Numero(entradas)} un.  ·  saídas: {Formatacao.Numero(saidas)} un.  ·  saldo do período: {entradas - saidas:+#,##0;-#,##0;0}";
            });
        }

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void Filtro_Alterado(object sender, EventArgs e)
        {
            if (!_carregandoFiltros) AgendarCarregamento();
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "movimentacoes",
                () => Exportacao.DeGrid(dgvMovimentacoes, "Movimentações de estoque", $"{dtpInicio.Value:dd/MM/yyyy} a {dtpFim.Value:dd/MM/yyyy} · {lblRodape.Text}"),
                paisagem: true);

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var coluna = dgvMovimentacoes.Columns[e.ColumnIndex].Name;
            if (coluna == "colTipo" && e.Value is TipoMovimentacao tipo)
            {
                if (tipo == TipoMovimentacao.Entrada) T.DesenharChipCelula(e, "Entrada", T.SalviaClara, T.SalviaEscura);
                else T.DesenharChipCelula(e, "Saída", T.ArgilaClara, T.ArgilaEscura);
            }
            else if (coluna == "colQuantidadeComSinal" && e.Value is int qtd)
            {
                e.CellStyle.ForeColor = qtd >= 0 ? T.SalviaEscura : T.ArgilaEscura;
                e.CellStyle.Font = T.Texto(9.75f, FontStyle.Bold);
            }
        }
    }
}
