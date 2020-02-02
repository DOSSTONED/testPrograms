using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DetectSteam
{
    /// <summary>
    /// 本来是想做一个启动程序看看有没有steam运行，防止盗版悲剧
    /// 结果发现可以用这个启动所有程序啊！^_^
    /// 稍微改改也能防止其他平台……
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /// 程序单独运行无意义
                /// 
                if (args.Length < 1) throw new ArgumentNullException("args", "This program cannot run alone!");
                /// 必须有程序文件啊
                /// 
                if (!File.Exists(args[0])) throw new FileNotFoundException();
                /// 如果这个程序需要steam…………
                /// 
                if (File.Exists(Path.GetDirectoryName(args[0]) + "\\steamclient.dll"))
                {
                    Process[] AllProcesses = Process.GetProcesses();
                    foreach (Process CurrentProcess in AllProcesses)
                    {
                        if (CurrentProcess.ProcessName.ToLower().StartsWith("steam"))
                        {
                            Console.WriteLine("Steam is currently running, cannot start current game. Press any key to exit.");
                            Console.ReadKey();
                            return;
                        }
                    }
                }



                Process GameProcess = new Process();
                GameProcess.StartInfo.FileName = args[0];
                List<string> GameParameter = new List<string>();
                for (int i = 1; i < args.Length; i++)
                    GameParameter.Add(" " + args[i]);
                GameProcess.StartInfo.Arguments = string.Concat(GameParameter);
                bool result = GameProcess.Start();
                //GameProcess.WaitForExit(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                for (int i = 0; i < args.Length; i++)
                    Console.WriteLine("arg[{0}] = {1}", i, args[i]);
                Console.WriteLine("Error...... Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
