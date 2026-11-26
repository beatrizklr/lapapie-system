using Lapapie.Controles;
using Lapapie.DAO;
using Lapapie.Forms.Comum;
using Lapapie.Models.Consultas;
using Lapapie.Relatorios;
using Lapapie.Util;
using T = Lapapie.Tema.Tema;

namespace Lapapie.Forms.Vendas
{
    /// <summary>
    /// Relatório de vendas por período: indicadores, gráficos (dia, categoria,
    /// forma de pagamento, vendedor), ranking de produtos e exportação PDF/Excel.
    /// </summary>
    public partial class UcRelatorioVendas : TelaBase
    {
        private readonly RelatorioDAO _dao = new();
        private bool _ajustandoDatas = true;

        // Dados do último relatório gerado (reutilizados na exportação)
        private IndicadoresVenda _indicadores = new();
        private List<ItemSerie> _diario = new(), _categorias = new(), _pagamentos = new(), _vendedores = new();
        private List<VendaPorProduto> _produtos = new();

        public UcRelatorioVendas()
        {
            InitializeComponent();
            if (!EmDesign) Configurar();
        }

        public override string Titulo => "Relatório de vendas";
        public override string Subtitulo => "Escolha o período: indicadores e gráficos são recalculados na hora.";

        private DateTime Inicio => dtpInicio.Value.Date;
        private DateTime Fim => dtpFim.Value.Date;
        private int? VendedorId => cboVendedor.SelectedValue as int?;

        private void Configurar()
        {
            T.EstilizarGrid(dgvProdutos);
            dgvProdutos.RowTemplate.Height = 34;
            dgvProdutos.ColumnHeadersHeight = 34;
            T.AdicionarColuna(dgvProdutos, nameof(VendaPorProduto.Produto), "Produto");
            T.AdicionarColuna(dgvProdutos, nameof(VendaPorProduto.Categoria), "Categoria", 130);
            T.AdicionarColuna(dgvProdutos, nameof(VendaPorProduto.Quantidade), "Qtd.", 60, T.FormatoInteiro);
            T.AdicionarColuna(dgvProdutos, nameof(VendaPorProduto.Faturamento), "Faturamento", 110, T.FormatoMoeda);

            graficoVendedores.DestacarUltima = true;
            graficoCategorias.DestacarUltima = true;

            try
            {
                var vendedores = new List<KeyValuePair<int?, string>> { new(null, "Todos") };
                vendedores.AddRange(new UsuarioDAO().Pesquisar(null, false)
                    .Where(u => u.Perfil != Models.Enums.PerfilUsuario.Estoquista)
                    .Select(u => new KeyValuePair<int?, string>(u.Id, u.Nome)));
                cboVendedor.DisplayMember = "Value";
                cboVendedor.ValueMember = "Key";
                cboVendedor.DataSource = vendedores;
            }
            catch (Exception ex) { Mensagem.TratarErro(ex, "Carregar vendedores"); }

            AplicarPeriodo();
            _ajustandoDatas = false;
            Reorganizar();
        }

        /// <summary>Converte o atalho de período escolhido em datas.</summary>
        private void AplicarPeriodo()
        {
            var hoje = DateTime.Today;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
            (DateTime ini, DateTime fim)? periodo = abasPeriodo.IndiceSelecionado switch
            {
                0 => (hoje.AddDays(-6), hoje),
                1 => (hoje.AddDays(-29), hoje),
                2 => (inicioMes, hoje),
                3 => (inicioMes.AddMonths(-1), inicioMes.AddDays(-1)),
                4 => (hoje.AddDays(-89), hoje),
                _ => null
            };
            if (periodo == null) return; // personalizado: mantém as datas digitadas
            _ajustandoDatas = true;
            dtpInicio.Value = periodo.Value.ini;
            dtpFim.Value = periodo.Value.fim;
            _ajustandoDatas = false;
        }

        private void abasPeriodo_SelecaoAlterada(object sender, EventArgs e)
        {
            if (_ajustandoDatas) return;
            AplicarPeriodo();
            Carregar();
        }

        private void Datas_Alteradas(object sender, EventArgs e)
        {
            if (_ajustandoDatas) return;
            if (sender is DateTimePicker && abasPeriodo.IndiceSelecionado != 5)
            {
                _ajustandoDatas = true;
                abasPeriodo.IndiceSelecionado = 5; // "Personalizado"
                _ajustandoDatas = false;
            }
            AgendarCarregamento();
        }

