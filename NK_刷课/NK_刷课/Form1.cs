using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHDocVw;
using System.IO;

namespace NK_刷课
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            textBoxURL.Text = webBrowser1.Url.ToString();
            //textBox2.Text=webBrowser1.

            
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //if (webBrowser1.Url.ToString() == "http://192.168.4.15/") //URL==输入的网页地址
            //{
                HtmlElementCollection hecl = webBrowser1.Document.All;
                string s = string.Empty;
                int k = 0;
                for (int i = 0; i < hecl.Count; i++)
                {
                    //if (hecl[i].Name == "submittype")
                    //    MessageBox.Show(hecl[i].OuterText);
                    //s += hecl[i].Name+"\n";
                    if (hecl[i].Name == "usercode_text")
                    {
                        hecl[i].InnerText = textBoxUsername.Text;
                    }
                    if (hecl[i].Name == "userpwd_text")
                    {
                        hecl[i].InnerText = textBoxUserpassword.Text;
                    }
                    if (hecl[i].Name == "submittype")
                    {


                        if (hecl[i].OuterHtml.Contains("确 认"))
                        {
                            k = i;
                        }
                    }
                }
                hecl[k].InvokeMember("click");

                Logined = true;
            //}

        }

        //private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        //{
        //    //捕获页面新连接  实现不调用IE只是内容显示在本窗口中

        //    //WebBrowser.StatusText 属性可以使用此属性在状态栏中显示 WebBrowser 控件的状态。

        //    //状态文本是包含信息（如鼠标指针悬停于超链接上时该超链接的 URL 以及当前正在加载的文档的 URL）的消息

        //    webBrowser1.Navigate(webBrowser1.StatusText.ToString());
        //    e.Cancel = true;
        //    //textBoxUsername.Text = webBrowser1.StatusText.ToString();
        //    listBoxLog.Items.Add(webBrowser1.StatusText);
        //    //让地址栏地址随着页面中点击链接而改变
        //}

        string postDataText = string.Empty;
        bool Logined = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)webBrowser1.ActiveXInstance;
            wb.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(WebBrowser_BeforeNavigate2);
            webBrowser1.Url = new Uri("http://192.168.4.15");
            textBoxURL.Text = "http://192.168.4.15";
        }

        private void WebBrowser_BeforeNavigate2(
            object pDisp,
            ref object URL,
            ref object Flags,
            ref object TargetFrameName,
            ref object PostData,
            ref object Headers,
            ref bool Cancel
            )
        {
           // postDataText = string.Empty;
           // postDataText += URL.ToString() + "\n";
            //postDataText += System.Text.Encoding.ASCII.GetString(PostData as byte[]);
            //postDataText += PostData.ToString();
            //textBox2.Text = postDataText;

            /*
            listBoxLog.Items.Add("pDisp:\t" + pDisp.ToString());
            listBoxLog.Items.Add("URL:\t" + URL.ToString());
            listBoxLog.Items.Add("Flags:\t" + Flags.ToString());
            listBoxLog.Items.Add("TargetFrameName:\t" + TargetFrameName.ToString());
            listBoxLog.Items.Add("PostData:\t" + PostData.ToString());
            listBoxLog.Items.Add("Headers:\t" + Headers.ToString());
            listBoxLog.Items.Add("Cancel:\t" + Cancel.ToString());
             */
        }

        private void buttonShua_Click(object sender, EventArgs e)
        {

            if (webBrowser1.Url.ToString() == "http://192.168.4.15/xsxk/swichAction.do" || webBrowser1.Url.ToString() == "http://192.168.4.15/xsxk/selectMianInitAction.do")
            {
                HtmlElementCollection hecl = webBrowser1.Document.All;
                //string s = string.Empty;
                int xuankeIndex = 0;
                for (int i = 0; i < hecl.Count; i++)
                {
                    //if (hecl[i].Name == "submittype")
                    //   MessageBox.Show(hecl[i].OuterText);
                    //s += hecl[i].Name + "\n";

                    if (hecl[i].Name == "xkxh1")
                    {
                        hecl[i].InnerText = xkxh1.Text;
                    }
                    if (hecl[i].Name == "xkxh2")
                    {
                        hecl[i].InnerText = xkxh2.Text;
                    }
                    if (hecl[i].Name == "xkxh3")
                    {
                        hecl[i].InnerText = xkxh3.Text;
                    }
                    if (hecl[i].Name == "xkxh4")
                    {
                        hecl[i].InnerText = xkxh4.Text;
                    }
                    if (hecl[i].Name == "xuanke" && hecl[i].OuterHtml == "<INPUT style=\"WIDTH: 50px\" onclick=\"operation.value='xuanke';submit();\" value=\"选 课\" type=button name=xuanke>")
                    {
                        xuankeIndex = i;
                    }

                }
                if (xuankeIndex > 0)
                {
                    //hecl[xuankeIndex].OuterText = "aaa";
                    hecl[xuankeIndex].InvokeMember("Click");

                }
                listBoxLog.Items.Add(DateTime.Now.ToLongTimeString() + "\t Refeshed");
            }
            else
            {
                webBrowser1.Navigate("http://192.168.4.15/xsxk/selectMianInitAction.do");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonShua_Click(sender, e);
            //timer1.Enabled = false;
            //MessageBox.Show(s);
        }

        private void buttonTui_Click(object sender, EventArgs e)
        {
            HtmlElementCollection hecl = webBrowser1.Document.All;
            //string s = string.Empty;
            int tuikeIndex = 0;
            for (int i = 0; i < hecl.Count; i++)
            {
                //if (hecl[i].Name == "submittype")
                 //   MessageBox.Show(hecl[i].OuterText);
                //s += hecl[i].Name + "\n";

                if (hecl[i].Name == "xkxh1")
                {
                    hecl[i].InnerText = xkxh1.Text;
                }
                if (hecl[i].Name == "xkxh2")
                {
                    hecl[i].InnerText = xkxh2.Text;
                }
                if (hecl[i].Name == "xkxh3")
                {
                    hecl[i].InnerText = xkxh3.Text;
                }
                if (hecl[i].Name == "xkxh4")
                {
                    hecl[i].InnerText = xkxh4.Text;
                }
                if (hecl[i].Name == "tuike")
                {
                    tuikeIndex = i;
                }
                
            }
            //if (tuikeIndex > 0)
                //hecl[tuikeIndex].InvokeMember("click");
        }

        private void buttonNavigate_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBoxURL.Text);
        }



        private void buttonTimer1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                buttonTimer1.Text = "刷！";
                timer1.Enabled = false;
            }
            else
            {
                buttonTimer1.Text = "正在刷...";
                timer1.Enabled = true;
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
           
                webBrowser1.Navigate("http://192.168.4.15/xsxk/selectMianInitAction.do");
                Logined = false;
           
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
           
            e.Cancel = true;
        }

        private void buttonxksc_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Directory.GetCurrentDirectory() + "\\xksc.htm");
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            listBoxLog.Items.Add(DateTime.Now.ToLongTimeString() + "Navigated");
            if (timer1.Enabled)
            {
                timer1_Tick(sender, e);
            }
        }

        #region 选课手册相关

        #endregion


        //private void buttonGo_Click(object sender, EventArgs e)
        //{
        //    webBrowser1.Url = new Uri(textBox1.Text);
        //}
    }
}
