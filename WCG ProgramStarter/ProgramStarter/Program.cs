using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace ProgramStarter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Environment.OSVersion.ToString();
            string[] allpaths = File.ReadAllLines(Environment.CurrentDirectory + "\\config.txt");
            Process startcalc = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            
            info.FileName = "calc.exe";
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            startcalc.StartInfo = info;


            RegistryKey rk;
            rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
            string OSname = rk.GetValue("ProductName").ToString();//产品名称（系统名称）
            //string serverpack = rk.GetValue("CSDVersion").ToString();//seserver pack版本号
            //string productID = rk.GetValue("ProductId").ToString();//系统激活序列号
            //string buildNumber = rk.GetValue("CurrentBuildNumber").ToString();//系统版本号
            rk.Close();

            if (OSname.ToLower().Contains("2008"))   //indicats win 2008 r2
            {
                
                if (allpaths[2].ToLower().Contains("server"))
                {
                    info.FileName = allpaths[3];
                }
            }
            else
            {
                if (allpaths[0].ToLower().Contains("win7"))
                {
                    info.FileName = allpaths[1];
                }
            }
            
            
            bool res = startcalc.Start();
            startcalc.WaitForExit(100);
        }
    }
}
