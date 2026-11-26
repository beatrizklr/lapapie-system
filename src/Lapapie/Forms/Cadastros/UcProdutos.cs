using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>
    /// Pesquisa e CRUD de produtos, com filtro por categoria e tipo.
    /// Duas visualizações: lista (grade) e vitrine (cartões com imagem).
    /// </summary>
    public partial class UcProdutos : TelaBase
    {
        private readonly ProdutoDAO _dao = new();
        private readonly CategoriaDAO _categoriaDAO = new();
        private bool _carregandoFiltros;
        private List<Produto> _produtos = new();

        public UcProdutos()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Produtos";
        public override string Subtitulo => "Catálogo da loja: materiais escolares, escritório, informática, impressão e livros.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvProdutos);
            dgvProdutos.RowTemplate.Height = 52;
            var imagem = new DataGridViewImageColumn
            {
                Name = "colImagem", HeaderText = "", Width = 64, ImageLayout = DataGridViewImageCellLayout.Zoom,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            imagem.DefaultCellStyle.Padding = new Padding(6, 3, 6, 3);
            imagem.DefaultCellStyle.NullValue = null;
            dgvProdutos.Columns.Add(imagem);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.Nome), "Produto", peso: 220);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.CategoriaNome), "Categoria", 140);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.Marca), "Marca", 120);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.CodigoBarras), "Código", 130);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.PrecoVenda), "Preço", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.QuantidadeDisponivel), "Disponível", 100, T.FormatoInteiro);
            T.AdicionarColuna(dgvProdutos, nameof(Produto.Ativo), "Situação", 100);

            bool podeEditar = Sessao.Pode(Funcionalidade.GerenciarProdutos);
            btnNovo.Visible = btnEditar.Visible = btnExcluir.Visible = podeEditar;
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            _carregandoFiltros = true;
            try
            {
                var itens = new List<KeyValuePair<int?, string>> { new(null, "Todas as categorias") };
                itens.AddRange(_categoriaDAO.ListarAtivas().Select(c => new KeyValuePair<int?, string>(c.Id, c.Nome)));
                cboCategoria.DisplayMember = "Value";
                cboCategoria.ValueMember = "Key";
                cboCategoria.DataSource = itens;
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Carregar categorias");
            }
            finally
            {
                _carregandoFiltros = false;
            }
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                TipoProduto? tipo = abasTipo.IndiceSelecionado switch { 1 => TipoProduto.Simples, 2 => TipoProduto.Kit, _ => null };
                _produtos = _dao.Pesquisar(txtPesquisa.Texto, cboCategoria.SelectedValue as int?, tipo,
                                           somenteAtivos: !chkInativos.Checked, comPromocoes: true);
                dgvProdutos.DataSource = new ListaOrdenavel<Produto>(_produtos);
                lblRodape.Text = Contagem(_produtos.Count, "produto", "produtos") +
                                 $"  ·  {_produtos.Count(p => p.PromocaoVigente != null)} em promoção agora";
                if (flpVitrine.Visible) MontarVitrine();
            });
        }

        /// <summary>Cria os cartões da vitrine (só quando essa visualização está aberta).</summary>
        private void MontarVitrine()
        {
            flpVitrine.SuspendLayout();
            foreach (Control c in flpVitrine.Controls.Cast<Control>().ToList()) c.Dispose();
            flpVitrine.Controls.Clear();
            foreach (var produto in _produtos)
            {
                var cartao = new CartaoProduto
                {
                    Produto = produto,
                    Imagem = ImagensProduto.Obter(produto.Id, produto.CategoriaId, 168),
                    Cursor = Cursors.Default
                };
                cartao.DoubleClick += (s, e) => Editar(produto);
                cartao.AdicionarClicado += (s, e) => Editar(produto);
                flpVitrine.Controls.Add(cartao);
            }
            flpVitrine.ResumeLayout();
        }

        private Produto Selecionado => dgvProdutos.CurrentRow?.DataBoundItem as Produto;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void Filtro_Alterado(object sender, EventArgs e)
        {
            if (!_carregandoFiltros) Carregar();
        }

        private void abasVisualizacao_SelecaoAlterada(object sender, EventArgs e)
        {
            bool vitrine = abasVisualizacao.IndiceSelecionado == 1;
            flpVitrine.Visible = vitrine;
            dgvProdutos.Visible = !vitrine;
            if (vitrine) MontarVitrine();
        }

        private void dgvProdutos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProdutos.Columns[e.ColumnIndex].Name != "colImagem") return;
            if (dgvProdutos.Rows[e.RowIndex].DataBoundItem is Produto p)
                e.Value = ImagensProduto.Obter(p.Id, p.CategoriaId, 96);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmProdutoEdicao(0, TipoProduto.Simples);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void Editar(Produto produto)
        {
            if (!Sessao.Pode(Funcionalidade.GerenciarProdutos)) return;
            using var form = new FrmProdutoEdicao(produto.Id, produto.Tipo);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um produto na lista."); return; }
            Editar(Selecionado);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var produto = Selecionado;
            if (produto == null) { Mensagem.Informar("Selecione um produto na lista."); return; }
            ExcluirOuInativar($"o produto \"{produto.Nome}\"", () => _dao.Excluir(produto.Id), () => _dao.Inativar(produto.Id));
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "produtos", () => Exportacao.DeGrid(dgvProdutos, "Produtos", lblRodape.Text), paisagem: true);

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && Selecionado != null) Editar(Selecionado);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            PintarChipAtivo(e, dgvProdutos);
            // Destaca disponibilidade zerada/baixa
            if (e.RowIndex >= 0 && dgvProdutos.Columns[e.ColumnIndex].Name == "colQuantidadeDisponivel"
                && dgvProdutos.Rows[e.RowIndex].DataBoundItem is Produto p && p.Ativo)
            {
                if (p.QuantidadeDisponivel <= 0)
                    e.CellStyle.ForeColor = T.ArgilaEscura;
                else if (!p.EhKit && p.QuantidadeDisponivel <= p.EstoqueMinimo)
                    e.CellStyle.ForeColor = T.DouradoEscuro;
            }
        }
    }
}
