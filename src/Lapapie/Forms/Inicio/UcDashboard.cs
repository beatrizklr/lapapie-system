using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models;
using Lapapie.Models.Consultas;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Inicio
{
    /// <summary>
    /// Tela inicial (dashboard): indicadores dos últimos 30 dias, gráficos,
    /// promoção em destaque, últimas vendas e disponibilidade dos kits.
    /// </summary>
    public partial class UcDashboard : TelaBase
    {
        private readonly RelatorioDAO _relatorioDAO = new();
        private readonly VendaDAO _vendaDAO = new();
        private readonly EstoqueDAO _estoqueDAO = new();
        private readonly PromocaoDAO _promocaoDAO = new();
        private readonly ProdutoDAO _produtoDAO = new();
        private readonly ToolTip _dica = new();

        public UcDashboard()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Início";

        public override string Subtitulo
        {
            get
            {
                var hora = DateTime.Now.Hour;
                var saudacao = hora < 12 ? "Bom dia" : hora < 18 ? "Boa tarde" : "Boa noite";
                var nome = Sessao.UsuarioLogado?.Nome.Split(' ')[0] ?? "";
                return $"{saudacao}, {nome}! Veja como anda a Lapapiê.";
            }
        }

        private void Configurar()
        {
            T.EstilizarGrid(dgvUltimas);
            dgvUltimas.RowTemplate.Height = 34;
            dgvUltimas.ColumnHeadersHeight = 32;
            T.AdicionarColuna(dgvUltimas, nameof(HistoricoVenda.VendaId), "Nº", 54, centro: true);
            T.AdicionarColuna(dgvUltimas, nameof(HistoricoVenda.DataVenda), "Data", 124, T.FormatoDataHora);
            T.AdicionarColuna(dgvUltimas, nameof(HistoricoVenda.Cliente), "Cliente");
            T.AdicionarColuna(dgvUltimas, nameof(HistoricoVenda.FormaPagamentoDescricao), "Pagamento", 120);
            T.AdicionarColuna(dgvUltimas, nameof(HistoricoVenda.ValorTotal), "Total", 96, T.FormatoMoeda);

            T.EstilizarGrid(dgvKits);
            dgvKits.RowTemplate.Height = 34;
            dgvKits.ColumnHeadersHeight = 32;
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.Kit), "Kit");
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.PrecoVenda), "Preço", 86, T.FormatoMoeda);
            T.AdicionarColuna(dgvKits, nameof(DisponibilidadeKit.QuantidadeDisponivel), "Disponíveis", 96, T.FormatoInteiro);

            graficoTop.FormatarValor = v => $"{v:0} un.";
            btnVerPromocao.Visible = Sessao.Pode(Funcionalidade.NovaVenda);
            Reorganizar();
        }

        public override void Carregar()
        {
            Executar(() =>
            {
                var hoje = DateTime.Today;
                var atual = _relatorioDAO.ObterIndicadores(hoje.AddDays(-29), hoje);
                var anterior = _relatorioDAO.ObterIndicadores(hoje.AddDays(-59), hoje.AddDays(-30));

                kpiFaturamento.Valor = Formatacao.Moeda(atual.Faturamento);
                Comparar(kpiFaturamento, atual.Faturamento, anterior.Faturamento);
                kpiVendas.Valor = Formatacao.Numero(atual.QuantidadeVendas);
                Comparar(kpiVendas, atual.QuantidadeVendas, anterior.QuantidadeVendas);
                kpiTicket.Valor = Formatacao.Moeda(atual.TicketMedio);
                Comparar(kpiTicket, atual.TicketMedio, anterior.TicketMedio);

                int baixo = _estoqueDAO.ContarEstoqueBaixo();
                kpiEstoque.Valor = $"{baixo} produto(s)";
                kpiEstoque.Detalhe = baixo == 0 ? "Tudo abastecido" : "Clique para ver e repor";
                kpiEstoque.Tendencia = baixo == 0 ? Tendencia.Alta : Tendencia.Alerta;

                graficoMensal.DefinirDados(_relatorioDAO.FaturamentoPorMes(6, hoje));
                graficoPagamentos.DefinirDados(_relatorioDAO.FaturamentoPorFormaPagamento(hoje.AddDays(-29), hoje));
                graficoTop.DefinirDados(_relatorioDAO.ProdutosMaisVendidos(hoje.AddDays(-29), hoje, 6)
                    .Select(p => new ItemSerie(p.Produto, p.Quantidade, p.Quantidade)));
                dgvUltimas.DataSource = _vendaDAO.ListarUltimas(6);
                dgvKits.DataSource = _estoqueDAO.ListarKits(somenteAtivos: true);
                MostrarOferta();
            });
        }

        /// <summary>Compara com os 30 dias anteriores e mostra a variação em %.</summary>
        private static void Comparar(CartaoIndicador cartao, decimal atual, decimal anterior)
        {
            if (anterior == 0)
            {
                cartao.Detalhe = "sem base de comparação";
                cartao.Tendencia = Tendencia.Neutra;
                return;
            }
            decimal variacao = (atual - anterior) / anterior * 100;
            cartao.Detalhe = $"{Math.Abs(variacao):0.0}% vs. 30 dias anteriores";
            cartao.Tendencia = variacao >= 0 ? Tendencia.Alta : Tendencia.Baixa;
        }

        /// <summary>Destaque da promoção vigente (ou da próxima agendada).</summary>
        private void MostrarOferta()
        {
            var promocoes = _promocaoDAO.Pesquisar(null);
            var promocao = promocoes.Where(p => p.Situacao == SituacaoPromocao.Vigente).OrderBy(p => p.DataFim).FirstOrDefault();
            bool agendada = false;
            if (promocao == null)
            {
                promocao = promocoes.Where(p => p.Situacao == SituacaoPromocao.Agendada).OrderBy(p => p.DataInicio).FirstOrDefault();
                agendada = promocao != null;
            }

            foreach (Control c in flpOfertaProdutos.Controls.Cast<Control>().ToList()) c.Dispose();
            flpOfertaProdutos.Controls.Clear();

            if (promocao == null)
            {
                faixaOferta.Text = "LAPAPIÊ";
                lblOfertaNome.Text = "Nenhuma promoção no momento";
                lblOfertaDescricao.Text = "Cadastre uma promoção para movimentar as vendas.";
                lblOfertaValor.Text = "";
                lblOfertaPeriodo.Text = "";
                return;
            }

            faixaOferta.Text = agendada ? "EM BREVE" : "OFERTA DA SEMANA";
            lblOfertaNome.Text = promocao.Nome;
            lblOfertaDescricao.Text = promocao.Descricao;
            lblOfertaValor.Text = promocao.TipoDesconto == Models.Enums.TipoDesconto.Percentual
                ? $"{promocao.ValorDesconto:0.##}% OFF"
                : $"−{Formatacao.Moeda(promocao.ValorDesconto)}";
            lblOfertaPeriodo.Text = agendada
                ? $"começa em {promocao.DataInicio:dd/MM}"
                : $"válida até {promocao.DataFim:dd/MM 'às' HH:mm}";
            btnVerPromocao.Visible = !agendada && Sessao.Pode(Funcionalidade.NovaVenda);

            var detalhe = _promocaoDAO.ObterPorId(promocao.Id);
            var produtos = _produtoDAO.Pesquisar(null).Where(p => detalhe.ProdutoIds.Contains(p.Id)).Take(3).ToList();
            foreach (var produto in produtos)
            {
                var foto = new PictureBox
                {
                    Size = new Size(104, 104),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = ImagensProduto.Obter(produto.Id, produto.CategoriaId, 104),
                    Margin = new Padding(0, 40, 2, 0)
                };
                _dica.SetToolTip(foto, $"{produto.Nome}\n{Formatacao.Moeda(produto.PrecoVenda)}");
                flpOfertaProdutos.Controls.Add(foto);
            }
        }

        // ---------------- Layout responsivo ----------------

        private void UcDashboard_Resize(object sender, EventArgs e) => Reorganizar();

        private void Reorganizar()
        {
            int largura = ClientSize.Width - (VerticalScroll.Visible ? 2 : 0) - 4;
            if (largura < 300) return;
            const int espaco = 16;
            SuspendLayout();
            var kpis = new Control[] { kpiFaturamento, kpiVendas, kpiTicket, kpiEstoque };
            int larguraKpi = (largura - espaco * 3) / 4;
            for (int i = 0; i < kpis.Length; i++)
                kpis[i].SetBounds(i * (larguraKpi + espaco) + AutoScrollPosition.X, AutoScrollPosition.Y, larguraKpi, 132);

            int esquerda = (int)((largura - espaco) * 0.6);
            int direita = largura - esquerda - espaco;
            int y = 148 + AutoScrollPosition.Y;
            cardMensal.SetBounds(AutoScrollPosition.X, y, esquerda, 320);
            cardPagamentos.SetBounds(AutoScrollPosition.X + esquerda + espaco, y, direita, 320);
            y += 336;
            cardOferta.SetBounds(AutoScrollPosition.X, y, esquerda, 340);
            cardTopProdutos.SetBounds(AutoScrollPosition.X + esquerda + espaco, y, direita, 340);
            y += 356;
            cardUltimas.SetBounds(AutoScrollPosition.X, y, esquerda, 300);
            cardKits.SetBounds(AutoScrollPosition.X + esquerda + espaco, y, direita, 300);
            flpOfertaProdutos.Width = Math.Max(120, pnlOfertaConteudo.Width - flpOfertaProdutos.Left - 10);
            ResumeLayout();
        }

        private void kpiEstoque_Click(object sender, EventArgs e) => Principal?.Navegar(Funcionalidade.EstoqueBaixo);

        private void btnVerPromocao_Click(object sender, EventArgs e) => Principal?.Navegar(Funcionalidade.NovaVenda);
    }
}
