using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace TCP查看.Net4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetTcp6Table([Out] byte[] tcpTable, ref int pdwSize, [In] bool bOrder);

        private void timer1_Tick(object sender, EventArgs e)
        {
            int pdwSize = 0;
            byte[] buffer = new byte[2000];
            int res = GetTcp6Table(buffer, ref pdwSize, true);
            if (res != 0)
            {
                buffer = new byte[pdwSize];
                res = GetTcp6Table(buffer, ref pdwSize, true);
                if (res != 0)
                    return;     // Error. You should handle it

            }

            //dataGridView1.Rows.Clear();
            //IPGlobalProperties ipgp = IPGlobalProperties.GetIPGlobalProperties();
            //TcpConnectionInformation[] allConnections = ipgp.GetActiveTcpConnections();
            //foreach (TcpConnectionInformation tci in allConnections)
            //{
            //    string[] address = new string[]{
            //        
            //        
            //        
            //        "10.0.0.1"
            //    };
            //    if (tci.State == TcpState.Established && address.Contains(tci.RemoteEndPoint.Address.ToString()))
            //        //if (tci.RemoteEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            //        dataGridView1.Rows.Add(tci.LocalEndPoint, tci.RemoteEndPoint, tci.State);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
