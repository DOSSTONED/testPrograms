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

namespace ProcessCreated
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

        ProcessWatcher pWatcher = null;
        //List<Win32_Process> WPs = new List<Win32_Process>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pWatcher = new ProcessWatcher();
            //pWatcher.EventArrived += new System.Management.EventArrivedEventHandler(pWatcher_EventArrived);
            

            pWatcher.ProcessCreated += new ProcessEventHandler(pWatcher_ProcessCreated);
            pWatcher.ProcessDeleted += new ProcessEventHandler(pWatcher_ProcessDeleted);
            pWatcher.Start();
        }

        void pWatcher_ProcessDeleted(Win32_Process proc)
        {
            Dispatcher.Invoke(
                new Action(
                    delegate()
                    {
                        listBox1.Items.Add(Converter(proc, "ProcessDeleted"));
                    }
                    ), null);
        }

        string Converter(Win32_Process wp, string behavior)
        {
            string ret=string.Empty;
            ret += DateTime.Now.ToString() + "\t" +wp.Name;
            ret += "\t" + wp.ProcessId;
            ret += "\t" + behavior;
            ret += "\t" + wp.ExecutablePath;
            return ret;
        }

        void pWatcher_ProcessCreated(Win32_Process proc)
        {
            Dispatcher.Invoke(
                new Action(
                    delegate()
                    {
                        listBox1.Items.Add(Converter(proc, "ProcessCreated"));
                    }
                    ), null);
            //throw new NotImplementedException();
        }

        void pWatcher_EventArrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            listBox1.Dispatcher.Invoke(
                new Action(
                    delegate() 
                    {
                        listBox1.Items.Add(e.NewEvent["TargetInstance"] as ManagementBaseObject);
                    }
                    ), null);
            //throw new NotImplementedException();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pWatcher != null)
                pWatcher.Stop();
        }
    }
}
