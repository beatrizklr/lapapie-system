using System.Runtime.InteropServices;

namespace Lapapie.Forms.Comum
{
    /// <summary>Permite arrastar janelas sem barra de título (login e diálogos).</summary>
    internal static class MoverJanela
    {
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static void Iniciar(Form form)
        {
            const int WM_NCLBUTTONDOWN = 0xA1, HTCAPTION = 0x2;
            ReleaseCapture();
            SendMessage(form.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
        }
    }
}
