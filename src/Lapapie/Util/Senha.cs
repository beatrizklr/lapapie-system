using System.Security.Cryptography;

namespace Lapapie.Util
{
    /// <summary>
    /// Proteção de senhas com PBKDF2 (algoritmo de "hash lento" com sal).
    /// <para>
    /// Por quê? Se alguém copiar a tabela de usuários, não deve conseguir ler as
    /// senhas. O hash é de mão única: dá para conferir se a senha digitada está
    /// certa, mas não dá para voltar ao texto original. O "sal" aleatório faz
    /// duas senhas iguais gerarem hashes diferentes.
    /// </para>
    /// <para>
    /// Compatibilidade: a massa de teste grava as senhas em texto puro. Por isso,
    /// <see cref="Conferir"/> aceita os dois formatos, e o login converte a senha
    /// para hash na primeira entrada bem-sucedida (migração transparente).
    /// Formato gravado: pbkdf2$iteracoes$sal(base64)$hash(base64) — cabe no VARCHAR(100).
    /// </para>
    /// </summary>
    public static class Senha
    {
        private const string Prefixo = "pbkdf2";
        private const int Iteracoes = 100_000;
        private const int TamanhoSal = 16;
        private const int TamanhoHash = 32;

        public static string GerarHash(string senha)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(TamanhoSal);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(senha, sal, Iteracoes, HashAlgorithmName.SHA256, TamanhoHash);
            return $"{Prefixo}${Iteracoes}${Convert.ToBase64String(sal)}${Convert.ToBase64String(hash)}";
        }

        public static bool EstaProtegida(string valorGravado) =>
            valorGravado != null && valorGravado.StartsWith(Prefixo + "$", StringComparison.Ordinal);

        /// <summary>Confere a senha digitada com o valor gravado no banco.</summary>
        public static bool Conferir(string senhaDigitada, string valorGravado)
        {
            if (string.IsNullOrEmpty(valorGravado) || senhaDigitada == null)
                return false;

            if (!EstaProtegida(valorGravado))
            {
                // Senha legada em texto puro (massa de teste).
                return CryptographicOperations.FixedTimeEquals(
                    System.Text.Encoding.UTF8.GetBytes(senhaDigitada),
                    System.Text.Encoding.UTF8.GetBytes(valorGravado));
            }

            var partes = valorGravado.Split('$');
            if (partes.Length != 4 || !int.TryParse(partes[1], out int iteracoes))
                return false;

            byte[] sal = Convert.FromBase64String(partes[2]);
            byte[] esperado = Convert.FromBase64String(partes[3]);
            byte[] calculado = Rfc2898DeriveBytes.Pbkdf2(senhaDigitada, sal, iteracoes, HashAlgorithmName.SHA256, esperado.Length);
            // Comparação em tempo constante: evita ataques que medem o tempo de resposta.
            return CryptographicOperations.FixedTimeEquals(esperado, calculado);
        }

        /// <summary>Regra mínima de senha forte para novos cadastros.</summary>
        public static string ValidarForca(string senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 6)
                return "A senha deve ter pelo menos 6 caracteres.";
            if (!senha.Any(char.IsLetter) || !senha.Any(char.IsDigit))
                return "A senha deve conter letras e números.";
            return null;
        }
    }
}
