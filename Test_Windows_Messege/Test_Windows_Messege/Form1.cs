using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Windows_Messege
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (buttonExit.Text == "Ready To Exit")
            {
                buttonExit.Text = "Exit";
                return;
            }
            
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            buttonFF.Enabled = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            buttonFF.Enabled = true;
        }

        private void buttonExit_MouseEnter(object sender, EventArgs e)
        {
            if (buttonExit.Text == "Exit")
            {
                Application.Exit();
                //buttonExit.Text = "Ready To Exit";
            }
            timer1.Enabled = true;
        }

        private void buttonExit_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
