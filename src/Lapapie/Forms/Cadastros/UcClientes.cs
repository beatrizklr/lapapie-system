using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>CRUD e pesquisa de clientes.</summary>
    public partial class UcClientes : TelaBase
    {
        private readonly ClienteDAO _dao = new();

        public UcClientes()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Clientes";
        public override string Subtitulo => "Quem compra na Lapapiê — pesquise por nome, CPF, e-mail ou telefone.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvClientes);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.Nome), "Nome", peso: 180);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.CpfFormatado), "CPF", 140);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.TelefoneFormatado), "Telefone", 150);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.Email), "E-mail", peso: 150);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.CidadeUf), "Cidade", 140);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.QuantidadeCompras), "Compras", 90, T.FormatoInteiro);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.UltimaCompra), "Última compra", 120, T.FormatoData);
            T.AdicionarColuna(dgvClientes, nameof(Cliente.Ativo), "Situação", 100);
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var clientes = _dao.Pesquisar(txtPesquisa.Texto, chkSomenteAtivos.Checked);
                dgvClientes.DataSource = new ListaOrdenavel<Cliente>(clientes);
                lblRodape.Text = Contagem(clientes.Count, "cliente", "clientes");
            });
        }

        private Cliente Selecionado => dgvClientes.CurrentRow?.DataBoundItem as Cliente;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void chkSomenteAtivos_CheckedChanged(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmClienteEdicao(null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um cliente na lista."); return; }
            Cliente cliente = null;
            if (!Executar(() => cliente = _dao.ObterPorId(Selecionado.Id))) return;
            using var form = new FrmClienteEdicao(cliente);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var cliente = Selecionado;
            if (cliente == null) { Mensagem.Informar("Selecione um cliente na lista."); return; }
            ExcluirOuInativar($"o cliente \"{cliente.Nome}\"", () => _dao.Excluir(cliente.Id), () => _dao.Inativar(cliente.Id));
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "clientes", () => Exportacao.DeGrid(dgvClientes, "Clientes", lblRodape.Text), paisagem: true);

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) => PintarChipAtivo(e, dgvClientes);
    }
}
