namespace Lapapie.Models.Consultas
{
    /// <summary>Totais de um período (dashboard e relatório de vendas).</summary>
    public class IndicadoresVenda
    {
        public int QuantidadeVendas { get; set; }
        public int VendasCanceladas { get; set; }
        public decimal Faturamento { get; set; }
        public decimal TotalDescontos { get; set; }
        public int ItensVendidos { get; set; }
        public int ClientesAtendidos { get; set; }

        public decimal TicketMedio => QuantidadeVendas == 0 ? 0 : Math.Round(Faturamento / QuantidadeVendas, 2);
    }
}
