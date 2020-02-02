using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 测试数据集
        /// </summary>
        List<data> testdata = new List<data>();

        NeuronNetworks nn = null;

        void XORTEST()
        {
            nn = new NeuronNetworks(2);
            List<data> testdata = new List<data>();
            testdata.Add(new data(0, 0, 0));
            testdata.Add(new data(0, 1, 1));
            testdata.Add(new data(1, 0, 1));
            testdata.Add(new data(1, 1, 0));


            nn.ε = 0.1;
            //nn.η = 0.5;
            DateTime _start = DateTime.Now;
            while (true)
            {
                ++nn.currentLearnRepeats;
                foreach (data d in testdata)
                {
                    nn.Train(d.input1, d.input2, d.output);
                }
                //if (nn.currentLearnRepeats % 5000 == 0)   // recheck whether it is converge.
                {
                    int coverged = 0;
                    for (int i = 0; i < testdata.Count; i++)
                    {
                        if (Math.Abs(nn.Train(testdata[i].input1, testdata[i].input2, testdata[i].output) - testdata[i].output) < nn.ε) coverged++;
                    }
                    if (coverged == testdata.Count) break;
                    if ((DateTime.Now - _start).Seconds > 5)//timeout
                    {
                        textBox1.Text += ("Request timeout.\r\n");
                        break;
                    }
                }

            }
            textBox1.Text += (DateTime.Now - _start);
            textBox1.Text += ("\r\n");
            textBox1.Text += (nn.currentLearnRepeats + "次迭代\r\n");
            foreach (data d in testdata)
            {
                textBox1.Text += string.Format("{0},{1}->{2}\r\n", d.input1, d.input2, nn.Display(d.input1, d.input2));
            }

            textBox1.Text += ("layer0tolayer1 weights:\r\n");
            for (int i = 0; i < nn.layer0tolayer1.Count; i++)
            {
                for (int j = 0; j < nn.layer0tolayer1[i].Length; j++)
                {
                    textBox1.Text += (nn.layer0tolayer1[i][j]);
                    textBox1.Text += ("\t");
                }
                textBox1.Text += ("\r\n");
            }

            output_the_layer(0, 0);
            output_the_layer(0, 1);
            output_the_layer(1, 0);
            output_the_layer(1, 1);

            textBox1.Text += ("layer1tolayer2 weights:\r\n");
            for (int i = 0; i < nn.layer1tolayer2.Length; i++)
            {
                textBox1.Text += (nn.layer1tolayer2[i]);
                textBox1.Text += ("\r\n");
            }
        }

        void output_the_layer(int x, int y)
        {
            nn.Display(x, y);
            textBox1.Text += (string.Format("({0},{1})layer1 results:\r\n",x,y));
            for (int i = 0; i < nn.layer1.Length; i++)
            {
                textBox1.Text += (nn.layer1[i]);
                textBox1.Text += ("\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            XORTEST();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (nn == null)
                return;// button1_Click(sender, e);
            var gX = panel1.CreateGraphics();
            gX.Clear(panel1.BackColor);
            /// 打算画500x500的，采用600的画布这样就可以有个边界，（0，0）在左上角的（50，50），（1，1）在右下的（550，550）
            /// 

            for (int i = 0; i < 600; i++)
                for (int j = 0; j < 600; j++)
                {
                    //if(Math.Abs( nn.Display((i-50.0)/500,(j-50.0)/500)-0.5)<1)
                    double temp = nn.Display((i - 50.0) / 500, (j - 50.0) / 500) * 255;
                    SolidBrush b = new SolidBrush(Color.FromArgb((int)temp, 255 - (int)temp, (int)temp));
                    gX.FillRectangle(b, i, j, 2, 2);
                    //if (Math.Abs(temp - 127) < 2)
                    //    gX.FillRectangle(Brushes.Purple, i, j, 2, 2);
                }

            gX.FillRectangle(Brushes.Red, 50, 50, 20, 20);
            gX.FillRectangle(Brushes.Red, 550, 550, 20, 20);
            gX.FillRectangle(Brushes.Blue, 550, 50, 20, 20);
            gX.FillRectangle(Brushes.Blue, 50, 550, 20, 20);
        }
    }


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
        public List<double[]> layer0tolayer1 = new List<double[]>();
        /// <summary>
        /// 中间层
        /// </summary>
        public double[] layer1 = new double[1];
        /// <summary>
        /// 中间层到输出层的权矩阵
        /// </summary>
        public double[] layer1tolayer2 = new double[1];
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
            for (int i = 0; i < layer1numbers; i++) /// give random weights.
            {
                layer0tolayer1.Add(new double[3]);  // 输入的话，有一个是线性分量的基础，所以用常量1
                layer0tolayer1[i][0] = 2 * ran.NextDouble() - 1;
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
            double estimatedOut = GiveResult(layer0_0, layer0_1);   // get the current output

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
        /// 任务，默认超时10s
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
}
