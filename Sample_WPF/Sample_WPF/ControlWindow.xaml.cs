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
using System.Speech.Synthesis;

namespace Sample_WPF
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        public ControlWindow()
        {
            InitializeComponent();
        }

        public ControlWindow(MainWindow wnd)
        {
            InitializeComponent();
            parentWnd = wnd;
        }

        private MainWindow parentWnd = new MainWindow();
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            //MessageBox.Show("wedfwfwefwe");
            if (parentWnd != null)
                parentWnd.controller("Hello");
        }

        private double BorderMargin = 0;
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

        private void buttonAero_Click(object sender, RoutedEventArgs e)
        {
            if (buttonAero.Content.ToString().ToLower() == "enable aero")
            {
                AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
                buttonAero.Content = "Disable Aero";
                return;
            }
            if (buttonAero.Content.ToString().ToLower() == "disable aero")
            {
                Background = Brushes.White;
                buttonAero.Content = "Enable Aero";
                return;
            }
        }

        private void buttonModeDownload_Click(object sender, RoutedEventArgs e)
        {
            parentWnd.controller("Download Mode");
        }

        private void buttonModeGame_Click(object sender, RoutedEventArgs e)
        {
            parentWnd.controller("Game Mode");
        }

        private void buttonSpeak_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer sp = new SpeechSynthesizer();
            sp.SpeakAsync(textBox1.Text);
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            parentWnd.controller(((RadioButton)sender).Content.ToString());
        }
    }
}