        public override void Carregar()
        {
            if (Inicio > Fim)
            {
                cardEvolucao.Subtitulo = "A data inicial é maior que a final — ajuste o período.";
                return;
            }
            Executar(() =>
            {
                _indicadores = _dao.ObterIndicadores(Inicio, Fim, VendedorId);
                _diario = _dao.FaturamentoPorDia(Inicio, Fim, VendedorId);
                _categorias = _dao.FaturamentoPorCategoria(Inicio, Fim, VendedorId);
                _pagamentos = _dao.FaturamentoPorFormaPagamento(Inicio, Fim, VendedorId);
                _vendedores = _dao.FaturamentoPorVendedor(Inicio, Fim);
                _produtos = _dao.ProdutosMaisVendidos(Inicio, Fim, null, VendedorId);

                kpiFaturamento.Valor = Formatacao.Moeda(_indicadores.Faturamento);
                kpiFaturamento.Detalhe = $"{(Fim - Inicio).Days + 1} dia(s) analisados";
                kpiVendas.Valor = Formatacao.Numero(_indicadores.QuantidadeVendas);
                kpiVendas.Detalhe = _indicadores.VendasCanceladas > 0 ? $"{_indicadores.VendasCanceladas} cancelada(s)" : "nenhuma cancelada";
                kpiVendas.Tendencia = _indicadores.VendasCanceladas > 0 ? Tendencia.Alerta : Tendencia.Neutra;
                kpiTicket.Valor = Formatacao.Moeda(_indicadores.TicketMedio);
                kpiTicket.Detalhe = $"{_indicadores.ClientesAtendidos} cliente(s) identificado(s)";
                kpiItens.Valor = Formatacao.Numero(_indicadores.ItensVendidos);
                kpiItens.Detalhe = _indicadores.QuantidadeVendas == 0 ? "" : $"{(decimal)_indicadores.ItensVendidos / _indicadores.QuantidadeVendas:0.0} por venda";
                kpiDescontos.Valor = Formatacao.Moeda(_indicadores.TotalDescontos);
                kpiDescontos.Detalhe = "concedidos em promoções";

                graficoDiario.DefinirDados(_diario);
                graficoCategorias.DefinirDados(_categorias);
                graficoPagamentos.DefinirDados(_pagamentos);
                graficoVendedores.DefinirDados(_vendedores);
                dgvProdutos.DataSource = new ListaOrdenavel<VendaPorProduto>(_produtos);
                cardDiarioTitulo();
            });
        }

        private void cardDiarioTitulo() =>
            cardEvolucao.Subtitulo = $"De {Inicio:dd/MM/yyyy} a {Fim:dd/MM/yyyy}" + (VendedorId.HasValue ? $" · {cboVendedor.Text}" : " · todos os vendedores");

        // ---------------- Layout responsivo ----------------

        private void pnlRolagem_Resize(object sender, EventArgs e) => Reorganizar();

        /// <summary>Distribui os cartões conforme a largura disponível.</summary>
        private void Reorganizar()
        {
            int largura = pnlRolagem.ClientSize.Width - 4;
            if (largura < 200) return;
            const int espaco = 16;
            int dx = pnlRolagem.AutoScrollPosition.X, dy = pnlRolagem.AutoScrollPosition.Y; // compensa a rolagem
            pnlRolagem.SuspendLayout();

            var kpis = new Control[] { kpiFaturamento, kpiVendas, kpiTicket, kpiItens, kpiDescontos };
            int larguraKpi = (largura - espaco * (kpis.Length - 1)) / kpis.Length;
            for (int i = 0; i < kpis.Length; i++)
                kpis[i].SetBounds(dx + i * (larguraKpi + espaco), dy, larguraKpi, 132);

            int y = 148 + dy;
            cardEvolucao.SetBounds(dx, y, largura, 300);
            y += 300 + espaco;
            int metade = (largura - espaco) / 2;
            cardCategorias.SetBounds(dx, y, metade, 330);
            cardPagamentos.SetBounds(dx + metade + espaco, y, largura - metade - espaco, 330);
            y += 330 + espaco;
            int esquerda = (int)(largura * 0.4);
            cardVendedores.SetBounds(dx, y, esquerda, 380);
            cardProdutos.SetBounds(dx + esquerda + espaco, y, largura - esquerda - espaco, 380);
            pnlRolagem.ResumeLayout();
        }

        // ---------------- Exportação ----------------

        private string Periodo => $"Período de {Inicio:dd/MM/yyyy} a {Fim:dd/MM/yyyy}" + (VendedorId.HasValue ? $" · vendedor: {cboVendedor.Text}" : "");

        private TabelaExportacao TabelaSerie(string titulo, string rotulo, IEnumerable<ItemSerie> serie, string rotuloQtd)
        {
            var itens = serie.ToList();
            decimal total = itens.Sum(i => i.Valor);
            var tabela = new TabelaExportacao(titulo, Periodo)
                .Coluna(rotulo, largura: 2.4f).Coluna(rotuloQtd, FormatoColuna.Inteiro).Coluna("Faturamento", FormatoColuna.Moeda).Coluna("Participação", FormatoColuna.Percentual);
            foreach (var i in itens)
                tabela.AdicionarLinha(i.Rotulo, i.Quantidade, i.Valor, total == 0 ? 0 : i.Valor / total * 100);
            tabela.Totais = new object[] { "Total", itens.Sum(i => i.Quantidade), total, 100m };
            return tabela;
        }

