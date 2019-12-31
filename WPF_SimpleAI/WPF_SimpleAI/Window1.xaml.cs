using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_SimpleAI
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        Button[,] Field = new Button[10,10];
        public Window1()
        {
            InitializeComponent();
            Random ra = new Random();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Field[i, j] = new Button();
                    Field[i, j].Background = Brushes.Orange;
                    Field[i, j].Width = 21;
                    Field[i, j].Height = 21;
                    Field[i, j].HorizontalAlignment = HorizontalAlignment.Left;
                    Field[i, j].VerticalAlignment = VerticalAlignment.Top;
                    Field[i, j].Margin = new Thickness(21 * i, 21 * j, 0, 0);
                    gridFieldZone.Children.Add(Field[i, j]);
                }
            Create_Filed();
        }



        #region GENE!!!

        /// <summary>
        /// 
        /// The gene indicates this :
        /// 0 -> left
        /// 1 -> top
        /// 2 -> right
        /// 3 -> bottom
        /// </summary>
        /// <returns></returns>

        const int numbers = 100;
        const int length = 30;// every gene has a maxlength 30
        string[] Genes = new string[numbers];
        double[] fitness = new double[numbers];
        double exchangerate = 0.02;
        double changerate = 0.001;


        string GetGene()
        {
            Random ran = new Random();
            string t = string.Empty;
            int i=0;
            while (i < length)
            {
                t += ran.Next(4).ToString();
                i++;
            }
            return t;
        }

        void InitGenes()
        {
            for (int i = 0; i < numbers; i++)
                Genes[i] = GetGene();
        }

        //string GenerateGene(string parent1, string parent2)
        //{
        //    string t = new string('0',30);
        //    for (int i = 0; i < length; i++)
        //    {
        //        Random r = new Random();
        //        if (r.Next(2) == 1)
        //        {
        //            t[i] = parent1[i];
        //        }
        //        else
        //        {
        //            t[i] = parent2[i];
        //        }
        //    }
        //    return t;
        //}

        #endregion

        void Create_Filed()
        {
            Random r = new Random();
            for (int k = 0; k < 10; k++)
            {
                int i = r.Next(10);
                int j = r.Next(10);
                Field[i, j].IsEnabled = false;
                Field[i, j].Background = Brushes.Black;
            }
        }

        
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            InitGenes();
            Create_Filed();
        }
    }
}
