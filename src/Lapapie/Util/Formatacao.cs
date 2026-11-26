using System.Globalization;

namespace Lapapie.Util
{
    /// <summary>Formatações de exibição no padrão brasileiro.</summary>
    public static class Formatacao
    {
        public static readonly CultureInfo PtBr = new("pt-BR");

        public static string Moeda(decimal valor) => valor.ToString("C2", PtBr);

        public static string Numero(decimal valor, int casas = 0) => valor.ToString("N" + casas, PtBr);

        public static string Data(DateTime data) => data.ToString("dd/MM/yyyy", PtBr);

        public static string DataHora(DateTime data) => data.ToString("dd/MM/yyyy HH:mm", PtBr);

        public static string Cpf(string cpf)
        {
            var d = Validacao.SomenteDigitos(cpf);
            return d.Length == 11 ? Convert.ToUInt64(d).ToString(@"000\.000\.000\-00") : cpf;
        }

        public static string Cnpj(string cnpj)
        {
            var d = Validacao.SomenteDigitos(cnpj);
            return d.Length == 14 ? Convert.ToUInt64(d).ToString(@"00\.000\.000\/0000\-00") : cnpj;
        }

        public static string Cep(string cep)
        {
            var d = Validacao.SomenteDigitos(cep);
            return d.Length == 8 ? $"{d[..5]}-{d[5..]}" : cep;
        }

        public static string Telefone(string telefone)
        {
            var d = Validacao.SomenteDigitos(telefone);
            return d.Length switch
            {
                11 => $"({d[..2]}) {d.Substring(2, 5)}-{d[7..]}",
                10 => $"({d[..2]}) {d.Substring(2, 4)}-{d[6..]}",
                _ => telefone
            };
        }

        /// <summary>Converte texto digitado ("1.234,50" ou "R$ 12,90") em decimal.</summary>
        public static bool TentarLerDecimal(string texto, out decimal valor)
        {
            texto = (texto ?? string.Empty).Replace("R$", string.Empty).Trim();
            return decimal.TryParse(texto, NumberStyles.Number, PtBr, out valor);
        }

        /// <summary>"Juliana Martins" → "JM" (usado no avatar).</summary>
        public static string Iniciais(string nome)
        {
            var partes = (nome ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 0) return "?";
            if (partes.Length == 1) return partes[0][..1].ToUpperInvariant();
            return (partes[0][..1] + partes[^1][..1]).ToUpperInvariant();
        }
    }
}
