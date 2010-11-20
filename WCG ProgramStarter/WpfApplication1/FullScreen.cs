using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GameManager_WPF
{
    static class FullScreen
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        //public static bool IsForegroundFullScreen()
        //{
        //    return IsForegroundFullScreen(null);
        //}

        //static bool IsForegroundFullScreen(Screen screen)
        //{
        //    if (screen == null)
        //    {
        //        screen = Screen.PrimaryScreen;
        //    }
        //    RECT rect = new RECT();
        //    GetWindowRect(new HandleRef(null, GetForegroundWindow()), ref rect);
        //    return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.Bounds);
        //}

    }
}
