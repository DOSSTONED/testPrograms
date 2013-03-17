using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace 网络电视选台
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VlcControl vc = new VlcControl();
            Vlc.DotNet.Core.Medias.PathMedia lm = new Vlc.DotNet.Core.Medias.PathMedia(@"[探索频道-重返51区].Discovery.Channel.Return.To.Area.51.PDTV.XviD-HEH.avi");
            vc.Play(lm);
            
        }
    }
}
