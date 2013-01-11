using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        // for ε = 0.01, I have:
        // learning rate, I use 0.01 first, and then 0.1(needs 200000), finally found 0.5 works well(40000 is ok,但是这种情况下可能不收敛，不收敛概率在10%以下).
        // learning rate for 0-1 layer
        //double η1_2 = 0.01; // learning rate for 1-2 layer

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
            Random ran = new Random();
            layer1 = new double[layer1numbers];
            layer1tolayer2 = new double[layer1numbers + 1];// 多一个，最后一个，用于线性分量1
            layer2tolayer1 = new double[layer1numbers];
            layer0tolayer1 = new List<double[]>();
            //layer1 = new List<double[]>(layer1numbers);
            for (int i = 0; i < layer1numbers + 1; i++) /// give random weights.
            {
                layer0tolayer1.Add(new double[3]);  // 输入的话，有一个是线性分量的基础，所以用常量1
                layer0tolayer1[i][0] = 2*ran.NextDouble()-1;
                layer0tolayer1[i][1] = 2 * ran.NextDouble() - 1;
                layer0tolayer1[i][2] = 2 * ran.NextDouble() - 1;

                layer1tolayer2[i] = 2 * ran.NextDouble() - 1;//最后一个为常量
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
            double estimatedOut = UpdateLayer1AndGiveFinalResult(layer0_0, layer0_1);   // get the current output

            //计算误差，并且反向修改权值
            误差 = estimatedOut * (1 - estimatedOut) * (target - estimatedOut);
            for (int i = 0; i < layer2tolayer1.Length; i++)
            {
                layer2tolayer1[i] = sigmoid(layer1[i]) * (1 - sigmoid(layer1[i])) * 误差 * layer1tolayer2[i];
            }

            // 调整权值
            for (int j = 0; j < layer1.Length; j++)
            {
                layer0tolayer1[j][0] += η * 1 * layer2tolayer1[j];
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
            layer1tolayer2[layer1tolayer2.Length - 1] += η * 1 * 误差;

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
            double estimatedOut = UpdateLayer1AndGiveFinalResult(layer0_0, layer0_1);   // get the current output

            return estimatedOut;
        }

        /// <summary>
        /// 1次计算并给出实际的输出
        /// 利用Sigmoid函数
        /// </summary>
        /// <param name="input1">输入1</param>
        /// <param name="input2">输入2</param>
        /// <returns>输出</returns>
        double UpdateLayer1AndGiveFinalResult(double input1, double input2)
        {
            for (int i = 0; i < layer1.Length; i++)
            {
                layer1[i] = 1 * layer0tolayer1[i][0] + input1 * layer0tolayer1[i][1] + input2 * layer0tolayer1[i][2];   //计算中间层的每个原始值
            }
            double ret = 0;
            for (int i = 0; i < layer1.Length; i++)
            {
                ret += sigmoid(layer1[i]) * layer1tolayer2[i];    //用sigmoid函数变换后得到输出层
            }
            ret += layer1tolayer2[layer1.Length] * 1;   // 加上一个常量
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
        public Jobs(int _m,int _testpoint)
        {
            m = _m;
            testpoint = _testpoint;
            timeout = 60;
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
            /// 异或的测试，这个比较省时间
            p.XORTEST();
            /// 公式的测试，这个耗时较大。
            p.FormulaTest();
        }

        /// <summary>
        /// 所有测试任务
        /// </summary>
        static Stack<Jobs> AllJobs = new Stack<Jobs>();
        /// <summary>
        /// 指示该位置是否已经作为样本
        /// </summary>
        bool[] generated = new bool[10000];
        /// <summary>
        /// 测试数据集
        /// </summary>
        List<data> testdata = new List<data>();

        /// <summary>
        /// 测试z=1/sqrt(1+x^2+y^2)
        /// </summary>
        void FormulaTest()//
        {
            //int timeout = 60;
            //List<uint> testpoints = new List<uint>(); ;
            Console.WriteLine("正在生成5000个数据，时间大概在2分钟");
            DateTime _s = DateTime.Now;
            
            /// 全部为false
            for (int i = 0; i < generated.Length; i++) generated[i] = false;
            int 已经生成的数据量 = 0;
            /// 生成数据集，要一半不同的数据……
            /// 
            while (已经生成的数据量 < 5000)
            {
                Random ran = new Random();
                int cur = ran.Next(10000);
                if (generated[cur])
                    continue;
                double a = cur - cur % 100;
                a = a / 10000;
                double b = cur % 100;
                b = b / 100;
                generated[cur] = true;
                testdata.Add(new data(a, b, 1 / Math.Sqrt(1 + a * a + b * b)));
                已经生成的数据量++;
            }
            Console.WriteLine(DateTime.Now - _s);
            /// 数据已经生成

            /// 添加任务
            AllJobs.Push(new Jobs(100, 15000));
            AllJobs.Push(new Jobs(100, 5000));
            AllJobs.Push(new Jobs(100, 100));
            AllJobs.Push(new Jobs(50, 15000));
            AllJobs.Push(new Jobs(50, 5000));
            AllJobs.Push(new Jobs(50, 100));
            AllJobs.Push(new Jobs(10, 5000));
            AllJobs.Push(new Jobs(10, 1000));
            AllJobs.Push(new Jobs(10, 100));
            
            
            
            //List<int> Ms = new List<int>();
            //Ms.Add(10);
            //Ms.Add(50);
            //Ms.Add(100);
            //testpoints.Add(100);
            //testpoints.Add(5000);
            //testpoints.Add(15000);
            ////testpoints.Add(30000);

            /// 开辟新线程来计算，可以有效利用多核
            Thread t1 = new Thread(new ThreadStart(() => dojob()));
            Thread t2 = new Thread(new ThreadStart(() => dojob()));

            t1.Start();
            t2.Start();

            /// 等待线程结束
            t1.Join();
            t2.Join();
            Console.WriteLine(DateTime.Now - _s);
            Console.WriteLine("结束");
            Console.ReadLine();
        }

        /// <summary>
        /// 线程执行的任务
        /// </summary>
        void dojob()
        {
            Jobs ajob = new Jobs(0, 0);
            /// 当前已经没有待执行的任务，退出
            while (AllJobs.Count > 0)
            {
                /// 否则选取一个任务
                ajob = AllJobs.Pop();
                /// 输出当前任务信息
                Console.WriteLine("Running for m={0},testpoint={1};", ajob.m, ajob.testpoint);
                /// 新建一个神经网络
                NeuronNetworks nn = new NeuronNetworks(ajob.m);
                /// 设定学习率
                nn.ε = 0.01;
                /// 当前的迭代次数
                nn.currentLearnRepeats = 0;

                DateTime _start = DateTime.Now;
                while (true)
                //(++nn.currentLearnRepeats < nn.maxLearnRepeats)
                {
                    ++nn.currentLearnRepeats;
                    foreach (data d in testdata)
                    {
                        nn.Train(d.input1, d.input2, d.output);
                    }
                    if (nn.currentLearnRepeats % ajob.testpoint == 0)   // recheck whether it is converge.
                    {
                        double errorsum = 0;
                        for (int i = 0; i < testdata.Count; i++)
                        {
                            double estimateOut = nn.Display(testdata[i].input1, testdata[i].input2) - testdata[i].output;
                            errorsum += estimateOut * estimateOut;
                        }
                        if (errorsum < 2 * nn.ε) break;
                        if ((DateTime.Now - _start).Seconds > ajob.timeout)//timeout
                        {
                            Console.WriteLine(DateTime.Now - _start);
                            Console.WriteLine("Request timeout.");
                            break;
                        }
                    }

                }
                Console.WriteLine("Task: m={0},testpoint={1};", ajob.m, ajob.testpoint);
                Console.WriteLine("开始计算差值");
                Console.WriteLine(nn.currentLearnRepeats);
                double sum = 0;
                foreach (data d in testdata)
                {
                    double t = (nn.Display(d.input1, d.input2) - d.output);
                    //Console.WriteLine("{0},{1}->{2}", d.input1, d.input2, nn.Display(d.input1, d.input2));
                    sum += t * t;
                }
                Console.WriteLine(DateTime.Now - _start);
                Console.WriteLine("样本训练后的总差值={0}", sum);

                sum = 0;
                for (int i = 0; i < generated.Length; i++)
                    if (!generated[i])
                    {
                        double a = i - i % 100;
                        a = a / 10000;
                        double b = i % 100;
                        b = b / 100;
                        double t = (nn.Display(a, b) - 1 / Math.Sqrt(1 + a * a + b * b));
                        sum += t * t;
                    }
                Console.WriteLine(DateTime.Now - _start);
                Console.WriteLine("未知样本的总差值={0}", sum);
            }
        }

        void XORTEST()
        {
            List<data> testdata = new List<data>();
            testdata.Add(new data(0, 0, 0));
            testdata.Add(new data(0, 1, 1));
            testdata.Add(new data(1, 0, 1));
            testdata.Add(new data(1, 1, 0));

            NeuronNetworks nn = new NeuronNetworks(2);
            Console.WriteLine("请等待……\n由于采用了0.001的误差判断，在调整学习率为0.5之后可以在20s内收敛，否则需要更多时间。采用0.01则在1秒之内就可以了。");
            nn.ε = 0.001;
            nn.η = 0.5;
            DateTime _start = DateTime.Now;
            while (true)//改为true之后快很多！！
            //(++nn.currentLearnRepeats < nn.maxLearnRepeats)
            {
                ++nn.currentLearnRepeats;
                foreach (data d in testdata)
                {
                    nn.Train(d.input1, d.input2, d.output);
                }
                if (nn.currentLearnRepeats % 5000 == 0)   // recheck whether it is converge.
                {
                    int coverged = 0;
                    for (int i = 0; i < testdata.Count; i++)
                    {
                        if (Math.Abs(nn.Train(testdata[i].input1, testdata[i].input2, testdata[i].output) - testdata[i].output) < nn.ε) coverged++;
                    }
                    if (coverged == testdata.Count) break;
                    if ((DateTime.Now - _start).Seconds > 20)//timeout
                    {
                        Console.WriteLine("Request timeout.");
                        break;
                    }
                }

            }
            Console.WriteLine(DateTime.Now - _start);
            Console.WriteLine(nn.currentLearnRepeats+"次迭代");
            foreach (data d in testdata)
            {
                Console.WriteLine("{0},{1}->{2}", d.input1, d.input2, nn.Display(d.input1, d.input2));
            }
            Console.WriteLine("按任意键继续");
            Console.ReadKey();
        }
    }
}
