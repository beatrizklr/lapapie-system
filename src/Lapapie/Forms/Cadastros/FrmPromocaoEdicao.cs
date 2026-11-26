using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro de promoção com seleção dos produtos participantes.</summary>
    public partial class FrmPromocaoEdicao : FrmDialogoBase
    {
        private readonly PromocaoDAO _dao = new();
        private readonly int _promocaoId;
        private Promocao _promocao;
        private List<Produto> _produtos = new();
        /// <summary>Ids marcados — mantidos mesmo quando o filtro esconde o produto.</summary>
        private readonly HashSet<int> _selecionados = new();
        private bool _preenchendoLista;

        public FrmPromocaoEdicao() : this(0) { }

        public FrmPromocaoEdicao(int promocaoId)
        {
            InitializeComponent();
            _promocaoId = promocaoId;
        }

        private void FrmPromocaoEdicao_Load(object sender, EventArgs e)
        {
            if (EmDesign) return;
            try
            {
                cboTipoDesconto.DisplayMember = "Value";
                cboTipoDesconto.ValueMember = "Key";
                cboTipoDesconto.DataSource = EnumBanco.ListarComDescricao<TipoDesconto>();

                var categorias = new List<KeyValuePair<int, string>> { new(0, "Todas as categorias") };
                categorias.AddRange(new CategoriaDAO().ListarAtivas().Select(c => new KeyValuePair<int, string>(c.Id, c.Nome)));
                cboFiltroCategoria.DisplayMember = "Value";
                cboFiltroCategoria.ValueMember = "Key";
                cboFiltroCategoria.DataSource = categorias;

                _produtos = new ProdutoDAO().Pesquisar(null);
                _promocao = _promocaoId == 0
                    ? new Promocao { DataInicio = DateTime.Today, DataFim = DateTime.Today.AddDays(8).AddSeconds(-1) }
                    : _dao.ObterPorId(_promocaoId);
                if (_promocao == null) throw new RegraNegocioException("Promoção não encontrada.");
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Abrir promoção");
                Close();
                return;
            }

            if (_promocaoId > 0) lblTitulo.Text = "EDITAR PROMOÇÃO";
            txtNome.Texto = _promocao.Nome;
            txtDescricao.Texto = _promocao.Descricao;
            cboTipoDesconto.SelectedValue = _promocao.TipoDesconto;
            if (_promocao.ValorDesconto > 0) txtValor.ValorDecimal = _promocao.ValorDesconto;
            dtpInicio.Value = _promocao.DataInicio;
            dtpFim.Value = _promocao.DataFim;
            chkAtivo.Checked = _promocao.Ativo;
            foreach (var id in _promocao.ProdutoIds) _selecionados.Add(id);
            PreencherLista();
            AtualizarPreview();
        }

        /// <summary>Mostra os produtos que passam no filtro, mantendo as marcações.</summary>
        private void PreencherLista()
        {
            _preenchendoLista = true;
            int categoria = cboFiltroCategoria.SelectedValue is int c ? c : 0;
            string texto = txtFiltroProduto.Texto.Trim();
            clbProdutos.BeginUpdate();
            clbProdutos.Items.Clear();
            foreach (var p in _produtos.Where(p => (categoria == 0 || p.CategoriaId == categoria) &&
                                                  (texto.Length == 0 || p.Nome.Contains(texto, StringComparison.CurrentCultureIgnoreCase))))
                clbProdutos.Items.Add(p, _selecionados.Contains(p.Id));
            clbProdutos.EndUpdate();
            _preenchendoLista = false;
            AtualizarContagem();
        }

        private void FiltroProdutos_Alterado(object sender, EventArgs e)
        {
            if (_produtos.Count > 0) PreencherLista();
        }

        private void clbProdutos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_preenchendoLista) return;
            var produto = (Produto)clbProdutos.Items[e.Index];
            if (e.NewValue == CheckState.Checked) _selecionados.Add(produto.Id); else _selecionados.Remove(produto.Id);
            BeginInvoke(new Action(() => { AtualizarContagem(); AtualizarPreview(); }));
        }

        private void MarcarVisiveis(bool marcar)
        {
            for (int i = 0; i < clbProdutos.Items.Count; i++)
                clbProdutos.SetItemChecked(i, marcar);
        }

        private void btnMarcarVisiveis_Click(object sender, EventArgs e) => MarcarVisiveis(true);
        private void btnDesmarcarVisiveis_Click(object sender, EventArgs e) => MarcarVisiveis(false);

        private void AtualizarContagem() =>
            lblSelecionados.Text = $"{_selecionados.Count} produto(s) selecionado(s)";

        private void Valores_Alterados(object sender, EventArgs e) => AtualizarPreview();

        /// <summary>Exemplo do efeito do desconto num produto selecionado.</summary>
        private void AtualizarPreview()
        {
            if (_promocao == null) return;
            var tipo = cboTipoDesconto.SelectedValue is TipoDesconto t ? t : TipoDesconto.Percentual;
            Formatacao.TentarLerDecimal(txtValor.Texto, out var valor);
            var simulada = new Promocao { TipoDesconto = tipo, ValorDesconto = valor, DataInicio = dtpInicio.Value, DataFim = dtpFim.Value, Ativo = chkAtivo.Checked };
            var exemplo = _produtos.FirstOrDefault(p => _selecionados.Contains(p.Id));
            string situacao = simulada.Situacao switch
            {
                SituacaoPromocao.Vigente => "Vigente agora",
                SituacaoPromocao.Agendada => $"Começa em {dtpInicio.Value:dd/MM 'às' HH:mm}",
                SituacaoPromocao.Encerrada => "Período já encerrado",
                _ => "Inativa"
            };
            lblPreview.Text = exemplo == null || valor <= 0
                ? $"{situacao}.\nSelecione produtos e informe o desconto para ver um exemplo."
                : $"{situacao}.\nExemplo: {exemplo.Nome}\nde {Formatacao.Moeda(exemplo.PrecoVenda)} por " +
                  $"{Formatacao.Moeda(exemplo.PrecoVenda - simulada.CalcularDesconto(exemplo.PrecoVenda, 1))} ({simulada.DescricaoDesconto} de desconto)";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _promocao.Nome = txtNome.Texto.Trim();
            _promocao.Descricao = txtDescricao.Texto.Trim();
            _promocao.TipoDesconto = (TipoDesconto)cboTipoDesconto.SelectedValue;
            _promocao.ValorDesconto = Formatacao.TentarLerDecimal(txtValor.Texto, out var valor) ? valor : 0;
            _promocao.DataInicio = dtpInicio.Value;
            _promocao.DataFim = dtpFim.Value;
            _promocao.Ativo = chkAtivo.Checked;
            _promocao.ProdutoIds = _selecionados.ToList();

            if (!ValidarOuAvisar(_promocao.Validar(), (txtNome, "nome"), (txtValor, "desconto"))) return;
            try
            {
                if (_promocaoId == 0) _dao.Inserir(_promocao); else _dao.Atualizar(_promocao);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Salvar promoção");
            }
        }
    }
}
