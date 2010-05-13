using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class ClassWin6Privileges
{
    static internal bool IsAdmin()
    {
        WindowsIdentity id = WindowsIdentity.GetCurrent();
        WindowsPrincipal p = new WindowsPrincipal(id);
        return p.IsInRole(WindowsBuiltInRole.Administrator);
    }

    public static class VistaSecurity
    {
        [DllImport("user32")]
        public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BCM_FIRST = 0x1600;
        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        /**/
        /// <summary>
        /// Add a shield icon to a button
        /// </summary>
        /// <param name="b">The button</param>
        static internal void AddShieldToButton(Button b)
        {
            b.FlatStyle = FlatStyle.System;
            SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }
    }

    internal static void RestartElevated()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.UseShellExecute = true;
        startInfo.WorkingDirectory = Environment.CurrentDirectory;
        startInfo.FileName = Application.ExecutablePath;
        startInfo.Verb = "runas";
        try
        {
            Process p = Process.Start(startInfo);
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            return; //If cancelled, do nothing
        }
        Application.Exit();
    }

}