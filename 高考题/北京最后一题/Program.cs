using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 北京最后一题
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = 2;
            double[,] a = new double[2, 2 * t + 1];
            Random ran = new Random();
            double sum = 0, minsum = 0;
            bool indicator = false;
            while (true)
            {
                indicator = false;
                for (int j = 0; j < 2 * t + 1; j++)
                {
                    for (int i = 0; i < 2; i++)
                        a[i, j] = 2 * ran.NextDouble() - 1;
                    if (Math.Abs(a[0, j] + a[1, j]) < 1)
                    {
                        indicator = true;
                        break;
                    }
                }
                if (indicator) continue;

                sum = 0;
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2 * t + 1; j++)
                        sum += a[i, j];
                if (sum != 0)
                    continue;

                // check col->minsum, row->sum
                minsum = 2;
                sum = 0;
                for (int j = 0; j < 2 * t + 1; j++)
                {
                    if (minsum > Math.Abs(a[0, j] + a[1, j]))
                        minsum = Math.Abs(a[0, j] + a[1, j]);
                    sum += a[0, j];
                }
                minsum = (minsum > Math.Abs(sum)) ? Math.Abs(sum) : minsum;

                if (minsum < 1)
                    continue;

                Console.WriteLine("minsum={0}", minsum);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2 * t + 1; j++)
                        Console.Write(a[i, j] + "\t");
                    Console.WriteLine();
                }

            }
        }
    }
}
