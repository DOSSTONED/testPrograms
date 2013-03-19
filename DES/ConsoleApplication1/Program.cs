using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] ori = File.ReadAllBytes(@"C:\Programs\Programming\Projects\DES\DES File Handle\bin\Debug\Person_100315.txt");
            byte[] crp = File.ReadAllBytes(@"C:\Programs\Programming\Projects\DES\DES File Handle\bin\Debug\Person_100315.txtencrypt");
            int[] sum_ori = new int[256];
            int[] sum_crp = new int[256];
            for (int i = 0; i < ori.Length; i++)
            {
                sum_crp[crp[i]]++;
                sum_ori[ori[i]]++;
            }

            for (int i = 0; i < 256; i++)
            {
                Console.WriteLine("{0} \t {1}", sum_ori[i], sum_crp[i]);
            }
        }
    }
}
