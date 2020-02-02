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
using Microsoft.Win32;

using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using System.Windows.Threading;
//using System.Windows.Media.Animation;

namespace 个人工具
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        const string WANTED = "CamVerify / Password\r\n" +
                                            "Program Certificated / Program Starter\r\n" +
                                            "Setup Menu\r\n" +
                                            "Web Browser (FileDownloader / Favorite Link / RSS)\r\n" +
                                            "每日课表(Import)\r\n" +
                                            "Notify(Abandoned)\r\n" +
                                            "Flashdisk(Finished) / Sys Protect" +
                                            "透明钟表(Import)\r\n";
        const ulong BorderMargin = 0;
        double _top = 0, _left = 0;
        


        public Window1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void WindowMain_ContentRendered(object sender, EventArgs e)
        //{
        //    AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        //}


        //private new void DragMove()
        //{


        //}

        private void buttonLock_Click(object sender, RoutedEventArgs e)
        {
            WindowLockVerify Leave = new WindowLockVerify();
            Leave.ShowDialog();

        }

        private void buttonAbt_Click(object sender, RoutedEventArgs e)
        {
            AboutBox abtbox = new AboutBox();
            abtbox.textBoxDescription.Text = WANTED;
            abtbox.ShowDialog();

        }

        private void DragThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            _left = Left;
            _top = Top;
            _left += (int)e.HorizontalChange;
            _top += (int)e.VerticalChange;

            if (_top < BorderMargin)
            {
                _top = 0;
            }
            if (_left < BorderMargin)
            {
                _left = 0;
            }
            if (_top > SystemParameters.WorkArea.Height - Height - BorderMargin)
            {
                _top = SystemParameters.WorkArea.Height - Height - 0;
            }
            if (_left > SystemParameters.WorkArea.Width - Width - BorderMargin)
            {
                _left = SystemParameters.WorkArea.Width - Width - 0;
            }
            this.Left = _left;
            this.Top = _top;
        }

        private void buttonProgStart_Click(object sender, RoutedEventArgs e)
        {
            WindowProgram winprog = new WindowProgram();
            
            winprog.Show();
            this.WindowState = WindowState.Minimized;
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("D:\\MyFile.bin",FileMode.Create, FileAccess.Read);
            //listprogs = (List<Progs>)formatter.Deserialize(stream);
            //stream.Close();
            //for (int i = 0; i < listprogs.Count; i++)
            //{
            //    listView1.Items.Add(listprogs[i]);
            //}

            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));


            Timer TimerForLable = new Timer(1000);
            TimerForLable.Elapsed += new ElapsedEventHandler(TimerForLable_Elapsed);
            labelTime.Content = DateTime.Now.ToLongTimeString();
            TimerForLable.Start();
            
        }

        void TimerForLable_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                labelTime.Content = DateTime.Now.ToLongTimeString();
            }));
        }
       
        private void WindowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("D:\\MyFile.bin",FileMode.Create, FileAccess.Write);
            //formatter.Serialize(stream, ls);
            //stream.Close();
        }



        //private void WindowMain_MouseMove(object sender, MouseEventArgs e)
        //{

        //    if (Top < BorderMargin)
        //    {
        //        Top = BorderMargin;
        //    }
        //    if (Left < BorderMargin)
        //    {
        //        Left = BorderMargin;
        //    }
        //    if (Top > SystemParameters.WorkArea.Height - Height - BorderMargin)
        //    {
        //        Top = SystemParameters.WorkArea.Height - Height - BorderMargin;
        //    }
        //    if (Left > SystemParameters.WorkArea.Width - Width - BorderMargin)
        //    {
        //        Left = SystemParameters.WorkArea.Width - Width - BorderMargin;
        //    }
        //}



        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        private void checkBoxChangeparent_Checked(object sender, RoutedEventArgs e)
        {
            //if (checkBoxChangeparent.IsChecked == true)
            //{
            //    IntPtr desktop = FindWindow(null, "Program Manager");
            //    IntPtr hwndOldParent = SetParent(new WindowInteropHelper(winprog).Handle, desktop);
            //}
            //else
            //{
            //    IntPtr hwndOldParent = SetParent(new WindowInteropHelper(winprog).Handle, new WindowInteropHelper(this).Handle);
            //}
        
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ResizeMode = ResizeMode.NoResize;
        }


    }
}
