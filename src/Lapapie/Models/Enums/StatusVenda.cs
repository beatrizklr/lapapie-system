using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Situação de uma venda.</summary>
    public enum StatusVenda
    {
        [Description("Concluída")]
        Concluida,

        [Description("Cancelada")]
        Cancelada
    }
}
