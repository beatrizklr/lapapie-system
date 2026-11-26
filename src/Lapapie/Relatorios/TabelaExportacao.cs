namespace Lapapie.Relatorios
{
    /// <summary>Como o valor de uma coluna deve ser formatado na exportação.</summary>
    public enum FormatoColuna { Texto, Inteiro, Moeda, Percentual, Data, DataHora }

    /// <summary>Definição de uma coluna exportada.</summary>
    public class ColunaExportacao
    {
        public string Cabecalho { get; set; }
        public FormatoColuna Formato { get; set; } = FormatoColuna.Texto;
        /// <summary>Largura relativa no PDF (1 = padrão).</summary>
        public float Largura { get; set; } = 1f;

        public ColunaExportacao(string cabecalho, FormatoColuna formato = FormatoColuna.Texto, float largura = 1f)
        {
            Cabecalho = cabecalho;
            Formato = formato;
            Largura = largura;
        }

        public bool AlinharDireita => Formato is FormatoColuna.Inteiro or FormatoColuna.Moeda or FormatoColuna.Percentual;
    }

    /// <summary>
    /// Estrutura neutra (sem dependência de tela) com os dados a exportar.
    /// As telas montam uma TabelaExportacao e os exportadores de Excel/PDF a desenham.
    /// </summary>
    public class TabelaExportacao
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public List<ColunaExportacao> Colunas { get; } = new();
        public List<object[]> Linhas { get; } = new();

        /// <summary>Linha de totais exibida no fim (opcional).</summary>
        public object[] Totais { get; set; }

        public TabelaExportacao(string titulo, string subtitulo = null)
        {
            Titulo = titulo;
            Subtitulo = subtitulo;
        }

        public TabelaExportacao Coluna(string cabecalho, FormatoColuna formato = FormatoColuna.Texto, float largura = 1f)
        {
            Colunas.Add(new ColunaExportacao(cabecalho, formato, largura));
            return this;
        }

        public void AdicionarLinha(params object[] valores) => Linhas.Add(valores);

        /// <summary>Texto formatado de uma célula (usado no PDF).</summary>
        public static string Formatar(object valor, FormatoColuna formato)
        {
            if (valor == null || valor == DBNull.Value) return "";
            var br = Util.Formatacao.PtBr;
            return formato switch
            {
                FormatoColuna.Moeda => Convert.ToDecimal(valor).ToString("C2", br),
                FormatoColuna.Inteiro => Convert.ToDecimal(valor).ToString("N0", br),
                FormatoColuna.Percentual => Convert.ToDecimal(valor).ToString("N1", br) + "%",
                FormatoColuna.Data => valor is DateTime d ? d.ToString("dd/MM/yyyy") : valor.ToString(),
                FormatoColuna.DataHora => valor is DateTime dh ? dh.ToString("dd/MM/yyyy HH:mm") : valor.ToString(),
                _ => valor.ToString()
            };
        }
    }

    /// <summary>Documento PDF composto: indicadores, gráficos (imagens) e tabelas.</summary>
    public class DocumentoRelatorio
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public List<(string Rotulo, string Valor)> Indicadores { get; } = new();
        public List<(string Titulo, byte[] Png)> Graficos { get; } = new();
        public List<TabelaExportacao> Tabelas { get; } = new();
        public bool Paisagem { get; set; }
    }
}
