/*

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
*/

using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraTest
{
    ///   
    /// 一个C#摄像头控制类  
    ///   
    public class VideoWork
    {
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        private IntPtr hWndC;
        private bool bWorkStart = false;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;
        private int mLeft;
        private int mTop;

        ///   
        /// 初始化显示图像  
        ///   
        /// 控件的句柄  
        /// 开始显示的左边距  
        /// 开始显示的上边距  
        /// 要显示的宽度  
        /// 要显示的长度  
        public VideoWork(IntPtr handle, int left, int top, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
            mLeft = left;
            mTop = top;
        }
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, long lParam);
        ///   
        /// 开始显示图像  
        ///   
        public void Start()
        {
            if (bWorkStart)
                return;
            bWorkStart = true;
            byte[] lpszName = new byte[100];
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);
            if (hWndC.ToInt32() != 0)
            {
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
                //Global.log.Write("SendMessage");  
            }
            return;
        }
        ///   
        /// 停止显示  
        ///   
        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bWorkStart = false;
        }
        ///   
        /// 抓图  
        ///   
        /// 要保存bmp文件的路径  
        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt64());
        }
    }
}

/*
 //这是一个控制摄像头进行拍照的类，我每次使用GrabImage抓图都是225K的一张照片，我想请问如何才能让我抓到的图片小一些，我想控制在70K左右。不知怎么让拍照的像素变小？  
    
 if(this.Request.QueryString["filename"]!=null)  
 {  
                 //获取原图片  
 string filename=this.Request.QueryString["filename"];  
 Bitmap bmpOld=new Bitmap(this.Server.MapPath("images/" + filename));  
     //计算缩小比例  
 double d1;  
 if(bmpOld.Height>bmpOld.Width)  
 d1=(double)(MaxLength/(double)bmpOld.Width);  
 else 
 d1=(double)(MaxLength/(double)bmpOld.Height);  
 //产生缩图  
 Bitmap bmpThumb=new Bitmap(bmpOld,(int)(bmpOld.Width*d1),(int)(bmpOld.Height*d1));  
 //清除缓冲  
 Response.Clear();  
 //生成图片  
 bmpThumb.Save(this.Response.OutputStream,ImageFormat.Jpeg);  
 Response.End();  
 //释放资源  
 bmpThumb.Dispose();  
 bmpOld.Dispose();  
} 
*/