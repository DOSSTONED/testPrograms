using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTML_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebBrowser webbr1 = new WebBrowser();

        private void Form1_Load(object sender, EventArgs e)
        {
            webbr1.Navigated+=new WebBrowserNavigatedEventHandler(webbr1_Navigated);
            webbr1.Navigate(@"E:\WEB\sites\site1\GRE.htm");
            
        }

        private void webbr1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            HtmlDocument htmlDoc = webbr1.Document;
            HtmlElement body = htmlDoc.Body;
            HtmlElementCollection words = body.Children;
            
        }
    }
}
