using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindF
{
    class Program
    {
        const string PASS = "012345678abcdeABCDEFGHIJKLMNfghijklmnUVWXYZxyzuvwopqrstOPQRST9";
        static void Main(string[] args)
        {
            int i = 0, j = 0;   // let us assume f(x) = i * x + j;
            for(i=0;i<62;i++)
                for (j = 0; j < 62; j++)
                {
                    if (F(i, j, 0) != 30)
                        continue;
                    if (F(i, j, 1) != 29)
                        continue;
                    if (F(i, j, 2) != 44)
                        continue;
                    if (F(i, j, 3) != 43)
                        continue;
                    Console.WriteLine("i = {0}, j = {1}", i, j);
                }
            Console.ReadKey();
        }

        static int F(int i, int j, int input)
        {
            return (i * input + j) % 62;
        }
    }
}
