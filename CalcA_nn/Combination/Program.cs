using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = Console.LargestWindowWidth;
            
            double[,] C = new double[3000, 3000];
            for (int n = 1; n < 30; n++)
            {
                C[n, 0] = 1;
                C[n, n] = 1;
                Console.Write("{0}\t", C[n, 0]);
                for (int i = 1; i < n; i++)
                    Console.Write("{0}\t", C[n, i] = C[n - 1, i] + C[n - 1, i - 1]);
                Console.Write("{0}\t", C[n, n]);
                Console.WriteLine();
            }

        }
    }
}
