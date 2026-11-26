using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>Saldos atuais de estoque, com entrada/saída manual e parâmetros (mínimo e localização).</summary>
    public partial class UcEstoque : TelaBase
    {
        private readonly EstoqueDAO _dao = new();
        private bool _carregandoFiltros;

        public UcEstoque()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Controle de estoque";
        public override string Subtitulo => "Saldo de cada produto. Toda entrada e saída fica registrada no histórico.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvEstoque);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.CodigoBarras), "Código", 130);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.ProdutoNome), "Produto", peso: 220);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.CategoriaNome), "Categoria", 140);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.Localizacao), "Localização", peso: 120);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.EstoqueMinimo), "Mínimo", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.Quantidade), "Saldo", 80, T.FormatoInteiro);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.UnidadeMedida), "Un.", 60);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.Situacao), "Situação", 110);
            T.AdicionarColuna(dgvEstoque, nameof(Estoque.AtualizadoEm), "Atualizado em", 140, T.FormatoDataHora);

            bool podeMovimentar = Sessao.Pode(Funcionalidade.MovimentarEstoque);
            btnEntrada.Visible = btnSaida.Visible = btnParametros.Visible = podeMovimentar;

            _carregandoFiltros = true;
            try
            {
                var itens = new List<KeyValuePair<int?, string>> { new(null, "Todas as categorias") };
                itens.AddRange(new CategoriaDAO().ListarAtivas().Select(c => new KeyValuePair<int?, string>(c.Id, c.Nome)));
                cboCategoria.DisplayMember = "Value";
                cboCategoria.ValueMember = "Key";
                cboCategoria.DataSource = itens;
            }
            catch (Exception ex) { Mensagem.TratarErro(ex, "Carregar categorias"); }
            finally { _carregandoFiltros = false; }
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                SituacaoEstoque? situacao = abasSituacao.IndiceSelecionado switch
                {
                    1 => SituacaoEstoque.Baixo,
                    2 => SituacaoEstoque.Zerado,
                    3 => SituacaoEstoque.Normal,
                    _ => null
                };
                var saldos = _dao.ListarSaldos(txtPesquisa.Texto, cboCategoria.SelectedValue as int?, situacao);
                dgvEstoque.DataSource = new ListaOrdenavel<Estoque>(saldos);
                lblRodape.Text = Contagem(saldos.Count, "produto", "produtos") +
                                 $"  ·  {Formatacao.Numero(saldos.Sum(s => s.Quantidade))} unidades em estoque" +
                                 $"  ·  {saldos.Count(s => s.Situacao != SituacaoEstoque.Normal)} abaixo do mínimo";
            });
        }

        private Estoque Selecionado => dgvEstoque.CurrentRow?.DataBoundItem as Estoque;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();

        private void Filtro_Alterado(object sender, EventArgs e)
        {
            if (!_carregandoFiltros) Carregar();
        }

        private void Movimentar(TipoMovimentacao tipo)
        {
            using var form = new FrmMovimentacao(Selecionado?.ProdutoId ?? 0, tipo);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Carregar();
                Principal?.AtualizarContadorEstoqueBaixo();
            }
        }

        private void btnEntrada_Click(object sender, EventArgs e) => Movimentar(TipoMovimentacao.Entrada);
        private void btnSaida_Click(object sender, EventArgs e) => Movimentar(TipoMovimentacao.Saida);

        private void btnParametros_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um produto na lista."); return; }
            using var form = new FrmEstoqueParametros(Selecionado);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Carregar();
                Principal?.AtualizarContadorEstoqueBaixo();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "estoque", () => Exportacao.DeGrid(dgvEstoque, "Posição de estoque", lblRodape.Text), paisagem: true);

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnParametros.Visible) btnParametros_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvEstoque.Columns[e.ColumnIndex].Name != "colSituacao" || e.Value is not SituacaoEstoque s) return;
            switch (s)
            {
                case SituacaoEstoque.Zerado: T.DesenharChipCelula(e, "Zerado", T.ArgilaClara, T.ArgilaEscura); break;
                case SituacaoEstoque.Baixo: T.DesenharChipCelula(e, "Baixo", T.DouradoClaro, T.DouradoEscuro); break;
                default: T.DesenharChipCelula(e, "Normal", T.SalviaClara, T.SalviaEscura); break;
            }
        }
    }
}
