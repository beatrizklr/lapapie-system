using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Lapapie.Util
{
    /// <summary>
    /// Converte enums do C# para os valores das colunas ENUM do MySQL e vice-versa.
    /// <para>
    /// Por quê? No C# a convenção é PascalCase (CartaoDebito), e no banco os valores
    /// foram gravados em MAIÚSCULAS_COM_SUBLINHADO (CARTAO_DEBITO). Esta classe faz a
    /// tradução automaticamente, sem precisar de um "switch" para cada enum.
    /// </para>
    /// </summary>
    public static class EnumBanco
    {
        /// <summary>CartaoDebito → "CARTAO_DEBITO".</summary>
        public static string ParaBanco<T>(T valor) where T : struct, Enum
        {
            var nome = valor.ToString();
            var sb = new StringBuilder();
            for (int i = 0; i < nome.Length; i++)
            {
                if (i > 0 && char.IsUpper(nome[i]))
                    sb.Append('_');
                sb.Append(char.ToUpperInvariant(nome[i]));
            }
            return sb.ToString();
        }

        /// <summary>"CARTAO_DEBITO" → CartaoDebito.</summary>
        public static T DoBanco<T>(string valor) where T : struct, Enum
        {
            var semSublinhado = valor.Replace("_", string.Empty);
            if (Enum.TryParse(semSublinhado, ignoreCase: true, out T resultado))
                return resultado;
            throw new ArgumentException($"Valor '{valor}' não corresponde a nenhum item de {typeof(T).Name}.");
        }

        /// <summary>Texto amigável definido no atributo [Description] do enum.</summary>
        public static string Descricao(this Enum valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());
            var atributo = campo?.GetCustomAttribute<DescriptionAttribute>();
            return atributo?.Description ?? valor.ToString();
        }

        /// <summary>Lista (valor, descrição) para preencher ComboBox.</summary>
        public static List<KeyValuePair<T, string>> ListarComDescricao<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>()
                       .Select(v => new KeyValuePair<T, string>(v, ((Enum)(object)v).Descricao()))
                       .ToList();
        }
    }
}
