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
using SBarHook;

namespace GameManager_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SlideBar SB = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SB = new SlideBar(SlideBarHandler, true);
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            if (SB != null)
                SB.StopAllEventWatcher(false);
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private int SBInputPos = 0;
        private void SlideBarHandler(SBarHook.SlideBar.SlideBarData sbData)
        {
            if (sbData.bEvent == SlideBar.Event.ServiceKey)
            {
                Dispatcher.Invoke(new Action(delegate
                    {
                        this.Show();
                        this.Activate();
                        this.Focus();
                    }), null);
                return;
            }
            if (sbData.bAction == SlideBar.SlideBarAction.On)
            {
                SBInputPos = sbData.bPosition;
            }

            Dispatcher.Invoke(new Action(delegate
            {
                Random ran = new Random();
                double estimated_value = sbData.bPosition + ran.NextDouble() - SBInputPos;
                estimated_value = Math.Abs(estimated_value) > 255 ? 255 : estimated_value;
                if (estimated_value > 0)
                {
                    progressBarLeft.Value = 0;
                    progressBarRight.Value = estimated_value;
                }
                else
                {
                    progressBarRight.Value = 0;
                    progressBarLeft.Value = -estimated_value;
                }
                //HSBColor sdb = new HSBColor(sbData.bPosition, 255f, 255f);
                //progressBar1.Foreground = new SolidColorBrush(HSBColor.FromHSB(sdb));
                //Color.FromRgb(sbData.bPosition, sbData.bPosition, sbData.bPosition));
            }), null
                );
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

    }
}
