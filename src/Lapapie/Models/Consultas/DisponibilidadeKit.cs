namespace Lapapie.Models.Consultas
{
    /// <summary>Linha da view vw_disponibilidade_kits: quantos kits dá para montar.</summary>
    public class DisponibilidadeKit
    {
        public int KitId { get; set; }
        public string Kit { get; set; }
        public decimal PrecoVenda { get; set; }
        public int QuantidadeDisponivel { get; set; }
        /// <summary>Soma dos preços dos componentes comprados separadamente.</summary>
        public decimal ValorAvulso { get; set; }
        public int QuantidadeComponentes { get; set; }
        public bool Ativo { get; set; } = true;

        public decimal Economia => Math.Max(0, ValorAvulso - PrecoVenda);
    }
}
