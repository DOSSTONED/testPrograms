using System;
using System.Media;


namespace TestLib_1
{
    public class Class1
    {

        public static void DOSSTONED_BG_OnStart()
        {
            //Console.WriteLine("You have call TestLib_1.Class1.DOSSTONED_BG_OnStart();");
            //ConsoleKeyInfo cki = Console.ReadKey();
            //Console.WriteLine(cki.KeyChar);
            System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", DateTime.Now.ToLongTimeString() + "TestLib_1.Class1.DOSSTONED_BG_OnStart();\r\n");
            //SoundPlayer sp = new SoundPlayer(@"C:\Windows\Media\SC2 Sound Scheme\Protoss\PhasePrism_Unload0.wav");
            //sp.Play();
            System.Threading.Thread.Sleep(10000);
            //System.Threading.Thread.SpinWait(1);
        }

        public static void DOSSTONED_BG_OnStop()
        {
            
            System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", DateTime.Now.ToLongTimeString() + "TestLib_1.Class1.DOSSTONED_BG_OnStop();\r\n");
            //Console.WriteLine("Ready to stop.");
        }

        public static void twst()
        {
            //Console.WriteLine("You have call TestLib_1.Class1.twst();");
        }

    }
}
