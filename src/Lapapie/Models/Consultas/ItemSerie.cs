namespace Lapapie.Models.Consultas
{
    /// <summary>Par rótulo/valor usado nos gráficos (ex.: "Out/26" → 1.234,50).</summary>
    public class ItemSerie
    {
        public string Rotulo { get; set; }
        public decimal Valor { get; set; }
        /// <summary>Valor auxiliar (ex.: quantidade de vendas do dia).</summary>
        public int Quantidade { get; set; }

        public ItemSerie() { }

        public ItemSerie(string rotulo, decimal valor, int quantidade = 0)
        {
            Rotulo = rotulo;
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}
