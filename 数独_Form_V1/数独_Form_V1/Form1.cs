using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 数独_Form_V1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private Button[] buttons = new Button[81];
        private void Form1_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 81; i++)
            {
                
                buttons[i] = new Button();
                buttons[i].Visible = true;
                buttons[i].Size = new Size(32, 32);
                buttons[i].Top = (i - i % 9) / 9 * 32 + 40;
                buttons[i].Left = (i % 9) * 32 + 40;
                buttons[i].KeyPress += new KeyPressEventHandler(Form1_KeyPress);
                Controls.Add(buttons[i]);
            }
        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button newbutton = (Button)sender;//
            if (char.IsDigit(e.KeyChar))
                newbutton.Text = e.KeyChar.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 81; i++)
            {
                int[] shudu = new int[81];
                shudu[81] = Convert.ToInt32(buttons[i].Text);
                SudokoAlg test1 = new SudokoAlg();
                test1.Enum(ref shudu);
            }
        }
    }
}
