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
        if (Environment.OSVersion.Version.Major < 6)
        {
            MessageBox.Show("You are running under Windows Vista.");
            return false;
        }
        if (!DwmIsCompositionEnabled())
        {
            MessageBox.Show("DWM Composition is not enabled.");
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

