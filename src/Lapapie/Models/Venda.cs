using Lapapie.Models.Enums;

namespace Lapapie.Models
{
    /// <summary>Cabeçalho de uma venda (tabela vendas).</summary>
    public class Venda
    {
        public int Id { get; set; }
        /// <summary>Null = consumidor não identificado.</summary>
        public int? ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public FormaPagamento FormaPagamento { get; set; } = FormaPagamento.Pix;
        public StatusVenda Status { get; set; } = StatusVenda.Concluida;
        public string Observacao { get; set; }

        public List<ItemVenda> Itens { get; set; } = new();

        // ---- Exibição ----
        public string ClienteNome { get; set; }
        public string UsuarioNome { get; set; }

        /// <summary>No banco é uma coluna GERADA (subtotal - desconto); aqui calculamos igual.</summary>
        public decimal ValorTotal => ValorSubtotal - ValorDesconto;

        public int TotalItens => Itens.Sum(i => i.Quantidade);

        /// <summary>
        /// Cálculo automático do total: soma os itens. Chamado sempre que o
        /// carrinho muda, para a tela e o banco usarem exatamente os mesmos valores.
        /// </summary>
        public void RecalcularTotais()
        {
            ValorSubtotal = Itens.Sum(i => i.ValorBruto);
            ValorDesconto = Itens.Sum(i => i.Desconto);
        }

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Itens.Count == 0) erros.Add("Adicione pelo menos um produto à venda.");
            if (Itens.Any(i => i.Quantidade <= 0)) erros.Add("Todas as quantidades devem ser maiores que zero.");
            if (Itens.GroupBy(i => i.ProdutoId).Any(g => g.Count() > 1)) erros.Add("Há produtos repetidos no carrinho.");
            if (UsuarioId <= 0) erros.Add("Usuário da venda não identificado.");
            if (Observacao?.Length > 255) erros.Add("A observação pode ter no máximo 255 caracteres.");
            return erros;
        }
    }
}
