using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XMLarhiver
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 1 && args[0] == "FAST")
                Application.Run(new Form1(true));
            else
                Application.Run(new Form1(false));
        }
    }
}
