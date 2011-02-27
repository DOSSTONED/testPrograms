using System;
using System.Runtime.InteropServices;
namespace SBarHook
{
	public class Win32
	{
		public struct CopySlideBarDataStruct
		{
			public IntPtr dwData;
			public int cbData;
			public SlideBar.SlideBarData lpData;
		}
		private const int WM_USER = 1024;
		public const int WM_SLIDEBAR = 1025;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	}
}
