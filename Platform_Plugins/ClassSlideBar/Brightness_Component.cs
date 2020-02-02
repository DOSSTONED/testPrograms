using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace ClassSlideBar
{
    public partial class UserControlSB : UserControl
    {

        private void SetBrightness(byte in_byte)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightnessMethods"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    obj.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, in_byte * 100 / byte.MaxValue }); //note the reversed order - won't work otherwise! error: this was too large or too small for a byte
                    break; //only work on the first object
                }
            }
        }
    }
}
