using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace BP神经网络
{
    /// <summary>
    /// 神经网络类
    /// </summary>
    class NeuronNetworks
    {
        /// <summary>
        /// 输入1
        /// </summary>
        double layer0_0 = 0;
        /// <summary>
        /// 输入2
        /// </summary>
        double layer0_1 = 0;
        /// <summary>
        /// 输入层到中间层的权矩阵
        /// </summary>
        List<double[]> layer0tolayer1 = new List<double[]>();
        /// <summary>
        /// 中间层
        /// </summary>
        double[] layer1 = new double[1];
        /// <summary>
        /// 中间层到输出层的权矩阵
        /// </summary>
        double[] layer1tolayer2 = new double[1];
        /// <summary>
        /// 反馈矩阵
        /// </summary>
        double[] layer2tolayer1 = new double[1];
        /// <summary>
        /// 最后的误差
        /// </summary>
        double 误差 = 0;
        //double layer2_0 = 0;    // only 1 out put.
        /// <summary>
        /// 误差目标
        /// </summary>
        public double ε = 0.01;
        /// <summary>
        /// 学习率
        /// </summary>
        public double η = 0.1;
        

        /// <summary>
        /// 最大迭代次数。之前做测试的时候用，后来发现由于本身一次迭代都很好时间，因此这个值也无太大意义，就是逻辑上保证会退出。
        /// </summary>
        public int maxLearnRepeats = 100000;
        /// <summary>
        /// 当前迭代次数（做了几套训练）
        /// </summary>
        public int currentLearnRepeats = 0;

        /// <summary>
        /// 神经网络的构造
        /// </summary>
        /// <param name="layer1numbers">中间层神经单元的数目</param>
        public NeuronNetworks(int layer1numbers)
        {
            /// 随机数
            Random ran = new Random();//DateTime.Now.Second);
            layer1 = new double[layer1numbers];
            layer1tolayer2 = new double[layer1numbers + 1];// 多一个，最后一个，用于线性分量1
            layer2tolayer1 = new double[layer1numbers];
            layer0tolayer1 = new List<double[]>();
            //layer1 = new List<double[]>(layer1numbers);
            for (int i = 0; i < layer1numbers; i++) /// give random weights.
            {
                layer0tolayer1.Add(new double[3]);  // 输入的话，有一个是线性分量的基础，所以用常量1
                layer0tolayer1[i][0] = 0.1 * (ran.NextDouble() - 0.5);
                layer0tolayer1[i][1] = 0.1 * (ran.NextDouble() - 0.5);
                layer0tolayer1[i][2] = 0.1 * (ran.NextDouble() - 0.5);

                layer1tolayer2[i] = 0.1 * (ran.NextDouble() - 0.5);//最后一个为常量

                //Console.Write(layer0tolayer1[i][0]);
                //Console.Write("\t");
                //Console.Write(layer0tolayer1[i][1]);
                //Console.Write("\t");
                //Console.Write(layer0tolayer1[i][2]);
                //Console.Write("\n");
                //Console.WriteLine(layer1tolayer2[i]);
            }

        }

        public NeuronNetworks(int layer1numbers, Random ran)
        {
            /// 随机数
            layer1 = new double[layer1numbers];
            layer1tolayer2 = new double[layer1numbers + 1];// 多一个，最后一个，用于线性分量1
            layer2tolayer1 = new double[layer1numbers];
            layer0tolayer1 = new List<double[]>();
            //layer1 = new List<double[]>(layer1numbers);
            for (int i = 0; i < layer1numbers; i++) /// give random weights.
            {
                layer0tolayer1.Add(new double[3]);  // 输入的话，有一个是线性分量的基础，所以用常量1
                layer0tolayer1[i][0] = 0.1 * (ran.NextDouble() - 0.0);
                layer0tolayer1[i][1] = 0.1 * (ran.NextDouble() - 0.0);
                layer0tolayer1[i][2] = 0.1 * (ran.NextDouble() - 0.0);

                layer1tolayer2[i] = 0.1 * (ran.NextDouble() - 0.0);//最后一个为常量

                //Console.Write(layer0tolayer1[i][0]);
                //Console.Write("\t");
                //Console.Write(layer0tolayer1[i][1]);
                //Console.Write("\t");
                //Console.Write(layer0tolayer1[i][2]);
                //Console.Write("\n");
                //Console.WriteLine(layer1tolayer2[i]);
            }

        }

        /// <summary>
        /// 训练本网络
        /// </summary>
        /// <param name="input1">输入1</param>
        /// <param name="input2">输入2</param>
        /// <param name="target">目标输出</param>
        /// <returns>实际输出</returns>
        public double Train(double input1, double input2, double target)
        {
            layer0_0 = input1;
            layer0_1 = input2;
            double estimatedOut = GiveResult(layer0_0, layer0_1);   // get the current output

            //计算误差，并且反向修改权值
            误差 =  (target - estimatedOut);//estimatedOut * (1 - estimatedOut) *
            for (int i = 0; i < layer2tolayer1.Length; i++)
            {
                layer2tolayer1[i] = sigmoid(layer1[i]) * (1 - sigmoid(layer1[i])) * 误差 * layer1tolayer2[i];
            }

            // 调整权值
            for (int j = 0; j < layer1.Length; j++)
            {
                layer0tolayer1[j][0] += η * (-1) * layer2tolayer1[j];
                layer0tolayer1[j][1] += η * input1 * layer2tolayer1[j];
                layer0tolayer1[j][2] += η * input2 * layer2tolayer1[j];
            }

            //layer0tolayer1[layer1.Length + 1][0] += η * 1 * 误差;
            //layer0tolayer1[layer1.Length + 1][1] += η * 1 * 误差;
            //layer0tolayer1[layer1.Length + 1][2] += η * 1 * 误差;
            for (int i = 0; i < layer1tolayer2.Length - 1; i++)
            {
                layer1tolayer2[i] += η * sigmoid(layer1[i]) * 误差;
            }
            layer1tolayer2[layer1tolayer2.Length - 1] += η * (-1) * 误差;

            return estimatedOut;
        }

        /// <summary>
        /// 显示本网络的实际输出
        /// </summary>
        /// <param name="input1">输入1</param>
        /// <param name="input2">输入2</param>
        /// <returns>实际输出</returns>
        public double Display(double input1, double input2)
        {
            layer0_0 = input1;
            layer0_1 = input2;
            double estimatedOut = GiveResult(layer0_0, layer0_1);   // get the current output

            return estimatedOut;
        }

        /// <summary>
        /// 1次计算并给出实际的输出
        /// 利用Sigmoid函数
        /// </summary>
        /// <param name="input1">输入1</param>
        /// <param name="input2">输入2</param>
        /// <returns>输出</returns>
        double GiveResult(double input1, double input2)
        {
            for (int i = 0; i < layer1.Length; i++)
            {
                layer1[i] = -1 * layer0tolayer1[i][0] + input1 * layer0tolayer1[i][1] + input2 * layer0tolayer1[i][2];   //计算中间层的每个原始值
            }
            double ret = 0;
            for (int i = 0; i < layer1.Length; i++)
            {
                ret += sigmoid(layer1[i]) * layer1tolayer2[i];    //用sigmoid函数变换后得到输出层
            }
            ret -= layer1tolayer2[layer1.Length] * 1;   // 加上一个常量
            return sigmoid(ret);  //返回一个sigmoid函数变换后的值
        }

        /// <summary>
        /// Sigmoid函数
        /// </summary>
        /// <param name="x">输入x</param>
        /// <returns>返回1 / (1 + e^(-x))</returns>
        double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public void DisplayWeight()
        {
            Console.WriteLine("layer0tolayer1 weights:");
            for (int i = 0; i < layer0tolayer1.Count; i++)
            {
                for (int j = 0; j < layer0tolayer1[i].Length; j++)
                {
                    Console.Write(layer0tolayer1[i][j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("layer1tolayer2 weights:");
            for (int i = 0; i < layer1tolayer2.Length; i++)
                Console.WriteLine(layer1tolayer2[i]);
        }
    }

    /// <summary>
    /// 包含两个输入和一个输出
    /// </summary>
    public class data
    {
        /// <summary>
        /// 输入1
        /// </summary>
        public double input1;
        /// <summary>
        /// 输入2
        /// </summary>
        public double input2;
        /// <summary>
        /// 预计输出
        /// </summary>
        public double output;
        /// <summary>
        /// 初始化该实例
        /// </summary>
        /// <param name="i1">输入1</param>
        /// <param name="i2">输入2</param>
        /// <param name="outp">预计输出</param>
        public data(double i1, double i2, double outp)
        {
            input1 = i1; input2 = i2; output = outp;
        }
    }

    /// <summary>
    /// 指派任务，包含m值和多少次检测一下
    /// </summary>
    public class Jobs
    {
        /// <summary>
        /// 中间层的神经元数目
        /// </summary>
        public int m;
        /// <summary>
        /// 两次检测之间的间隔，比较小的话会经常检测而耗费时间，比较大的话可能已经收敛了但是还没有检测到
        /// </summary>
        public int testpoint;
        /// <summary>
        /// 超时，可设定60s，如果在检测的时候已经超时，那就强制退出该次任务
        /// </summary>
        public int timeout;
        /// <summary>
        /// 任务，默认超时60s
        /// </summary>
        /// <param name="_m">中间神经元个数</param>
        /// <param name="_testpoint">测试次数间隔</param>
        public Jobs(int _m, int _testpoint)
        {
            m = _m;
            testpoint = _testpoint;
            timeout = 10;
        }
        /// <summary>
        /// 任务
        /// </summary>
        /// <param name="_m">中间神经元个数</param>
        /// <param name="_testpoint">测试次数间隔</param>
        /// <param name="_timeout">手动指定超时</param>
        public Jobs(int _m, int _testpoint, int _timeout)
        {
            m = _m;
            testpoint = _testpoint;
            timeout = _timeout;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            /// 公式的测试，这个耗时较大。
            p.FormulaTest();
        }

        /// <summary>
        /// 所有测试任务
        /// </summary>
        static Stack<Jobs> AllJobs = new Stack<Jobs>();
        /// <summary>
        /// 原始数据集
        /// </summary>
        List<data> ClassifiedData = new List<data>();
        /// <summary>
        /// 待测数据集
        /// </summary>
        List<data> TestData = new List<data>();
        /// <summary>
        /// 测试
        /// </summary>
        void FormulaTest()//
        {
            List<Matrix<double>> C0 = new List<Matrix<double>>(), C1 = new List<Matrix<double>>();
            List<Matrix<double>> Test0 = new List<Matrix<double>>(), Test1 = new List<Matrix<double>>();
            var m2 = new DenseMatrix(new[,] { { 1.0 }, { 0.5 } });
            var k2 = new DenseMatrix(new[,] { { 1.0 } });
            var v2 = new DenseMatrix(new[,] { { 3.0, 1.0 }, { 1.0, 2.0 } });

            var m1 = new DenseMatrix(new[,] { { 0.0 }, { 0.0 } });
            var k1 = new DenseMatrix(new[,] { { 1.0 } });
            var v1 = new DenseMatrix(new[,] { { 1.0, 0.0 }, { 0.0, 1.0 } });
            
            ClassifiedData.Clear();
            for (int i = 0; i < 50; i++)
            {
                C0.Add(MatrixNormal.Sample(new Random(), m2, v2, k2));
                ClassifiedData.Add(new data(C0[0][0, 0], C0[0][1, 0], 0));
                C1.Add(MatrixNormal.Sample(new Random(), m1, v1, k1));
                ClassifiedData.Add(new data(C1[0][0, 0], C1[0][1, 0], 1));
            }
            for (int i = 0; i < 20; i++)
            {
                Test0.Add(MatrixNormal.Sample(new Random(), m2, v2, k2));
                Test1.Add(MatrixNormal.Sample(new Random(), m1, v1, k1));
                TestData.Add(new data(Test0[0][0, 0], Test0[0][1, 0], 0));
                C1.Add(MatrixNormal.Sample(new Random(), m1, v1, k1));
                TestData.Add(new data(Test1[0][0, 0], Test1[0][1, 0], 1));
            }


            int indicator = 0;
            int endNetworks = 50;
            int startNetworks = 1;
            //int seed = 0;
            
            do
            {
                indicator = 0;
                for (int ii = startNetworks; ii <= endNetworks; ii++)
                {
                    Jobs ajob = new Jobs(ii, 10);

                    /// 新建一个神经网络
                    NeuronNetworks nn = new NeuronNetworks(ajob.m);//, new Random(DateTime.Now.Millisecond));
                    /// 当前的迭代次数
                    nn.currentLearnRepeats = 0;

                    DateTime _start = DateTime.Now;
                    while (true)
                    //(++nn.currentLearnRepeats < nn.maxLearnRepeats)
                    {
                        ++nn.currentLearnRepeats;
                        foreach (data d in ClassifiedData)
                        {
                            nn.Train(d.input1, d.input2, d.output);
                        }
                        //if (nn.currentLearnRepeats % ajob.testpoint == 0)   // recheck whether it is converge.
                        {
                            double errorsum = 0;
                            for (int i = 0; i < ClassifiedData.Count; i++)
                            {
                                double estimateOut = nn.Display(ClassifiedData[i].input1, ClassifiedData[i].input2) - ClassifiedData[i].output;
                                errorsum += (estimateOut * estimateOut) / 2;
                            }
                            if (errorsum < nn.ε) break;
                            if ((DateTime.Now - _start).Seconds > ajob.timeout)//timeout
                            {
                                Console.WriteLine(DateTime.Now - _start);
                                Console.WriteLine("Request timeout.");
                                break;
                            }
                        }

                    }

                    double sum = 0;
                    foreach (data d in TestData)
                    {
                        double t = (nn.Display(d.input1, d.input2) - d.output);
                        //Console.WriteLine("{0},{1}->{2}", d.input1, d.input2, nn.Display(d.input1, d.input2));
                        sum += t * t;
                    }
                    //Console.WriteLine(DateTime.Now - _start);未知样本的总差值=
                    if (sum > 10) indicator++;


                    //Console.WriteLine("Task: m={0},testpoint={1};", ajob.m, ajob.testpoint);
                    //Console.WriteLine("开始计算差值");
                    //Console.WriteLine(nn.currentLearnRepeats);
                    sum = 0;
                    foreach (data d in ClassifiedData)
                    {
                        double t = (nn.Display(d.input1, d.input2) - d.output);
                        //Console.WriteLine("{0},{1}->{2}", d.input1, d.input2, nn.Display(d.input1, d.input2));
                        sum += t * t;
                    }
                    //Console.WriteLine(DateTime.Now - _start);样本训练后的总差值=
                    Console.Write("{0}\t", sum);

                    sum = 0;
                    foreach (data d in TestData)
                    {
                        double t = (nn.Display(d.input1, d.input2) - d.output);
                        //Console.WriteLine("{0},{1}->{2}", d.input1, d.input2, nn.Display(d.input1, d.input2));
                        //if (t > 0.7) t = 1;
                        //else t = 0;
                        sum += t * t;
                    }
                    //Console.WriteLine(DateTime.Now - _start);未知样本的总差值=
                    Console.WriteLine("{0}", sum);
                    //if (sum < 10) Console.ReadKey();
                    if (indicator >= (endNetworks - startNetworks) * 0.9) Console.Clear();
                }

            } while (indicator > 5);
            Console.WriteLine("Press any key to exit.");
            if(indicator<5)
            Console.ReadKey();
        }



    }
}
