using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Forma de cálculo do desconto de uma promoção.</summary>
    public enum TipoDesconto
    {
        [Description("Percentual (%)")]
        Percentual,

        [Description("Valor fixo por unidade (R$)")]
        ValorFixo
    }
}
