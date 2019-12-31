using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace FindWindow_Dolphin
{

    class Program
    {
        static List<double>
             fpsx64 = new List<double>(),
             fpsx86 = new List<double>(),
             vpsx64 = new List<double>(),
             vpsx86 = new List<double>(),
             speedx64 = new List<double>(),
             speedx86 = new List<double>();

        internal struct GameInfo
        {
            public double Speed;
            public double FPS;
            public double VPS;
        }

        static void Main(string[] args)
        {
            Program curProgram = new Program();

            Console.WriteLine("Enter c to enter detect mode:");
            if (Console.ReadKey().Key == ConsoleKey.C)
            {
                Console.WriteLine("Now is detecting...");
                while (true)
                {
                    Thread.Sleep(1000);
                    Process[] ps = Process.GetProcessesByName("Dolphin");
                    if (ps.Length != 0)
                    {
                        foreach (Process p in ps)
                        {
                            string title = ps[0].MainWindowTitle;
                            if (title.ToUpper().Contains("FPS"))
                            {
                                GameInfo gi = curProgram.ParseTitle(title);
                                if (gi.Speed + gi.FPS + gi.VPS == 0)
                                    continue;
                                if (title.Contains("JIT64")) // this is x64 dolphin;
                                {
                                    fpsx64.Add(gi.FPS);
                                    vpsx64.Add(gi.VPS);
                                    speedx64.Add(gi.Speed);
                                }
                                else // this is x86 dolphin;
                                {
                                    fpsx86.Add(gi.FPS);
                                    vpsx86.Add(gi.VPS);
                                    speedx86.Add(gi.Speed);
                                }
                            }
                        }
                    }
                    Console.Clear();
                    Console.WriteLine(
                        "\tX64:\tX86\r\n"
                        + " FPS :\t{0}\t{1}\r\n"
                        + " VPS :\t{2}\t{3}\r\n"
                        + "SPEED:\t{4}\t{5}\r\n"
                        + "Improved FPS ratio: {6}\r\n",
                    new object[] { 
                        fpsx64.Sum() / (fpsx64.Count+1), fpsx86.Sum() / (fpsx86.Count+1),
                        vpsx64.Sum() /( vpsx64.Count+1), vpsx86.Sum() / (vpsx86.Count+1),
                        speedx64.Sum() / (speedx64.Count+1), speedx86.Sum() / (speedx86.Count+1),
                        fpsx64.Sum() / (fpsx64.Count+1) /(fpsx86.Sum()) * (fpsx86.Count+1) - 1
                    });
                }
            }



        }

        internal GameInfo ParseTitle(string str)
        {
            string fps = str.Substring(str.IndexOf("FPS: ") + 5, str.IndexOf("VPS:") - (str.IndexOf("FPS: ") + 5) - 2);
            string vps = str.Substring(str.IndexOf("VPS: ") + 5, str.IndexOf("SPEED") - (str.IndexOf("VPS: ") + 5) - 2);
            string speed = str.Substring(str.IndexOf("SPEED: ") + 7, str.IndexOf("%") - (str.IndexOf("SPEED: ") + 7));
            GameInfo g;
            g.FPS = double.Parse(fps);
            g.VPS = double.Parse(vps);
            g.Speed = double.Parse(speed) / 100;
            return g;
        }
    }
}
