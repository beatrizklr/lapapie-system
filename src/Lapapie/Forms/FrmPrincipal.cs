using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Cadastros;
using Lapapie.Forms.Comum;
using Lapapie.Forms.ControleEstoque;
using Lapapie.Forms.Inicio;
using Lapapie.Forms.Vendas;
using Lapapie.Tema;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms
{
    /// <summary>
    /// Janela principal: menu lateral (montado conforme o perfil do usuário),
    /// cabeçalho com o título da tela e a área onde as telas são exibidas.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        /// <summary>Uma entrada do menu: a qual funcionalidade ela leva e como criar a tela.</summary>
        private record EntradaMenu(string Secao, Funcionalidade Funcionalidade, string Texto, string Icone, Func<TelaBase> CriarTela, string Atalho = "");

        // A ordem desta lista é a ordem do menu.
        private static readonly EntradaMenu[] Entradas =
        {
            new("VISÃO GERAL", Funcionalidade.Dashboard, "Início", Icones.Inicio, () => new UcDashboard(), "F1"),
            new("VENDAS", Funcionalidade.NovaVenda, "Nova venda", Icones.Carrinho, () => new UcNovaVenda(), "F2"),
            new("VENDAS", Funcionalidade.HistoricoVendas, "Histórico de vendas", Icones.Historico, () => new UcHistoricoVendas()),
            new("VENDAS", Funcionalidade.RelatorioVendas, "Relatório de vendas", Icones.Grafico, () => new UcRelatorioVendas()),
            new("ESTOQUE", Funcionalidade.ConsultarEstoque, "Controle de estoque", Icones.Biblioteca, () => new UcEstoque()),
            new("ESTOQUE", Funcionalidade.EstoqueBaixo, "Estoque baixo", Icones.Alerta, () => new UcEstoqueBaixo()),
            new("ESTOQUE", Funcionalidade.Movimentacoes, "Movimentações", Icones.Sincronizar, () => new UcMovimentacoes()),
            new("ESTOQUE", Funcionalidade.PedidosCompra, "Pedidos de compra", Icones.Documento, () => new UcPedidosCompra()),
            new("CADASTROS", Funcionalidade.ConsultarProdutos, "Produtos", Icones.Pacote, () => new UcProdutos()),
            new("CADASTROS", Funcionalidade.Kits, "Kits e combos", Icones.Loja, () => new UcKits()),
            new("CADASTROS", Funcionalidade.Categorias, "Categorias", Icones.Etiqueta, () => new UcCategorias()),
            new("CADASTROS", Funcionalidade.Clientes, "Clientes", Icones.Contato, () => new UcClientes()),
            new("CADASTROS", Funcionalidade.Fornecedores, "Fornecedores", Icones.Pessoas, () => new UcFornecedores()),
            new("CADASTROS", Funcionalidade.Promocoes, "Promoções", Icones.Cartao, () => new UcPromocoes()),
            new("ADMINISTRAÇÃO", Funcionalidade.Usuarios, "Usuários", Icones.Grupo, () => new UcUsuarios()),
        };

        private readonly Dictionary<Funcionalidade, ItemMenu> _itensMenu = new();
        private TelaBase _telaAtual;
        private Funcionalidade _funcionalidadeAtual;

        /// <summary>true quando a pessoa clicou em "Sair" (volta para o login em vez de fechar o sistema).</summary>
        public bool VoltarParaLogin { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch (Exception) { }
            T.AplicarBarraTitulo(this);
            picLogo.Image = ImagensProduto.Recurso(Path.Combine("Marca", "assinatura-horizontal-negativo.png"));

            var usuario = Sessao.UsuarioLogado;
            lblNomeUsuario.Text = usuario.Nome;
            lblPerfilUsuario.Text = usuario.PerfilDescricao;
            avatarUsuario.Text = Formatacao.Iniciais(usuario.Nome);
            lblData.Text = DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy", Formatacao.PtBr);
            btnNovaVendaTopo.Visible = Sessao.Pode(Funcionalidade.NovaVenda);

            MontarMenu();
            Navegar(Funcionalidade.Dashboard);
        }

        /// <summary>Cria os itens de menu apenas para as funcionalidades que o perfil pode acessar.</summary>
        private void MontarMenu()
        {
            flpMenu.SuspendLayout();
            flpMenu.Controls.Clear();
            string secaoAtual = null;
            foreach (var entrada in Entradas.Where(x => Sessao.Pode(x.Funcionalidade)))
            {
                if (entrada.Secao != secaoAtual)
                {
                    secaoAtual = entrada.Secao;
                    flpMenu.Controls.Add(new Label
                    {
                        Text = secaoAtual,
                        AutoSize = false,
                        Size = new Size(198, 30),
                        TextAlign = ContentAlignment.BottomLeft,
                        Font = T.Fonte(T.FamiliaTitulo, 8f),
                        ForeColor = T.LacreDourado,
                        Margin = new Padding(14, 6, 0, 4)
                    });
                }
                var item = new ItemMenu
                {
                    Text = entrada.Texto,
                    Icone = entrada.Icone,
                    Atalho = entrada.Atalho,
                    Size = new Size(212, 38),
                    Margin = new Padding(0, 1, 0, 1),
                    Tag = entrada.Funcionalidade
                };
                item.Click += (s, e) => Navegar((Funcionalidade)((Control)s).Tag);
                _itensMenu[entrada.Funcionalidade] = item;
                flpMenu.Controls.Add(item);
            }
            flpMenu.ResumeLayout();
            AtualizarContadorEstoqueBaixo();
        }

        /// <summary>Mostra no menu quantos produtos estão com estoque baixo.</summary>
        public void AtualizarContadorEstoqueBaixo()
        {
            if (!_itensMenu.TryGetValue(Funcionalidade.EstoqueBaixo, out var item)) return;
            try
            {
                int quantidade = new EstoqueDAO().ContarEstoqueBaixo();
                item.Atalho = quantidade > 0 ? quantidade.ToString() : "";
                item.Invalidate();
            }
            catch (Exception)
            {
                // Contador é apenas informativo; erros de banco aparecem nas próprias telas.
            }
        }

        /// <summary>Abre uma tela na área de conteúdo.</summary>
        public void Navegar(Funcionalidade funcionalidade)
        {
            // Mesma tela: apenas recarrega (não perde o que está em andamento).
            if (_telaAtual != null && funcionalidade == _funcionalidadeAtual)
            {
                _telaAtual.Carregar();
                return;
            }
            if (_telaAtual != null && !_telaAtual.PodeSair()) return;

            var entrada = Entradas.FirstOrDefault(x => x.Funcionalidade == funcionalidade);
            if (entrada == null || !Sessao.Pode(funcionalidade))
            {
                Mensagem.Alerta("Seu perfil não tem acesso a esta funcionalidade.", "Acesso restrito");
                return;
            }

            Cursor = Cursors.WaitCursor;
            pnlConteudo.SuspendLayout();
            try
            {
                var anterior = _telaAtual;
                _telaAtual = entrada.CriarTela();
                _telaAtual.Dock = DockStyle.Fill;
                pnlConteudo.Controls.Add(_telaAtual);
                if (anterior != null)
                {
                    pnlConteudo.Controls.Remove(anterior);
                    anterior.Dispose();
                }
                _funcionalidadeAtual = funcionalidade;

                lblTitulo.Text = _telaAtual.Titulo.ToUpperInvariant();
                lblSubtitulo.Text = _telaAtual.Subtitulo;
                foreach (var (f, item) in _itensMenu) item.Selecionado = f == funcionalidade;
                btnNovaVendaTopo.Visible = Sessao.Pode(Funcionalidade.NovaVenda) && funcionalidade != Funcionalidade.NovaVenda;
            }
            finally
            {
                pnlConteudo.ResumeLayout();
                Cursor = Cursors.Default;
            }
            _telaAtual.Carregar();
            AtualizarContadorEstoqueBaixo();
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    Navegar(Funcionalidade.Dashboard);
                    e.Handled = true;
                    break;
                case Keys.F2 when Sessao.Pode(Funcionalidade.NovaVenda):
                    Navegar(Funcionalidade.NovaVenda);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    _telaAtual?.Carregar();
                    AtualizarContadorEstoqueBaixo();
                    e.Handled = true;
                    break;
            }
        }

        private void btnNovaVendaTopo_Click(object sender, EventArgs e) => Navegar(Funcionalidade.NovaVenda);

        private void picLogo_Click(object sender, EventArgs e) => Navegar(Funcionalidade.Dashboard);

        private bool _saidaConfirmada;

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (_telaAtual != null && !_telaAtual.PodeSair()) return;
            _saidaConfirmada = true;
            VoltarParaLogin = true;
            Close();
        }

        /// <summary>Fechar pelo "X" também respeita a venda em andamento.</summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_saidaConfirmada && e.CloseReason == CloseReason.UserClosing && _telaAtual != null && !_telaAtual.PodeSair())
            {
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }
    }
}
