using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCreateAutorun
{
    class Program
    {
        static void Main(string[] args)
        {
            string CurrentPath = System.IO.Directory.GetCurrentDirectory() ;
            string CurrentRoot = System.IO.Path.GetPathRoot( CurrentPath );
            if ( ! System.IO.File.Exists( CurrentRoot + "Autorun.inf" ) )//  File is not exist
            {
               //Console.WriteLine("File not exist"+CurrentRoot);
               System.IO.File.Create( CurrentRoot + "Autorun.inf" );
               //Console.ReadLine();
               return;
            }
            //Console.WriteLine("File exist");
            //Console.ReadLine();
       }
        
    }
}
