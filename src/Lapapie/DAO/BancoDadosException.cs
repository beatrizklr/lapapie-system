using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Tipos de erro de banco que a interface sabe tratar.</summary>
    public enum TipoErroBanco
    {
        Conexao,
        RegistroDuplicado,
        RegistroEmUso,
        ReferenciaInexistente,
        RestricaoViolada,
        Concorrencia,
        Desconhecido
    }

    /// <summary>
    /// Exceção "traduzida" a partir de uma MySqlException.
    /// <para>
    /// Por quê? O MySQL devolve mensagens técnicas em inglês ("Cannot delete or update a
    /// parent row..."). A camada DAO converte essas mensagens para algo que a pessoa
    /// usuária entende, sem que os formulários precisem conhecer códigos do MySQL.
    /// </para>
    /// </summary>
    public class BancoDadosException : Exception
    {
        public TipoErroBanco Tipo { get; }
        public int CodigoMySql { get; }

        public BancoDadosException(TipoErroBanco tipo, string mensagem, int codigo, Exception interna)
            : base(mensagem, interna)
        {
            Tipo = tipo;
            CodigoMySql = codigo;
        }

        // Nome da restrição UNIQUE → mensagem amigável
        private static readonly Dictionary<string, string> MensagensDuplicidade = new(StringComparer.OrdinalIgnoreCase)
        {
            ["uk_usuarios_login"] = "Já existe um usuário com este login.",
            ["uk_clientes_cpf"] = "Já existe um cliente cadastrado com este CPF.",
            ["uk_categorias_nome"] = "Já existe uma categoria com este nome.",
            ["uk_fornecedores_cnpj"] = "Já existe um fornecedor cadastrado com este CNPJ.",
            ["uk_produtos_codigo_barras"] = "Já existe um produto com este código de barras.",
            ["PRIMARY"] = "Este registro já existe."
        };

        /// <summary>Converte a exceção do conector MySQL em uma mensagem amigável.</summary>
        public static BancoDadosException Traduzir(MySqlException ex)
        {
            int codigo = ex.Number;
            switch (codigo)
            {
                case 1062: // ER_DUP_ENTRY
                    var chave = MensagensDuplicidade.Keys.FirstOrDefault(k => ex.Message.Contains(k, StringComparison.OrdinalIgnoreCase));
                    return new BancoDadosException(TipoErroBanco.RegistroDuplicado,
                        chave != null ? MensagensDuplicidade[chave] : "Já existe um registro com estes dados.", codigo, ex);

                case 1451: // ER_ROW_IS_REFERENCED_2 — chave estrangeira impede a exclusão
                    return new BancoDadosException(TipoErroBanco.RegistroEmUso,
                        "Este registro está ligado a outros dados (vendas, movimentações, pedidos...) e não pode ser excluído.", codigo, ex);

                case 1452: // ER_NO_REFERENCED_ROW_2
                    return new BancoDadosException(TipoErroBanco.ReferenciaInexistente,
                        "Um dos registros relacionados não existe mais. Atualize a tela e tente novamente.", codigo, ex);

                case 3819: // ER_CHECK_CONSTRAINT_VIOLATED
                    return new BancoDadosException(TipoErroBanco.RestricaoViolada,
                        "Os dados não respeitam uma regra do banco de dados: " + ex.Message, codigo, ex);

                case 1406: // ER_DATA_TOO_LONG
                    return new BancoDadosException(TipoErroBanco.RestricaoViolada,
                        "Um dos campos ultrapassou o tamanho máximo permitido.", codigo, ex);

                case 1205: // lock wait timeout
                case 1213: // deadlock
                    return new BancoDadosException(TipoErroBanco.Concorrencia,
                        "Outra pessoa está alterando os mesmos dados agora. Tente novamente em instantes.", codigo, ex);

                case 1045: // acesso negado
                    return new BancoDadosException(TipoErroBanco.Conexao,
                        "Usuário ou senha do MySQL incorretos. Confira a configuração da conexão.", codigo, ex);

                case 1049: // banco inexistente
                    return new BancoDadosException(TipoErroBanco.Conexao,
                        "O banco 'lapapie' não foi encontrado. Execute o script database/lapapie_create.sql.", codigo, ex);

                case 1042: // não achou o servidor
                case 0:
                    return new BancoDadosException(TipoErroBanco.Conexao,
                        "Não foi possível conectar ao MySQL. Verifique se o servidor está ligado e a configuração da conexão.", codigo, ex);

                default:
                    return new BancoDadosException(TipoErroBanco.Desconhecido,
                        $"Erro no banco de dados ({codigo}): {ex.Message}", codigo, ex);
            }
        }
    }
}
