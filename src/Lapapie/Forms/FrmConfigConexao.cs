using Lapapie.Conexao;
using Lapapie.Forms.Comum;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms
{
    /// <summary>Permite ajustar servidor, porta, banco, usuário e senha do MySQL sem recompilar.</summary>
    public partial class FrmConfigConexao : FrmDialogoBase
    {
        public FrmConfigConexao()
        {
            InitializeComponent();
        }

        private void FrmConfigConexao_Load(object sender, EventArgs e)
        {
            var cfg = ConexaoBD.Configuracao;
            txtServidor.Texto = cfg.Servidor;
            txtPorta.Texto = cfg.Porta.ToString();
            txtBanco.Texto = cfg.Banco;
            txtUsuario.Texto = cfg.Usuario;
            txtSenha.Texto = cfg.Senha;
        }

        private ConfiguracaoConexao LerCampos() => new()
        {
            Servidor = Validacao.Vazio(txtServidor.Texto) ? "localhost" : txtServidor.Texto.Trim(),
            Porta = int.TryParse(txtPorta.Texto, out var porta) && porta > 0 ? porta : 3306,
            Banco = Validacao.Vazio(txtBanco.Texto) ? "lapapie" : txtBanco.Texto.Trim(),
            Usuario = txtUsuario.Texto.Trim(),
            Senha = txtSenha.Texto
        };

        private bool Testar()
        {
            lblResultado.Text = "Testando...";
            lblResultado.ForeColor = T.TextoSecundario;
            lblResultado.Refresh();
            Cursor = Cursors.WaitCursor;
            bool ok = ConexaoBD.TestarConexao(LerCampos(), out var erro);
            Cursor = Cursors.Default;
            lblResultado.Text = ok ? "✓ Conexão realizada com sucesso!" : "✗ " + erro;
            lblResultado.ForeColor = ok ? T.SalviaEscura : T.ArgilaEscura;
            return ok;
        }

        private void btnTestar_Click(object sender, EventArgs e) => Testar();

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Validacao.Vazio(txtUsuario.Texto))
            {
                txtUsuario.ComErro = true;
                Mensagem.Alerta("Informe o usuário do MySQL.");
                return;
            }
            if (!Testar() && !Mensagem.Confirmar("A conexão falhou. Deseja salvar mesmo assim?", "Salvar configuração", "Salvar", "Revisar"))
                return;
            try
            {
                var cfg = LerCampos();
                cfg.Salvar();
                ConexaoBD.Configuracao = cfg;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Salvar configuração da conexão");
            }
        }
    }
}
