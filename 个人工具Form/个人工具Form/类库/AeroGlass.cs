using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 个人工具Form.类库
{
    class AeroGlass
    {
        int en;
        GraphicsPath fontpath;
        GraphicsPath glowpath;
        PathGradientBrush pathbrush;
        Graphics gph;

        //API 结构声明

        public struct MARGINS
        {
            public int m_Left;
            public int m_Right;
            public int m_Top;
            public int m_Buttom;
        };

        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref int enabledptr);
        [DllImport("dwmapi.dll")]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margin);
        public Form1()
        {
            InitializeComponent();
           en=0;
            MARGINS mg=new MARGINS();
            mg.m_Buttom = -1;
            mg.m_Left = -1;
            mg.m_Right = -1;
            mg.m_Top = -1 ;
            //判断Vista系统
            if (System.Environment.OSVersion.Version.Major >= 6)             
            {
                DwmIsCompositionEnabled(ref en);    //检测Aero是否为打开
                if(en>0)
                {
                      DwmExtendFrameIntoClientArea(this.Handle, ref mg);

                }else{
                      MessageBox.Show("Desktop Composition is Disabled!");
                }
            }else{
                 MessageBox.Show("Please run this on Windows Vista.");
            }
            this.Paint += new PaintEventHandler(Form1_Paint);
        }


//本文来自CSDN博客，转载请标明出处：http://blog.csdn.net/SnowRen3074/archive/2008/10/23/3129306.aspx
    }
}
