using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FlashPlayer_UsingActiveX_CS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.GetCommandLineArgs().Length == 2)
                Application.Run(new Form1(Environment.GetCommandLineArgs()[1]));
            else
                Application.Run(new Form1());
        }
    }
}
