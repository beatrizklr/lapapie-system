using Lapapie.Models;
using Lapapie.Util;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Lapapie.Relatorios
{
    /// <summary>
    /// Gera PDFs com a biblioteca QuestPDF: relatórios com tabelas/gráficos e o
    /// comprovante de venda. O layout segue a identidade visual Lapapiê.
    /// </summary>
    public static class ExportadorPdf
    {
        private const string Fonte = "Segoe UI";

        static ExportadorPdf()
        {
            // Licença gratuita para uso acadêmico/pequenas empresas (exigida pela QuestPDF).
            QuestPDF.Settings.License = LicenseType.Community;
            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;
        }

        /// <summary>Exporta uma única tabela (ex.: lista de clientes).</summary>
        public static void ExportarTabela(string caminho, TabelaExportacao tabela, bool paisagem = false)
        {
            var documento = new DocumentoRelatorio { Titulo = tabela.Titulo, Subtitulo = tabela.Subtitulo, Paisagem = paisagem };
            tabela.Titulo = null; // o título já vai no cabeçalho da página
            documento.Tabelas.Add(tabela);
            ExportarRelatorio(caminho, documento);
        }

        /// <summary>Exporta um documento composto (indicadores + gráficos + tabelas).</summary>
        public static void ExportarRelatorio(string caminho, DocumentoRelatorio relatorio)
        {
            Document.Create(container =>
            {
                container.Page(pagina =>
                {
                    pagina.Size(relatorio.Paisagem ? PageSizes.A4.Landscape() : PageSizes.A4);
                    pagina.Margin(32);
                    pagina.PageColor(Colors.White);
                    pagina.DefaultTextStyle(t => t.FontFamily(Fonte).FontSize(9).FontColor(Marca.TintaGrafite));

                    pagina.Header().Element(c => Cabecalho(c, relatorio.Titulo, relatorio.Subtitulo));
                    pagina.Content().PaddingVertical(12).Column(coluna =>
                    {
                        coluna.Spacing(14);

                        if (relatorio.Indicadores.Count > 0)
                        {
                            coluna.Item().Row(linha =>
                            {
                                linha.Spacing(8);
                                foreach (var (rotulo, valor) in relatorio.Indicadores)
                                {
                                    linha.RelativeItem().Background(Marca.PapelAlgodao).Padding(10).Column(k =>
                                    {
                                        k.Item().Text(rotulo.ToUpperInvariant()).FontSize(7).LetterSpacing(0.05f).FontColor(Marca.TextoSuave);
                                        k.Item().Text(valor).FontSize(14).Bold();
                                    });
                                }
                            });
                        }

                        // Gráficos dois a dois
                        foreach (var par in relatorio.Graficos.Chunk(2))
                        {
                            coluna.Item().Row(linha =>
                            {
                                linha.Spacing(10);
                                foreach (var (titulo, png) in par)
                                {
                                    linha.RelativeItem().Border(0.5f).BorderColor(Marca.Linha).Padding(8).Column(g =>
                                    {
                                        g.Item().Text(titulo).Bold().FontSize(10);
                                        g.Item().PaddingTop(4).Image(png).FitWidth();
                                    });
                                }
                                if (par.Length == 1) linha.RelativeItem();
                            });
                        }

                        foreach (var tabela in relatorio.Tabelas)
                            coluna.Item().Element(c => DesenharTabela(c, tabela));
                    });

                    pagina.Footer().Element(Rodape);
                });
            }).GeneratePdf(caminho);
        }

        private static void Cabecalho(IContainer container, string titulo, string subtitulo)
        {
            container.BorderBottom(1.5f).BorderColor(Marca.ArgilaRosada).PaddingBottom(8).Row(linha =>
            {
                linha.RelativeItem().Column(c =>
                {
                    c.Item().Text((titulo ?? "").ToUpperInvariant()).FontSize(18).Bold().LetterSpacing(0.02f);
                    if (!string.IsNullOrEmpty(subtitulo))
                        c.Item().Text(subtitulo).Italic().FontSize(10).FontColor(Marca.ArgilaRosada);
                });
                if (File.Exists(Marca.CaminhoLogo))
                    linha.ConstantItem(120).AlignRight().AlignMiddle().Height(40).Image(Marca.CaminhoLogo).FitArea();
            });
        }

        private static void Rodape(IContainer container)
        {
            container.BorderTop(0.5f).BorderColor(Marca.Linha).PaddingTop(6).Row(linha =>
            {
                linha.RelativeItem().Text($"Lapapiê · Sistema Gerenciador de Papelaria · gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                     .FontSize(7).FontColor(Marca.TextoSuave);
                linha.ConstantItem(80).AlignRight().Text(t =>
                {
                    t.DefaultTextStyle(s => s.FontSize(7).FontColor(Marca.TextoSuave));
                    t.Span("Página ");
                    t.CurrentPageNumber();
                    t.Span(" de ");
                    t.TotalPages();
                });
            });
        }

        private static void DesenharTabela(IContainer container, TabelaExportacao tabela)
        {
            container.Column(coluna =>
            {
                if (!string.IsNullOrEmpty(tabela.Titulo))
                    coluna.Item().PaddingBottom(4).Text(tabela.Titulo).Bold().FontSize(11);

                coluna.Item().Table(t =>
                {
                    t.ColumnsDefinition(def =>
                    {
                        foreach (var c in tabela.Colunas) def.RelativeColumn(c.Largura);
                    });

                    t.Header(h =>
                    {
                        foreach (var c in tabela.Colunas)
                        {
                            var celula = h.Cell().Background(Marca.TintaGrafite).PaddingVertical(5).PaddingHorizontal(4);
                            (c.AlinharDireita ? celula.AlignRight() : celula.AlignLeft())
                                .Text(c.Cabecalho).FontColor(Marca.PapelAlgodao).Bold().FontSize(8);
                        }
                    });

                    int indice = 0;
                    foreach (var valores in tabela.Linhas)
                    {
                        string fundo = indice++ % 2 == 0 ? "#FFFFFF" : Marca.Superficie;
                        EscreverLinha(t, tabela, valores, fundo, negrito: false);
                    }
                    if (tabela.Totais != null)
                        EscreverLinha(t, tabela, tabela.Totais, Marca.PapelAlgodao, negrito: true);
                });

                if (tabela.Linhas.Count == 0)
                    coluna.Item().Padding(8).Text("Nenhum registro no período.").Italic().FontColor(Marca.TextoSuave);
            });
        }

        private static void EscreverLinha(TableDescriptor t, TabelaExportacao tabela, object[] valores, string fundo, bool negrito)
        {
            for (int i = 0; i < tabela.Colunas.Count; i++)
            {
                var coluna = tabela.Colunas[i];
                var texto = i < valores.Length ? TabelaExportacao.Formatar(valores[i], coluna.Formato) : "";
                var celula = t.Cell().Background(fundo).BorderBottom(0.5f).BorderColor(Marca.Linha).PaddingVertical(4).PaddingHorizontal(4);
                var span = (coluna.AlinharDireita ? celula.AlignRight() : celula.AlignLeft()).Text(texto);
                if (negrito) span.Bold();
            }
        }

        /// <summary>Comprovante de venda em formato de cupom (largura de 80 mm).</summary>
        public static void ExportarComprovante(string caminho, Venda venda)
        {
            Document.Create(container =>
            {
                container.Page(pagina =>
                {
                    pagina.ContinuousSize(80, Unit.Millimetre);
                    pagina.Margin(6, Unit.Millimetre);
                    pagina.DefaultTextStyle(t => t.FontFamily(Fonte).FontSize(8).FontColor(Marca.TintaGrafite));

                    pagina.Content().Column(c =>
                    {
                        c.Spacing(3);
                        if (File.Exists(Marca.CaminhoLogo))
                            c.Item().AlignCenter().Height(34).Image(Marca.CaminhoLogo).FitArea();
                        c.Item().AlignCenter().Text("papelaria · escritório · informática").Italic().FontSize(7).FontColor(Marca.ArgilaRosada);
                        c.Item().PaddingVertical(4).LineHorizontal(0.5f).LineColor(Marca.TintaGrafite);

                        c.Item().Text($"COMPROVANTE DE VENDA Nº {venda.Id}").Bold().FontSize(9);
                        c.Item().Text($"Data: {venda.DataVenda:dd/MM/yyyy HH:mm}");
                        c.Item().Text($"Cliente: {venda.ClienteNome}");
                        c.Item().Text($"Atendido por: {venda.UsuarioNome}");
                        if (venda.Status == Models.Enums.StatusVenda.Cancelada)
                            c.Item().Text("*** VENDA CANCELADA ***").Bold().FontColor(Marca.ArgilaRosada);
                        c.Item().PaddingVertical(4).LineHorizontal(0.5f).LineColor(Marca.Linha);

                        foreach (var item in venda.Itens)
                        {
                            c.Item().Text(item.ProdutoNome).SemiBold();
                            c.Item().Row(r =>
                            {
                                r.RelativeItem().Text($"{item.Quantidade} x {Formatacao.Moeda(item.PrecoUnitario)}");
                                r.ConstantItem(70).AlignRight().Text(Formatacao.Moeda(item.ValorBruto));
                            });
                            if (item.Desconto > 0)
                                c.Item().Row(r =>
                                {
                                    r.RelativeItem().Text("  desconto promocional").FontColor(Marca.Salvia);
                                    r.ConstantItem(70).AlignRight().Text("-" + Formatacao.Moeda(item.Desconto)).FontColor(Marca.Salvia);
                                });
                        }

                        c.Item().PaddingVertical(4).LineHorizontal(0.5f).LineColor(Marca.TintaGrafite);
                        void Total(string rotulo, string valor, bool destaque = false)
                        {
                            c.Item().Row(r =>
                            {
                                var a = r.RelativeItem().Text(rotulo);
                                var b = r.ConstantItem(80).AlignRight().Text(valor);
                                if (destaque) { a.Bold().FontSize(11); b.Bold().FontSize(11); }
                            });
                        }
                        Total("Subtotal", Formatacao.Moeda(venda.ValorSubtotal));
                        Total("Descontos", "-" + Formatacao.Moeda(venda.ValorDesconto));
                        Total("TOTAL", Formatacao.Moeda(venda.ValorTotal), destaque: true);
                        c.Item().Text($"Pagamento: {venda.FormaPagamento.Descricao()}");
                        c.Item().PaddingTop(8).AlignCenter().Text("Agradecemos a preferência!").Italic().FontColor(Marca.ArgilaRosada);
                        c.Item().AlignCenter().Text("Documento sem valor fiscal").FontSize(6).FontColor(Marca.TextoSuave);
                    });
                });
            }).GeneratePdf(caminho);
        }
    }
}
