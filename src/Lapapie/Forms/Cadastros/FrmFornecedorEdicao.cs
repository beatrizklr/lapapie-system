using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Util;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro/edição de fornecedor.</summary>
    public partial class FrmFornecedorEdicao : FrmDialogoBase
    {
        private readonly FornecedorDAO _dao = new();
        private readonly Fornecedor _fornecedor;
        private readonly bool _novo;

        public FrmFornecedorEdicao() : this(null) { }

        public FrmFornecedorEdicao(Fornecedor fornecedor)
        {
            InitializeComponent();
            _novo = fornecedor == null;
            _fornecedor = fornecedor == null ? new Fornecedor() : fornecedor.Clonar();
        }

        private void FrmFornecedorEdicao_Load(object sender, EventArgs e)
        {
            if (_novo) return;
            lblTitulo.Text = "EDITAR FORNECEDOR";
            txtRazaoSocial.Texto = _fornecedor.RazaoSocial;
            txtNomeFantasia.Texto = _fornecedor.NomeFantasia;
            txtCnpj.Texto = Formatacao.Cnpj(_fornecedor.Cnpj);
            txtEmail.Texto = _fornecedor.Email;
            txtTelefone.Texto = _fornecedor.Telefone;
            txtContato.Texto = _fornecedor.NomeContato;
            txtCidade.Texto = _fornecedor.Cidade;
            txtUf.Texto = _fornecedor.Uf;
            chkAtivo.Checked = _fornecedor.Ativo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _fornecedor.RazaoSocial = txtRazaoSocial.Texto.Trim();
            _fornecedor.NomeFantasia = txtNomeFantasia.Texto.Trim();
            _fornecedor.Cnpj = txtCnpj.SomenteDigitos;
            _fornecedor.Email = txtEmail.Texto.Trim();
            _fornecedor.Telefone = txtTelefone.Texto.Trim();
            _fornecedor.NomeContato = txtContato.Texto.Trim();
            _fornecedor.Cidade = txtCidade.Texto.Trim();
            _fornecedor.Uf = txtUf.Texto.Trim().ToUpperInvariant();
            _fornecedor.Ativo = chkAtivo.Checked;

            if (!ValidarOuAvisar(_fornecedor.Validar(), (txtRazaoSocial, "razão"), (txtCnpj, "CNPJ"), (txtEmail, "e-mail"), (txtUf, "UF")))
                return;
            try
            {
                if (_novo) _dao.Inserir(_fornecedor); else _dao.Atualizar(_fornecedor);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (ex is BancoDadosException { Tipo: TipoErroBanco.RegistroDuplicado }) txtCnpj.ComErro = true;
                Mensagem.TratarErro(ex, "Salvar fornecedor");
            }
        }
    }
}
