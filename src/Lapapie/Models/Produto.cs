using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Produto vendido pela papelaria (tabela produtos). Pode ser SIMPLES ou KIT.</summary>
    public class Produto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public TipoProduto Tipo { get; set; } = TipoProduto.Simples;
        public string UnidadeMedida { get; set; } = "UN";
        public decimal PrecoVenda { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // ---- Dados relacionados (preenchidos pelo DAO conforme a consulta) ----
        public string CategoriaNome { get; set; }

        /// <summary>
        /// Quantidade disponível para venda: saldo do estoque (SIMPLES) ou
        /// quantos kits dá para montar com os componentes (KIT).
        /// </summary>
        public int QuantidadeDisponivel { get; set; }
        public int EstoqueMinimo { get; set; }

        /// <summary>Melhor promoção vigente para o produto (null se não houver).</summary>
        public Promocao PromocaoVigente { get; set; }

        /// <summary>Saldo/parâmetros de estoque (usado no cadastro de produto SIMPLES).</summary>
        public Estoque Estoque { get; set; }

        /// <summary>Componentes, quando o produto é um KIT.</summary>
        public List<KitItem> ItensKit { get; set; } = new();

        /// <summary>Fornecedores que vendem este produto (N:N).</summary>
        public List<FornecedorProduto> Fornecedores { get; set; } = new();

        public bool EhKit => Tipo == TipoProduto.Kit;

        /// <summary>Preço unitário já com a promoção vigente aplicada.</summary>
        public decimal PrecoPromocional => PromocaoVigente == null
            ? PrecoVenda
            : PrecoVenda - PromocaoVigente.CalcularDesconto(PrecoVenda, 1);

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Validacao.Vazio(Nome)) erros.Add("Informe o nome do produto.");
            else if (Nome.Length > 120) erros.Add("O nome pode ter no máximo 120 caracteres.");
            if (CategoriaId <= 0) erros.Add("Selecione a categoria.");
            if (PrecoVenda <= 0) erros.Add("O preço de venda deve ser maior que zero.");
            if (!Validacao.Vazio(CodigoBarras) && !Validacao.CodigoBarrasValido(CodigoBarras))
                erros.Add("O código de barras deve ter de 8 a 14 dígitos numéricos.");
            if (Validacao.Vazio(UnidadeMedida)) erros.Add("Informe a unidade de medida.");
            if (Descricao?.Length > 500) erros.Add("A descrição pode ter no máximo 500 caracteres.");

            if (EhKit)
            {
                if (ItensKit.Count == 0) erros.Add("Um kit precisa ter pelo menos um produto na composição.");
                if (ItensKit.Any(i => i.ProdutoId == Id && Id > 0)) erros.Add("Um kit não pode conter a si mesmo.");
                if (ItensKit.Any(i => i.Quantidade <= 0)) erros.Add("As quantidades do kit devem ser maiores que zero.");
            }
            if (Fornecedores.Any(f => f.PrecoCusto < 0)) erros.Add("O preço de custo não pode ser negativo.");
            if (Estoque != null) erros.AddRange(Estoque.Validar());
            return erros;
        }

        public override string ToString() => Nome;
    }
}
