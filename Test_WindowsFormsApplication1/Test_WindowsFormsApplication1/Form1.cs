using System;
using System.Windows.Forms;

namespace Test_WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int a = r.Next(1, 100);
            Button b = sender as Button;
            if (b != null)
            {
                MessageBox.Show(b.Text, a.ToString());
            }
        }
    }
}
