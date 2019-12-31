using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.NetworkInformation;

namespace NBTSTAT_CS
{
    public partial class Form1 : Form
    {
        public string ip = string.Empty;
        public Form1()
        {


            InitializeComponent();

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            listBoxResult.Items.Clear();
            listBoxResult.Items.Add("Result:");
            //IP正则表达式
            string ipRegexString = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)|(\*)$";
            //如果IP地址是错的，禁止
            if (!Regex.IsMatch(textBoxIP.Text, ipRegexString))
            {
                MessageBox.Show("参数ip错误：错误的IP地址" + textBoxIP.Text);
                // throw new Exception("参数ip错误：错误的IP地址" + textBoxIP.Text);
            }
            else if (Regex.IsMatch(textBoxIP.Text, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(\*)$"))
            {
                string head = textBoxIP.Text.Remove(textBoxIP.Text.Length - 1, 1); // Delete "*"
                for (int i = 1; i < 255; i++)
                {
                    GetInfo(head + i.ToString());
                }
            }
            else
            {
                textBoxHost.Text = "Getting...";
                textBoxGroup.Text = "Getting...";
                textBoxUser.Text = "Getting...";
                textBoxMAC.Text = "Getting...";
                GetInfo(textBoxIP.Text);
            }
        }

        private void GetInfo(string IP_in)
        {
            //listBoxError.Items.Add(DateTime.Now.ToLongTimeString() + " " + IP_in + " Detecting");
            byte[] bs = new byte[50] { 0x0, 0x00, 0x0, 0x10, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x43, 0x4b, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x0, 0x0, 0x21, 0x0, 0x1 };
            byte[] Buf = new byte[500];
            byte[,] recv = new byte[18, 28];
            string str = string.Empty, strHost = string.Empty, Group = string.Empty, User = string.Empty, strMac = string.Empty;
            int receive, macline = 0, usernum = 0;
            string[] domainuser = new string[2];
            domainuser[0] = string.Empty;
            domainuser[1] = string.Empty;

            try
            {
                IPEndPoint senderIPEP = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)senderIPEP;

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IP_in), 137); // nbtstat使用137端口

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
                server.SendTo(bs, bs.Length, SocketFlags.None, ipep);
                receive = server.ReceiveFrom(Buf, ref Remote);
                server.Close();

                if (receive > 0)
                {
                    recv = new byte[18, (receive - 56) % 18];

                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            recv[j, k] = Buf[57 + 18 * k + j];
                        }
                    }

                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        str = string.Empty;
                        if (System.Convert.ToString(recv[15, k], 16) == "0" && (System.Convert.ToString(recv[16, k], 16) == "4" || System.Convert.ToString(recv[16, k], 16) == "44"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            strHost = str.Trim();
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "0" && (System.Convert.ToString(recv[16, k], 16) == "84" || System.Convert.ToString(recv[16, k], 16).ToUpper() == "C4"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            Group = str.Trim();
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "3" && (System.Convert.ToString(recv[16, k], 16) == "4" || System.Convert.ToString(recv[16, k], 16) == "44"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            domainuser[usernum] = str.Trim();
                            usernum++;
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "0" && System.Convert.ToString(recv[16, k], 16) == "0" && System.Convert.ToString(recv[17, k], 16) == "0")
                        {
                            macline = k;

                            for (int i = 0; i < 6; i++)
                            {
                                if (i < 5)
                                {
                                    strMac += System.Convert.ToString(recv[i, macline], 16).PadLeft(2, '0').ToUpper() + ":";
                                }
                                if (i == 5)
                                {
                                    strMac += System.Convert.ToString(recv[i, macline], 16).PadLeft(2, '0').ToUpper();
                                }
                            }
                            k = (receive - 56) % 18;
                        }
                    }
                    User = domainuser[1];
                    if (string.IsNullOrEmpty(domainuser[1])) { User = domainuser[0]; }
                    textBoxHost.Text = strHost;
                    textBoxGroup.Text = Group;
                    textBoxUser.Text = User;
                    textBoxMAC.Text = strMac;
                    listBoxResult.Items.Add("IP:\t" + IP_in);
                    listBoxResult.Items.Add("Host:\t" + strHost);
                    listBoxResult.Items.Add("Group:\t" + Group);
                    listBoxResult.Items.Add("User:\t" + User);
                    listBoxResult.Items.Add("MAC:\t" + strMac);
                    listBoxResult.Items.Add("");
                }
            }
            catch (SocketException ex)
            {
                // MessageBox.Show(ex.Message);
                listBoxError.Items.Add("获取" + IP_in + "的信息出错");
                Ping ping = new Ping();
                
                ping.Send()
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(this.BackColor.ToString());
        }


    }
}
