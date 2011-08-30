using System;
using System.Collections.Generic;
using System.Text;

namespace Math_Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random ran = new Random();

            /*

            /// Test the function accuracy
            /// 
            while (ran.Next(10) > 0)
            {
                double x = ran.NextDouble();
                uint n = (uint)ran.Next(500);
                if (Math.Math.Pow(x, n) != System.Math.Pow(x, n))
                {
                    Console.WriteLine("{0}", Math.Math.Pow(x, n) - System.Math.Pow(x, n));
                }
            }

             */

            /// 170! ~ E306
            /// 171! = Infinity
            /// 
            n阶乘(170);



            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        static void x的n次方()
        {
            Random ran = new Random();

            int i = 0, totalLength = 12345;
            double[] xs = new double[totalLength];
            uint[] ns = new uint[totalLength];
            double[] results = new double[totalLength];
            for (i = 0; i < totalLength; i++)
            {
                xs[i] = ran.NextDouble();
                ns[i] = (uint)ran.Next(totalLength);
                results[i] = 0;
            }

            DateTime _begin;
            DateTime _end;

            _begin = DateTime.Now;
            for (i = 0; i < totalLength; i++)
            {
                results[i] = Math.Math.Pow(xs[i], ns[i]);
            }
            _end = DateTime.Now;

            Console.WriteLine("MyPow() consumes {0} seconds.", (_end - _begin).TotalSeconds);

            results = new double[totalLength];
            for (i = 0; i < totalLength; i++)
            {
                results[i] = 0;
            }

            _begin = DateTime.Now;
            for (i = 0; i < totalLength; i++)
            {
                results[i] = System.Math.Pow(xs[i], ns[i]);
            }
            _end = DateTime.Now;

            Console.WriteLine("Pow() consumes {0} seconds.", (_end - _begin).TotalSeconds);

            results = new double[totalLength];
            for (i = 0; i < totalLength; i++)
            {
                results[i] = 0;
            }

            _begin = DateTime.Now;
            for (i = 0; i < totalLength; i++)
            {
                results[i] = Math.Math.Pow_Normal(xs[i], ns[i]);
            }
            _end = DateTime.Now;

            Console.WriteLine("Pow() consumes {0} seconds.", (_end - _begin).TotalSeconds);

        }


        static void n阶乘(int TotalLength)
        {
            int i = 0;
            DateTime _begin, _end;
            double[] res = new double[TotalLength];
            //int[] primes = Math.Math.GivePrimes(TotalLength);
            
            /// May be can try another way.
            /// 
            /*
            for (i = 0; i < TotalLength; i++)
            {
                res[i] = 0;
            }

            _begin = DateTime.Now;
            for (i = 0; i < TotalLength; i++)
                res[i] = Math.Math.Factorial(i);
            _end = DateTime.Now;

            Console.WriteLine("Fact() Consumes {0} seconds.", (_end - _begin).TotalSeconds);

             */


            /// Time consumes too much.
            /* 
            for (i = 0; i < TotalLength; i++)
            {
                res[i] = 0;
            }

            _begin = DateTime.Now;
            for (i = 0; i < TotalLength; i++)
                res[i] = Math.Math.Factorial_Normal(i, primes);
            _end = DateTime.Now;

            Console.WriteLine("FactNorm() Consumes {0} seconds.", (_end - _begin).TotalSeconds);
             
             */

            for (i = 0; i < TotalLength; i++)
            {
                res[i] = i + 1;
            }

            _begin = DateTime.Now;
            for (i = 1; i < TotalLength; i++)
                res[i] = res[i - 1] * (i + 1);
            _end = DateTime.Now;

            Console.WriteLine("UsePreviousFact() Consumes {0} seconds.", (_end - _begin).TotalSeconds);

            Console.WriteLine("{0}! = {1}", res.Length, res[res.Length - 1]);
           
        }

    }
}
