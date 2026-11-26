using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Direção da movimentação de estoque.</summary>
    public enum TipoMovimentacao
    {
        [Description("Entrada")]
        Entrada,

        [Description("Saída")]
        Saida
    }
}
