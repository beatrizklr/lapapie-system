using Lapapie.Models.Enums;

namespace Lapapie.Models
{
    /// <summary>Linha de uma venda (tabela itens_venda — N:N entre vendas e produtos).</summary>
    public class ItemVenda
    {
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        /// <summary>Preço no momento da venda: mudar o preço do produto depois não altera o histórico.</summary>
        public decimal PrecoUnitario { get; set; }
        /// <summary>Desconto TOTAL do item (todas as unidades).</summary>
        public decimal Desconto { get; set; }

        // ---- Exibição / apoio ----
        public string ProdutoNome { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public string PromocaoNome { get; set; }

        /// <summary>Promoção usada para recalcular o desconto quando a quantidade muda.</summary>
        public Promocao Promocao { get; set; }

        public decimal ValorBruto => Quantidade * PrecoUnitario;
        public decimal Subtotal => ValorBruto - Desconto;

        /// <summary>Recalcula o desconto a partir da promoção (se houver).</summary>
        public void RecalcularDesconto() =>
            Desconto = Promocao?.CalcularDesconto(PrecoUnitario, Quantidade) ?? 0m;
    }
}
