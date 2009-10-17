using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace 个人工具
{
    class AeroGlass
    {
        struct MARGINS
        {
            public MARGINS(Thickness t)
            {
                Left = (int)t.Left;
                Right = (int)t.Right;
                Top = (int)t.Top;
                Bottom = (int)t.Bottom;
            }
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;

        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();


        public static bool ExtendGlassFrame(Window window, Thickness margin)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
                throw new InvalidOperationException("窗口由于未被绘制而无法使用玻璃特效.");

            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                if (DwmIsCompositionEnabled())
                {
                    window.Background = Brushes.Transparent;
                    HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

                    MARGINS margins = new MARGINS(margin);
                    DwmExtendFrameIntoClientArea(hwnd, ref margins);
                }
                else
                {
                    MessageBox.Show("玻璃特效未打开！");
                }
            }
            else
            {
                MessageBox.Show("系统不是 Windows Vista 或者 Windows 7.");
            }


            //本文来自CSDN博客，转载请标明出处：http://blog.csdn.net/SnowRen3074/archive/2008/10/23/3129306.aspx
            // Set the background to transparent from both the WPF and Win32 perspectives

            //SolidColorBrush background = new SolidColorBrush(Colors.Green);
            //background.Opacity = 0.5;


            return true;
        }
    }
}
