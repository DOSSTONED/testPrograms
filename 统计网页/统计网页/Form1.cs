using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;

namespace 统计网页
{
    public partial class Form1 : Form
    {

        public class searchEngines
        {
            public static string query(int kind, string query, int page)
            {
                switch (kind)
                {
                    case 1:
                        //byte[] gb2312bytes = Encoding.GetEncoding("GB2312").GetBytes(string.Format("http://cn.bing.com/search?q={0}&first={1}", query, page == 1 ? 0 : (page * 10 - 11)));
                        //byte[] utf8s = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.UTF8, gb2312bytes);
                        //string msg = Encoding.UTF8.GetString(utf8s);
                        return string.Format("http://cn.bing.com/search?q={0}&first={1}", HttpUtility.UrlEncode(query, Encoding.UTF8), page == 1 ? 0 : (page * 10 - 11));
                    case 2:
                        return string.Format("www.baidu.com/s?wd={0}&pn={1}", query, page * 10 - 10);
                    case 3:
                        return string.Format("www.so.com/s?q={0}&pn={1}", HttpUtility.UrlEncode(query, Encoding.UTF8), page);
                    case 4:
                        return string.Format("www.google.com/search?q={0}&start={1}", query, page * 10 - 10);
                    case 5:
                        return string.Format("www.soso.com/q?w={0}&pg={1}", query, page * 10 - 10);
                    case 6:
                        return string.Format("www.sogou.com/web?query={0}&page={1}", query, page);
                }
                return string.Empty;
            }
        }

        public Form1()
        {
            InitializeComponent();
            listBoxTitle.SelectedIndex = 0;
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxTitle.SelectedItem == null)
                listBoxTitle.SelectedIndex = 0;
            else
            {
                if (listBoxTitle.SelectedIndex < 1)
                    listBoxTitle.SelectedIndex = 0;
                else
                    listBoxTitle.SelectedIndex = listBoxTitle.SelectedIndex - 1;
            }
            textBoxTitle.Text = listBoxTitle.SelectedItem as string;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!isFinished)
            {
                //string text = textBoxURLs.Text;
                foreach (HtmlElement htmlElement in webBrowser1.Document.All)
                {
                    string tabItem = htmlElement.InnerText;
                    string linkItem = htmlElement.GetAttribute("HREF").ToString();
                    if (linkItem != string.Empty && tabItem != null)
                    {
                        if (htmlElement.InnerHtml.ToLower().Contains("em") || htmlElement.InnerHtml.ToLower().Contains("strong"))
                        {    //text += tabItem + "\t" + linkItem + "\r\n";
                            //if (linkItem.Contains(@"baidu.com/link"))
                            //{
                            //    //WebClient wc = new WebClient();
                            //    //wc.DownloadData(linkItem);
                            //    WebRequest request = WebRequest.Create(linkItem);
                            //    WebResponse response;
                            //    try
                            //    {
                            //        response = request.GetResponse();
                            //        linkItem = response.ResponseUri.ToString();
                            //    }

                            //    catch (WebException ex)
                            //    {
                            //        //Thread.SpinWait(1000);
                            //        //response = request.GetResponse();
                            //    }
                            //    //Stream data = response.GetResponseStream();
                            //    //string html = String.Empty;
                            //    //using (StreamReader sr = new StreamReader(data))
                            //    //{
                            //    //    html = sr.ReadToEnd();
                            //    //}

                            //}
                            if (linkItem.Contains("cn.bing.com/")) continue;
                            listBoxURL.Items.Add(tabItem + "\t" + linkItem);
                        }
                    }
                }
            }
            
            //textBoxURLs.Text = text;
            groupBox1.Enabled = true;
            if (allQuerys.Count > 0)
            {
                listBoxURL.SelectionMode = SelectionMode.None;
                isFinished = false;
                webBrowser1.Navigate(allQuerys.Pop());
            }
            else
            {
                listBoxURL.SelectionMode = SelectionMode.One;
                isFinished = true;
                //listBoxURL.SelectedIndex = 0;
            }
            //ReorganizeStrings();
        }

        static bool isFinished = false;
        

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            isFinished = false;
            webBrowser1.Navigate(searchEngines.query(Convert.ToInt32(numericUpDown1.Value), textBoxTitle.Text, Convert.ToInt32(numericUpDown2.Value)));
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (listBoxTitle.SelectedItem == null)
                listBoxTitle.SelectedIndex = 0;
            else
            {
                if (listBoxTitle.SelectedIndex >= listBoxTitle.Items.Count - 1)
                    listBoxTitle.SelectedIndex = listBoxTitle.Items.Count - 1;
                else
                    listBoxTitle.SelectedIndex = listBoxTitle.SelectedIndex + 1;
            }
            textBoxTitle.Text = listBoxTitle.SelectedItem as string;
        }

        Stack<string> allQuerys = new Stack<string>();

        private void buttonSum_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            listBoxURL.Items.Clear();
            for (int i = 6; i >= 1; i--)
                for (int page = 1; page < 11; page++)
                {

                    allQuerys.Push(searchEngines.query(i, textBoxTitle.Text, page));
                }
            isFinished = false;
            webBrowser1.Navigate(allQuerys.Pop());
            b.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxTitle.Text = listBoxTitle.SelectedItem as string;
        }

        private void listBoxURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFinished) return;
            string cur = listBoxURL.SelectedItem as string;
            if (cur != null)
                webBrowser1.Navigate(cur.Split('\t')[1]);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            groupBox1.Enabled = false;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listBoxURL.SelectedItem == null) return;
            int se = listBoxURL.SelectedIndex;
            listBoxURL.Items.Remove(listBoxURL.SelectedItem);
            if (se < listBoxURL.Items.Count)
                listBoxURL.SelectedIndex = se;
        }

        private void buttonyYAO_Click(object sender, EventArgs e)
        {
            if (listBoxURL.SelectedItem == null) return;
            //
            int se = listBoxURL.SelectedIndex;
            if (se < listBoxURL.Items.Count - 1)
                listBoxURL.SelectedIndex++;
                //listBoxURL.SelectedIndex = se + 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string str = string.Empty;
            for (int i = 0; i < listBoxURL.Items.Count; i++)
            {
                str += (listBoxURL.Items[i] as string) + "\r\n";
            }
            textBoxURLs.Text = str;
            Clipboard.SetText(str);
            File.WriteAllText(".\\" +  textBoxTitle.Text + ".txt", str);
        }
    }
}
