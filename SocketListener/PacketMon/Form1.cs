using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Org.Mentalis.Network.PacketMonitor;

namespace PacketMon
{
    public partial class Form1 : Form
    {
        PacketMonitor PMonitor = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PMonitor = new PacketMonitor(System.Net.IPAddress.Parse("DOSSTONED"));
            PMonitor.NewPacket += new NewPacketEventHandler(PMonitor_NewPacket);
            PMonitor.Start6();
        }

        void PMonitor_NewPacket(PacketMonitor pm, Packet p)
        {
            p.DestinationAddress.ToString();//throw new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PMonitor != null)
                PMonitor.Stop();
        }

    }
}
