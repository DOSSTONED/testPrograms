using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace CS_P2P_Chat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private TcpListener TcpListener;
        private Thread sends;
        private Thread Listener;


        private void button1_Click(object sender, EventArgs e)
        {
            if (sends == null || !sends.IsAlive)
            {
                sends = new Thread(new ThreadStart(send));
                sends.Start();
            }
            else
                MessageBox.Show("发送过快!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void send()
        {
            if (this.textBoxIP.Text.Length < 7)
            {
                MessageBox.Show("IP地址错误!", "错误信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox1.Text.Length < 1)
            {
                return;
            }
            try
            {
                string Message = this.textBoxName.Text + ":" + this.textBox1.Text;
                TcpClient TcpClient = new TcpClient(this.textBoxIP.Text, 19808);
                NetworkStream tcpStream = TcpClient.GetStream();
                StreamWriter stream = new StreamWriter(tcpStream);
                stream.Flush();
                stream.Write(Message);
                stream.Close();
                TcpClient.Close();
                MessageBox.Show(Message + "\n");
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "错误信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sends.Abort();
            }
        }

        private void StartListen()
        {
            this.TcpListener = new TcpListener(19808);
            this.TcpListener.Start();
            while (true)
            {
                TcpClient TcpClient = this.TcpListener.AcceptTcpClient();
                NetworkStream MyStream = TcpClient.GetStream();
                byte[] bytes = new byte[2048];
                int bytesRead = MyStream.Read(bytes, 0, bytes.Length);
                string message = System.Text.Encoding.UTF8.GetString(bytes, 0, bytesRead);
                MessageBox.Show(message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Listener = new Thread(new ThreadStart(StartListen));
            this.Listener.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Listener != null)
                this.Listener.Abort();
            if (this.TcpListener != null)
                this.TcpListener.Stop();
        }
    }
}
