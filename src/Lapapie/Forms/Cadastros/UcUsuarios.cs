using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>CRUD de usuários (somente administradores).</summary>
    public partial class UcUsuarios : TelaBase
    {
        private readonly UsuarioDAO _dao = new();

        public UcUsuarios()
        {
            InitializeComponent();
            if (!EmDesign) ConfigurarGrid();
        }

        public override string Titulo => "Usuários";
        public override string Subtitulo => "Quem acessa o sistema e o que cada perfil pode fazer.";

        private void ConfigurarGrid()
        {
            T.EstilizarGrid(dgvUsuarios);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.Nome), "Nome", peso: 160);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.Login), "Login", 180);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.PerfilDescricao), "Perfil", 150);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.UltimoLogin), "Último acesso", 150, T.FormatoDataHora);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.CriadoEm), "Cadastrado em", 130, T.FormatoData);
            T.AdicionarColuna(dgvUsuarios, nameof(Usuario.Ativo), "Situação", 100);
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var usuarios = _dao.Pesquisar(txtPesquisa.Texto, chkSomenteAtivos.Checked);
                dgvUsuarios.DataSource = new ListaOrdenavel<Usuario>(usuarios);
                lblRodape.Text = Contagem(usuarios.Count, "usuário", "usuários");
            });
        }

        private Usuario Selecionado => dgvUsuarios.CurrentRow?.DataBoundItem as Usuario;

        private void txtPesquisa_Pesquisar(object sender, EventArgs e) => Carregar();
        private void chkSomenteAtivos_CheckedChanged(object sender, EventArgs e) => Carregar();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var form = new FrmUsuarioEdicao(null);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Selecionado == null) { Mensagem.Informar("Selecione um usuário na lista."); return; }
            using var form = new FrmUsuarioEdicao(Selecionado);
            if (form.ShowDialog(this) == DialogResult.OK) Carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var usuario = Selecionado;
            if (usuario == null) { Mensagem.Informar("Selecione um usuário na lista."); return; }
            if (usuario.Id == Sessao.UsuarioId)
            {
                Mensagem.Alerta("Você não pode excluir o próprio usuário enquanto está conectado.");
                return;
            }
            ExcluirOuInativar($"o usuário \"{usuario.Login}\"", () => _dao.Excluir(usuario.Id), () => _dao.Inativar(usuario.Id));
        }

        private void btnExportar_Click(object sender, EventArgs e) =>
            Exportacao.MostrarMenu(btnExportar, "usuarios", () => Exportacao.DeGrid(dgvUsuarios, "Usuários", lblRodape.Text));

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, e);
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) => PintarChipAtivo(e, dgvUsuarios);
    }
}
