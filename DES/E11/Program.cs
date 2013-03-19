using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E11
{
    public class point
    {
        public int x;
        public int y;
        public void print()
        {
            Console.WriteLine("X={0}, Y={1}", (11+x)%11, (11+y)%11);
        }
    }
    class Program
    {
        static point add(point p, point q)
        {
            point r = new point();
            int lambda = 0;
            if ((p.x == q.x) && (p.y == q.y))
            {
                lambda = (3 * p.x * p.x + 1) * giveInverse11(2 * p.y) % 11;
            }
            else
                lambda = (q.y - p.y) * giveInverse11(q.x - p.x) % 11;
            r.x = (lambda * lambda - p.x - q.x) % 11;
            r.y = (lambda * (p.x - r.x) - p.y) % 11;
            return r;
        }
        static int giveInverse11(int ori)
        {
            int ret = 0;
            for (int i = 1; i < 11; i++)
                if (ori * i % 11 == 1) return ori;
            return ret;
        }

        static int sqrt(int ori)
        {
            ori = ori % 11;
            int ret = -1;
            for (ret = 0; ret < 11; ret++)
                if (ret * ret % 11 == ori) return ret;
            return -1;
        }
        static void Main(string[] args)
        {
            //point start = new point();

            //start.x = 2; start.y = 5;
            //point ad = start;
            //for (int i = 0; i < 20; i++)
            //{
            //    ad.print();
            //    ad = add(start, ad);
            //}

            for (int i = 0; i < 11; i++)
                Console.WriteLine("x={0} y={1}", i, sqrt(i * i * i + i + 6));
            
        }
    }
}
