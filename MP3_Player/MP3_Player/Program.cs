using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MP3_Player
{
    static class Program
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string strFileName = @"126q.wma"
            ////string PlayCommand = "Open wavefile!" + strFileName + " alias MediaFile";
            //string sCommand = "open \"" + strFileName + "\" type mpegvideo alias MediaFile";
            //mciSendString(sCommand, null, 0, IntPtr.Zero);
            //sCommand = "Play MediaFile";
            //mciSendString(sCommand, null, 0, IntPtr.Zero);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


    }
}
