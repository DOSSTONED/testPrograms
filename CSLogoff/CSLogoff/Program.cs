using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CSLogoff
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
           Confirmation con= new Confirmation();
            SystemEvents.SessionSwitch+=new SessionSwitchEventHandler(con.SystemEvents_SessionSwitch);
            Application.Run(con);
        }
    }
}
