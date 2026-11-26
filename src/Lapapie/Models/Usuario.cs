using Lapapie.Models.Enums;
using Lapapie.Util;

namespace Lapapie.Models
{
    /// <summary>Pessoa que acessa o sistema (tabela usuarios).</summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        /// <summary>Hash da senha (ou texto puro, nos registros antigos da massa de teste).</summary>
        public string Senha { get; set; }
        public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Vendedor;
        public bool Ativo { get; set; } = true;
        public DateTime? UltimoLogin { get; set; }
        public DateTime CriadoEm { get; set; }

        public string PerfilDescricao => Perfil.Descricao();

        /// <summary>Valida os campos. "senhaDigitada" só é exigida no cadastro.</summary>
        public List<string> Validar(string senhaDigitada, bool exigirSenha)
        {
            var erros = new List<string>();
            if (Validacao.Vazio(Nome)) erros.Add("Informe o nome.");
            else if (Nome.Length > 100) erros.Add("O nome pode ter no máximo 100 caracteres.");

            if (Validacao.Vazio(Login)) erros.Add("Informe o login.");
            else if (Login.Length > 50) erros.Add("O login pode ter no máximo 50 caracteres.");
            else if (Login.Contains(' ')) erros.Add("O login não pode conter espaços.");

            if (exigirSenha || !string.IsNullOrEmpty(senhaDigitada))
            {
                var problema = Util.Senha.ValidarForca(senhaDigitada);
                if (problema != null) erros.Add(problema);
            }
            return erros;
        }

        public override string ToString() => Nome;
    }
}
