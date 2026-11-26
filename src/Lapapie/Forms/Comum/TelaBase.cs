using System.ComponentModel;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Comum
{
    /// <summary>
    /// Base de todas as telas exibidas dentro da janela principal.
    /// <para>
    /// Cada tela é um UserControl (e não um Form) para ser "encaixada" na área de
    /// conteúdo da janela principal, como as páginas de um site. A classe base
    /// padroniza título, carregamento de dados e tratamento de erros.
    /// </para>
    /// </summary>
    public class TelaBase : UserControl
    {
        public TelaBase()
        {
            BackColor = T.Fundo;
            Font = T.Texto(10f);
            DoubleBuffered = true;
        }

        /// <summary>Título grande exibido no topo da janela principal.</summary>
        [Browsable(false)]
        public virtual string Titulo => string.Empty;

        /// <summary>Frase em itálico abaixo do título.</summary>
        [Browsable(false)]
        public virtual string Subtitulo => string.Empty;

        /// <summary>true quando a tela está aberta no designer do Visual Studio (não acessa o banco).</summary>
        protected bool EmDesign => LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;

        /// <summary>Carrega (ou recarrega) os dados da tela. Chamado pela janela principal e pela tecla F5.</summary>
        public virtual void Carregar() { }

        /// <summary>
        /// Chamado antes de sair da tela (trocar de menu, sair ou fechar o sistema).
        /// Telas com trabalho em andamento (ex.: carrinho do caixa) pedem confirmação.
        /// </summary>
        public virtual bool PodeSair() => true;

        private System.Windows.Forms.Timer _temporizadorCarga;

        /// <summary>
        /// Recarrega os dados só depois que a pessoa para de mexer nos filtros
        /// (evita várias consultas enquanto ela digita uma data, por exemplo).
        /// </summary>
        protected void AgendarCarregamento(int milissegundos = 600)
        {
            if (_temporizadorCarga == null)
            {
                _temporizadorCarga = new System.Windows.Forms.Timer();
                _temporizadorCarga.Tick += (s, e) => { _temporizadorCarga.Stop(); Carregar(); };
            }
            _temporizadorCarga.Stop();
            _temporizadorCarga.Interval = milissegundos;
            _temporizadorCarga.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _temporizadorCarga?.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>A janela principal (para navegar entre telas).</summary>
        protected FrmPrincipal Principal => FindForm() as FrmPrincipal;

        /// <summary>
        /// Executa uma ação mostrando o cursor de espera e tratando qualquer exceção.
        /// Retorna true se deu certo.
        /// </summary>
        protected bool Executar(Action acao, string contexto = null)
        {
            try
            {
                UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                acao();
                return true;
            }
            catch (Exception ex)
            {
                UseWaitCursor = false;
                Mensagem.TratarErro(ex, contexto ?? GetType().Name);
                return false;
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Exclui um registro. Se o banco recusar porque há histórico ligado a ele
        /// (chave estrangeira), oferece a INATIVAÇÃO — a exclusão lógica preserva
        /// vendas, compras e movimentações antigas.
        /// </summary>
        protected void ExcluirOuInativar(string descricao, Action excluir, Action inativar)
        {
            if (!Mensagem.Confirmar($"Deseja excluir {descricao}?\nEsta ação não pode ser desfeita.", "Excluir registro", "Excluir", "Cancelar", perigoso: true))
                return;
            try
            {
                excluir();
                Mensagem.Sucesso($"{Primeira(descricao)} foi excluído(a).");
            }
            catch (DAO.BancoDadosException ex) when (ex.Tipo == DAO.TipoErroBanco.RegistroEmUso && inativar != null)
            {
                if (Mensagem.Confirmar($"{Primeira(descricao)} possui histórico no sistema (vendas, compras ou movimentações) e por isso não pode ser apagado(a).\n\n" +
                                       "Deseja INATIVAR? Deixará de aparecer nas pesquisas, mas o histórico continua preservado.",
                                       "Não é possível excluir", "Inativar", "Cancelar"))
                {
                    if (Executar(inativar)) Mensagem.Sucesso($"{Primeira(descricao)} foi inativado(a).");
                }
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Excluir " + descricao);
            }
            Carregar();
        }

        private static string Primeira(string texto) => string.IsNullOrEmpty(texto) ? texto : char.ToUpper(texto[0]) + texto[1..];

        /// <summary>Desenha a coluna booleana "Ativo" como uma etiqueta colorida.</summary>
        protected static void PintarChipAtivo(DataGridViewCellPaintingEventArgs e, DataGridView grid, string nomeColuna = "colAtivo")
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || grid.Columns[e.ColumnIndex].Name != nomeColuna) return;
            bool ativo = e.Value is bool b && b;
            T.DesenharChipCelula(e, ativo ? "Ativo" : "Inativo",
                ativo ? T.SalviaClara : T.ArgilaClara, ativo ? T.SalviaEscura : T.ArgilaEscura);
        }

        /// <summary>Texto do rodapé: "25 clientes".</summary>
        protected static string Contagem(int quantidade, string singular, string plural) =>
            quantidade == 0 ? $"Nenhum {singular} encontrado" : $"{quantidade} {(quantidade == 1 ? singular : plural)}";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (EmDesign) return;
            Tema.Tema.EstilizarControlesPadrao(this);
        }
    }
}
