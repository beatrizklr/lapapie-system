using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>
    /// Combos e kits: lista a disponibilidade calculada pela view
    /// vw_disponibilidade_kits e mostra a composição de cada kit.
    /// </summary>
    public partial class UcKits : TelaBase
    {
        private readonly EstoqueDAO _estoqueDAO = new();
        private readonly ProdutoDAO _produtoDAO = new();
        private List<DisponibilidadeKit> _kits = new();

        public UcKits()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Kits e combos";
        public override string Subtitulo => "Conjuntos vendidos juntos. O componente mais escasso define quantos kits dá para montar.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvKits);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.Kit), "Kit");
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.QuantidadeComponentes), "Itens", 70, T.FormatoInteiro);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.PrecoVenda), "Preço", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.Economia), "Economia", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.QuantidadeDisponivel), "Disponíveis", 110, T.FormatoInteiro);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.Ativo), "Situação", 100);

            T.EstilizarGrid(dgvComponentes);
            dgvComponentes.RowTemplate.Height = 34;
            dgvComponentes.ColumnHeadersHeight = 34;
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.ProdutoNome), "Produto");
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.Quantidade), "Qtd.", 56, T.FormatoInteiro);
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.EstoqueDisponivel), "Estoque", 74, T.FormatoInteiro);

            bool podeEditar = Sessao.Pode(Funcionalidade.GerenciarProdutos);
            btnNovo.Visible = btnEditar.Visible = podeEditar;
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                _kits = _estoqueDAO.ListarKits(somenteAtivos: !chkInativos.Checked);
                var filtro = txtPesquisa.Texto.Trim();
                var visiveis = _kits.Where(k => filtro.Length == 0 || k.Kit.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)).ToList();
                dgvKits.DataSource = new ListaOrdenavel<DisponibilidadeKit>(visiveis);
                lblRodape.Text = Contagem(visiveis.Count, "kit", "kits");
                MostrarDetalhe();
            });
        }

        private DisponibilidadeKit Selecionado => dgvKits.CurrentRow?.DataBoundItem as DisponibilidadeKit;

        private void MostrarDetalhe()
        {
            var kit = Selecionado;
            cardDetalhe.Visible = kit != null;
            if (kit == null) return;
            try
            {
                var componentes = _produtoDAO.ListarComponentes(kit.KitId);
                dgvComponentes.DataSource = componentes;
                picKit.Image = ImagensProduto.Obter(kit.KitId, 9, 140);
                lblNomeKit.Text = kit.Kit;
                lblPrecoKit.Text = Formatacao.Moeda(kit.PrecoVenda);
                lblEconomia.Text = kit.Economia > 0
                    ? $"Avulso sairia {Formatacao.Moeda(kit.ValorAvulso)} — economia de {Formatacao.Moeda(kit.Economia)}"
                    : $"Avulso: {Formatacao.Moeda(kit.ValorAvulso)}";
                var limitante = componentes.OrderBy(c => c.EstoqueDisponivel / Math.Max(1, c.Quantidade)).FirstOrDefault();
                lblDisponibilidade.Text = kit.QuantidadeDisponivel > 0
                    ? $"● {kit.QuantidadeDisponivel} kit(s) disponível(is).\nQuem limita: {limitante?.ProdutoNome}."
                    : $"● Indisponível: falta {limitante?.ProdutoNome}.";
                lblDisponibilidade.ForeColor = kit.QuantidadeDisponivel > 0 ? T.SalviaEscura : T.ArgilaEscura;
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Detalhe do kit");
            }
        }

        private void dgvKits_SelectionChanged(object sender, EventArgs e) => MostrarDetalhe();
        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void chkInativos_CheckedChanged(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmProdutoEdicao(0, TipoProduto.Kit);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um kit na lista."); return; }
            using var form = new FrmProdutoEdicao(Selecionado.KitId, TipoProduto.Kit);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnEditar.Visible) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) => PintarChipAtivo(e, dgvKits);
    }
}
