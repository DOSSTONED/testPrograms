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

namespace WPFCSTEST
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

        
        private int i=0;
        private List<string> aa=new List<string>();
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text!="")
            {
                CheckBox Newcheckbox=new CheckBox();
                
                Newcheckbox.Content=textBox1.Text;
                //Newcheckbox.Width = 120;
                //Newcheckbox.Height = 16;
                //Newcheckbox.Visibility = Visibility.Visible;
                //this.AddChild(Newcheckbox);
                listBox1.Items.Add(Newcheckbox);
            }
            
           
         
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox defaultcheck1 = new CheckBox();
            CheckBox defaultcheck2=new CheckBox();
            defaultcheck1.Content = "exe";
            listBox1.Items.Add(defaultcheck1);
            defaultcheck2.Content = "scr";
            listBox1.Items.Add(defaultcheck2);
        }
    }
}
