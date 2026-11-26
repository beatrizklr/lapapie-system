using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>SIMPLES tem saldo próprio em estoque; KIT é um combo montado a partir de outros produtos.</summary>
    public enum TipoProduto
    {
        [Description("Simples")]
        Simples,

        [Description("Kit / combo")]
        Kit
    }
}
