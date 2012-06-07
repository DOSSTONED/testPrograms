using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高考题江西最后一题
{
    class Program
    {
        static void Main(string[] args)
        {
            double an = 0, an_1 = 0;
            double c = 0.2501;
            int loop = 0;
            while (loop<50)
            {
                loop++;
                an = -an_1 * an_1 + an_1 + c;
                //if (an < an_1)
                //{
                Console.WriteLine("loop={0},{1}", loop, an);
                //    break;
                //}
                an_1 = an;
            }
        }
    }
}
