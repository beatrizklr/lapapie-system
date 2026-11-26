namespace Lapapie.Models
{
    /// <summary>Situação do saldo em relação ao estoque mínimo.</summary>
    public enum SituacaoEstoque { Normal, Baixo, Zerado }

    /// <summary>
    /// Saldo atual de um produto SIMPLES (tabela estoque, relação 1:1 com produtos).
    /// </summary>
    public class Estoque
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int EstoqueMinimo { get; set; } = 5;
        public string Localizacao { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // ---- Dados do produto (vindos do JOIN, só para exibição) ----
        public string ProdutoNome { get; set; }
        public string CodigoBarras { get; set; }
        public string CategoriaNome { get; set; }
        public string UnidadeMedida { get; set; }

        public SituacaoEstoque Situacao =>
            Quantidade <= 0 ? SituacaoEstoque.Zerado
            : Quantidade <= EstoqueMinimo ? SituacaoEstoque.Baixo
            : SituacaoEstoque.Normal;

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (EstoqueMinimo < 0) erros.Add("O estoque mínimo não pode ser negativo.");
            if (Localizacao?.Length > 50) erros.Add("A localização pode ter no máximo 50 caracteres.");
            return erros;
        }
    }
}
