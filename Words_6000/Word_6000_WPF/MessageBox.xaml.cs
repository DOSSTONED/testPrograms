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
using System.Windows.Shapes;

namespace Word_6000_WPF
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        private static MessageBoxResult Result = MessageBoxResult.No;
        internal int countdown = 3;

        public static MessageBoxResult ShowMsgBox(string content, string title)
        {
            MessageBox mbox = new MessageBox();
            mbox.Title = title;
            mbox.textBlock1.Text = title + "\r\n" + content;
            mbox.ShowDialog();
            return Result;//throw new NotImplementedException();
        }

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Close();
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Start();
        }


        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            countdown--;
            if (countdown < 1)
            {
                Result = MessageBoxResult.No;
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                    delegate()
                    {
                        Close();
                    }
                    )
                    );//Close();//throw new NotImplementedException();
            }
        }

    }
}
