using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace CS_P2P_Chat_V6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Socket listener;
        private Socket socket;
        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.IPv6Any, 16385));
            listener.Listen(0);
            socket = listener.Accept();
            listener.Close();

            byte[] b = new byte[11];
            int len;
            while ((len = socket.Receive(b)) != 0)
            {
                System.Console.WriteLine("RX: " + System.Text.ASCIIEncoding.ASCII.GetString(b, 0, len));
                b = new byte[11];
            }
            socket.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!Socket.SupportsIPv6)
            {
                Console.Error.WriteLine("Your system does not support IPv6\r\n" +
                    "Check you have IPv6 enabled and have changed machine.config");
                return;
            }

            IPAddress ipa = IPAddress.Parse(textBoxIP.Text);
            IPEndPoint ipeh = new IPEndPoint(ipa, 16385);
            Socket connection = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            connection.Connect(ipeh);

            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(textBox1.Text);
            connection.Send(b);

            connection.Close();

            MessageBox.Show(textBox1.Text+"\nSend");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }


 

    }
}
