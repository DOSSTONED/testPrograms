using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;

namespace 传送门
{
    class Program
    {
        const string TargetTransferredPath = "D:\\IFEO\\Alert_ProtossProtossWarpInComplete.wav";


        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "PLAYIFEOSOUND")
            {

                SoundPlayer snd = new SoundPlayer(TargetTransferredPath);
                try
                {
                    snd.Load();
                    snd.Play();
                    Thread.Sleep(3000);
                }
                catch
                {
                }
            }
            
        }
    }
}
