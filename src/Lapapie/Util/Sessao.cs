using Lapapie.Models;
using Lapapie.Models.Enums;

namespace Lapapie.Util
{
    /// <summary>Funcionalidades do sistema que dependem de permissão.</summary>
    public enum Funcionalidade
    {
        Dashboard,
        NovaVenda,
        HistoricoVendas,
        CancelarVenda,
        RelatorioVendas,
        Clientes,
        ConsultarProdutos,
        GerenciarProdutos,
        Categorias,
        Fornecedores,
        Kits,
        ConsultarEstoque,
        MovimentarEstoque,
        Movimentacoes,
        EstoqueBaixo,
        PedidosCompra,
        Promocoes,
        Usuarios
    }

    /// <summary>
    /// Guarda quem está logado e decide o que cada perfil pode fazer.
    /// Centralizar a regra aqui evita espalhar "if (perfil == ...)" pelas telas.
    /// </summary>
    public static class Sessao
    {
        public static Usuario UsuarioLogado { get; private set; }

        public static bool Autenticado => UsuarioLogado != null;

        public static void Iniciar(Usuario usuario) => UsuarioLogado = usuario;

        public static void Encerrar() => UsuarioLogado = null;

        // Matriz de permissões: quais perfis acessam cada funcionalidade.
        private static readonly Dictionary<Funcionalidade, PerfilUsuario[]> Permissoes = new()
        {
            [Funcionalidade.Dashboard] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor, PerfilUsuario.Estoquista },
            [Funcionalidade.NovaVenda] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor },
            [Funcionalidade.HistoricoVendas] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor },
            [Funcionalidade.CancelarVenda] = new[] { PerfilUsuario.Admin },
            [Funcionalidade.RelatorioVendas] = new[] { PerfilUsuario.Admin },
            [Funcionalidade.Clientes] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor },
            [Funcionalidade.ConsultarProdutos] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor, PerfilUsuario.Estoquista },
            [Funcionalidade.GerenciarProdutos] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.Categorias] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.Fornecedores] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.Kits] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor, PerfilUsuario.Estoquista },
            [Funcionalidade.ConsultarEstoque] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor, PerfilUsuario.Estoquista },
            [Funcionalidade.MovimentarEstoque] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.Movimentacoes] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.EstoqueBaixo] = new[] { PerfilUsuario.Admin, PerfilUsuario.Vendedor, PerfilUsuario.Estoquista },
            [Funcionalidade.PedidosCompra] = new[] { PerfilUsuario.Admin, PerfilUsuario.Estoquista },
            [Funcionalidade.Promocoes] = new[] { PerfilUsuario.Admin },
            [Funcionalidade.Usuarios] = new[] { PerfilUsuario.Admin },
        };

        /// <summary>O usuário logado tem acesso à funcionalidade?</summary>
        public static bool Pode(Funcionalidade funcionalidade)
        {
            if (UsuarioLogado == null) return false;
            return Permissoes.TryGetValue(funcionalidade, out var perfis) && perfis.Contains(UsuarioLogado.Perfil);
        }

        /// <summary>Id do usuário logado (gravado em vendas, pedidos e movimentações).</summary>
        public static int UsuarioId => UsuarioLogado?.Id
            ?? throw new InvalidOperationException("Nenhum usuário está logado.");
    }
}
