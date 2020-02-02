using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem2
{
    class Program
    {
        class ThreeDVector//since we have x0 = -1, so we ignore the i1 value.
        {
            public double i1;
            public double i2;
            public double i3;
            public double EstimatedValue;

            public ThreeDVector(double p1, double p2, double p3)
            {
                i1 = p1; i2 = p2; i3 = p3;
            }
            public ThreeDVector(double p1, double p2, double p3, double estimate)
            {
                i1 = p1; i2 = p2; i3 = p3; EstimatedValue = estimate;
            }

            static public ThreeDVector operator +(ThreeDVector t1, ThreeDVector t2)
            {
                return new ThreeDVector(t1.i1 + t2.i1, t1.i2 + t2.i2, t1.i3 + t2.i3);
            }

            //static public ThreeDVector operator -(ThreeDVector t1, ThreeDVector t2)
            //{
            //    return new ThreeDVector(t1.i1 - t2.i1, t1.i2 - t2.i2, t1.i3 - t2.i3);
            //}

            static public double operator *(ThreeDVector t1, ThreeDVector t2)
            {
                return t1.i1 * t2.i1 + t1.i2 * t2.i2 + t1.i3 * t2.i3;
            }

            static public ThreeDVector operator *(double t1, ThreeDVector t2)
            {
                return new ThreeDVector(t1 * t2.i1, t1 * t2.i2, t1 * t2.i3);
            }

            static public ThreeDVector operator *(ThreeDVector t2, double t1)
            {
                return new ThreeDVector(t1 * t2.i1, t1 * t2.i2, t1 * t2.i3);
            }

            public double Norm()
            {
                //if (double.IsInfinity(i2))
                //    return Math.Sqrt(i1 * i1 + i3 * i3);
                //else
                //    if (double.IsInfinity(i3))
                //        return Math.Sqrt(i1 * i1 + i2 * i2);
                //    else
                        return Math.Sqrt(i1 * i1 + i2 * i2 + i3 * i3);
            }
        }

        static void Main(string[] args)
        {
            int curLoops = 0;
            Random ran = new Random();
            // 学习率和alpha
            double nita = 0.1, alpha = 10;

            // 使用新老w值，其实用一个也可以的。用2个调试的时候看结果方便。
            // 第一个值要是负数！
            ThreeDVector wOLD = new ThreeDVector(-alpha * ran.NextDouble(), alpha * ran.NextDouble(), alpha * ran.NextDouble());
            ThreeDVector w = new ThreeDVector(0, 0, 0);
            
            // 添加要求的向量
            List<ThreeDVector> allVectors = new List<ThreeDVector>();
            allVectors.Add(new ThreeDVector(-1, 1, 1, 8));
            allVectors.Add(new ThreeDVector(-1, -10, 1, 8));
            allVectors.Add(new ThreeDVector(-1, 1, -10, 8));
            allVectors.Add(new ThreeDVector(-1, 3, 2, -3));
            allVectors.Add(new ThreeDVector(-1, 10, 2, -3));
            allVectors.Add(new ThreeDVector(-1, 3, 10, -3));


            // 当前迭代不能超过1000次，这个可以改，主要是防止死循环
            while (curLoops++ < 1000)
            {
                int indicator = 0;
                
                foreach (ThreeDVector tdv in allVectors)
                {
                    if (tdv.EstimatedValue > 0) // 如果已经达到期望，那这个就跳过
                        if (w * tdv > tdv.EstimatedValue)
                            continue;
                    if (tdv.EstimatedValue < 0)
                        if (w * tdv < tdv.EstimatedValue)
                            continue;

                    // 做向量相加
                    w = wOLD + (nita * (tdv.EstimatedValue - Math.Sign(tdv * wOLD))) * tdv;
                }

                // 检查所有是否达到期望
                foreach (ThreeDVector tdv in allVectors)
                {
                    if (tdv.EstimatedValue > 0)
                        if (w * tdv > tdv.EstimatedValue)
                            indicator++;
                    if (tdv.EstimatedValue < 0)
                        if (w * tdv < tdv.EstimatedValue)
                            indicator++;
                }
                if (indicator >= allVectors.Count) break;

                // 走到这里肯定是没达到期望，所以继续迭代
                wOLD = w;
                Console.WriteLine("w[0]={0}, w[1]={1}, w[2]={2}", w.i1, w.i2, w.i3);
            }

            // 超过迭代预期，退出
            if (curLoops >= 1000) Console.WriteLine("Cannot find result.");

            // 显示当前的值
            foreach (ThreeDVector tdv in allVectors)
                Console.WriteLine("x0={0}, x1={1}, x2={2}, EstimatedOut={3}, ActualOut={4}",
                    tdv.i1, tdv.i2, tdv.i3, tdv.EstimatedValue,
                    w * tdv
                    );

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
