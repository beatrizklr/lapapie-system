using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Models.Consultas
{
    /// <summary>Linha da view vw_historico_vendas.</summary>
    public class HistoricoVenda
    {
        public int VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public StatusVenda Status { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }

        public string FormaPagamentoDescricao => FormaPagamento.Descricao();
    }
}
