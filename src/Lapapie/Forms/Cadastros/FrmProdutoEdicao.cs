using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>
    /// Cadastro completo de produto: dados gerais, estoque, fornecedores (N:N com
    /// preço de custo) e, se for KIT, a composição do combo.
    /// </summary>
    public partial class FrmProdutoEdicao : FrmDialogoBase
    {
        private readonly ProdutoDAO _dao = new();
        private readonly int _produtoId;
        private readonly TipoProduto _tipoInicial;
        private Produto _produto;
        private string _imagemEscolhida;
        private readonly List<FornecedorProduto> _fornecedores = new();
        private readonly List<KitItem> _componentes = new();
        private List<Produto> _simples = new();

        private bool Novo => _produtoId == 0;

        public FrmProdutoEdicao() : this(0, TipoProduto.Simples) { }

        /// <param name="produtoId">0 para cadastrar.</param>
        /// <param name="tipoInicial">Tipo sugerido no cadastro (ex.: Kit, vindo da tela de kits).</param>
        public FrmProdutoEdicao(int produtoId, TipoProduto tipoInicial)
        {
            InitializeComponent();
            _produtoId = produtoId;
            _tipoInicial = tipoInicial;
        }

        private void FrmProdutoEdicao_Load(object sender, EventArgs e)
        {
            if (EmDesign) return;
            ConfigurarGrids();
            try
            {
                CarregarListas();
                _produto = Novo ? new Produto { Tipo = _tipoInicial, Estoque = new Estoque() } : _dao.ObterPorId(_produtoId);
                if (_produto == null) throw new RegraNegocioException("Produto não encontrado.");
                PreencherCampos();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Abrir produto");
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            MostrarSecao(0);
        }

        private void ConfigurarGrids()
        {
            T.EstilizarGrid(dgvFornecedores);
            dgvFornecedores.RowTemplate.Height = 36;
            T.AdicionarColuna(dgvFornecedores, nameof(FornecedorProduto.FornecedorNome), "Fornecedor");
            T.AdicionarColuna(dgvFornecedores, nameof(FornecedorProduto.CodigoNoFornecedor), "Código", 100);
            T.AdicionarColuna(dgvFornecedores, nameof(FornecedorProduto.PrecoCusto), "Custo", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvFornecedores, nameof(FornecedorProduto.PrazoEntregaDias), "Prazo (dias)", 100, T.FormatoInteiro);

            T.EstilizarGrid(dgvComponentes);
            dgvComponentes.RowTemplate.Height = 36;
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.ProdutoNome), "Produto");
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.Quantidade), "Qtd.", 70, T.FormatoInteiro);
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.PrecoUnitario), "Preço un.", 100, T.FormatoMoeda);
            T.AdicionarColuna(dgvComponentes, nameof(KitItem.EstoqueDisponivel), "Estoque", 80, T.FormatoInteiro);
        }

        private void CarregarListas()
        {
            cboCategoria.DisplayMember = nameof(Categoria.Nome);
            cboCategoria.ValueMember = nameof(Categoria.Id);
            cboCategoria.DataSource = new CategoriaDAO().ListarAtivas();

            cboTipo.DisplayMember = "Value";
            cboTipo.ValueMember = "Key";
            cboTipo.DataSource = EnumBanco.ListarComDescricao<TipoProduto>();

            cboUnidade.Items.AddRange(new object[] { "UN", "CX", "PCT", "RESMA", "KIT", "BL", "CJ" });

            cboFornecedor.DisplayMember = nameof(Fornecedor.NomeExibicao);
            cboFornecedor.ValueMember = nameof(Fornecedor.Id);
            cboFornecedor.DataSource = new FornecedorDAO().ListarAtivos();

            _simples = _dao.Pesquisar(null, tipo: TipoProduto.Simples).Where(p => p.Id != _produtoId).ToList();
            cboComponente.DisplayMember = nameof(Produto.Nome);
            cboComponente.ValueMember = nameof(Produto.Id);
            cboComponente.DataSource = _simples;
        }

        private void PreencherCampos()
        {
            if (!Novo)
            {
                lblTitulo.Text = _produto.EhKit ? "EDITAR KIT" : "EDITAR PRODUTO";
                Text = $"Produto nº {_produto.Id}";
                cboTipo.Enabled = false; // o tipo não muda depois do cadastro (o histórico de estoque depende dele)
            }
            else if (_produto.EhKit)
            {
                lblTitulo.Text = "NOVO KIT";
            }

            txtNome.Texto = _produto.Nome;
            if (_produto.CategoriaId > 0) cboCategoria.SelectedValue = _produto.CategoriaId;
            else if (_produto.EhKit)
            {
                // Sugere a categoria de kits, se existir
                var kits = ((List<Categoria>)cboCategoria.DataSource).FirstOrDefault(c => c.Nome.Contains("Kit", StringComparison.OrdinalIgnoreCase));
                if (kits != null) cboCategoria.SelectedValue = kits.Id;
            }
            cboTipo.SelectedValue = _produto.Tipo;
            txtCodigoBarras.Texto = _produto.CodigoBarras;
            txtMarca.Texto = _produto.Marca;
            cboUnidade.Text = _produto.EhKit && Novo ? "KIT" : _produto.UnidadeMedida;
            if (_produto.PrecoVenda > 0) txtPreco.ValorDecimal = _produto.PrecoVenda;
            txtDescricao.Texto = _produto.Descricao;
            chkAtivo.Checked = _produto.Ativo;
            if (_produto.PromocaoVigente != null)
                lblPromocao.Text = $"Em promoção agora: {_produto.PromocaoVigente.Nome} ({_produto.PromocaoVigente.DescricaoDesconto}) → {Formatacao.Moeda(_produto.PrecoPromocional)}";

            var estoque = _produto.Estoque ?? new Estoque();
            lblSaldo.Text = $"{estoque.Quantidade} {_produto.UnidadeMedida}";
            nudMinimo.Value = Math.Min(nudMinimo.Maximum, estoque.EstoqueMinimo);
            txtLocalizacao.Texto = estoque.Localizacao;
            lblEstoqueInicial.Visible = nudEstoqueInicial.Visible = Novo;
            lblSaldoTitulo.Visible = lblSaldo.Visible = !Novo;

            _fornecedores.AddRange(_produto.Fornecedores);
            _componentes.AddRange(_produto.ItensKit);
            AtualizarFornecedores();
            AtualizarComponentes();
            picImagem.Image = ImagensProduto.Obter(_produto.Id, _produto.CategoriaId, 200);
            AtualizarResumo();
        }

        // ---------------- Navegação entre seções ----------------

        private TipoProduto TipoSelecionado => cboTipo.SelectedValue is TipoProduto t ? t : TipoProduto.Simples;

        private void abasSecoes_SelecaoAlterada(object sender, EventArgs e) => MostrarSecao(abasSecoes.IndiceSelecionado);

        private void MostrarSecao(int indice)
        {
            bool kit = TipoSelecionado == TipoProduto.Kit;
            // Kit não tem estoque próprio; produto simples não tem composição.
            if (indice == 1 && kit) indice = 3;
            if (indice == 3 && !kit) indice = 1;
            if (abasSecoes.IndiceSelecionado != indice) { abasSecoes.IndiceSelecionado = indice; return; }

            pnlDados.Visible = indice == 0;
            pnlEstoque.Visible = indice == 1;
            pnlFornecedores.Visible = indice == 2;
            pnlKit.Visible = indice == 3;
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool kit = TipoSelecionado == TipoProduto.Kit;
            abasSecoes.DefinirItens(new[]
            {
                ("Dados gerais", (object)0),
                (kit ? "Estoque (componentes)" : "Estoque", 1),
                ("Fornecedores", 2),
                ("Composição do kit", 3)
            }, abasSecoes.IndiceSelecionado);
            if (kit && string.IsNullOrEmpty(cboUnidade.Text)) cboUnidade.Text = "KIT";
            AtualizarResumo();
        }

        // ---------------- Imagem ----------------

        private void btnEscolherImagem_Click(object sender, EventArgs e)
        {
            using var dialogo = new OpenFileDialog { Filter = "Imagens|*.png;*.jpg;*.jpeg;*.bmp", Title = "Imagem do produto" };
            if (dialogo.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                using var memoria = new MemoryStream(File.ReadAllBytes(dialogo.FileName));
                picImagem.Image = new Bitmap(Image.FromStream(memoria));
                _imagemEscolhida = dialogo.FileName;
            }
            catch (Exception)
            {
                Mensagem.Alerta("Não foi possível abrir esta imagem. Escolha um arquivo PNG ou JPG.");
            }
        }

        private void btnGerarCodigo_Click(object sender, EventArgs e)
        {
            try { txtCodigoBarras.Texto = _dao.SugerirCodigoBarras(); }
            catch (Exception ex) { Mensagem.TratarErro(ex, "Gerar código"); }
        }

        // ---------------- Fornecedores (N:N) ----------------

        private void btnAdicionarFornecedor_Click(object sender, EventArgs e)
        {
            if (cboFornecedor.SelectedItem is not Fornecedor fornecedor) return;
            if (!Formatacao.TentarLerDecimal(txtCusto.Texto, out var custo) || custo < 0)
            {
                txtCusto.ComErro = true;
                Mensagem.Alerta("Informe o preço de custo deste fornecedor.");
                return;
            }
            var existente = _fornecedores.FirstOrDefault(f => f.FornecedorId == fornecedor.Id);
            if (existente == null)
            {
                existente = new FornecedorProduto { FornecedorId = fornecedor.Id, FornecedorNome = fornecedor.NomeExibicao };
                _fornecedores.Add(existente);
            }
            existente.CodigoNoFornecedor = txtCodigoFornecedor.Texto.Trim();
            existente.PrecoCusto = custo;
            existente.PrazoEntregaDias = (int)nudPrazo.Value;
            txtCodigoFornecedor.Texto = txtCusto.Texto = "";
            AtualizarFornecedores();
        }

        private void btnRemoverFornecedor_Click(object sender, EventArgs e)
        {
            if (dgvFornecedores.CurrentRow?.DataBoundItem is FornecedorProduto fp)
            {
                _fornecedores.Remove(fp);
                AtualizarFornecedores();
            }
        }

        private void AtualizarFornecedores()
        {
            dgvFornecedores.DataSource = null;
            dgvFornecedores.DataSource = _fornecedores.OrderBy(f => f.PrecoCusto).ToList();
            AtualizarResumo();
        }

        // ---------------- Composição do kit ----------------

        private void btnAdicionarComponente_Click(object sender, EventArgs e)
        {
            if (cboComponente.SelectedItem is not Produto componente) return;
            var existente = _componentes.FirstOrDefault(c => c.ProdutoId == componente.Id);
            if (existente == null)
            {
                existente = new KitItem
                {
                    ProdutoId = componente.Id,
                    ProdutoNome = componente.Nome,
                    PrecoUnitario = componente.PrecoVenda,
                    EstoqueDisponivel = componente.QuantidadeDisponivel
                };
                _componentes.Add(existente);
            }
            existente.Quantidade = (int)nudQtdComponente.Value;
            AtualizarComponentes();
        }

        private void btnRemoverComponente_Click(object sender, EventArgs e)
        {
            if (dgvComponentes.CurrentRow?.DataBoundItem is KitItem item)
            {
                _componentes.Remove(item);
                AtualizarComponentes();
            }
        }

        private void AtualizarComponentes()
        {
            dgvComponentes.DataSource = null;
            dgvComponentes.DataSource = _componentes.ToList();
            if (_componentes.Count == 0)
            {
                lblResumoKit.Text = "Adicione os produtos que formam o kit.";
            }
            else
            {
                decimal avulso = _componentes.Sum(c => c.ValorAvulso);
                int disponivel = _componentes.Min(c => c.EstoqueDisponivel / Math.Max(1, c.Quantidade));
                lblResumoKit.Text = $"Itens avulsos: {Formatacao.Moeda(avulso)}  ·  Dá para montar {disponivel} kit(s) com o estoque atual.";
            }
            AtualizarResumo();
        }

        // ---------------- Resumo lateral ----------------

        private void Valores_Alterados(object sender, EventArgs e) => AtualizarResumo();

        private void AtualizarResumo()
        {
            if (_produto == null) return;
            Formatacao.TentarLerDecimal(txtPreco.Texto, out var preco);
            var linhas = new List<string> { "Preço de venda", Formatacao.Moeda(preco) };
            lblMargem.Text = string.Empty;
            if (TipoSelecionado == TipoProduto.Kit && _componentes.Count > 0)
            {
                decimal avulso = _componentes.Sum(c => c.ValorAvulso);
                linhas.Add("");
                linhas.Add($"Avulso: {Formatacao.Moeda(avulso)}");
                linhas.Add(avulso > preco ? $"Cliente economiza {Formatacao.Moeda(avulso - preco)}" : "Kit sem desconto em relação ao avulso");
            }
            else if (_fornecedores.Count > 0 && preco > 0)
            {
                decimal menorCusto = _fornecedores.Min(f => f.PrecoCusto);
                decimal margem = (preco - menorCusto) / preco * 100;
                linhas.Add("");
                linhas.Add($"Menor custo: {Formatacao.Moeda(menorCusto)}");
                linhas.Add($"Margem: {margem:0.0}%");
                lblMargem.Text = $"Com o menor custo ({Formatacao.Moeda(menorCusto)}), a margem bruta é de {margem:0.0}% sobre o preço de venda.";
            }
            lblResumo.Text = string.Join(Environment.NewLine, linhas);
        }

        // ---------------- Salvar ----------------

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _produto.Nome = txtNome.Texto.Trim();
            _produto.CategoriaId = cboCategoria.SelectedValue is int categoria ? categoria : 0;
            _produto.Tipo = TipoSelecionado;
            _produto.CodigoBarras = txtCodigoBarras.Texto.Trim();
            _produto.Marca = txtMarca.Texto.Trim();
            _produto.UnidadeMedida = cboUnidade.Text.Trim().ToUpperInvariant();
            _produto.PrecoVenda = Formatacao.TentarLerDecimal(txtPreco.Texto, out var preco) ? preco : 0;
            _produto.Descricao = txtDescricao.Texto.Trim();
            _produto.Ativo = chkAtivo.Checked;
            _produto.Fornecedores = _fornecedores.ToList();
            _produto.ItensKit = _produto.EhKit ? _componentes.ToList() : new List<KitItem>();
            _produto.Estoque = _produto.EhKit ? null : new Estoque
            {
                ProdutoId = _produto.Id,
                EstoqueMinimo = (int)nudMinimo.Value,
                Localizacao = txtLocalizacao.Texto.Trim()
            };

            var erros = _produto.Validar();
            if (!ValidarOuAvisar(erros, (txtNome, "nome"), (txtPreco, "preço"), (txtCodigoBarras, "código")))
            {
                if (erros.Any(x => x.Contains("kit", StringComparison.OrdinalIgnoreCase))) MostrarSecao(3);
                else if (erros.Any(x => x.Contains("custo"))) MostrarSecao(2);
                else MostrarSecao(0);
                return;
            }

            try
            {
                if (Novo) _dao.Inserir(_produto, (int)nudEstoqueInicial.Value, Sessao.UsuarioId);
                else _dao.Atualizar(_produto);

                if (_imagemEscolhida != null)
                    ImagensProduto.Salvar(_produto.Id, _imagemEscolhida);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (ex is BancoDadosException { Tipo: TipoErroBanco.RegistroDuplicado }) txtCodigoBarras.ComErro = true;
                Mensagem.TratarErro(ex, "Salvar produto");
            }
        }
    }
}
