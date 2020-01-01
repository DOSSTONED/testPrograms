using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage:\r\nGiveMatrix.exe _rows_ _columes_ _RepeatTimes_.");
            }
            else
            {
                int m = int.Parse(args[0]), n = int.Parse(args[1]), RepeatTimes = int.Parse(args[2]);
                int i = 0, j = 0, k = 0;
                //float[][] f=new float[m]{new float[n]};
                float f = 0;
                Random r = new Random();
                for (k = 0; k < RepeatTimes; k++)
                {
                    for (i = 0; i < m; i++)
                    {
                        for (j = 0; j < n; j++)
                        {
                            f = (float)r.NextDouble() * 100;
                            Console.Write(f.ToString("F4") + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            
        }
    }
}
