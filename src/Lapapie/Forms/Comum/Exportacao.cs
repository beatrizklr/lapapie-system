using System.Diagnostics;
using Lapapie.Relatorios;
using Lapapie.Util;

namespace Lapapie.Forms.Comum
{
    /// <summary>Liga as telas aos exportadores de Excel e PDF (menu, diálogo de salvar e abertura do arquivo).</summary>
    public static class Exportacao
    {
        /// <summary>Monta uma tabela de exportação com as colunas visíveis de um DataGridView.</summary>
        public static TabelaExportacao DeGrid(DataGridView grid, string titulo, string subtitulo = null)
        {
            var tabela = new TabelaExportacao(titulo, subtitulo);
            var colunas = grid.Columns.Cast<DataGridViewColumn>()
                              .Where(c => c.Visible && c is not DataGridViewImageColumn && c is not DataGridViewButtonColumn)
                              .OrderBy(c => c.DisplayIndex).ToList();
            foreach (var coluna in colunas)
            {
                var formato = coluna.DefaultCellStyle.Format switch
                {
                    "C2" => FormatoColuna.Moeda,
                    "N0" => FormatoColuna.Inteiro,
                    "dd/MM/yyyy" => FormatoColuna.Data,
                    "dd/MM/yyyy HH:mm" => FormatoColuna.DataHora,
                    _ => FormatoColuna.Texto
                };
                float largura = coluna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill ? 2.2f : Math.Max(0.6f, coluna.Width / 110f);
                tabela.Coluna(coluna.HeaderText, formato, largura);
            }

            foreach (DataGridViewRow linha in grid.Rows)
            {
                var valores = colunas.Select(c =>
                {
                    var valor = linha.Cells[c.Index].Value;
                    return valor switch
                    {
                        Enum e => e.Descricao(),
                        bool b => b ? "Sim" : "Não",
                        _ => valor
                    };
                }).ToArray();
                tabela.AdicionarLinha(valores);
            }
            return tabela;
        }

        /// <summary>Mostra um menu "Excel / PDF" logo abaixo do botão clicado.</summary>
        public static void MostrarMenu(Control origem, string nomeArquivo, Func<TabelaExportacao> montarTabela, bool paisagem = false)
        {
            var menu = new ContextMenuStrip { Font = Tema.Tema.Texto(10f), ShowImageMargin = false, BackColor = Tema.Tema.Superficie };
            // O menu é descartado só depois que a ação termina (ela abre diálogos modais).
            void RodarEDescartar(Action acao)
            {
                try { acao(); }
                finally { origem.BeginInvoke(new Action(menu.Dispose)); }
            }
            menu.Items.Add("Planilha Excel (.xlsx)", null, (s, e) => RodarEDescartar(() => ParaExcel(nomeArquivo, montarTabela())));
            menu.Items.Add("Documento PDF (.pdf)", null, (s, e) => RodarEDescartar(() =>
                ParaPdf(nomeArquivo, caminho => ExportadorPdf.ExportarTabela(caminho, montarTabela(), paisagem))));
            menu.Closed += (s, e) =>
            {
                if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
                    origem.BeginInvoke(new Action(menu.Dispose));
            };
            menu.Show(origem, new Point(0, origem.Height + 4));
        }

        public static void ParaExcel(string nomeArquivo, params TabelaExportacao[] tabelas)
        {
            var caminho = PerguntarCaminho(nomeArquivo, "Planilha do Excel|*.xlsx", ".xlsx");
            if (caminho == null) return;
            Executar(() => ExportadorExcel.Exportar(caminho, tabelas), caminho);
        }

        /// <summary>Pergunta onde salvar e executa a geração do PDF.</summary>
        public static void ParaPdf(string nomeArquivo, Action<string> gerar)
        {
            var caminho = PerguntarCaminho(nomeArquivo, "Documento PDF|*.pdf", ".pdf");
            if (caminho == null) return;
            Executar(() => gerar(caminho), caminho);
        }

        private static string PerguntarCaminho(string nomeArquivo, string filtro, string extensao)
        {
            using var dialogo = new SaveFileDialog
            {
                Filter = filtro,
                FileName = $"{nomeArquivo}-{DateTime.Now:yyyyMMdd-HHmm}{extensao}",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                OverwritePrompt = true
            };
            return dialogo.ShowDialog() == DialogResult.OK ? dialogo.FileName : null;
        }

        private static void Executar(Action gerar, string caminho)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                gerar();
                Cursor.Current = Cursors.Default;
                if (Mensagem.Confirmar($"Arquivo gerado em:\n{caminho}\n\nDeseja abri-lo agora?", "Exportação concluída", "Abrir arquivo", "Agora não"))
                    Process.Start(new ProcessStartInfo(caminho) { UseShellExecute = true });
            }
            catch (IOException ex)
            {
                Mensagem.Alerta("Não foi possível gravar o arquivo. Se ele estiver aberto no Excel ou no leitor de PDF, feche-o e tente novamente.\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Mensagem.TratarErro(ex, "Exportação");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
