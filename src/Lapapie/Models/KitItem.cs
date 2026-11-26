namespace Lapapie.Models
{
    /// <summary>
    /// Um componente de um kit/combo (tabela kit_itens).
    /// Ex.: o "Kit Volta às Aulas" contém 3 canetas azuis → KitItem(produto=caneta, quantidade=3).
    /// </summary>
    public class KitItem
    {
        public int KitId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; } = 1;

        // ---- Exibição ----
        public string ProdutoNome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int EstoqueDisponivel { get; set; }

        /// <summary>Quanto os itens custariam se comprados separadamente.</summary>
        public decimal ValorAvulso => PrecoUnitario * Quantidade;
    }
}
