using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TL_MR11U_Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text += string.Format("Monitor started on {0}\r\n", DateTime.Now);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                WebRequest wr = HttpWebRequest.Create("http://192.168.4.15/userRpm/SystemLogRpm.htm?logType=0&logLevel=7&pageNum=1");
                //wr.GetRequestStream();
                WebClient wc = new WebClient();
                wc.BaseAddress = "http://192.168.4.15/userRpm/SystemLogRpm.htm?logType=0&logLevel=7&pageNum=1";
                wc.Credentials = new NetworkCredential("DOSSTONED", "PASSWORD");
                byte[] bytes = wc.DownloadData(wc.BaseAddress);
                string str = Encoding.GetEncoding("GB2312").GetString(bytes);
                string[] lines = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("Time"))
                    { str = line; break; }
                }
                textBox1.Text += str + "\r\n";
                Random ran = new Random();
                timer1.Interval = ran.Next(45000, 75000);
            }
            catch(Exception ex)
            {
                textBox1.Text += string.Format("ERROR!! on {0}\r\nReason: {1}", DateTime.Now, ex.Message);
                timer1.Enabled = false;
            }
        }
    }
}
