namespace Lapapie.Models
{
    /// <summary>
    /// Relação N:N entre fornecedor e produto (tabela fornecedor_produto):
    /// quanto cada fornecedor cobra por um produto e em quantos dias entrega.
    /// </summary>
    public class FornecedorProduto
    {
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }
        public string CodigoNoFornecedor { get; set; }
        public decimal PrecoCusto { get; set; }
        public int? PrazoEntregaDias { get; set; }

        // ---- Exibição ----
        public string FornecedorNome { get; set; }
        public string ProdutoNome { get; set; }
    }
}
