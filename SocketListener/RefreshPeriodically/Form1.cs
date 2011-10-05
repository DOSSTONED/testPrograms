using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Net;
using IpHlpApidotnet;

namespace RefreshPeriodically
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
            //IPGlobalProperties ipGlobal = IPGlobalProperties.GetIPGlobalProperties();
            listBox1.Items.Clear();
            /*
             Array.ForEach<TcpConnectionInformation>(ipGlobal.GetActiveTcpConnections(),
                delegate(TcpConnectionInformation i)
                {

                    if (i.RemoteEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
                        
                            listBox1.Items.Add(string.Format("{0}, {1}, {2}", i.LocalEndPoint, i.RemoteEndPoint, i.State));
                }

            ); 
             */

            //IPHlpAPI32Wrapper.
        }


    }
}
