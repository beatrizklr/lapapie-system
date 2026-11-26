using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>CRUD de fornecedores.</summary>
    public partial class UcFornecedores : TelaBase
    {
        private readonly FornecedorDAO _dao = new();

        public UcFornecedores()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Fornecedores";
        public override string Subtitulo => "Empresas que abastecem a loja. Os custos por produto ficam no cadastro de produtos.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvFornecedores);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.NomeExibicao), "Fornecedor", peso: 120);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.RazaoSocial), "Razão social", peso: 170);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.CnpjFormatado), "CNPJ", 160);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.NomeContato), "Contato", 150);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.Telefone), "Telefone", 130);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.CidadeUf), "Cidade", 140);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.QuantidadeProdutos), "Produtos", 90, T.FormatoInteiro);
            T.AdicionarColuna(dgvFornecedores, nameof(Fornecedor.Ativo), "Situação", 100);
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var fornecedores = _dao.Pesquisar(txtPesquisa.Texto, chkSomenteAtivos.Checked);
                dgvFornecedores.DataSource = new ListaOrdenavel<Fornecedor>(fornecedores);
                lblRodape.Text = Contagem(fornecedores.Count, "fornecedor", "fornecedores");
            });
        }

        private Fornecedor Selecionado => dgvFornecedores.CurrentRow?.DataBoundItem as Fornecedor;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void chkSomenteAtivos_CheckedChanged(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmFornecedorEdicao(null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um fornecedor na lista."); return; }
            using var form = new FrmFornecedorEdicao(Selecionado);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var fornecedor = Selecionado;
            if (fornecedor == null) { Mensagem.Informar("Selecione um fornecedor na lista."); return; }
            ExcluirOuInativar($"o fornecedor \"{fornecedor.NomeExibicao}\"", () => _dao.Excluir(fornecedor.Id), () => _dao.Inativar(fornecedor.Id));
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "fornecedores", () => Exportacao.DeGrid(dgvFornecedores, "Fornecedores", lblRodape.Text), paisagem: true);

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) => PintarChipAtivo(e, dgvFornecedores);
    }
}
