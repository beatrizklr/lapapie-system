using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Empresa que fornece produtos à papelaria (tabela fornecedores).</summary>
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string NomeContato { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }

        /// <summary>Quantidade de produtos que este fornecedor oferece (consulta).</summary>
        public int QuantidadeProdutos { get; set; }

        public string NomeExibicao => string.IsNullOrWhiteSpace(NomeFantasia) ? RazaoSocial : NomeFantasia;
        public string CnpjFormatado => Formatacao.Cnpj(Cnpj);
        public string CidadeUf => string.IsNullOrEmpty(Cidade) ? "—" : $"{Cidade}/{Uf}";

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Validacao.Vazio(RazaoSocial)) erros.Add("Informe a razão social.");
            else if (RazaoSocial.Length > 150) erros.Add("A razão social pode ter no máximo 150 caracteres.");
            if (Validacao.Vazio(Cnpj)) erros.Add("Informe o CNPJ.");
            else if (!Validacao.CnpjValido(Cnpj)) erros.Add("CNPJ inválido (confira os dígitos).");
            if (!Validacao.Vazio(Email) && !Validacao.EmailValido(Email)) erros.Add("E-mail inválido.");
            if (!Validacao.Vazio(Uf) && !Validacao.UfValida(Uf)) erros.Add("UF inválida.");
            return erros;
        }

        /// <summary>Cópia rasa: a tela de edição altera a cópia e só grava se a pessoa salvar.</summary>
        public Fornecedor Clonar() => (Fornecedor)MemberwiseClone();

        public override string ToString() => NomeExibicao;
    }
}
