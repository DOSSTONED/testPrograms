/* 
 * DHNN问题
 * 课后习题：已经给定4个顶点及其相邻边的权值，利用此权值进行：
 * 计算每个状态的能量函数，求出其稳定点和异步、同步方式下达到稳定点初始状态的个数
 * 
 * 解决方法：
 * 定义全局变量：
 *  w 表示权向量矩阵
 *  theta表示对应的线性量
 * 所用到的函数：
 *  E(v)            表示v对应的能量
 *  getNextAsync(v) 表示v由异步方式得到下一个向量
 *  getNextSync(v)  表示v由同步方式得到下一个向量
 * 为了方便，采用将整数和对应的二进制转换来得到向量，因此引入两个函数：
 *  convert     将一个整数变为对应向量
 *  convertBack 将一个向量变为对应整数
*/
using System;

namespace DHNN
{
    class Program
    {
        static void Main(string[] args)
        {
            /// 能量值
            /// 
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine("E[{0}] = {1}.",i,E(convert(i)));
            }
            /// 异步
            /// 
            Console.WriteLine("\n异步方式\n");
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine("i = {0}", i);
                int currentStep = 0;
                int current = i;
                int next = -1;
                while (current != next)
                {
                    /// 除了第一步，后面的要更新current为上一步计算出来的current。
                    if (currentStep != 0)
                        current = next;
                    currentStep++;
                    /// 获取下一个‘向量’
                    next = convertBack(
                        getNextAsync(
                        convert(current)));
                }
                Console.WriteLine("Original is: {2}, Fixed Point is: {0}, cost steps: {1}.", current, currentStep, i);
            }
            /// 同步
            /// 
            Console.WriteLine("\n同步方式\n");
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine("i = {0}", i);
                int currentStep = 0;
                int current = i;
                int next = -1;
                /// 这个是用来检查循环而使用的
                bool[] check = new bool[16];
                for (int iii = 0; iii < 16; iii++) check[iii] = false;

                while (current != next)
                {
                    /// 除了第一步，后面的要更新current为上一步计算出来的next。
                    if (currentStep != 0)
                        current = next;
                    currentStep++;
                    /// 获取下一个‘向量’
                    next = convertBack(
                        getNextSync(
                        convert(current)));

                    Console.Write("{0}-->", next);
                    /// 这里检查是否构成了循环
                    if (check[next])
                    {
                        if (next != current)
                            Console.WriteLine("Loop!");
                        else
                            Console.WriteLine();
                        break;
                    }
                    check[next] = true;
                }
                Console.WriteLine("Original is: {2}, Fixed Point is: {0}, cost steps: {1}.", current, currentStep, i);
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        /// <summary>
        /// 权向量矩阵
        /// </summary>
        static double[] w = new double[]
        {
            0,3.4,2.8,-3.1,
            3.4,0,4.7,-1.2,
            2.8,4.7,0,-5.9,
            -3.1,-1.2,-5.9,0
        };
        /// <summary>
        /// theta值，也就是线性偏量
        /// </summary>
        static double[] theta = new double[]
        {
            6.3,-4.3,-2.5,-9.6
        };
        /// <summary>
        /// 将一个整数变为对应的二进制
        /// </summary>
        /// <param name="i">输入整数（0~15）</param>
        /// <returns>对应的向量</returns>
        static double[] convert(int i)
        {
            double[] ret = new double[4];
            for (int j = 0; j < 4; j++)
                ret[j] = (i % Math.Pow(2, j + 1) - i % Math.Pow(2, j)) / Math.Pow(2, j) == 0 ? -1 : 1;
            return ret;
        }
        /// <summary>
        /// 将输入向量（1，-1）变为整数
        /// </summary>
        /// <param name="v">输入向量</param>
        /// <returns>对应整数</returns>
        static int convertBack(double[] v)
        {
            double ret = 0;
            ret = 8 * (v[3] + 1) / 2 + 4 * (v[2] + 1) / 2 + 2 * (v[1] + 1) / 2 + (v[0] + 1) / 2;
            return (int)ret;
        }
        /// <summary>
        /// 异步方式的下一个v，采用选取随机的神经元进行演化，因而结果有不确定性
        /// </summary>
        /// <param name="v">当前向量 V_t</param>
        /// <returns>下一步向量 V_t+1</returns>
        static double[] getNextAsync(double[] v)
        {
            double[] ret = new double[4];
            for (int ii = 0; ii < 4; ii++) ret[ii] = v[ii];
            /// 随机选取某个神经元，对其进行演化。
            Random r = new Random();
            /// 随机选择一个小于4的非负数
            int i = r.Next(4);
            ret[i] = 0;
            /// 对该元素进行迭代
            for (int j = 0; j < 4; j++)
                ret[i] = ret[i] += w[4 * i + j] * v[j];
            ret[i] = ret[i] < 0 ? -1 : 1;
            return ret;
        }
        /// <summary>
        /// 同步方式下的下一个v
        /// </summary>
        /// <param name="v">当前向量 V_t</param>
        /// <returns>下一步向量 V_t+1</returns>
        static double[] getNextSync(double[] v)
        {
            double[] ret = new double[4];
            for (int i = 0; i < 4; i++)
            {
                ret[i] = 0;
                for (int j = 0; j < 4; j++)
                {
                    /// 认为初始赋值就是v，然后I那一项就变为0了；
                    ret[i] += w[4 * i + j] * v[j];
                }
                ret[i] = ret[i] < 0 ? -1 : 1;
            }
            return ret;
        }
        /// <summary>
        /// 能量函数
        /// </summary>
        /// <param name="V">当前向量V</param>
        /// <returns>能量值</returns>
        static double E(double[] V)
        {
            double 能量 = 0;
            /// 后面线性部分
            for (int i = 0; i < 4; i++)
                能量 += theta[i] * V[i];
            /// 前面权矩阵部分
            /// 
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    能量 += w[4 * i + j] * V[i] * V[j] * (-0.5);
            return 能量;
        }
    }
}
