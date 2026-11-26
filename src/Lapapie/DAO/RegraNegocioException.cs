namespace Lapapie.DAO
{
    /// <summary>
    /// Lançada quando uma operação fere uma regra do negócio
    /// (ex.: estoque insuficiente, venda já cancelada, usuário inativo).
    /// A mensagem já é pensada para ser mostrada na tela.
    /// </summary>
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem) { }
    }
}
