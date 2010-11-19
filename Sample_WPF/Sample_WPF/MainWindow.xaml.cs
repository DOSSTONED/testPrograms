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
using System.Speech.Synthesis;
using System.Timers;

namespace Sample_WPF
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


        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ControlWindow ctrlWnd = new ControlWindow(this);
            ctrlWnd.Show();
        }


        double BorderMargin = 5;
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double tmpLeft = Left, tmpTop = Top;
            tmpLeft += e.HorizontalChange;
            tmpTop += e.VerticalChange;

            /// must put codes here to deal with some effects on moving the window

            if (tmpLeft + Width + BorderMargin > SystemParameters.WorkArea.Right)
                tmpLeft = SystemParameters.WorkArea.Right - Width - BorderMargin;
            if (tmpLeft - BorderMargin < SystemParameters.WorkArea.Left)
                tmpLeft = SystemParameters.WorkArea.Left + BorderMargin;
            if (tmpTop - BorderMargin < SystemParameters.WorkArea.Top)
                tmpTop = SystemParameters.WorkArea.Top + BorderMargin;
            if (tmpTop + Height + BorderMargin > SystemParameters.WorkArea.Bottom)
                tmpTop = SystemParameters.WorkArea.Bottom - Height - BorderMargin;



            Left = tmpLeft;
            Top = tmpTop;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            labelSize.Content = "Size: " + this.Width + "x" + this.Height;
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }


        public void controller(string str)
        {
            SpeechSynthesizer spch = new SpeechSynthesizer();
            if (str == "Hello")
            {
                //MessageBox.Show("Hello");
            }
            if (str == "下载模式")
            {
                spch.SpeakAsync("Change to Download settings.");
                brush1.Color = Brushes.Azure.Color;
                brush2.Color = Brushes.DarkGray.Color;
                //MessageBox.Show("Enter Download mode.");
                labelStatus.Content = "下载模式";
            }
            if (str == "游戏模式")
            {
                
                spch.SpeakAsync("Change to game settings.");
                brush1.Color = Brushes.MediumVioletRed.Color;
                brush2.Color = Brushes.Red.Color;
                labelStatus.Content = "游戏模式";
            }
            if (str == "正常模式")
            {

                spch.SpeakAsync("Change to normal settings.");
                brush1.Color = Color.FromArgb((byte)0xFF,(byte)0x10,(byte)0xB3,(byte)0xF6);//Brushes.MediumVioletRed.Color;
                brush2.Color = Brushes.Black.Color;
                labelStatus.Content = "正常模式";
            }
            if (str == "文档模式")
            {

                spch.SpeakAsync("Change to document settings.");
                brush2.Color = Brushes.Azure.Color;
                brush1.Color = Brushes.Blue.Color;
                labelStatus.Content = "文档模式";
            }


            if (str == "Normal Mode")
            {

                spch.SpeakAsync("Change to game settings.");
                brush1.Color = Brushes.MediumVioletRed.Color;
                brush2.Color = Brushes.Red.Color;
                labelStatus.Content = "正常模式";
            }

            this.Show();
        }

        private void buttonIKnow_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer spch = new SpeechSynthesizer();
            spch.Speak("Server shut down.");
            Application.Current.Shutdown();
        }

        private void windowNotify_Activated(object sender, EventArgs e)
        {
            
        }


    }
}
