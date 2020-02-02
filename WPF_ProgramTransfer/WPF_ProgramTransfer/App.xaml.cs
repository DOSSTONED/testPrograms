using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;

namespace WPF_ProgramTransfer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string Program = string.Empty;
            base.OnStartup(e);
            if (e.Args.Length > 0)   /* test command-line params */
            {
                /* do stuff without a GUI */
                
                    /// Args[0] indicates the file path
                    /// but for the IFEO transferred paths, Args[0] stands for the program which is hijacked, Args[1] imply the file path.
                    /// 

                    switch (Path.GetExtension(e.Args[0]))
                    {
                        case ".rm":
                        case ".rmvb":
                            Program = @"E:\TOOLS\mplayerc.exe";
                            break;
                        default:
                            break;
                    }
                
                if (Program != string.Empty)
                    System.Diagnostics.Process.Start(Program, e.Args[0]);

                /// The next transfer is recongnized by the file name, not extension
                /// 
                if (e.Args.Length > 1)
                {
                    /// For test
                   // for (int i = 0; i < e.Args.Length; i++)
                   // { MessageBox.Show(i.ToString() + "\t" + e.Args[i]); }
                    ///


                        if (Path.GetFileName(e.Args[0]).ToLowerInvariant() == "notepad.exe")
                        {
                            System.Diagnostics.Process.Start(@"E:\MyApps\PortableApps\Notepad++Portable\Notepad++Portable.exe", e.Args[1]);
                        }
                }
            }
            else
            {
                new Window1().ShowDialog();
            }
            this.Shutdown();
        }
    }
}
