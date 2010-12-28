using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace powerpolicy
{
    public partial class Form1 : Form
    {
        #region Enumeration

        internal enum PowerDataAccessor
        {
            /// <summary>
            /// Used with PowerSettingAccessCheck to check for group policy
            /// overrides for AC power settings. 
            /// </summary>
            AccessACPowerSettingIndex = 0,   // 0x0

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for group policy 
            /// overrides for DC power settings. 
            /// </summary>
            AccessDCPowerSettingIndex = 1,// 0x1

            /// <summary>
            /// Used to enumerate power schemes with PowerEnumerate and with
            /// PowerSettingAccessCheck to check for restricted access to 
            /// specific power schemes. 
            /// </summary>
            AccessScheme = 16,// 0x10

            /// <summary>
            /// Used to enumerate subgroups with PowerEnumerate. 
            /// </summary>
            AccessSubgroup = 17,// 0x11

            /// <summary>
            /// Used to enumerate individual power settings with 
            /// PowerEnumerate. 
            /// </summary>
            AccessIndividualSetting = 18,// 0x12

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for 
            /// group policy overrides for active power schemes. 
            /// </summary>
            AccessActiveScheme = 19, // 0x13

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for 
            /// restricted access for creating power schemes. 
            /// </summary>
            AccessCreateScheme = 20 // 0x14

        };
        #endregion


        [DllImport("powrprof.dll", EntryPoint = "PowerEnumerate", SetLastError = true)]
        internal static extern UInt32 PowerEnumerate(IntPtr rootPowerKey, IntPtr schemeGuid, IntPtr subgroupOfPowerSettingsGuid, PowerDataAccessor accessFlags, UInt32 index, IntPtr buffer, ref UInt32 bufferSize);





        public Form1()
        {
            InitializeComponent();
        }





    }
}
