using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Util;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro/edição de cliente.</summary>
    public partial class FrmClienteEdicao : FrmDialogoBase
    {
        private readonly ClienteDAO _dao = new();
        private readonly Cliente _cliente;
        private readonly bool _novo;

        /// <summary>Construtor usado pelo designer.</summary>
        public FrmClienteEdicao() : this(null) { }

        /// <param name="cliente">null para cadastrar um novo cliente.</param>
        public FrmClienteEdicao(Cliente cliente)
        {
            InitializeComponent();
            _novo = cliente == null;
            _cliente = cliente ?? new Cliente();
        }

        private void FrmClienteEdicao_Load(object sender, EventArgs e)
        {
            if (_novo) return;
            lblTitulo.Text = "EDITAR CLIENTE";
            Text = "Cliente nº " + _cliente.Id;
            txtNome.Texto = _cliente.Nome;
            txtCpf.Texto = _cliente.Cpf == null ? "" : Formatacao.Cpf(_cliente.Cpf);
            txtTelefone.Texto = _cliente.Telefone;
            txtEmail.Texto = _cliente.Email;
            txtCep.Texto = _cliente.Cep == null ? "" : Formatacao.Cep(_cliente.Cep);
            txtLogradouro.Texto = _cliente.Logradouro;
            txtNumero.Texto = _cliente.Numero;
            txtComplemento.Texto = _cliente.Complemento;
            txtBairro.Texto = _cliente.Bairro;
            txtCidade.Texto = _cliente.Cidade;
            txtUf.Texto = _cliente.Uf;
            chkAtivo.Checked = _cliente.Ativo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _cliente.Nome = txtNome.Texto.Trim();
            _cliente.Cpf = txtCpf.SomenteDigitos;
            _cliente.Telefone = txtTelefone.Texto.Trim();
            _cliente.Email = txtEmail.Texto.Trim();
            _cliente.Cep = txtCep.SomenteDigitos;
            _cliente.Logradouro = txtLogradouro.Texto.Trim();
            _cliente.Numero = txtNumero.Texto.Trim();
            _cliente.Complemento = txtComplemento.Texto.Trim();
            _cliente.Bairro = txtBairro.Texto.Trim();
            _cliente.Cidade = txtCidade.Texto.Trim();
            _cliente.Uf = txtUf.Texto.Trim().ToUpperInvariant();
            _cliente.Ativo = chkAtivo.Checked;

            if (!ValidarOuAvisar(_cliente.Validar(), (txtNome, "nome"), (txtCpf, "CPF"), (txtEmail, "e-mail"), (txtCep, "CEP"), (txtUf, "UF")))
                return;

            try
            {
                if (_novo) _dao.Inserir(_cliente); else _dao.Atualizar(_cliente);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (ex is BancoDadosException { Tipo: TipoErroBanco.RegistroDuplicado }) txtCpf.ComErro = true;
                Mensagem.TratarErro(ex, "Salvar cliente");
            }
        }
    }
}
