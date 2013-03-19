using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tables
{
    class Program
    {
        static int[,] m3 = new int[6, 6];
        static void Main(string[] args)
        {
            for (int k = 1; k <= 5; k++)
                for (int p = 1; p <= 5; p++)
                {
                    m3[f2(f1(k), p), k] = p;

                }


            for (int k = 1; k <= 5; k++)
            {
                for (int p = 1; p <= 5; p++)
                {
                    Console.Write(m3[k, p] + "\t");

                }
                Console.WriteLine();
            }
        }

        static int f1(int x)
        {
            int[] re = new int[] {0, 5, 4, 2, 3, 1 };
            return re[x];
        }

        static int f2(int x, int y)
        {
            int[,] re = new int[,]{
                {0,0,0,0,0,0},
                {0,5,2,3,4,1},
                {0,4,2,5,1,3},
                {0,1,3,2,4,5},
                {0,3,1,4,2,5},
                {0,2,5,3,4,1}
            };
            return re[x, y];
        }
    }
}
