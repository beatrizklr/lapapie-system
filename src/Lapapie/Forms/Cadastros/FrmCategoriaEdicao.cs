using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;

namespace Lapapie.Forms.Cadastros
{
    /// <summary>Cadastro/edição de categoria.</summary>
    public partial class FrmCategoriaEdicao : FrmDialogoBase
    {
        private readonly CategoriaDAO _dao = new();
        private readonly Categoria _categoria;
        private readonly bool _nova;

        public FrmCategoriaEdicao() : this(null) { }

        public FrmCategoriaEdicao(Categoria categoria)
        {
            InitializeComponent();
            _nova = categoria == null;
            // Trabalhamos numa cópia: se a pessoa cancelar, a linha do grid não muda.
            _categoria = categoria == null ? new Categoria()
                : new Categoria { Id = categoria.Id, Nome = categoria.Nome, Descricao = categoria.Descricao, Ativo = categoria.Ativo };
        }

        private void FrmCategoriaEdicao_Load(object sender, EventArgs e)
        {
            if (_nova) return;
            lblTitulo.Text = "EDITAR CATEGORIA";
            txtNome.Texto = _categoria.Nome;
            txtDescricao.Texto = _categoria.Descricao;
            chkAtivo.Checked = _categoria.Ativo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            _categoria.Nome = txtNome.Texto.Trim();
            _categoria.Descricao = txtDescricao.Texto.Trim();
            _categoria.Ativo = chkAtivo.Checked;
            if (!ValidarOuAvisar(_categoria.Validar(), (txtNome, "nome"), (txtDescricao, "descrição"))) return;
            try
            {
                if (_nova) _dao.Inserir(_categoria); else _dao.Atualizar(_categoria);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Salvar categoria");
            }
        }
    }
}
