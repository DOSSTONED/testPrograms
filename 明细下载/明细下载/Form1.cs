using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 明细下载
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            for (int i = 600840; i < 600850; i++)
            {
                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri("http://market.finance.sina.com.cn/downxls.php?date=2009-11-13&symbol=sh" + i.ToString()), System.IO.Directory.GetCurrentDirectory() + "\\..\\" + i.ToString() + ".xls");
            }
            

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();
            listBoxLog.Items.Add(e.ToString() + "\n Finished");
        }
    }
}
