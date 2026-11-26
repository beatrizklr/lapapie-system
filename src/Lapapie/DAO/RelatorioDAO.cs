using Lapapie.Models.Consultas;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>
    /// Consultas agregadas (GROUP BY) usadas pelo dashboard e pelo relatório de vendas.
    /// Só vendas CONCLUÍDAS entram nos valores; canceladas são contadas à parte.
    /// Todas as consultas recebem um período [inicio, fim] por dia inteiro.
    /// </summary>
    public class RelatorioDAO
    {
        private static void Periodo(MySqlCommand cmd, DateTime inicio, DateTime fim)
        {
            cmd.Parameters.AddWithValue("@inicio", inicio.Date);
            cmd.Parameters.AddWithValue("@fim", fim.Date.AddDays(1)); // limite exclusivo
        }

        /// <summary>Filtro opcional por vendedor (null = todos).</summary>
        private static string FiltroVendedor(MySqlCommand cmd, int? vendedorId, string alias = "v")
        {
            if (!vendedorId.HasValue) return string.Empty;
            cmd.Parameters.AddWithValue("@vendedor", vendedorId.Value);
            return $" AND {alias}.usuario_id = @vendedor";
        }

        public IndicadoresVenda ObterIndicadores(DateTime inicio, DateTime fim, int? vendedorId = null)
        {
            return DaoUtil.Executar(conexao =>
            {
                var indicadores = new IndicadoresVenda();
                using (var cmd = DaoUtil.Comando(conexao, ""))
                {
                    Periodo(cmd, inicio, fim);
                    cmd.CommandText = @"SELECT
                            SUM(v.status = 'CONCLUIDA') AS qtd,
                            SUM(v.status = 'CANCELADA') AS canceladas,
                            COALESCE(SUM(CASE WHEN v.status = 'CONCLUIDA' THEN v.valor_total END), 0) AS faturamento,
                            COALESCE(SUM(CASE WHEN v.status = 'CONCLUIDA' THEN v.valor_desconto END), 0) AS descontos,
                            COUNT(DISTINCT CASE WHEN v.status = 'CONCLUIDA' THEN v.cliente_id END) AS clientes
                        FROM vendas v
                        WHERE v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId);
                    using var r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        indicadores.QuantidadeVendas = r.Inteiro("qtd");
                        indicadores.VendasCanceladas = r.Inteiro("canceladas");
                        indicadores.Faturamento = r.Valor("faturamento");
                        indicadores.TotalDescontos = r.Valor("descontos");
                        indicadores.ClientesAtendidos = r.Inteiro("clientes");
                    }
                }
                using (var cmd = DaoUtil.Comando(conexao, ""))
                {
                    Periodo(cmd, inicio, fim);
                    cmd.CommandText = @"SELECT COALESCE(SUM(iv.quantidade), 0)
                        FROM itens_venda iv JOIN vendas v ON v.id = iv.venda_id
                        WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId);
                    indicadores.ItensVendidos = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return indicadores;
            });
        }

        /// <summary>Faturamento por dia. Dias sem venda aparecem com zero (o gráfico fica contínuo).</summary>
        public List<ItemSerie> FaturamentoPorDia(DateTime inicio, DateTime fim, int? vendedorId = null)
        {
            var dados = DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT DATE(v.data_venda) AS dia, SUM(v.valor_total) AS total, COUNT(*) AS qtd
                    FROM vendas v
                    WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId) + @"
                    GROUP BY DATE(v.data_venda)";
                return DaoUtil.Listar(cmd, r => (Dia: r.Data("dia").Date, Total: r.Valor("total"), Qtd: r.Inteiro("qtd")))
                              .ToDictionary(x => x.Dia);
            });

            var serie = new List<ItemSerie>();
            for (var dia = inicio.Date; dia <= fim.Date; dia = dia.AddDays(1))
            {
                dados.TryGetValue(dia, out var d);
                serie.Add(new ItemSerie(dia.ToString("dd/MM"), d.Total, d.Qtd));
            }
            return serie;
        }

        /// <summary>Faturamento dos últimos N meses (inclui o mês atual).</summary>
        public List<ItemSerie> FaturamentoPorMes(int meses, DateTime referencia)
        {
            var primeiroMes = new DateTime(referencia.Year, referencia.Month, 1).AddMonths(-(meses - 1));
            var dados = DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao,
                    @"SELECT YEAR(data_venda) AS ano, MONTH(data_venda) AS mes, SUM(valor_total) AS total, COUNT(*) AS qtd
                      FROM vendas
                      WHERE status = 'CONCLUIDA' AND data_venda >= @inicio
                      GROUP BY YEAR(data_venda), MONTH(data_venda)");
                cmd.Parameters.AddWithValue("@inicio", primeiroMes);
                return DaoUtil.Listar(cmd, r => (Ano: r.Inteiro("ano"), Mes: r.Inteiro("mes"), Total: r.Valor("total"), Qtd: r.Inteiro("qtd")))
                              .ToDictionary(x => (x.Ano, x.Mes));
            });

            var serie = new List<ItemSerie>();
            for (int i = 0; i < meses; i++)
            {
                var mes = primeiroMes.AddMonths(i);
                dados.TryGetValue((mes.Year, mes.Month), out var d);
                var rotulo = mes.ToString("MMM/yy", Formatacao.PtBr).Replace(".", "");
                serie.Add(new ItemSerie(char.ToUpper(rotulo[0]) + rotulo[1..], d.Total, d.Qtd));
            }
            return serie;
        }

        public List<ItemSerie> FaturamentoPorCategoria(DateTime inicio, DateTime fim, int? vendedorId = null)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT c.nome AS rotulo, SUM(iv.subtotal) AS total, SUM(iv.quantidade) AS qtd
                    FROM itens_venda iv
                    JOIN vendas v     ON v.id = iv.venda_id
                    JOIN produtos p   ON p.id = iv.produto_id
                    JOIN categorias c ON c.id = p.categoria_id
                    WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId) + @"
                    GROUP BY c.id, c.nome
                    ORDER BY total DESC";
                return DaoUtil.Listar(cmd, r => new ItemSerie(r.Texto("rotulo"), r.Valor("total"), r.Inteiro("qtd")));
            });
        }

        public List<ItemSerie> FaturamentoPorFormaPagamento(DateTime inicio, DateTime fim, int? vendedorId = null)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT v.forma_pagamento, SUM(v.valor_total) AS total, COUNT(*) AS qtd
                    FROM vendas v
                    WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId) + @"
                    GROUP BY v.forma_pagamento
                    ORDER BY total DESC";
                return DaoUtil.Listar(cmd, r => new ItemSerie(
                    EnumBanco.DoBanco<FormaPagamento>(r.Texto("forma_pagamento")).Descricao(), r.Valor("total"), r.Inteiro("qtd")));
            });
        }

        public List<ItemSerie> FaturamentoPorVendedor(DateTime inicio, DateTime fim)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT u.nome AS rotulo, SUM(v.valor_total) AS total, COUNT(*) AS qtd
                    FROM vendas v JOIN usuarios u ON u.id = v.usuario_id
                    WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim
                    GROUP BY u.id, u.nome
                    ORDER BY total DESC";
                return DaoUtil.Listar(cmd, r => new ItemSerie(r.Texto("rotulo"), r.Valor("total"), r.Inteiro("qtd")));
            });
        }

        /// <summary>Ranking de produtos. limite = null traz todos.</summary>
        public List<VendaPorProduto> ProdutosMaisVendidos(DateTime inicio, DateTime fim, int? limite, int? vendedorId = null)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT p.id, p.nome, c.nome AS categoria, SUM(iv.quantidade) AS qtd, SUM(iv.subtotal) AS total
                    FROM itens_venda iv
                    JOIN vendas v     ON v.id = iv.venda_id
                    JOIN produtos p   ON p.id = iv.produto_id
                    JOIN categorias c ON c.id = p.categoria_id
                    WHERE v.status = 'CONCLUIDA' AND v.data_venda >= @inicio AND v.data_venda < @fim" + FiltroVendedor(cmd, vendedorId) + @"
                    GROUP BY p.id, p.nome, c.nome
                    ORDER BY qtd DESC, total DESC" + (limite.HasValue ? " LIMIT @limite" : "");
                if (limite.HasValue) cmd.Parameters.AddWithValue("@limite", limite.Value);
                return DaoUtil.Listar(cmd, r => new VendaPorProduto
                {
                    ProdutoId = r.Inteiro("id"),
                    Produto = r.Texto("nome"),
                    Categoria = r.Texto("categoria"),
                    Quantidade = r.Inteiro("qtd"),
                    Faturamento = r.Valor("total")
                });
            });
        }

        /// <summary>Lista detalhada de vendas do período (para exportação).</summary>
        public List<HistoricoVenda> ListarVendas(DateTime inicio, DateTime fim, int? vendedorId = null)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                Periodo(cmd, inicio, fim);
                cmd.CommandText = @"SELECT h.*, v.observacao FROM vw_historico_vendas h
                    JOIN vendas v ON v.id = h.venda_id
                    WHERE h.data_venda >= @inicio AND h.data_venda < @fim" + FiltroVendedor(cmd, vendedorId) + @"
                    ORDER BY h.data_venda";
                return DaoUtil.Listar(cmd, VendaDAO.MapearHistorico);
            });
        }

        /// <summary>Data da venda mais recente (útil para sugerir o período do relatório).</summary>
        public DateTime? DataUltimaVenda()
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "SELECT MAX(data_venda) FROM vendas");
                var valor = cmd.ExecuteScalar();
                return valor == null || valor == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(valor);
            });
        }
    }
}
