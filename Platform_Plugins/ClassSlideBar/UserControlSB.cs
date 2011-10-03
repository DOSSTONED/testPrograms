using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using System.Speech.Synthesis;
using System.Management;

namespace ClassSlideBar
{
    public enum SBMode
    {
        [Description("Nothing to do.")]
        Nothing,
        [Description("Volume Control.")]
        VolumeControl,
        [Description("Change music to the previous or next track.")]
        ChangeMusicTrackControl,
        [Description("Brightness Control.")]
        BrightnessControl
    }

    internal enum WMIACPI_IO_Status : byte
    {
        Unregistration = 0,
        Registration,
        Disconnect = 16,
        Connect,
        LED_OFF = 32,
        LED_ON
    }

    internal enum SlideBarEventType : byte
    {
        SlideBar = 0x2A,
        SchemeButton = 0x19
    }

    public partial class UserControlSB : UserControl
    {
        /// Volume set must be done in DOSSTONED_LOAD_FE() function
        /// Using this function in current environment would lead to a CLSID cannot find exception.
        /// Wired.
        /// 

        public UserControlSB()
        {
            InitializeComponent();
        }



        //public void SetMode(SBMode m)
        //{
        //    switch (m)
        //    {
        //        case SBMode.VolumeControl:
        //            radioButtonVol.Checked = true;
        //            break;

        //        case SBMode.BrightnessControl:
        //            radioButtonBrightness.Checked = true;
        //            break;
        //    }
        //}

        public SBMode SlideBar_EventHandler(byte[] eventBytes)    // raw byte[] input
        {
            
            SBMode SBMODE = SBMode.Nothing;
            //SpeechSynthesizer ss = new SpeechSynthesizer();
            
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    labelSB.Text = "位置=" + eventBytes[19] +
                    "速度=" + eventBytes[20] +
                    "动作=" + eventBytes[18] +
                    "模式=" + eventBytes[1] +
                    "呼吸灯效=" + eventBytes[21] +
                    "触钮灯效=" + eventBytes[22] +
                    "启用=" + eventBytes[2]
                    ;
                    
                    
 
                    SBMODE = (SBMode)comboBox1.SelectedItem;
                    if (eventBytes[1] == 25)
                    {
                        if (comboBox1.SelectedIndex + 1 >= comboBox1.Items.Count)
                        {
                            comboBox1.SelectedIndex = 0;
                        }
                        else
                            comboBox1.SelectedIndex++;
                        
                        //ss.SpeakAsync("Changed to " + comboBox1.Items[comboBox1.SelectedIndex]);
                    }
                });
            }

            if (eventBytes[1] == 25) return SBMode.Nothing;
            try
            {
                switch (SBMODE)
                {
                    case SBMode.BrightnessControl:
                        SetBrightness(eventBytes[19]);
                        break;
                    case SBMode.ChangeMusicTrackControl:
                        PressMediaButton(eventBytes[19], eventBytes[18]);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return SBMODE;

        }

        private void UserControlSB_Load(object sender, EventArgs e)
        {
            foreach (SBMode sbm in Enum.GetValues(typeof(SBMode)))
            {
                comboBox1.Items.Add(sbm);
            }
            comboBox1.SelectedIndex = 0;
        }

 

        private void SetStatusBit(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb == null) return;
            string str = cb.Text;
            
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = (byte)(str.Contains("Breathe") ? 43 : str[0] - 40);
            array[9] = 3;
            array[10] = 0;
            array[16] = (byte)(((CheckBox)sender).Checked ? 1 : 0);
            array[68] = (byte)(((CheckBox)sender).Checked ? 1 : 0);


            SelectQuery query = new SelectQuery("WMIACPI_IO");
            ManagementScope scope = new ManagementScope("root\\WMI");
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    managementObject.SetPropertyValue("IOBytes", array);
                    managementObject.Put();
                }
            }
            if (managementObjectSearcher != null)
                managementObjectSearcher.Dispose();

        }





    }
}
