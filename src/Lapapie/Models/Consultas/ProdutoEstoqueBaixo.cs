namespace Lapapie.Models.Consultas
{
    /// <summary>Linha da view vw_produtos_estoque_baixo.</summary>
    public class ProdutoEstoqueBaixo
    {
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public int EstoqueMinimo { get; set; }
        public int Deficit { get; set; }
        public string Localizacao { get; set; }

        /// <summary>Sugestão de compra: repor até o dobro do mínimo.</summary>
        public int SugestaoCompra => Math.Max(1, EstoqueMinimo * 2 - Quantidade);
    }
}
