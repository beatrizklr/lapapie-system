using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Motivo que gerou a movimentação de estoque.</summary>
    public enum MotivoMovimentacao
    {
        [Description("Venda")]
        Venda,

        [Description("Compra")]
        Compra,

        [Description("Ajuste de inventário")]
        Ajuste,

        [Description("Devolução")]
        Devolucao,

        [Description("Perda / avaria")]
        Perda
    }
}
