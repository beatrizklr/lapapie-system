using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Situação de um pedido de compra feito a um fornecedor.</summary>
    public enum StatusPedidoCompra
    {
        [Description("Aberto")]
        Aberto,

        [Description("Recebido")]
        Recebido,

        [Description("Cancelado")]
        Cancelado
    }
}
