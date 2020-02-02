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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace 个人工具
{
    /// <summary>
    /// WindowLockVerify.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLockVerify : Window
    {
        /// <summary>
        /// Constants
        /// </summary>
        /// 
        /*
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int HTCAPTION = 0x02;
        const int WM_HOTKEY = 0x0312;//按快捷键
        */
        const double TotalLeftSeconds = 1200.0;

        /// <summary>
        /// Variables
        /// </summary>
        /// 
        //DispatcherTimer timer = new DispatcherTimer();
        //double leftseconds;
        
        IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        int WTS_CURRENT_SESSION = -1;

        DoubleAnimation progressAnimation = new DoubleAnimation(0,100, new Duration(TimeSpan.FromSeconds(TotalLeftSeconds)),FillBehavior.HoldEnd);

        /// <summary>
        /// Imports
        /// </summary>
        /// 
        [DllImport("Wtsapi32.dll", SetLastError = true)]
        extern static bool WTSDisconnectSession(IntPtr hServer, int sessionId, [MarshalAs(UnmanagedType.Bool)] bool bWait);
        
        /// <summary>
        /// Functions
        /// </summary>
        /// 
        public WindowLockVerify()
        {
            InitializeComponent();
            //leftseconds = TotalLeftSeconds;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Start();
            progressAnimation.Completed += new EventHandler(progressAnimation_Completed);
            //progressAnimation.CurrentTimeInvalidated += new EventHandler(progressAnimation_CurrentTimeInvalidated);
            progressBar1.BeginAnimation(ProgressBar.ValueProperty, progressAnimation);
        }

        void progressAnimation_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            buttonOK.Content = progressAnimation.GetCurrentValue(0,100,progressAnimation.CreateClock());
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox1.Password == "123456")
            {
                this.Close();
            }
            
        }
        
        /*
        void timer_Tick(object sender, EventArgs e)
        {
            if (leftseconds > 0)
            {
                buttonOK.Content = leftseconds--;
                //progressBar1.Value = (1 - leftseconds / TotalLeftSeconds) * 100;
                
            }
            else
            {
                timer.Stop();
                if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
                {
                    MessageBox.Show("WTSDisconnectSession Failed: " + Marshal.GetLastWin32Error());
                }
            }
        }
         
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.WndProc));
            }
        }

        
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCLBUTTONDOWN && wParam.ToInt32() == HTCAPTION)
                handled = true;

            switch (msg)
            {
                case WM_HOTKEY:
                    ProcessHotkey();//调用主处理程序
                    handled = true;
                    break;
            }

            return IntPtr.Zero;

        }

        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.Key.ToString());
            if (e.Key == System.Windows.Input.Key.RWin || e.Key == System.Windows.Input.Key.LWin)
            {
                Regex r = new Regex(@"^[A-Za-z]");
                if (!r.IsMatch(e.Key.ToString()))
                {
                    MessageBox.Show("");
                }
                e.Handled = true;
            }
        }
            private void Form1_Load(object sender, EventArgs e)
            {
                RegisterHotKey(Handle, 100, 1, Keys.F4);
            }
            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                UnregisterHotKey(Handle, 100);
            }
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
        IntPtr hWnd,
        int id,
        int fsModifiers,//alt = 1, none = 0, win = 8;
        Keys virtualKey
        );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
        IntPtr hWnd,
        int id
        );
        

        //void ProcessHotkey(){            //这里是处理快捷键的地方,为空就好了        }
*/



        private void progressAnimation_Completed(object sender, EventArgs e)
        {
                if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
                {
                    MessageBox.Show("WTSDisconnectSession Failed: " + Marshal.GetLastWin32Error());
                    LostFocus += new RoutedEventHandler(WindowLockVerify_LostFocus);
                }
        }

        void WindowLockVerify_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
            {
                MessageBox.Show("WTSDisconnectSession Failed: " + Marshal.GetLastWin32Error());
            }
        }


    }
}
