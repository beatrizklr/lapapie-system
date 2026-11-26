using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>
    /// Registro histórico de toda entrada e saída de estoque (tabela movimentacoes_estoque).
    /// O saldo da tabela estoque é o resultado da soma destes registros: guardar o
    /// histórico permite auditar "por que o saldo está assim?".
    /// </summary>
    public class MovimentacaoEstoque
    {
        public long Id { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public MotivoMovimentacao Motivo { get; set; }
        public int Quantidade { get; set; }
        public int? VendaId { get; set; }
        public int? PedidoCompraId { get; set; }
        public string Observacao { get; set; }
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        // ---- Exibição ----
        public string ProdutoNome { get; set; }
        public string UsuarioNome { get; set; }

        /// <summary>Quantidade com sinal: +10 para entrada, -3 para saída.</summary>
        public int QuantidadeComSinal => Tipo == TipoMovimentacao.Entrada ? Quantidade : -Quantidade;

        public string MotivoDescricao => Motivo.Descricao();

        public string Origem => VendaId.HasValue ? $"Venda nº {VendaId}"
            : PedidoCompraId.HasValue ? $"Pedido nº {PedidoCompraId}"
            : "Manual";

        /// <summary>Mesmas regras das restrições CHECK do banco (ck_mov_*).</summary>
        public List<string> Validar()
        {
            var erros = new List<string>();
            if (ProdutoId <= 0) erros.Add("Selecione o produto.");
            if (Quantidade <= 0) erros.Add("A quantidade deve ser maior que zero.");
            if (Motivo == MotivoMovimentacao.Venda && (Tipo != TipoMovimentacao.Saida || VendaId == null))
                erros.Add("Movimentações de venda são geradas automaticamente pela tela de vendas.");
            if (Motivo == MotivoMovimentacao.Compra && (Tipo != TipoMovimentacao.Entrada || PedidoCompraId == null))
                erros.Add("Entradas de compra são geradas ao receber um pedido de compra.");
            if (Motivo == MotivoMovimentacao.Perda && Tipo != TipoMovimentacao.Saida)
                erros.Add("Perda só pode ser uma saída.");
            if (Motivo == MotivoMovimentacao.Ajuste || Motivo == MotivoMovimentacao.Perda)
            {
                if (Validacao.Vazio(Observacao)) erros.Add("Descreva o motivo na observação.");
            }
            if (Observacao?.Length > 255) erros.Add("A observação pode ter no máximo 255 caracteres.");
            return erros;
        }
    }
}
