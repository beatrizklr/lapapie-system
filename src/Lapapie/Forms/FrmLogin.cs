using Lapapie.Conexao;
using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms
{
    /// <summary>Tela de login. Fecha com DialogResult.OK quando a autenticação dá certo.</summary>
    public partial class FrmLogin : Form
    {
        private readonly UsuarioDAO _usuarioDAO = new();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch (Exception) { }
            picLogo.Image = ImagensProduto.Recurso(Path.Combine("Marca", "assinatura-horizontal-colorido.png"));
            pnlIlustracao.Imagem = ImagensProduto.Recurso("hero.png");
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            txtLogin.Focus();
            VerificarConexao();
        }

        /// <summary>Mostra, no rodapé, se o banco está acessível.</summary>
        private void VerificarConexao()
        {
            lblStatusBanco.Text = "● Verificando conexão...";
            lblStatusBanco.ForeColor = T.TextoSecundario;
            Refresh();
            bool ok = ConexaoBD.TestarConexao(ConexaoBD.Configuracao, out var erro);
            var cfg = ConexaoBD.Configuracao;
            lblStatusBanco.Text = ok
                ? $"● Conectado ao MySQL em {cfg.Servidor}:{cfg.Porta} (banco {cfg.Banco})"
                : "● Sem conexão com o banco — " + erro;
            lblStatusBanco.ForeColor = ok ? T.SalviaEscura : T.ArgilaEscura;
            lblStatusBanco.MaximumSize = new Size(440, 0);
        }

        private void btnEntrar_Click(object sender, EventArgs e) => Entrar();

        private void Campos_TeclaPressionada(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            if (sender == txtLogin && txtSenha.Texto.Length == 0) txtSenha.Focus();
            else Entrar();
        }

        private void Entrar()
        {
            lblErro.Text = string.Empty;
            txtLogin.ComErro = txtSenha.ComErro = false;

            // Validação dos campos obrigatórios
            if (Validacao.Vazio(txtLogin.Texto))
            {
                lblErro.Text = "Informe o usuário.";
                txtLogin.ComErro = true;
                txtLogin.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSenha.Texto))
            {
                lblErro.Text = "Informe a senha.";
                txtSenha.ComErro = true;
                txtSenha.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnEntrar.Enabled = false;
                var usuario = _usuarioDAO.Autenticar(txtLogin.Texto, txtSenha.Texto);
                if (usuario == null)
                {
                    lblErro.Text = "Usuário ou senha incorretos.";
                    txtSenha.ComErro = true;
                    txtSenha.Texto = string.Empty;
                    txtSenha.Focus();
                    return;
                }
                Sessao.Iniciar(usuario);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (RegraNegocioException ex)
            {
                lblErro.Text = ex.Message;
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Login");
                VerificarConexao();
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEntrar.Enabled = true;
            }
        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {
            using var form = new FrmConfigConexao();
            if (form.ShowDialog(this) == DialogResult.OK)
                VerificarConexao();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Arrastar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MoverJanela.Iniciar(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var caneta = new Pen(T.BordaForte);
            e.Graphics.DrawRectangle(caneta, 0, 0, Width - 1, Height - 1);
        }
    }
}
