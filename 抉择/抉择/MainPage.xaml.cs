using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace 抉择
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void buttonDecide_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> tbs = new List<TextBox>();
            if (textBox1.Text != string.Empty)
            {
                tbs.Add(textBox1);
            }
            if (textBox2.Text != string.Empty)
            {
                tbs.Add(textBox2);
            }
            if (textBox3.Text != string.Empty)
            {
                tbs.Add(textBox3);
            }
            if (textBox4.Text != string.Empty)
            {
                tbs.Add(textBox4);
            }

            if (tbs.Count == 0)
            {
                MessageBox.Show("给个选项吧", "为啥没选项呢", MessageBoxButton.OK);
                return;
            }
            Random ran = new Random();
            int RanNum = ran.Next(0, tbs.Count);
            MessageBox.Show(tbs[RanNum].Text,"你选择了这个!",MessageBoxButton.OK);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox2.Text = "不" + textBox1.Text;
        }
    }
}