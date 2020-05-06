using System;
using System.Windows.Forms;

namespace NUSConverter
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NUSConverterGUI());
            }
            else
            {
                NUSConverterCMD cmd = new NUSConverterCMD();
                cmd.Run(args);
            }
        }
    }
}
