//勾股定理，斜边不超过50的所有边长为整数的三角形
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCThatIsLessThan50
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            DateTime begin = DateTime.Now;
            for (int dis = 1; dis < 51;dis++ )
                foreach (double f in GiveOrientation(dis))
                {
                    //Console.WriteLine("Degree = {0}π", f / Math.PI);
                    count++;
                    //Console.WriteLine("a={0}, b={1}, c={2}", i, j, Math.Sqrt(i * i + j * j));
                }
            Console.WriteLine("{0} counts. Consumes {1} millseconds.", count, (DateTime.Now-begin).Milliseconds);
            Console.ReadKey();
        }



        static List<double> GiveOrientation(int maxDistance)// this gives all directions of c=maxDistance, c^2>=a^2+b^2, where a and b are all integers.
        {
            List<double> ret = new List<double>();
            for (int i = maxDistance; i >= -maxDistance; i--)
                for (int j = maxDistance; j >= -maxDistance; j--)
                {
                    if (i * i + j * j > maxDistance * maxDistance) continue;
                    if ((i == 0) && (j == 0)) continue;
                    if (ret.IndexOf((double)Math.Acos(i / Math.Sqrt(i * i + j * j))) != -1) continue;
                    ret.Add((double)Math.Acos(i / Math.Sqrt(i * i + j * j)));
                    ret.Add((double)Math.PI + (double)Math.Acos(i / Math.Sqrt(i * i + j * j)));
                    
                }
            return ret;
        }
    }
}
