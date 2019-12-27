using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;


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
        if (!DwmIsCompositionEnabled())
        {
            /// must put some codes here to get the display under the XP or non-aero environments
            /// 
            
            //window.Opacity = 0.5;
            //window.Background.Opacity = 0.5;
            return false;
        }

		IntPtr hwnd = new WindowInteropHelper(window).Handle;
		if (hwnd == IntPtr.Zero)
		throw new InvalidOperationException("The Window must be shown before extending glass.");

		// Set the background to transparent from both the WPF and Win32 perspectives

		//SolidColorBrush background = new SolidColorBrush(Colors.Green);
		//background.Opacity = 0.5;

		window.Background = Brushes.Transparent;
		HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

		MARGINS margins = new MARGINS(margin);
		DwmExtendFrameIntoClientArea(hwnd, ref margins);
		return true;
	}
}

