using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Situação de uma promoção em relação à data atual.</summary>
    public enum SituacaoPromocao { Vigente, Agendada, Encerrada, Inativa }

    /// <summary>Promoção com período de validade (tabela promocoes).</summary>
    public class Promocao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoDesconto TipoDesconto { get; set; } = TipoDesconto.Percentual;
        public decimal ValorDesconto { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Today;
        public DateTime DataFim { get; set; } = DateTime.Today.AddDays(7).AddSeconds(-1);
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }

        /// <summary>Produtos participantes (relação N:N promocao_produto).</summary>
        public List<int> ProdutoIds { get; set; } = new();

        /// <summary>Preenchido na listagem (COUNT).</summary>
        public int QuantidadeProdutos { get; set; }

        public SituacaoPromocao Situacao => ObterSituacao(DateTime.Now);

        public SituacaoPromocao ObterSituacao(DateTime referencia)
        {
            if (!Ativo) return SituacaoPromocao.Inativa;
            if (referencia < DataInicio) return SituacaoPromocao.Agendada;
            if (referencia > DataFim) return SituacaoPromocao.Encerrada;
            return SituacaoPromocao.Vigente;
        }

        /// <summary>"15%" ou "R$ 2,00/un".</summary>
        public string DescricaoDesconto => TipoDesconto == TipoDesconto.Percentual
            ? $"{ValorDesconto:0.##}%"
            : $"{Formatacao.Moeda(ValorDesconto)}/un";

        /// <summary>
        /// Calcula o desconto TOTAL de um item (já multiplicado pela quantidade).
        /// Regra: percentual incide sobre o total do item; valor fixo é por unidade
        /// e nunca pode deixar o item com preço negativo.
        /// </summary>
        public decimal CalcularDesconto(decimal precoUnitario, int quantidade)
        {
            decimal bruto = precoUnitario * quantidade;
            decimal desconto = TipoDesconto == TipoDesconto.Percentual
                ? bruto * ValorDesconto / 100m
                : ValorDesconto * quantidade;
            return Math.Round(Math.Min(desconto, bruto), 2, MidpointRounding.AwayFromZero);
        }

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Validacao.Vazio(Nome)) erros.Add("Informe o nome da promoção.");
            else if (Nome.Length > 100) erros.Add("O nome pode ter no máximo 100 caracteres.");
            if (ValorDesconto <= 0) erros.Add("O valor do desconto deve ser maior que zero.");
            if (TipoDesconto == TipoDesconto.Percentual && ValorDesconto > 100)
                erros.Add("O desconto percentual não pode passar de 100%.");
            if (DataFim <= DataInicio) erros.Add("A data final deve ser posterior à data inicial.");
            if (ProdutoIds.Count == 0) erros.Add("Selecione pelo menos um produto participante.");
            return erros;
        }

        public override string ToString() => Nome;
    }
}
