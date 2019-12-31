using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DWMAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("dwmapi.dll", EntryPoint = "#105")]
        static extern int DwmpSetColorizationParameters();

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            DwmpSetColorizationParameters();
        }


    }
}
