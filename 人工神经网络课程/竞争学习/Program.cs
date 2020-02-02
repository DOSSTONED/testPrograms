using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 竞争学习
{
    class Program
    {
        static double eta = 0.02;
        static double[,] w = new double[3, 2];
        static void Main(string[] args)
        {
            simple();
            inhibit();

        }

        static double F(double x)
        {
            if (x < 0) return 0;
            if (x > 1) return 1;
            return x;
        }

        static void simple()
        {
            Random r = new Random();
            double sum = 0;
            double delta = 0;

            /// patterns
            /// 
            pattern[] p = new pattern[]{
            new pattern(1,0,1),
            new pattern(1,0,0),
            new pattern(0,1,1),
            new pattern(0,1,0)
            };


            do
            {
                /// sum w is 1
                /// 
                sum = 0;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        w[i, j] = r.NextDouble();
                        sum += w[i, j];
                    }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        w[i, j] = w[i, j] / sum;
                    }

                /// give h
                double[] h = new double[2];

                int curIdx = r.Next(4);
                h[0] = p[curIdx].idx[0] * w[0, 0] + p[curIdx].idx[1] * w[1, 0] + p[curIdx].idx[2] * w[2, 0];
                h[1] = p[curIdx].idx[0] * w[0, 1] + p[curIdx].idx[1] * w[1, 1] + p[curIdx].idx[2] * w[2, 1];

                /// compare
                int maxV = (h[0] >= h[1]) ? 0 : 1;
                /// M is the sum(u_j_k)
                int M = p[curIdx].idx[0] + p[curIdx].idx[1] + p[curIdx].idx[2];
                /// delta, when delta is small, the system is stable and stop
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        delta += Math.Abs(eta * (p[curIdx].idx[i] / M - w[i, j]));
                    }
                /// change weights.
                for (int i = 0; i < 3; i++)
                    w[i, maxV] = w[i, maxV] + eta * (p[curIdx].idx[i] / M - w[i, maxV]);
            } while (delta < 0.00001);

            /// output the w
            /// 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(w[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

            for (int k = 0; k < p.Length; k++)
            {
                Console.WriteLine("{0}, {1}, {2} ", p[k].idx[0], p[k].idx[1], p[k].idx[2]);
                double[] h = new double[2];
                h[0] = p[k].idx[0] * w[0, 0] + p[k].idx[1] * w[1, 0] + p[k].idx[2] * w[2, 0];
                h[1] = p[k].idx[0] * w[0, 1] + p[k].idx[1] * w[1, 1] + p[k].idx[2] * w[2, 1];
                Console.WriteLine("h0 = {0}, h1 = {1}", h[0], h[1]);
                Console.WriteLine("Class = {0}", (h[0] >= h[1]) ? 0 : 1);
            }
            Console.ReadKey();
        }

        static void inhibit()
        {
            Random r = new Random();
            double sum = 0;
            double delta = 0;

            /// patterns
            /// 
            pattern[] p = new pattern[]{
            new pattern(1,0,1),
            new pattern(1,0,0),
            new pattern(0,1,1),
            new pattern(0,1,0)
            };


            do
            {
                /// sum w is 1
                /// 
                sum = 0;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        w[i, j] = r.NextDouble();
                        sum += w[i, j];
                    }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        w[i, j] = w[i, j] / sum;
                    }

                /// give h
                double[] h = new double[2];

                int curIdx = r.Next(4);
                h[0] = p[curIdx].idx[0] * w[0, 0] + p[curIdx].idx[1] * w[1, 0] + p[curIdx].idx[2] * w[2, 0];
                h[1] = p[curIdx].idx[0] * w[0, 1] + p[curIdx].idx[1] * w[1, 1] + p[curIdx].idx[2] * w[2, 1];

                /// inhibit part.
                double newh0 = 0, newh1 = 0;
                while (Math.Abs(newh0 - newh1) < 0.5)
                {
                    double alpha = 0.2;
                    double beta = 0.2;
                    double Q = h[0] + h[1];
                    newh0 = F(h[0]) + alpha / Q * h[0] - beta / Q * h[1];
                    newh1 = F(h[1]) + alpha / Q * h[1] - beta / Q * h[0];
                    h[0] = newh0;
                    h[1] = newh1;
                }


                /// compare
                int maxV = (h[0] >= h[1]) ? 0 : 1;
                /// M is the sum(u_j_k)
                int M = p[curIdx].idx[0] + p[curIdx].idx[1] + p[curIdx].idx[2];
                /// delta, when delta is small, the system is stable and stop
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        delta += Math.Abs(eta * (p[curIdx].idx[i] / M - w[i, j]));
                    }
                /// change weights.
                for (int i = 0; i < 3; i++)
                    w[i, maxV] = w[i, maxV] + eta * (p[curIdx].idx[i] / M - w[i, maxV]);
            } while (delta < 0.00001);

            /// output the w
            /// 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(w[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

            for (int k = 0; k < p.Length; k++)
            {
                Console.WriteLine("{0}, {1}, {2} ", p[k].idx[0], p[k].idx[1], p[k].idx[2]);
                double[] h = new double[2];
                h[0] = p[k].idx[0] * w[0, 0] + p[k].idx[1] * w[1, 0] + p[k].idx[2] * w[2, 0];
                h[1] = p[k].idx[0] * w[0, 1] + p[k].idx[1] * w[1, 1] + p[k].idx[2] * w[2, 1];
                Console.WriteLine("h0 = {0}, h1 = {1}", h[0], h[1]);
                Console.WriteLine("Class = {0}", (h[0] >= h[1]) ? 0 : 1);
            }
            Console.ReadKey();
        }
    }

    class pattern
    {
        public pattern(int i, int j, int k)
        {
            idx[0] = i; idx[1] = j; idx[2] = k;
        }
        public int[] idx=new int[3];
        
    }
}
