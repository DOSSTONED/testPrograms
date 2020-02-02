using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Platform_Menu
{
    public partial class Main : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        IntPtr slidebarHandle = IntPtr.Zero;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                if (b.Text == "Show")
                    ShowWindow(slidebarHandle, 1);
                if (b.Text == "Hide")
                    ShowWindow(slidebarHandle, 0);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.MainWindowTitle.Contains("Game Manager"))
                    slidebarHandle = p.MainWindowHandle;
            }

            //slidebarHandle = FindWindow(null, "SlideBar config");
            textBox1.Text = slidebarHandle.ToString();
            //ShowWindow(p.Handle, 0);

        }
    }
}
