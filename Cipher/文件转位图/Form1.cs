using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace 文件转位图
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            //byte[] imageData = Encoding.Default.GetBytes(textBox1.Text);
            byte[] imageData = new byte[pictureBox1.Width * pictureBox1.Height * 4];
            for (int i = 0; i < imageData.Length; i++)
                imageData[i] = (byte)ran.Next(0, 255);
            unsafe
            {
                fixed (byte* ptr = imageData)
                {
                    using (Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height, pictureBox1.Width * 4, PixelFormat.Format32bppArgb, new IntPtr(ptr)))
                    {
                        pictureBox1.Image = image;
                        //image.Save(fileName);
                    }
                }
            }
        }
    }
}
