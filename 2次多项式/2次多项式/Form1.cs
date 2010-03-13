using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2次多项式
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            display(textBox1.Text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            display((int)numericUpDown1.Value);
        }

        private void display(string str)
        {
            display((int)Convert.ToDecimal(str));
        }

        private void display(int i)
        {
            try
            {
                int x = (int)numericUpDown1.Value;
                long y = 10 * x * x - 10;
                labelOut.Text = y.ToString();
            }
            catch
            {
                labelOut.Text = "wrong input!";
            }
        }
    }
}
