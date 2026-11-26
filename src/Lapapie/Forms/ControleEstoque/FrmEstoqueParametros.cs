using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;

namespace Lapapie.Forms.ControleEstoque
{
    /// <summary>Edita estoque mínimo e localização de um produto.</summary>
    public partial class FrmEstoqueParametros : FrmDialogoBase
    {
        private readonly Estoque _estoque;

        public FrmEstoqueParametros() : this(new Estoque()) { }

        public FrmEstoqueParametros(Estoque estoque)
        {
            InitializeComponent();
            _estoque = estoque;
        }

        private void FrmEstoqueParametros_Load(object sender, EventArgs e)
        {
            lblSubtitulo.Text = _estoque.ProdutoNome;
            nudMinimo.Value = Math.Min(nudMinimo.Maximum, Math.Max(0, _estoque.EstoqueMinimo));
            txtLocalizacao.Texto = _estoque.Localizacao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var copia = new Estoque { EstoqueMinimo = (int)nudMinimo.Value, Localizacao = txtLocalizacao.Texto.Trim() };
            if (!ValidarOuAvisar(copia.Validar(), (txtLocalizacao, "localização"))) return;
            try
            {
                new EstoqueDAO().AtualizarParametros(_estoque.ProdutoId, copia.EstoqueMinimo, copia.Localizacao);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Salvar parâmetros de estoque");
            }
        }
    }
}