        private TabelaExportacao TabelaProdutos(int? limite)
        {
            var tabela = new TabelaExportacao(limite.HasValue ? $"Top {limite} produtos" : "Vendas por produto", Periodo)
                .Coluna("Produto", largura: 3f).Coluna("Categoria", largura: 1.4f).Coluna("Quantidade", FormatoColuna.Inteiro).Coluna("Faturamento", FormatoColuna.Moeda);
            foreach (var p in limite.HasValue ? _produtos.Take(limite.Value) : _produtos)
                tabela.AdicionarLinha(p.Produto, p.Categoria, p.Quantidade, p.Faturamento);
            if (!limite.HasValue)
                tabela.Totais = new object[] { "Total", "", _produtos.Sum(p => p.Quantidade), _produtos.Sum(p => p.Faturamento) };
            return tabela;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            var documento = new DocumentoRelatorio { Titulo = "Relatório de vendas", Subtitulo = Periodo };
            documento.Indicadores.Add(("Faturamento", Formatacao.Moeda(_indicadores.Faturamento)));
            documento.Indicadores.Add(("Vendas", Formatacao.Numero(_indicadores.QuantidadeVendas)));
            documento.Indicadores.Add(("Ticket médio", Formatacao.Moeda(_indicadores.TicketMedio)));
            documento.Indicadores.Add(("Itens vendidos", Formatacao.Numero(_indicadores.ItensVendidos)));
            documento.Indicadores.Add(("Descontos", Formatacao.Moeda(_indicadores.TotalDescontos)));
            try
            {
                documento.Graficos.Add(("Faturamento por dia", graficoDiario.GerarPng(1000, 300)));
                documento.Graficos.Add(("Formas de pagamento", graficoPagamentos.GerarPng(560, 300)));
                documento.Graficos.Add(("Por categoria", graficoCategorias.GerarPng(560, 330)));
                documento.Graficos.Add(("Por vendedor", graficoVendedores.GerarPng(560, 300)));
            }
            catch (Exception ex)
            {
                Log.Erro(ex, "Imagem dos gráficos"); // o PDF sai mesmo sem os gráficos
            }
            documento.Tabelas.Add(TabelaSerie("Por categoria", "Categoria", _categorias, "Itens"));
            documento.Tabelas.Add(TabelaSerie("Por forma de pagamento", "Forma de pagamento", _pagamentos, "Vendas"));
            documento.Tabelas.Add(TabelaSerie("Por vendedor", "Vendedor", _vendedores, "Vendas"));
            documento.Tabelas.Add(TabelaProdutos(15));

            Exportacao.ParaPdf("relatorio-vendas", caminho => ExportadorPdf.ExportarRelatorio(caminho, documento));
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var resumo = new TabelaExportacao("Resumo", Periodo).Coluna("Indicador", largura: 2).Coluna("Valor", largura: 1.5f);
            resumo.AdicionarLinha("Faturamento", Formatacao.Moeda(_indicadores.Faturamento));
            resumo.AdicionarLinha("Vendas concluídas", _indicadores.QuantidadeVendas.ToString());
            resumo.AdicionarLinha("Vendas canceladas", _indicadores.VendasCanceladas.ToString());
            resumo.AdicionarLinha("Ticket médio", Formatacao.Moeda(_indicadores.TicketMedio));
            resumo.AdicionarLinha("Itens vendidos", _indicadores.ItensVendidos.ToString());
            resumo.AdicionarLinha("Descontos concedidos", Formatacao.Moeda(_indicadores.TotalDescontos));
            resumo.AdicionarLinha("Clientes identificados", _indicadores.ClientesAtendidos.ToString());

            var vendas = new TabelaExportacao("Vendas", Periodo)
                .Coluna("Nº", FormatoColuna.Inteiro).Coluna("Data", FormatoColuna.DataHora).Coluna("Cliente").Coluna("Vendedor")
                .Coluna("Pagamento").Coluna("Situação").Coluna("Itens", FormatoColuna.Inteiro)
                .Coluna("Subtotal", FormatoColuna.Moeda).Coluna("Desconto", FormatoColuna.Moeda).Coluna("Total", FormatoColuna.Moeda);
            try
            {
                foreach (var v in _dao.ListarVendas(Inicio, Fim, VendedorId))
                    vendas.AdicionarLinha(v.VendaId, v.DataVenda, v.Cliente, v.Vendedor, v.FormaPagamento.Descricao(), v.Status.Descricao(),
                                          v.TotalItens, v.ValorSubtotal, v.ValorDesconto, v.ValorTotal);
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Listar vendas para exportação");
                return;
            }

            var diario = new TabelaExportacao("Faturamento diário", Periodo).Coluna("Dia").Coluna("Vendas", FormatoColuna.Inteiro).Coluna("Faturamento", FormatoColuna.Moeda);
            foreach (var d in _diario) diario.AdicionarLinha(d.Rotulo, d.Quantidade, d.Valor);

            Exportacao.ParaExcel("relatorio-vendas", resumo, vendas, TabelaProdutos(null),
                TabelaSerie("Categorias", "Categoria", _categorias, "Itens"),
                TabelaSerie("Pagamentos", "Forma de pagamento", _pagamentos, "Vendas"),
                TabelaSerie("Vendedores", "Vendedor", _vendedores, "Vendas"), diario);
        }
    }
}
