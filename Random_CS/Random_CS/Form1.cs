using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Random_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            try
            {
                textBox3.Text = ran.Next(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).ToString();
            }
            catch
            {
                textBox3.Text = ran.Next().ToString();
            }
        }
    }
}
