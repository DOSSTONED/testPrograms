using System;

namespace Parzen
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.h = 1;
            p.test(p.h, p);

            p.h = 0.1;
            p.test(p.h, p);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// 测试函数
        /// </summary>
        /// <param name="h">h值</param>
        /// <param name="p">Program class，用于调用函数</param>
        void test(double h, Program p)
        {
            double tmp0, tmp1, tmp2;


            double[] test1 = new double[] { 0.5, 1, 0 };
            double[] test2 = new double[] { 0.31, 1.51, -0.5 };
            double[] test3 = new double[] { -0.3, 0.44, -0.1 };
            
            
            // For test1
            tmp0 = p.paz(test1, 0);
            tmp1 = p.paz(test1, 1);
            tmp2 = p.paz(test1, 2);

            if ((tmp0 >= tmp1) && (tmp0 >= tmp2))
                Console.WriteLine("Class = 0, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp0 <= tmp1) && (tmp1 >= tmp2))
                Console.WriteLine("Class = 1, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp2 >= tmp1) && (tmp0 <= tmp2))
                Console.WriteLine("Class = 2, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            
            // For test2
            tmp0 = p.paz(test2, 0);
            tmp1 = p.paz(test2, 1);
            tmp2 = p.paz(test2, 2);

            if ((tmp0 >= tmp1) && (tmp0 >= tmp2))
                Console.WriteLine("Class = 0, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp0 <= tmp1) && (tmp1 >= tmp2))
                Console.WriteLine("Class = 1, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp2 >= tmp1) && (tmp0 <= tmp2))
                Console.WriteLine("Class = 2, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            
            // For test3
            tmp0 = p.paz(test3, 0);
            tmp1 = p.paz(test3, 1);
            tmp2 = p.paz(test3, 2);

            if ((tmp0 >= tmp1) && (tmp0 >= tmp2))
                Console.WriteLine("Class = 0, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp0 <= tmp1) && (tmp1 >= tmp2))
                Console.WriteLine("Class = 1, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
            if ((tmp2 >= tmp1) && (tmp0 <= tmp2))
                Console.WriteLine("Class = 2, tmp0 = {0}, tmp1 = {1}, tmp2 = {2}.", tmp0, tmp1, tmp2);
        }

        /// <summary>
        /// 窗函数（正态分布）
        /// </summary>
        /// <param name="u">向量u</param>
        /// <returns>u向量的正态分布，Math.Exp(-0.5 * (u[0] * u[0] + u[1] * u[1] + u[2] * u[2])) / Math.Sqrt(2.0 * Math.PI)</returns>
        double WindowFunction(double[] u)
        {
            return Math.Exp(-0.5 * (u[0] * u[0] + u[1] * u[1] + u[2] * u[2])) / Math.Sqrt(2.0 * Math.PI);
        }
        /// <summary>
        /// Parzen算法
        /// </summary>
        /// <param name="x">待测向量</param>
        /// <param name="classNum">第几类的类别号</param>
        /// <returns>估计值（最大的为最近似的）</returns>
        double paz(double[] x, int classNum)
        {
            double hn = h / Math.Sqrt(N);
            double p = 0;
            for (int j = 0; j < N; j++)
                p += WindowFunction(new double[] { x[0] - samples[3 * j + classNum, 0] ,
                x[1]-samples[3*j+classNum,1],
                x[2]-samples[3*j+classNum,2]});
            return p / (N * hn);
        }

        double h;
        int N = 10;

        /// <summary>
        /// 给定的样本数据，其中3k+0表示第一类，3k+1表示第二类，3k+2表示第三类
        /// 每一类10个样本，共30个样本
        /// </summary>
        double[,] samples = new double[,]
								{
								{0.28,1.31,-6.2},
								{0.011,1.03,-0.21}
                                ,{1.36,2.17,0.14}
                                ,{2,0.07,0.58}
                                ,{-0.78,1.27,1.28}
                                ,{0.08,1.41,1.45}
                                ,{-0.38,3,1.54}
                                ,{2.01,-1.63,0.13}
                                ,{3.12,0.16,1.22}
                                ,{0.99,0.69,4}
                                ,{-0.44,1.18,-4.32}
                                ,{-0.21,1.23,-0.11}
                                ,{2.46,2.19,1.31}
                                ,{5,-0.81,0.21}
                                ,{5.73,-2.18,1.39}
                                ,{-0.19,0.68,0.79}
                                ,{0.87,6,1.52}
                                ,{3.16,2.77,0.34}
                                ,{1.96,-0.16,2.51}
                                ,{3.22,1.35,7}
                                ,{2.20,2.42,-0.19}
                                ,{-1.38,0.94,0.45}
                                ,{0.60,2.44,0.92}
                                ,{8,0.91,1.94}
                                ,{6.21,-0.12,0.82}
                                ,{0.17,0.64,0.13}
                                ,{0.97,9,0.65}
                                ,{1.93,4.38,-1.44}
                                ,{2.31,0.14,0.85}
                                ,{0.58,0.99,10}
                                ,{-0.26,0.82,-0.96}
                                ,{0.26,1.94,0.08}
                                ,{0.66,0.51,0.88}	

								};

    }
}

