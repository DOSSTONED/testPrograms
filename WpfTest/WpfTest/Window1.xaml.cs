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


namespace WpfTest
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello world!", "Hi", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
        }
    }
}
