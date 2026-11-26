using System.ComponentModel;

namespace Lapapie.Forms.Comum
{
    /// <summary>
    /// BindingList que permite ordenar o DataGridView clicando no cabeçalho.
    /// (Uma List&lt;T&gt; comum não sabe se ordenar sozinha quando ligada a um grid.)
    /// </summary>
    public class ListaOrdenavel<T> : BindingList<T>
    {
        private bool _ordenada;
        private ListSortDirection _direcao;
        private PropertyDescriptor _propriedade;

        public ListaOrdenavel(IEnumerable<T> itens) : base(itens.ToList()) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _ordenada;
        protected override ListSortDirection SortDirectionCore => _direcao;
        protected override PropertyDescriptor SortPropertyCore => _propriedade;

        protected override void ApplySortCore(PropertyDescriptor propriedade, ListSortDirection direcao)
        {
            if (Items is not List<T> lista) return;
            Comparison<T> comparar = (a, b) =>
            {
                var va = propriedade.GetValue(a) as IComparable;
                var vb = propriedade.GetValue(b);
                if (va == null) return vb == null ? 0 : -1;
                if (vb == null) return 1;
                return va.CompareTo(vb);
            };
            lista.Sort(direcao == ListSortDirection.Ascending ? comparar : (a, b) => comparar(b, a));
            _propriedade = propriedade;
            _direcao = direcao;
            _ordenada = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore() => _ordenada = false;
    }
}
