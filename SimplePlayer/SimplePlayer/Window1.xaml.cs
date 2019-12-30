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
using System.IO;
using System.Windows.Threading;

namespace SimplePlayer
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private string[] LRCLines = null;
        private int startLine = 0;
        public Window1()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Source = new Uri(@"E:\Music\一个人的冬天.mp3");
            LRCLines = File.ReadAllLines(@"E:\Music\一个人的冬天.lrc");
            for (int i=0;i< LRCLines.Length;i++)
            {
                if (LRCLines[i].Contains("[00:00.00]"))
                    startLine = i;
            }
            mediaElement1.Play();
            mediaElement1.Stop();

        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.NaturalDuration.HasTimeSpan)
                sliderPlay.Maximum = mediaElement1.NaturalDuration.TimeSpan.TotalSeconds;
            mediaElement1.Play();

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(UpdateLRCDisplay);

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            dispatcherTimer.Start();


        }


        private void UpdateLRCDisplay(object sender, EventArgs e)
        {
            foreach (string str in LRCLines)
            {
                if (str.Contains(mediaElement1.Position.ToString().Substring(0,8)))
                {
                    labelDisplayLRC.Content = str.Substring(9);
                }
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }







    }


    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public class timeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((TimeSpan)value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }


}
