using Lapapie.Models.Enums;

namespace Lapapie.Models
{
    /// <summary>Compra feita a um fornecedor (tabela pedidos_compra).</summary>
    public class PedidoCompra
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public DateTime? DataPrevistaEntrega { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public StatusPedidoCompra Status { get; set; } = StatusPedidoCompra.Aberto;
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }

        public List<ItemPedidoCompra> Itens { get; set; } = new();

        // ---- Exibição ----
        public string FornecedorNome { get; set; }
        public string UsuarioNome { get; set; }
        public int QuantidadeItens { get; set; }

        public bool PodeSerAlterado => Status == StatusPedidoCompra.Aberto;

        public void RecalcularTotal() => ValorTotal = Itens.Sum(i => i.Subtotal);

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (FornecedorId <= 0) erros.Add("Selecione o fornecedor.");
            if (Itens.Count == 0) erros.Add("Adicione pelo menos um produto ao pedido.");
            if (Itens.Any(i => i.Quantidade <= 0)) erros.Add("As quantidades devem ser maiores que zero.");
            if (Itens.Any(i => i.CustoUnitario < 0)) erros.Add("O custo unitário não pode ser negativo.");
            if (Itens.GroupBy(i => i.ProdutoId).Any(g => g.Count() > 1)) erros.Add("Há produtos repetidos no pedido.");
            if (DataPrevistaEntrega.HasValue && DataPrevistaEntrega.Value.Date < DataPedido.Date)
                erros.Add("A previsão de entrega não pode ser anterior à data do pedido.");
            if (Observacao?.Length > 255) erros.Add("A observação pode ter no máximo 255 caracteres.");
            return erros;
        }
    }
}
