using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 感知机Display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double[] Sample = new double[8] { 1, 1, 1, -1, 1, -1, 1, -1 };
        List<string> processView = new List<string>();

        private void label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label l = sender as Label;
            if (l == null) return;
            string text = l.Content as string;
            if (text == string.Empty || text == null) throw new Exception("This label is not a Legal label.");

            string[] strs = text.Split('=');
            // str1 = (0,0,0) str2 = 1 or -1;
            string[] places = strs[0].Split(new char[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs[1] == "-1") strs[1] = "1";
            else
            {
                if (strs[1] == "1") strs[1] = "-1";
            }
            Sample[int.Parse(places[0]) * 4 + int.Parse(places[1]) * 2 + int.Parse(places[2])] = int.Parse(strs[1]);

            l.Content = strs[0] + "=" + strs[1];
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            /// 变量及初始化
            /// 
            listBoxProcess.Items.Clear();
            processView.Clear();

            /// 当前循环次数，有多少已经固定了
            /// 
            int curLoops = 0, fixedNumber = 0;
            Random ran = new Random();

            /// 学习率，theta，α
            double nita = 0.1, theta = 0, alpha = 1;
            /// 权向量
            double[] w = new double[3];
            /// 初始化
            for (int i = 0; i < 3; i++) w[i] = alpha * ran.NextDouble();
            theta = alpha * ran.NextDouble();


            /// 迭代
            /// 
            while (curLoops++ < 10000)
            {
                if (curLoops % 8 == 0)  /// 8个为一组，每一组要重新计数
                    fixedNumber = 0;
                int estimatedOutput =   /// 预估输出

                (Math.Sign(
                    w[0] * Math.Abs(curLoops % 8 - curLoops % 4) / 4 +
                    w[1] * Math.Abs(curLoops % 4 - curLoops % 2) / 2 +
                    w[2] * (curLoops % 2) -
                    theta
                    ) == -1) ? -1 : 1;

                //listBoxProcess.Items
                processView.Add(string.Format("Step {4}: y={0}x1+{1}x2+{2}x3-{3}",
                    w[0], w[1], w[2], theta, curLoops));


                if (Math.Abs(nita * (Sample[curLoops % 8] - estimatedOutput) * Math.Abs(curLoops % 8 - curLoops % 4) / 4) < 0.00001)
                    fixedNumber++;
                w[0] = w[0] + nita * (Sample[curLoops % 8] - estimatedOutput) * Math.Abs(curLoops % 8 - curLoops % 4) / 4;
                if (Math.Abs(nita * (Sample[curLoops % 8] - estimatedOutput) * Math.Abs(curLoops % 4 - curLoops % 2) / 2) < 0.00001)
                    fixedNumber++;
                w[1] = w[1] + nita * (Sample[curLoops % 8] - estimatedOutput) * Math.Abs(curLoops % 4 - curLoops % 2) / 2;
                if (Math.Abs(nita * (Sample[curLoops % 8] - estimatedOutput) * (curLoops % 2))<0.00001)
                    fixedNumber++;
                w[2] = w[2] + nita * (Sample[curLoops % 8] - estimatedOutput) * (curLoops % 2);
                if (Math.Abs(nita * (Sample[curLoops % 8] - estimatedOutput)) < 0.00001)
                    fixedNumber++;
                theta = theta - nita * (Sample[curLoops % 8] - estimatedOutput);

                labelResult.Content = string.Format("y={0:F4}x1+{1:F4}x2+{2:F4}x3-{3:F4}",
                    w[0], w[1], w[2], theta);
                if (fixedNumber > 31)//indicates all paras are fixed.
                    break;
            }

            if (curLoops > 9999)
            {
                labelResult.Content = "Cannot find the result.";
                listBoxProcess.Items.Clear();
                listBoxResult.Items.Clear();
            }
            else
            {
                listBoxResult.Items.Clear();
                for (int i = 0; i < 8; i++)
                {
                    /*
                    listBoxProcess.Items.Add(
                        string.Format("y = {4}, F({0},{1},{2}) = {3}",
                        (i - i % 4) / 4,
                        (i % 4 - i % 2) / 2,
                        i % 2,
                        w[0] * (i - i % 4) / 4 + w[1] * (i % 4 - i % 2) / 2 + w[2] * (i % 2) - theta,
                        Sample[i]
                        )
                        );
                     */

                    listBoxResult.Items.Add(
                        string.Format("y = {4}, F({0},{1},{2}) = {3}",
                        (i - i % 4) / 4,
                        (i % 4 - i % 2) / 2,
                        i % 2,
                        w[0] * (i - i % 4) / 4 + w[1] * (i % 4 - i % 2) / 2 + w[2] * (i % 2) - theta,
                        Sample[i]
                        )
                        );
                }

                if (listBoxProcess.Visibility == System.Windows.Visibility.Visible)
                    for (int i = 0; i < processView.Count; i++)
                        listBoxProcess.Items.Add(processView[i]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void buttonChangeView_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b.Content.ToString().ToLower().Contains("result"))
            {
                b.Content = "Go to process";
                listBoxProcess.Visibility = System.Windows.Visibility.Hidden;
                listBoxResult.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                b.Content = "Go to result only";
                listBoxResult.Visibility = System.Windows.Visibility.Hidden;
                listBoxProcess.Visibility = System.Windows.Visibility.Visible;
                if (listBoxProcess.Items.Count == 0)
                    for (int i = 0; i < processView.Count; i++)
                        listBoxProcess.Items.Add(processView[i]);
                
            }
        }

        private void buttonAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "感知机小程序：by \r\n" +
                "功能：\r\n" +
                "1. 双击label可以更改取值{-1, 1}\r\n" +
                "2. 点击Go按钮获取结果\r\n" +
                "3. 右下角的按钮用于切换显示最终结果或者显示计算过程"
                );
        }
    }
}
