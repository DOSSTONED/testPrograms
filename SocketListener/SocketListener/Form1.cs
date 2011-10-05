using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace SocketListener
{
    /// <summary>
    /// 接收网络数据的状态对象
    /// </summary>
    class ReadStateObject
    {
        /// <summary>
        /// 基础网络套接字
        /// </summary>
        public Socket handler;
        /// <summary>
        /// 数据缓冲区大小
        /// </summary>
        public const int MAX_PACKET_SIZE = 65535;
        /// <summary>
        /// 接收数据的缓冲区
        /// </summary>
        public byte[] buffer = new byte[MAX_PACKET_SIZE];
    }

    public partial class Form1 : Form
    {
        Socket listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Unspecified);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                listeningSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                listeningSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                byte[] inn = new byte[4] { 1, 0, 0, 0 };
                byte[] outt = new byte[4];
                listeningSocket.IOControl(IOControlCode.ReceiveAll, inn, outt);
                listeningSocket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));
                //listeningSocket.BeginAccept(new AsyncCallback(OnRecvNet), listeningSocket);
                ReadStateObject state = new ReadStateObject();
                state.handler = listeningSocket;
                listeningSocket.BeginReceive(
                    state.buffer,
                    0,
                    ReadStateObject.MAX_PACKET_SIZE,
                    SocketFlags.None,
                    new AsyncCallback(ReadCallback),
                    state);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void ReadCallback(IAsyncResult result) 
        {
            result.ToString();
        }


        private void printIpPacket(byte[] data, int length)
        {
            listBox1.Items.Add("-----------------Packet Begins-----------------\n");
            listBox1.Items.Add(string.Format("IP Version: {0}, Packet Size: {1}bytes, Id: {2}\n",
                        (data[0] >> 4), (data[2] * 256) + data[3], (data[4] * 256) + data[5]));

            listBox1.Items.Add(string.Format("Fragment: {0}, TTL: {1}, HL: {2}wds, Protocol: {3}\n",
                        ((int)(data[6] >> 4) * 256) + data[7], data[8], ((char)(data[0] << 4)) >> 4, data[9]));

            listBox1.Items.Add(string.Format("string.Format(Source: {0}.{1}.{2}.{3}, Destination: {4}.{5}.{6}.{7}\n",
                        data[12], data[13], data[14], data[15],
                        data[16], data[17], data[18], data[19]));

            //the data inside the packet starts at --> data+(((char)(data[0]<<4))>>2)
            //new data length --> length-(((char)(data[0]<<4))>>2)
            //continue printing the rest of the headers :o	

            listBox1.Items.Add("\n------------------Packet Ends------------------\n");
        }
    }
}
