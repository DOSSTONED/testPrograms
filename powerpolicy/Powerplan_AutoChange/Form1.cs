using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Powerplan_AutoChange
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr activeGuidPtr = IntPtr.Zero;

            Guid VideoSettingGuid = new Guid();
            UInt32 index = 0;
            UInt32 BufferSize = (UInt32)Marshal.SizeOf(typeof(Guid));
            while (0 == PowerScheme.PowerEnumerate(
                IntPtr.Zero, activeGuidPtr, new Guid("7516b95f-f776-4464-8c53-06167f40cc99"), 18, index, ref VideoSettingGuid, ref BufferSize))
            {
                listBox1.Items.Add(VideoSettingGuid.ToString() + ": ");

                UInt32 size = 1024;
                IntPtr temp = Marshal.AllocHGlobal(1024);
                IntPtr type = IntPtr.Zero;

                PowerScheme.PowerReadACValue(IntPtr.Zero, activeGuidPtr, IntPtr.Zero, VideoSettingGuid, ref type, ref temp, ref size);

                listBox1.Items.Add(Marshal.PtrToStringUni(temp));
                Marshal.FreeHGlobal(temp);
                index++;
            }

        }
    }
}
