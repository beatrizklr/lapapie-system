namespace Lapapie.Relatorios
{
    /// <summary>
    /// Cores da identidade visual Lapapiê em formato hexadecimal, para uso nos
    /// documentos exportados (PDF/Excel), que não dependem do Windows Forms.
    /// </summary>
    public static class Marca
    {
        public const string PapelAlgodao = "#F3ECE2";
        public const string TintaGrafite = "#33363D";
        public const string ArgilaRosada = "#C98B7B";
        public const string Salvia = "#8E9B83";
        public const string LacreDourado = "#B8955A";
        public const string Superficie = "#FBF8F3";
        public const string Linha = "#E6DCCD";
        public const string TextoSuave = "#6B6E75";

        /// <summary>Caminho do logotipo usado no cabeçalho dos PDFs.</summary>
        public static string CaminhoLogo => Path.Combine(AppContext.BaseDirectory, "Recursos", "Marca", "assinatura-horizontal-colorido.png");
    }
}
