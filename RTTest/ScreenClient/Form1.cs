using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ScreenClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient uc = new TcpClient();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            uc.Connect(new IPEndPoint(IPAddress.Loopback, 4002));
            uc.SendBufferSize = 3 * 1024 * 1024;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            byte[] bs = imageToByteArray(CaptureScreen());
            uc.GetStream().Write(bs, 0, bs.Length);
        }

        private Image CaptureScreen()
        {
            Rectangle screenSize = Screen.PrimaryScreen.Bounds;
            Bitmap target = new Bitmap(screenSize.Width, screenSize.Height);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(screenSize.Width, screenSize.Height));
            }
            return target;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
