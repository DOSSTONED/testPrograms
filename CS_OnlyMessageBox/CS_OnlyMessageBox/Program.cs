using System;
using System.Windows.Forms;

namespace CS_OnlyMessageBox
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

            /*  
            / Orinally designed for different architacture
            string[] Args = Environment.GetCommandLineArgs();
            string Para="";
            if (Args.GetLength(0) >= 2)
            {
                Para=Args[1];
            }
            MessageBox.Show("Error in executing the program "+ Para,"64-bit Program Error",MessageBoxButtons.OK,MessageBoxIcon.Error );
            */

            /// Unified
            /// 

            
            string[] Args = Environment.GetCommandLineArgs();
            string ProcArch = "%PROCESSOR_ARCHITECTURE%",CrntPath = "%CurrentPath%";

            /// Default codes. Replaced on 091109 
            /// 

            //if (Args.GetLength(0) >= 3)
            //{
            //    ProcArch = Args[1];
            //    CrntPath = Args[2];
            //}
            //MessageBox.Show("Error in executing the program. (" + ProcArch + "-bit)", CrntPath, MessageBoxButtons.OK, MessageBoxIcon.Error);

            /// New codes added on 091109
            /// Default is x86 architect
            /// 

            if (Args.Length >= 2)
            {
                CrntPath = Args[Args.Length - 1];
                ProcArch = "32";
                if (Args[Args.Length - 2] == "64")
                {
                    ProcArch = "64";
                }
            }
            string info = "the message is:\n\nError in executing the program. (" + ProcArch + "-bit)";
            for (int i = 0; i < Args.Length; i++)
            {
                info += "\nArgs[" + i.ToString() + "]: " + Args[i];
            }
                MessageBox.Show(info, CrntPath, MessageBoxButtons.OK, MessageBoxIcon.Error);

            /// Not solved yet:
            /// How to decide a program is x86 or x64 ?
            /// = = 
            /// (Not allowed to see the taskmgr to get "*32")
            /// 
        }
    }
}
