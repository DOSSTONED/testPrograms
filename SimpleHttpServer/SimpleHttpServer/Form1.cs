using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace SimpleHttpServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static HttpListener hlistener = new HttpListener();
        Thread t;
        string Headers = "<html><head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF8\">\r\n<title>DOSSTONED's ANSWER</title>\n</head>\n<body>\r\n";

        private void Form1_Load(object sender, EventArgs e)
        {
            hlistener.Prefixes.Add("http://10.10.10.10:41519/519/");
            hlistener.Start();
            t = new Thread(new ThreadStart(Http));
            t.Start();
            
        }

        void Http()
        {
            while (true)
            {
                HttpListenerContext hlc = hlistener.GetContext();
                HttpListenerRequest request = hlc.Request;

                

                // Obtain a response object.
                HttpListenerResponse response = hlc.Response;
                // Construct a response.
                listBox1.Items.Add(request.RawUrl);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Headers + Properties.Resources.ResponseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null)
                t.Abort();
            if (hlistener != null)
                hlistener.Stop();
        }
    }
}
