using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Router
{
    public partial class Form1 : Form
    {
        WebClient postClient = new WebClient();
        HttpWebRequest hwr = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data;

            string requestURL = "http://192.168.0.1/goform/AdvSetWan";
            data = "GO=wan_connectd.asp&rebootTag=&WANT2=3&WANT1=2&PUN=3:\r\nbqUt9oqDn&PPW=41519&MTU=1492&SVC=&AC=&PIDL=60&PCM=2&hour1=0&minute1=0&hour2=0&minute2=0";
            postClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] postData = Encoding.ASCII.GetBytes(data);
            byte[] responseData = postClient.UploadData(requestURL, "POST", postData);
            textBox1.Text=Encoding.Default.GetString(responseData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data;

            string requestURL = "http://192.168.0.1/LoginCheck";
            data = "Username=admin&checkEn=0&Password=admin";
            //postClient.Headers.Set("Host", "192.168.0.1");
            
            //postClient.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:9.0.1) Gecko/20100101 Firefox/9.0.1");
            //postClient.Headers.Set("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //postClient.Headers.Set("Accept-Language", "zh-cn,en-us;q=0.7,en;q=0.3");
            //postClient.Headers.Set("Accept-Encoding", "gzip, deflate");
            //postClient.Headers.Set("Accept-Charset", "gb18030,utf-8;q=0.7,*;q=0.7");
            ////postClient.Headers.Set("Connection", "keep-alive");
            //postClient.Headers.Set("Referer", "http://192.168.0.1/login.asp");
            
            postClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = Encoding.ASCII.GetBytes(data);
            byte[] responseData = postClient.UploadData(requestURL, "POST", postData);
            textBox1.Text = Encoding.Default.GetString(responseData);
            //hwr = HttpWebRequest.Create("http://192.168.0.1/LoginCheck") as HttpWebRequest;
            //string data = "Username=admin&checkEn=0&Password=admin";

            //byte[] postData = Encoding.ASCII.GetBytes(data);
            //hwr.Method = "POST";
            //hwr.GetRequestStream().Write(postData, 0, postData.Length);
            //WebResponse wr = hwr.GetResponse();
            //StreamReader sr = new StreamReader(wr.GetResponseStream());

            //textBox1.Text = sr.ReadToEnd();
        }
    }
}
