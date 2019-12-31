using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoogleMusicBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }

        private void NodesAdd(TreeNode parentNode, HtmlElementCollection hec)
        {
            for (int i = 0; i < hec.Count; i++)
            {
                parentNode.Nodes.Add(hec[i].OuterHtml);
                //if (hec[i].)
                //{
                    
                //}
                if (hec[i].CanHaveChildren)
                {
                    NodesAdd(parentNode.LastNode, hec[i].Children);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            HtmlElementCollection hecl = webBrowser1.Document.All;
            for (int i = 0; i < hecl.Count; i++)
            {
                treeView1.Nodes.Add(hecl[i].OuterHtml);
                NodesAdd(treeView1.Nodes[i], hecl[i].Children);

            }

        }
    }
}
