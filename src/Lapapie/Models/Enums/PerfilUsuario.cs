using System.ComponentModel;

namespace Lapapie.Models.Enums
{
    /// <summary>Perfis de acesso (coluna usuarios.perfil). Cada perfil enxerga um conjunto de telas.</summary>
    public enum PerfilUsuario
    {
        [Description("Administrador")]
        Admin,

        [Description("Vendedor")]
        Vendedor,

        [Description("Estoquista")]
        Estoquista
    }
}
