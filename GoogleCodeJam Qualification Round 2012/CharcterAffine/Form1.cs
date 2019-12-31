using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CharcterAffine
{
    public partial class Form1 : Form
    {
        int EffA = 0, EffB = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            EffA = trackBar1.Value;
            EffB = (textBox2.Text[0] - 65 - (textBox1.Text[0] - 65) * EffA) % 26;
            label1.Text = string.Format("EffA = {0}, EffB = {1}", EffA, EffB);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = string.Empty;
            for (int i = 0; i < textBox3.Text.Length; i++)
            {
                textBox4.Text += (char)(((textBox3.Text[i] - 65) * EffA + EffB) % 26 + 65);
            }
        }
    }
}
