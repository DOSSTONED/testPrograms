using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace parseText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] strs = textBox1.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strs)
            {
                listBox1.Items.Add(str);
            }
        }

        private void buttonWriteback_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                str += (listBox1.Items[i] as string) + "\r\n";
            }
            textBox1.Text = str;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cur = listBox1.SelectedItem as string;
            if (cur != null)
                webBrowser1.Navigate(cur.Split('\t')[1]);
        }

        private void buttonyYAO_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            //
            int se = listBox1.SelectedIndex;
            if (se < listBox1.Items.Count - 1)
                listBox1.SelectedIndex++;
                //listBoxURL.SelectedIndex = se + 1;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            int se = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            if (se < listBox1.Items.Count)
                listBox1.SelectedIndex = se;
        }
    }
}
