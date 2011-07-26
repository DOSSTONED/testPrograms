using System;
using System.Collections.Generic;
using System.Text;

namespace Random_1
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 0;
            while (i++ < 50)
            {
                GiveNumber(1000);

            }
            i = 0;
            while (i++ < 10)
            {
                GiveNumber(10000);
            }
            Console.ReadKey();
        }

        static void GiveNumber(int max)
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            
            int n1 = ran.Next(max);
            int n2 = ran.Next(max);
            if (ran.Next(2) == 0)
            {
                Console.WriteLine("{0}+{1}", n1, n2);
            }
            else
            {
                Console.WriteLine((n1 > n2 ? "{0}-{1}" : "{1}-{0}"), n1, n2);
            }
            System.Threading.Thread.Sleep(1);
        }
    }
}
