using System.Text.RegularExpressions;

namespace Lapapie.Util
{
    /// <summary>
    /// Regras de validação reutilizadas pelos Models (CPF, CNPJ, e-mail etc.).
    /// O banco também tem restrições CHECK, mas validar antes evita uma ida
    /// ao servidor e permite mostrar uma mensagem amigável para quem usa.
    /// </summary>
    public static class Validacao
    {
        private static readonly Regex RegexEmail =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        private static readonly HashSet<string> UfsValidas = new()
        {
            "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA",
            "PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO"
        };

        /// <summary>Remove tudo que não for dígito: "123.456.789-09" → "12345678909".</summary>
        public static string SomenteDigitos(string texto) =>
            string.IsNullOrEmpty(texto) ? string.Empty : new string(texto.Where(char.IsDigit).ToArray());

        public static bool Vazio(string texto) => string.IsNullOrWhiteSpace(texto);

        public static bool EmailValido(string email) => RegexEmail.IsMatch(email ?? string.Empty);

        public static bool UfValida(string uf) => UfsValidas.Contains((uf ?? string.Empty).ToUpperInvariant());

        /// <summary>
        /// Valida CPF pelos dígitos verificadores (módulo 11).
        /// Os dois últimos números do CPF são calculados a partir dos 9 primeiros,
        /// então dá para detectar erros de digitação sem consultar a Receita.
        /// </summary>
        public static bool CpfValido(string cpf)
        {
            cpf = SomenteDigitos(cpf);
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] numeros = cpf.Select(c => c - '0').ToArray();
            for (int posicao = 9; posicao <= 10; posicao++)
            {
                int soma = 0;
                for (int i = 0; i < posicao; i++)
                    soma += numeros[i] * (posicao + 1 - i);
                int digito = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (numeros[posicao] != digito)
                    return false;
            }
            return true;
        }

        /// <summary>Valida CNPJ pelos dígitos verificadores.</summary>
        public static bool CnpjValido(string cnpj)
        {
            cnpj = SomenteDigitos(cnpj);
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
                return false;

            int[] pesos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] numeros = cnpj.Select(c => c - '0').ToArray();

            int Calcular(int[] pesos)
            {
                int soma = 0;
                for (int i = 0; i < pesos.Length; i++)
                    soma += numeros[i] * pesos[i];
                return soma % 11 < 2 ? 0 : 11 - soma % 11;
            }

            return numeros[12] == Calcular(pesos1) && numeros[13] == Calcular(pesos2);
        }

        /// <summary>Código de barras EAN-8/EAN-13/GTIN-14: apenas dígitos, 8 a 14 posições.</summary>
        public static bool CodigoBarrasValido(string codigo)
        {
            codigo = codigo?.Trim() ?? string.Empty;
            return codigo.Length >= 8 && codigo.Length <= 14 && codigo.All(char.IsDigit);
        }
    }
}
