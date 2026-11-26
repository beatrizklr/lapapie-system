using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Agrupamento de produtos (tabela categorias).</summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;

        /// <summary>Preenchido apenas nas consultas (COUNT de produtos).</summary>
        public int QuantidadeProdutos { get; set; }

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Validacao.Vazio(Nome)) erros.Add("Informe o nome da categoria.");
            else if (Nome.Length > 60) erros.Add("O nome pode ter no máximo 60 caracteres.");
            if (Descricao?.Length > 255) erros.Add("A descrição pode ter no máximo 255 caracteres.");
            return erros;
        }

        public override string ToString() => Nome;
    }
}
