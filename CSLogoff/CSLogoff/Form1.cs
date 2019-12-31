using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace CSLogoff
{
    public partial class Confirmation : Form
    {

        public Confirmation()
        {
            InitializeComponent();
        }

        private void Confirmation_Deactivate(object sender, EventArgs e)
        {
            //MessageBox.Show("Deactived");
            //SendKeys.Send();

        }

        internal void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            //MessageBox.Show("Switch");
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {

            IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
            int WTS_CURRENT_SESSION = -1;

            if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
            {
                MessageBox.Show("WTSDisconnectSession Failed: " + Marshal.GetLastWin32Error());
            }
        }

        [DllImport("Wtsapi32.dll", SetLastError = true)]
        extern static bool WTSDisconnectSession(IntPtr hServer, int sessionId, [MarshalAs(UnmanagedType.Bool)] bool bWait);



    }
}
