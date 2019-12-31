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
using System.Threading;

namespace 秒表
{
    public partial class MainPage : PhoneApplicationPage
    {
        enum TimerState
        {
            Running,
            Stopped
        }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        static DateTime _start, _end;
        private Timer t;
        private TimerState _CurrentState = TimerState.Stopped;

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            
            if (_CurrentState == TimerState.Stopped)
            {
                buttonStart.Content = "停止";
                _CurrentState = TimerState.Running;
                _start = DateTime.Now;
                t = new Timer(MyTimerCallback, textBlockTime, 0, 33);
                return;
            }
            if (_CurrentState == TimerState.Running)
            {
                _CurrentState = TimerState.Stopped;
                buttonStart.Content = "开始";
                _end = DateTime.Now;
                t.Dispose();
                textBlockTime.Text = (_end - _start).TotalSeconds.ToString("f2") + "s";
                listBox1.Items.Add((_end - _start).TotalSeconds);
                double time = 0;
                foreach (object o in listBox1.Items)
                {
                    time += (double)o;
                }
                textBlockAvg.Text = string.Format("Avg: {0}s", (time / listBox1.Items.Count).ToString("f2"));
                
                return;
            }
        }

        private static void MyTimerCallback(object state)
        {
            TextBlock outputBlock = (TextBlock)state;
            string msg = (DateTime.Now - _start).TotalSeconds.ToString("f2") + "s";

            outputBlock.Dispatcher.BeginInvoke(delegate() { outputBlock.Text = msg; });
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            t.Dispose();
            Thread.Sleep(10);
            _CurrentState = TimerState.Stopped;
            buttonStart.Content = "开始";
            textBlockTime.Text = "0s";
            textBlockAvg.Text = "Avg: 0s";
            listBox1.Items.Clear();
        }

        private void textBlockTime_Tap(object sender, GestureEventArgs e)
        {
            buttonStart_Click(sender, null);
        }
    }
}