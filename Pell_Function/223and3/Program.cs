// 223
// 224 = 32*7
// 225 = 15*15

// x*x - 223*y*y = -3;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Numerics;

namespace _223and3
{
    class Program
    {
        static long count = 20;
        static void Main(string[] args)
        {
            //double x = 0, y = 0, result = 0;
            //double max = 1;


            long x = 0, y = 0;
            long mod = 170000;
            bool hasRoot = true;

            DateTime _start = DateTime.Now;
            //while (count > 0)
            //{
            //    Thread t1 = new Thread(unused => Task1(max, ref count));
            //    Thread t2 = new Thread(unused => Task2(max, ref count));
            //    t1.Start();
            //    t2.Start();

            //    t1.Join();
            //    t2.Join();
            //    y++;
            //    x = Math.Round((Math.Sqrt(223) * y) - 1);
            //    result = x * x - 223 * y * y;
            //    while (result < 10)
            //    {
            //        x++;
            //        result = x * x - 223 * y * y;
            //        if (Math.Abs(x * x - 223 * y * y) < 10)
            //            Console.WriteLine(
            //                "x={0},y={1}, x*x-223*y*y = {2}",
            //                x,
            //                y,
            //                x * x - 223 * y * y, count--);
            //    }


            //}


            #region 看看x*x - 223*y*y + 3在mod n的情况下是否有解
            //while (mod < 10000000 && hasRoot)
            //{
            //    hasRoot = false;
            //    mod++;
            //    for (x = 0; x < mod; x++)
            //    {
            //        for (y = 0; y < mod; y++)
            //            if ((x * x - 223 * y * y + 3) % mod == 0)
            //            {
            //                hasRoot = true;
            //                //Console.WriteLine("{0}^2-223*{1}^2==-3 mod {2}", x, y, mod);
            //                break;
            //            }
            //        if (hasRoot) break;
            //    }

            //    if (mod % 10000 == 0) Console.WriteLine("{0} seconds,mod={1}", (DateTime.Now - _start).TotalSeconds, mod);
            //}

            #endregion

            #region x*x - 223*y*y + 3在mod 15的解
            int n = 5;
            while (n < 10000)
            {
                n++;
                int appears = 0;
                bool jumpout = false;
                for (x = 1; x <= n / 2; x++)
                {
                    for (y = 1; y <= n / 2; y++)
                    {
                        if ((x * x - 223 * y * y + 3) % n == 0)
                        {
                            appears++;//Console.WriteLine("{0}^2-223*{1}^2==-3 mod {2}", x, y, n);
                            if (appears > 2)
                            { jumpout = true; continue; }
                        }
                    }
                    if (jumpout) continue;
                }
                if (!jumpout)
                {
                    appears = 0;
                    for (x = 1; x <= n / 2; x++)
                    {
                        for (y = 1; y <= n / 2; y++)
                        {
                            if ((x * x - 223 * y * y + 3) % n == 0)
                            {
                                Console.WriteLine("{0}^2-223*{1}^2==-3 mod {2}", x, y, n);
                            }
                        }
                    }
                }
            }
            #endregion

            Console.WriteLine("Consumes {0} seconds.", (DateTime.Now - _start).TotalSeconds);
        }


        static void Task1(long max, ref long cnt)
        {
            // Part 1 i = max;
            for (int j = 0; j < max; j++)
                if (Math.Abs(223 * max * max - j * j) < 10)
                    Console.WriteLine(
                        "i={0},j={1},223 * i * i - j * j = {2}",
                        max,
                        j,
                        223 * max * max - j * j, count--);
        }

        static void Task2(long max, ref long cnt)
        {
            // Part 2 j = max;
            for (int i = 0; i < max; i++)
                if (Math.Abs(223 * i * i - max * max) < 10)
                    Console.WriteLine(
                        "i={0},j={1},223 * i * i - j * j = {2}",
                        i,
                        max,
                        223 * i * i - max * max, count--);
        }

        #region removed
        /*
        // Part 1 i = max;
                for (j = 0; j < max; j++)
                    if (Math.Abs(223 * max * max - j * j) < 10)
                        Console.WriteLine(
                            "{3}: i={0},j={1}, 223*i^2 - j^2 = {2}",
                            max,
                            j,
                            223 * max * max - j * j,
                            DateTime.Now,
                            count--);

                // Part 2 j = max;
                for (i = 0; i < max; i++)
                    if (Math.Abs(223 * i * i - max * max) < 10)
                        Console.WriteLine(
                            "{3}: i={0},j={1}, 223*i^2 - j^2 = {2}",
                            i,
                           max,
                            223 * i * i - max * max,
                            DateTime.Now,
                            count--);
        */
        #endregion
    }
}
