using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro/edição de usuário, com perfil de acesso e senha.</summary>
    public partial class FrmUsuarioEdicao : FrmDialogoBase
    {
        private readonly UsuarioDAO _dao = new();
        private readonly Usuario _usuario;
        private readonly bool _novo;

        public FrmUsuarioEdicao() : this(null) { }

        public FrmUsuarioEdicao(Usuario usuario)
        {
            InitializeComponent();
            _novo = usuario == null;
            _usuario = usuario == null ? new Usuario()
                : new Usuario { Id = usuario.Id, Nome = usuario.Nome, Login = usuario.Login, Perfil = usuario.Perfil, Ativo = usuario.Ativo };
        }

        private void FrmUsuarioEdicao_Load(object sender, EventArgs e)
        {
            cboPerfil.DisplayMember = "Value";
            cboPerfil.ValueMember = "Key";
            cboPerfil.DataSource = EnumBanco.ListarComDescricao<PerfilUsuario>();
            cboPerfil.SelectedValue = _usuario.Perfil;

            if (_novo) return;
            lblTitulo.Text = "EDITAR USUÁRIO";
            lblSenha.Text = "Nova senha";
            lblConfirmacao.Text = "Confirmar nova senha";
            lblDicaSenha.Text = "Deixe a senha em branco para manter a atual.";
            txtNome.Texto = _usuario.Nome;
            txtLogin.Texto = _usuario.Login;
            chkAtivo.Checked = _usuario.Ativo;
            if (_usuario.Id == Sessao.UsuarioId)
            {
                // Evita que o administrador tire o próprio acesso sem querer.
                chkAtivo.Enabled = false;
                cboPerfil.Enabled = false;
            }
        }

        private void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPerfil.SelectedValue is not PerfilUsuario perfil) return;
            lblDescricaoPerfil.Text = perfil switch
            {
                PerfilUsuario.Admin => "Acesso completo, incluindo usuários, promoções, relatórios e cancelamento de vendas.",
                PerfilUsuario.Vendedor => "Caixa, histórico de vendas e clientes. Consulta produtos e estoque.",
                _ => "Produtos, categorias, fornecedores, estoque, movimentações e pedidos de compra."
            };
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _usuario.Nome = txtNome.Texto.Trim();
            _usuario.Login = txtLogin.Texto.Trim();
            _usuario.Perfil = (PerfilUsuario)cboPerfil.SelectedValue;
            _usuario.Ativo = chkAtivo.Checked;

            var erros = _usuario.Validar(txtSenha.Texto, exigirSenha: _novo);
            if (txtSenha.Texto != txtConfirmacao.Texto) erros.Add("A confirmação não confere com a senha.");
            if (!ValidarOuAvisar(erros, (txtNome, "nome"), (txtLogin, "login"), (txtSenha, "senha"), (txtConfirmacao, "confirmação")))
                return;

            try
            {
                if (_novo) _dao.Inserir(_usuario, txtSenha.Texto);
                else _dao.Atualizar(_usuario, txtSenha.Texto);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (ex is BancoDadosException { Tipo: TipoErroBanco.RegistroDuplicado }) txtLogin.ComErro = true;
                Mensagem.TratarErro(ex, "Salvar usuário");
            }
        }
    }
}
