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
using System.Management;

namespace Brightness_Interaction_WPF
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
        byte[] BrightnessLevels = null;
        public ManagementEventWatcher BrightnessWatcher = null;
        /// <summary>
        /// On window loaded
        /// </summary>
        /// <param name="sender">the event provider</param>
        /// <param name="e">routed event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightness"))/// WMI is case-insensitive, but we here recommended that to use the proper upper case to improve readibility
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    BrightnessLevels = obj["Level"] as byte[];
                    slider1.Value = (byte)obj["CurrentBrightness"];
                    tb1.Text = BrightnessLevels.Count().ToString();
                    foreach (byte b in BrightnessLevels)
                    {
                        slider1.Ticks.Add(b);
                    }
                    break;
                }
            }
            BrightnessWatcher = new ManagementEventWatcher("root\\WMI", "SELECT * FROM WmiMonitorBrightnessEvent");
            if (BrightnessWatcher != null)
            {
                BrightnessWatcher.EventArrived += new EventArrivedEventHandler(BrightnessWatcher_EventArrived);
                BrightnessWatcher.Start();
            }
        }

        void BrightnessWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //e.ToString();
            slider1.Dispatcher.Invoke(new Action(
                delegate()
                {
                    slider1.Value = (byte)e.NewEvent["Brightness"];
                }), null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (BrightnessWatcher != null)
            {
                BrightnessWatcher.Stop();
            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (BrightnessWatcher != null)
                BrightnessWatcher.Stop();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightnessMethods"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    obj.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, e.NewValue }); //note the reversed order - won't work otherwise! error: this was too large or too small for a byte
                    break; //only work on the first object
                }
            }
            if (BrightnessWatcher != null)
                BrightnessWatcher.Start();
        }
    }
}
