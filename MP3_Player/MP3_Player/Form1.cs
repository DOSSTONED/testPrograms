using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MP3_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        static extern Int32 mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);

        private void Form1_Load(object sender, EventArgs e)
        {
            string strFileName = @"music.ape";
            //string sCommand = "Open wavefile!" + strFileName + " alias MediaFile";
            string sCommand = "open \"" + strFileName + "\" alias MediaFile";// type mpegvideo
            int error1 = mciSendString(sCommand, null, 0, IntPtr.Zero);
            if (error1 != 0)
            {
                StringBuilder sb = new StringBuilder(200);
                mciGetErrorString(error1, sb, sb.Capacity);
                MessageBox.Show(sb.ToString());
            }
            else
            {
                sCommand = "Play MediaFile";
                int error2 = mciSendString(sCommand, null, 0, IntPtr.Zero);
            }
        }
    }
}
