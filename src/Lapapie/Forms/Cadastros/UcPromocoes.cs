using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro de promoções (relação N:N promoção × produto).</summary>
    public partial class UcPromocoes : TelaBase
    {
        private readonly PromocaoDAO _dao = new();

        public UcPromocoes()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Promoções";
        public override string Subtitulo => "Descontos com data marcada. O caixa aplica sozinho a melhor promoção vigente.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvPromocoes);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.Nome), "Promoção", peso: 140);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.Descricao), "Descrição", peso: 200);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.DescricaoDesconto), "Desconto", 110, direita: true);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.DataInicio), "Início", 140, T.FormatoDataHora);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.DataFim), "Fim", 140, T.FormatoDataHora);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.QuantidadeProdutos), "Produtos", 90, T.FormatoInteiro);
            T.AdicionarColuna(dgvPromocoes, nameof(Promocao.Situacao), "Situação", 120);
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var promocoes = _dao.Pesquisar(txtPesquisa.Texto);
                dgvPromocoes.DataSource = new ListaOrdenavel<Promocao>(promocoes);
                int vigentes = promocoes.Count(p => p.Situacao == SituacaoPromocao.Vigente);
                lblRodape.Text = Contagem(promocoes.Count, "promoção", "promoções") + $"  ·  {vigentes} vigente(s) agora";
            });
        }

        private Promocao Selecionada => dgvPromocoes.CurrentRow?.DataBoundItem as Promocao;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmPromocaoEdicao(0);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionada == null) { Mensagem.Informar("Selecione uma promoção na lista."); return; }
            using var form = new FrmPromocaoEdicao(Selecionada.Id);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var promocao = Selecionada;
            if (promocao == null) { Mensagem.Informar("Selecione uma promoção na lista."); return; }
            // Vendas guardam o desconto aplicado em cada item, então apagar a promoção não altera o histórico.
            ExcluirOuInativar($"a promoção \"{promocao.Nome}\"", () => _dao.Excluir(promocao.Id), null);
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPromocoes.Columns[e.ColumnIndex].Name != "colSituacao" || e.Value is not SituacaoPromocao s) return;
            var (texto, fundo, cor) = s switch
            {
                SituacaoPromocao.Vigente => ("Vigente", T.SalviaClara, T.SalviaEscura),
                SituacaoPromocao.Agendada => ("Agendada", T.DouradoClaro, T.DouradoEscuro),
                SituacaoPromocao.Encerrada => ("Encerrada", T.SuperficieAlternada, T.TextoSecundario),
                _ => ("Inativa", T.ArgilaClara, T.ArgilaEscura)
            };
            T.DesenharChipCelula(e, texto, fundo, cor);
        }
    }
}
