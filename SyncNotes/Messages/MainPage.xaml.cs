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
using IPv6_Test;

namespace Messages
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private bool isDragging = false;
        int x, y;

        private void textBlock1_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock cur = sender as TextBlock;
            if (cur == null) return;

            isDragging = true;
            //x = e.X; y = e.Y;
        }

        private void textBlock1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                TextBlock cur = sender as TextBlock;
                //cur.Margin.Left = e.GetPosition(null).X + cur.Margin.Left - x;
                //cur.Margin.Top = e.Y + cur.Margin.Top - y;
            }
        }

        private void textBlock1_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void textBlock1_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

            if (e.TotalManipulation.Translation.X < -100)// move the object to left
            {
                textBlockSendIndicator.Text = "Must sent";
                //DoubleAnimation da = new DoubleAnimation();
                //da.Duration = new Duration(new TimeSpan(0, 0, 2));
                //da.To = 0;
                //Storyboard myStoryboard = new Storyboard();
                //myStoryboard.Children.Add(da);
                //Storyboard.SetTargetName(da, "textBlock1");
                //Storyboard.SetTargetProperty(da, new PropertyPath(Rectangle.OpacityProperty));
                //myStoryboard.Begin();
                FadeTextBlock.Begin();

                client.Send("__TEXT__" + textBlockText.Text);
                if (client.Receive() == "__TEXT__RECV")
                {
                    textBlockSendIndicator.Text = "Successful sent.";
                    
                }
            }
            else
            {
                textBlockSendIndicator.Text = "Dont send.";
            }
        }

        void FadeTextBlock_Completed(object sender, EventArgs e)
        {
            Random ran = new Random();
            textBlockText.Opacity = 1;
            TextBox1.Text = ran.Next(10000) + "Success.";


            try
            {
                TranslateTransform tt = textBlockText.RenderTransform as TranslateTransform;
                if (tt != null)
                {
                    tt.X = 110;
                    tt.Y = 90;
                }

                /// cannot move the textBlock!!!
                /// 
                //Canvas.SetLeft(textBlockText, 110);
                //Canvas.SetTop(textBlockText, 90);

                //this.UpdateLayout();

                if (!canvas1.Children.Contains(textBlockText))
                    canvas1.Children.Add(textBlockText);

                //textBlockText.RenderTransformOrigin.X = 110;
                //textBlockText.RenderTransformOrigin.Y = 90;

                //Dispatcher.BeginInvoke(new Action(delegate()
                //    {
                //        //textBlockText.SetValue(Canvas.TopProperty, 90.0);
                //        //textBlockText.SetValue(Canvas.LeftProperty, 110.0);
                //        Canvas.SetLeft(textBlockText, 110);
                //        Canvas.SetTop(textBlockText, 90);
                //    }
                //));
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Timer t = new Timer(Period);
            
            // Attempt to connect to the echo server
            string result = client.Connect(HostIP, DOSCUS_PORT);

            //t.Change(5000, 50);

            //Random ran = new Random();
            //// Attempt to send our message to be echoed to the echo server
            //result = client.Send(ran.Next().ToString());

            //// Receive a response from the echo server
            //result = client.Receive();


            FadeTextBlock.Completed += new EventHandler(FadeTextBlock_Completed);
        }

        // Instantiate the SocketClient
        SocketClient client = new SocketClient();
        const int DOSCUS_PORT = 41519;  // Customed port.
        string HostIP = "192.168.4.15";

        /// <summary>
        /// This is intended for Recving information from PC to phone.
        /// </summary>
        /// <param name="o"></param>
        void Period(object o)
        {


            string result;

            Random ran = new Random();
            // Attempt to send our message to be echoed to the echo server
            result = client.Send(ran.Next().ToString());

            // Receive a response from the echo server
            result = client.Receive();


        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Close the socket connection explicitly
            client.Close();
        }

        private void textBlock1_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            client.Send("__MOVE__X=" + e.DeltaManipulation.Translation.X);// + "Y=" + e.DeltaManipulation.Translation.Y);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            client.Close();
            client.Connect(HostIP, DOSCUS_PORT);
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBlockText.Text = TextBox1.Text;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button1.Focus();
            }
        }
    }
}