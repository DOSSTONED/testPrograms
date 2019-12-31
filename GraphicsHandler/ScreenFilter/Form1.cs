using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Bitmap CaptureScreen()
        {
            Bitmap b = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(0, 0, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap bm = CaptureScreen();
            for (int i = 0; i < bm.HorizontalResolution; i++)
                for (int j = 0; j < bm.VerticalResolution; j++)
                {
                    if( bm.GetPixel(i, j).R >= 0x90)
                        
                            {
                                    bm.SetPixel(i, j, Color.Red);
                                
                            }
                }
            //this.DrawToBitmap(bm, this.DisplayRectangle);
            pictureBox1.Image = bm as Image;
        }

    }
}
