using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Formas de pagamento aceitas no caixa.</summary>
    public enum FormaPagamento
    {
        [Description("Dinheiro")]
        Dinheiro,

        [Description("PIX")]
        Pix,

        [Description("Cartão de débito")]
        CartaoDebito,

        [Description("Cartão de crédito")]
        CartaoCredito
    }
}
