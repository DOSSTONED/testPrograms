using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLib_2
{
    public class Class1
    {
        public static void DOSSTONED_BG_OnStart()
        {
            Console.WriteLine("You have call TestLib_2.Class1.DOSSTONED_BG_OnStart();");
        }

        public static void DOSSTONED_BG_OnStop()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"C:\Windows\Media\SC2 Sound Scheme\Protoss\PhasePrism_Unload0.wav");
            sp.Play();
            Console.WriteLine("Ready to stop.");
        }

        public static void z453()
        {
            Console.WriteLine("You have call TestLib_2.Class1.z453();");
        }
    }
}
