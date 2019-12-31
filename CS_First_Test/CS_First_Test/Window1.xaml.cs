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

namespace CS_First_Test
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Question, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message", "Message", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

    }
}
