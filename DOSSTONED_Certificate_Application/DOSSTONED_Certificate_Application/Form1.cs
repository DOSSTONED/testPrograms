using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DOSSTONED_Certificate_Application
{
    public partial class Form1 : Form
    {
        private const int WM_DEVICECHANGE = 0x219;
        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_CONFIGCHANGECANCELED = 0x19;
        private const int DBT_CONFIGCHANGED = 0x18;
        private const int DBT_CUSTOMEVENT = 0x8006;
        private const int DBT_DEVICEQUERYREMOVE = 0x8001;
        private const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        private const int DBT_DEVICEREMOVEPENDING = 0x8003;
        private const int DBT_DEVICETYPESPECIFIC = 0x8005;
        private const int DBT_DEVNODES_CHANGED = 0x7;
        private const int DBT_QUERYCHANGECONFIG = 0x17;
        private const int DBT_USERDEFINED = 0xFFFF;

        private const int WM_CREATE = 0x0001;
        private int TimeLeft = 3;
        
        public Form1()
        {
            InitializeComponent();
        }
        
        protected override void WndProc(ref   Message m)
        {
            switch (m.Msg)
            {
                case WM_CREATE:
                    MessageBox.Show(m.ToString());
                    Show();
                    TimeLeft = 5;
                    TimerDisappear.Enabled = true;
                    break;
                case DBT_DEVICEARRIVAL:
                    MessageBox.Show("FlashDisk Insert");
                    break;

            }

            base.WndProc(ref   m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Width, System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            TimeLeft = 5;
            TimerDisappear.Enabled = true;
        }

        private void TimerDisappear_Tick(object sender, EventArgs e)
        {
            if (TimeLeft < 1)
                Hide();
            else
                TimeLeft--;
        }
    }
}

//http://hi.baidu.com/zzzkkk666/blog/item/580baf1bfbacb4188718bf7c.html Here are the showing forms methods
/*
 * 
 * http://blog.csdn.net/swimmer2000/archive/2008/12/01/3420661.aspx
 * This discribed what Win32 Application will do after it begins to start
 * 
 * 
*/