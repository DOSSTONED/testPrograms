using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcA_nn
{
    class Program
    {
        static void Main(string[] args)
        {
            int total=10;
            double[,] a = new double[total, total];
            double[] sum = new double[total];
            for (int n = 1; n < total; n++)
            {
                a[n, n] = 1;
                a[n, 1] = 1;
                for (int k = 1; k < n; k++)
                {
                    a[n, k] = k * a[n - 1, k] + a[n - 1, k - 1];
                    Console.Write(a[n, k] + "\t");
                }
                Console.Write(a[n, n]);
                sum[n] = 0;
                for (int i = 1; i <= n; i++)
                    sum[n] += a[n, i];
                //double test = 0;
                //for (int k = 1; k <= n; k++)
                //    test += Math.Pow(k, n + 1 - k);
                //test = test - (n-1) * (n-1) + n - 2;
                //Console.WriteLine();
                //Console.WriteLine("{0}, test={1}", sum[n], test);
                //Console.WriteLine("-----------");
                Console.WriteLine();
            }

        }
    }
}
