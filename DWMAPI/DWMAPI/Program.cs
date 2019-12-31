using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DWMAPI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f1 = new Form1();
            //f1.SetDesktopLocation(0, 0);
            //f1.StartPosition = FormStartPosition.Manual;
            //f1.Location = new System.Drawing.Point(0, 0);
            //f1.TopMost = true;
            Application.Run(f1);
        }
    }
}
