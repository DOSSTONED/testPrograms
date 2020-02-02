using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 刷帖子
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isRefreshEnabled = false;

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (isRefreshEnabled)
                webBrowser1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isRefreshEnabled = !isRefreshEnabled;
            if (isRefreshEnabled)
            {
                webBrowser1.Navigate(textBox1.Text);
            }
            
        }
    }
}
