using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>CRUD de categorias de produtos.</summary>
    public partial class UcCategorias : TelaBase
    {
        private readonly CategoriaDAO _dao = new();

        public UcCategorias()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Categorias";
        public override string Subtitulo => "Organize os produtos em grupos — eles viram filtros no caixa e nos relatórios.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvCategorias);
            T.AdicionarColuna(dgvCategorias, nameof(Categoria.Nome), "Nome", 240);
            T.AdicionarColuna(dgvCategorias, nameof(Categoria.Descricao), "Descrição");
            T.AdicionarColuna(dgvCategorias, nameof(Categoria.QuantidadeProdutos), "Produtos ativos", 140, T.FormatoInteiro);
            T.AdicionarColuna(dgvCategorias, nameof(Categoria.Ativo), "Situação", 110);
            bool podeEditar = Sessao.Pode(Funcionalidade.Categorias);
            btnNovo.Visible = btnEditar.Visible = btnExcluir.Visible = podeEditar;
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var categorias = _dao.Pesquisar(txtPesquisa.Texto, chkSomenteAtivos.Checked);
                dgvCategorias.DataSource = new ListaOrdenavel<Categoria>(categorias);
                lblRodape.Text = Contagem(categorias.Count, "categoria", "categorias");
            });
        }

        private Categoria Selecionada => dgvCategorias.CurrentRow?.DataBoundItem as Categoria;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void chkSomenteAtivos_CheckedChanged(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmCategoriaEdicao(null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionada == null) { Mensagem.Informar("Selecione uma categoria na lista."); return; }
            using var form = new FrmCategoriaEdicao(Selecionada);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var categoria = Selecionada;
            if (categoria == null) { Mensagem.Informar("Selecione uma categoria na lista."); return; }
            ExcluirOuInativar($"a categoria \"{categoria.Nome}\"", () => _dao.Excluir(categoria.Id), () => _dao.Inativar(categoria.Id));
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "categorias", () => Exportacao.DeGrid(dgvCategorias, "Categorias", lblRodape.Text));

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnEditar.Visible) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) => PintarChipAtivo(e, dgvCategorias);
    }
}
