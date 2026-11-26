namespace Lapapie.Models
{
    /// <summary>Linha de um pedido de compra (tabela itens_pedido_compra — N:N pedido × produto).</summary>
    public class ItemPedidoCompra
    {
        public int PedidoCompraId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal CustoUnitario { get; set; }

        public string ProdutoNome { get; set; }

        public decimal Subtotal => Quantidade * CustoUnitario;
    }
}
