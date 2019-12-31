using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DWMAPI_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        uint color = 0;
        bool opaque = false;

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)]out bool ColorizationOpaqueBlend);
        [DllImport("dwmapi.dll", EntryPoint = "#104")]
        static extern int DwmpSetColorization(UInt32 ColorizationColor, bool ColorizationOpaqueBlend, UInt32 Opacity);

        private void button1_Click(object sender, EventArgs e)
        {
            
            DwmGetColorizationColor(out color, out opaque);
            MessageBox.Show(color.ToString("x8"), opaque.ToString());
            updateRGB();
        }

        private void updateRGB()
        {
            uint RGB = (color - color % 256) / 256;
            uint B = RGB % 256;
            uint G = ((RGB - RGB % 256) / 256) % 256;
            uint R = (RGB - RGB % 65536) / 65536;
            numericUpDownB.Value = B;//throw new NotImplementedException();
            numericUpDownG.Value = G;
            numericUpDownR.Value = R;
        }

        private UInt32 getCurrentRGB()
        {
            return ((UInt32)numericUpDownR.Value * 65536 + (UInt32)numericUpDownG.Value * 256 + (UInt32)numericUpDownB.Value) * 256;
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            DwmpSetColorization(getCurrentRGB(), true, uint.MaxValue);
        }
    }
}
