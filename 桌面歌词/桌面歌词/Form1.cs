using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 桌面歌词
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.FormBorderStyle == FormBorderStyle.None)
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.Fixed3D;
            else
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.BackColor == Color.Fuchsia)
                Form1.ActiveForm.BackColor = ColorTranslator.FromHtml("Control");
            else
                Form1.ActiveForm.BackColor = Color.Fuchsia;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.TopMost == false)
                Form1.ActiveForm.TopMost = true;
            else
                Form1.ActiveForm.TopMost = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - Width, System.Windows.Forms.SystemInformation.WorkingArea.Height - Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }


    }
}
