using ClosedXML.Excel;

namespace Lapapie.Relatorios
{
    /// <summary>Gera planilhas .xlsx (uma aba por tabela) com a biblioteca ClosedXML.</summary>
    public static class ExportadorExcel
    {
        public static void Exportar(string caminho, params TabelaExportacao[] tabelas)
        {
            using var pasta = new XLWorkbook();
            var nomesUsados = new HashSet<string>();

            foreach (var tabela in tabelas)
            {
                var aba = pasta.Worksheets.Add(NomeAba(tabela.Titulo, nomesUsados));
                int linha = 1;

                aba.Cell(linha, 1).Value = tabela.Titulo;
                aba.Cell(linha, 1).Style.Font.SetBold().Font.SetFontSize(16).Font.SetFontColor(XLColor.FromHtml(Marca.TintaGrafite));
                linha++;
                aba.Cell(linha, 1).Value = (tabela.Subtitulo ?? "") + $"  ·  Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}";
                aba.Cell(linha, 1).Style.Font.SetItalic().Font.SetFontColor(XLColor.FromHtml(Marca.ArgilaRosada));
                linha += 2;

                int linhaCabecalho = linha;
                for (int c = 0; c < tabela.Colunas.Count; c++)
                {
                    var celula = aba.Cell(linha, c + 1);
                    celula.Value = tabela.Colunas[c].Cabecalho;
                    celula.Style.Font.SetBold()
                        .Font.SetFontColor(XLColor.FromHtml(Marca.PapelAlgodao))
                        .Fill.SetBackgroundColor(XLColor.FromHtml(Marca.TintaGrafite))
                        .Alignment.SetHorizontal(tabela.Colunas[c].AlinharDireita ? XLAlignmentHorizontalValues.Right : XLAlignmentHorizontalValues.Left);
                }

                foreach (var valores in tabela.Linhas)
                {
                    linha++;
                    EscreverLinha(aba, linha, tabela, valores);
                    if ((linha - linhaCabecalho) % 2 == 0)
                        aba.Range(linha, 1, linha, tabela.Colunas.Count).Style.Fill.SetBackgroundColor(XLColor.FromHtml(Marca.Superficie));
                }

                if (tabela.Linhas.Count > 0)
                    aba.Range(linhaCabecalho, 1, linha, tabela.Colunas.Count).SetAutoFilter();

                if (tabela.Totais != null)
                {
                    linha++;
                    EscreverLinha(aba, linha, tabela, tabela.Totais);
                    var faixa = aba.Range(linha, 1, linha, tabela.Colunas.Count);
                    faixa.Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.FromHtml(Marca.PapelAlgodao));
                    faixa.Style.Border.SetTopBorder(XLBorderStyleValues.Thin).Border.SetTopBorderColor(XLColor.FromHtml(Marca.TintaGrafite));
                }

                aba.SheetView.FreezeRows(linhaCabecalho);
                aba.Columns(1, tabela.Colunas.Count).AdjustToContents(linhaCabecalho, linha);
                foreach (var coluna in aba.Columns(1, tabela.Colunas.Count))
                    if (coluna.Width > 60) coluna.Width = 60;
            }

            pasta.SaveAs(caminho);
        }

        private static void EscreverLinha(IXLWorksheet aba, int linha, TabelaExportacao tabela, object[] valores)
        {
            for (int c = 0; c < tabela.Colunas.Count && c < valores.Length; c++)
            {
                var celula = aba.Cell(linha, c + 1);
                var valor = valores[c];
                var formato = tabela.Colunas[c].Formato;
                if (valor == null || valor == DBNull.Value) continue;

                switch (formato)
                {
                    case FormatoColuna.Moeda:
                        celula.Value = Convert.ToDecimal(valor);
                        celula.Style.NumberFormat.Format = "\"R$\" #,##0.00";
                        break;
                    case FormatoColuna.Inteiro:
                        celula.Value = Convert.ToDecimal(valor);
                        celula.Style.NumberFormat.Format = "#,##0";
                        break;
                    case FormatoColuna.Percentual:
                        celula.Value = Convert.ToDecimal(valor) / 100m;
                        celula.Style.NumberFormat.Format = "0.0%";
                        break;
                    case FormatoColuna.Data when valor is DateTime d:
                        celula.Value = d;
                        celula.Style.DateFormat.Format = "dd/mm/yyyy";
                        break;
                    case FormatoColuna.DataHora when valor is DateTime dh:
                        celula.Value = dh;
                        celula.Style.DateFormat.Format = "dd/mm/yyyy hh:mm";
                        break;
                    default:
                        celula.Value = valor.ToString();
                        break;
                }
            }
        }

        /// <summary>O Excel limita nomes de aba a 31 caracteres e proíbe alguns símbolos.</summary>
        private static string NomeAba(string titulo, HashSet<string> usados)
        {
            var nome = new string((titulo ?? "Dados").Where(ch => !"[]:*?/\\".Contains(ch)).ToArray()).Trim();
            if (nome.Length > 28) nome = nome[..28];
            if (string.IsNullOrEmpty(nome)) nome = "Dados";
            var final = nome;
            int n = 2;
            while (!usados.Add(final)) final = $"{nome} {n++}";
            return final;
        }
    }
}
