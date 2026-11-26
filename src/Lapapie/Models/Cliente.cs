using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Cliente da papelaria (tabela clientes). CPF e endereço são opcionais.</summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }

        // ---- Preenchidos na pesquisa (subconsultas em vendas) ----
        public int QuantidadeCompras { get; set; }
        public DateTime? UltimaCompra { get; set; }

        // ---- Propriedades de exibição (não são colunas) ----
        public string CpfFormatado => string.IsNullOrEmpty(Cpf) ? "—" : Formatacao.Cpf(Cpf);
        public string TelefoneFormatado => string.IsNullOrEmpty(Telefone) ? "—" : Formatacao.Telefone(Telefone);
        public string CidadeUf => string.IsNullOrEmpty(Cidade) ? "—" : $"{Cidade}/{Uf}";

        public List<string> Validar()
        {
            var erros = new List<string>();
            if (Validacao.Vazio(Nome)) erros.Add("Informe o nome do cliente.");
            else if (Nome.Length > 100) erros.Add("O nome pode ter no máximo 100 caracteres.");

            if (!Validacao.Vazio(Cpf) && !Validacao.CpfValido(Cpf))
                erros.Add("CPF inválido (confira os dígitos).");
            if (!Validacao.Vazio(Email) && !Validacao.EmailValido(Email))
                erros.Add("E-mail inválido.");
            if (!Validacao.Vazio(Cep) && Validacao.SomenteDigitos(Cep).Length != 8)
                erros.Add("O CEP deve ter 8 dígitos.");
            if (!Validacao.Vazio(Uf) && !Validacao.UfValida(Uf))
                erros.Add("UF inválida.");
            return erros;
        }

        public override string ToString() => Nome;
    }
}
