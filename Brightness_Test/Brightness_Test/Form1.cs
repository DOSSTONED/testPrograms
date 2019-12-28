using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Brightness_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region aerotest

        uint col = 0;
        WDM_COLORIZATION_PARAMS backup;
        uint backup_clr;
        bool backup_bool;
        private void button1_Click(object sender, EventArgs e)
        {
            
            //bool opaque = false;
            //DwmGetColorizationColor(out col, out opaque);
            Random ran = new Random();
            /*
            byte[] color1 = new byte[3];
            
            ran.NextBytes(color1);
            DwmpSetColorization(BitConverter.ToUInt32(color1, 0), false, 0);
             */

            col = (UInt32)ran.Next() & 0xFFFFFF;

            //DwmpSetColorization(col, false, 0);
            //SetBrightness((byte)150);

            patra.Color1 = col;
            patra.Color2 = col;
            DwmSetColorizationParameters(patra, 0);

            DwmpSetColorization(toGet, false, 0);
        }

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(WDM_COLORIZATION_PARAMS parameters, uint uUnknown);
        [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false)]
        public static extern void DwmGetColorizationParameters(out WDM_COLORIZATION_PARAMS parameters);

        private WDM_COLORIZATION_PARAMS patra = new WDM_COLORIZATION_PARAMS();
        public struct WDM_COLORIZATION_PARAMS
        {
            public uint Color1;
            public uint Color2;
            public uint Intensity;
            public uint Unknown1;
            public uint Unknown2;
            public uint Unknown3;
            public uint Opaque;
        }


        [DllImport("dwmapi.dll", EntryPoint = "#104")]
        static extern int DwmpSetColorization(UInt32 ColorizationColor, bool ColorizationOpaqueBlend, UInt32 Opacity);

        [DllImport("dwmapi.dll")]
        private static extern void DwmGetColorizationColor(out uint ColorizationColor, out bool ColorizationOpaqueBlend);

        #endregion

        static void SetBrightness(byte targetBrightness)
        {
            //define scope (namespace)
            System.Management.ManagementScope x = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightnessMethods");

            //output current brightness
            System.Management.ManagementObjectSearcher mox = new System.Management.ManagementObjectSearcher(x, q);

            System.Management.ManagementObjectCollection mok = mox.Get();

            foreach (System.Management.ManagementObject o in mok)
            {
                o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness }); //note the reversed order - won't work otherwise!
                break; //only work on the first object
            }

            mox.Dispose();
            mok.Dispose();
        }


        static byte GetBrightness()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);

            System.Management.ManagementObjectCollection moc = mos.Get();

            //store result
            byte curBrightness = 0;

            foreach (System.Management.ManagementObject o in moc)
            {
                curBrightness = (byte)o.GetPropertyValue("CurrentBrightness");
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();

            return curBrightness;
        }

        static byte[] GetBrightnessLevels()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);

            System.Management.ManagementObjectCollection moc = mos.Get();

            //store result
            byte[] BrightnessLevels = new byte[0];

            foreach (System.Management.ManagementObject o in moc)
            {
                BrightnessLevels = (byte[])o.GetPropertyValue("Level");
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();

            return BrightnessLevels;
        }

        byte[] brightnessLevels;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetBrightness(brightnessLevels[trackBar1.Value - 1]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            brightnessLevels = GetBrightnessLevels();
            //MessageBox.Show(GetBrightness().ToString() + "\n" + GetBrightnessLevels());
            byte currentLevel = GetBrightness();
            if (brightnessLevels.Length < 15)
            {
                trackBar1.TickFrequency = 1;
                trackBar1.LargeChange = 1;
                trackBar1.SmallChange = 1;
            }
            else
            {
                trackBar1.TickFrequency = 5;
                trackBar1.LargeChange = 5;
                trackBar1.SmallChange = 1;
            }
            trackBar1.Maximum = brightnessLevels.Length;
            trackBar1.Minimum = 1;
            
            for (int i = 0; i < brightnessLevels.Length; i++)
            {
                if (brightnessLevels[i] == currentLevel)
                {
                    trackBar1.Value = i + 1;
                    break;
                }
            }

            DwmGetColorizationColor(out backup_clr, out backup_bool);
            //DwmGetColorizationParameters(out backup);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DwmSetColorizationParameters(backup, 0);
        }

        const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        protected override void WndProc(ref Message m)
        {


            if (m.Msg == WM_DWMCOLORIZATIONCOLORCHANGED)
            {
                label1.Text = "LParam: " + m.LParam.ToString();
                label2.Text = "WParam: " + m.WParam.ToString();
            }
            base.WndProc(ref m);
        }

        uint toGet = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (label2.Text.Contains("WParam: "))
            {
                toGet = (uint)Convert.ToInt32(label2.Text.Substring(7));
                label3.Text = Convert.ToString(toGet, 16);
            }
        }
    }
}
