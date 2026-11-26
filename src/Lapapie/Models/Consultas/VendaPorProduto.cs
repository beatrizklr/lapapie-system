namespace Lapapie.Models.Consultas
{
    /// <summary>Ranking de produtos vendidos em um período.</summary>
    public class VendaPorProduto
    {
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal Faturamento { get; set; }
    }
}
