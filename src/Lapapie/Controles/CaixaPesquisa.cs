using System.ComponentModel;
using Lapapie.Tema;

namespace Lapapie.Controles
{
    /// <summary>
    /// Campo de pesquisa com lupa. Dispara <see cref="Pesquisar"/> alguns
    /// milissegundos depois que a pessoa para de digitar (técnica "debounce"),
    /// evitando uma consulta ao banco a cada tecla.
    /// </summary>
    [ToolboxItem(true), DefaultEvent(nameof(Pesquisar))]
    public class CaixaPesquisa : CampoTexto
    {
        private readonly System.Windows.Forms.Timer _temporizador = new() { Interval = 350 };

        /// <summary>A pessoa parou de digitar (ou apertou Enter).</summary>
        public event EventHandler Pesquisar;

        public CaixaPesquisa()
        {
            Icone = Icones.Pesquisar;
            Placeholder = "Pesquisar...";
            _temporizador.Tick += (s, e) => { _temporizador.Stop(); Pesquisar?.Invoke(this, EventArgs.Empty); };
            TextoAlterado += (s, e) => { _temporizador.Stop(); _temporizador.Start(); };
            TeclaPressionada += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _temporizador.Stop();
                    e.SuppressKeyPress = true;
                    Pesquisar?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        [Category("Lapapiê"), DefaultValue(350), Description("Espera (ms) após a última tecla.")]
        public int Atraso { get => _temporizador.Interval; set => _temporizador.Interval = Math.Max(1, value); }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _temporizador.Dispose();
            base.Dispose(disposing);
        }
    }
}
