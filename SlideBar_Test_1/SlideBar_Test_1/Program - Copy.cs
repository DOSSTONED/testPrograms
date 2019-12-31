using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SBarHook;
using CoreAudioApi;
using System.ComponentModel;

namespace SlideBar_Test_1
{
    static class Program
    {

        static private ContextMenuStrip notifyIconMenu;
        static ToolStripMenuItem menuItemExit;
        static ToolStripMenuItem menuItemUseForBrightness;
        static ToolStripMenuItem menuItemUseForVolumeControl;
        static private NotifyIcon notifyIcon提示;
        ///语音部分///static private SpeechSynthesizer speech = new SpeechSynthesizer();


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            #region 初始化组件
            notifyIcon提示 = new NotifyIcon();
            /// 初始化部分组件
            notifyIconMenu = new ContextMenuStrip();
            

            notifyIcon提示.Icon = Properties.Resources.SlideBar;
            notifyIcon提示.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon提示.BalloonTipTitle = "提示";
            notifyIcon提示.Visible = true;

            menuItemExit = new ToolStripMenuItem("Exit", null, menuExit);
            menuItemUseForVolumeControl = new ToolStripMenuItem("Control Volume", null, menuVolumeControl);
            menuItemUseForBrightness = new ToolStripMenuItem("Control Brightness", null, menuBrightness);

            menuItemUseForBrightness.CheckedChanged += new EventHandler(menuItemUseForBrightness_CheckedChanged);
            menuItemUseForVolumeControl.CheckedChanged += new EventHandler(menuItemUseForVolumeControl_CheckedChanged);

            notifyIconMenu.Items.Add(menuItemUseForBrightness);
            notifyIconMenu.Items.Add(menuItemUseForVolumeControl);
            notifyIconMenu.Items.Add(menuItemExit);

            notifyIcon提示.Text = "正在启动相应硬件……请稍后";
            notifyIcon提示.ContextMenuStrip = notifyIconMenu;

            #endregion
            ///语音部分///speech.Volume = 10;
            notifyIcon提示.ShowBalloonTip(1000, "提示", "正在启动程序", ToolTipIcon.Info);
            
            slideBar = new SlideBar(new SlideBarEventCallback(sb_sbArriveEvent), true);
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            volumeDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            
            notifyIcon提示.ShowBalloonTip(1000, "提示", "程序已启动", ToolTipIcon.Info);

            
            Application.Run();

            ///语音部分///speech.Speak("Bye bye.");

            notifyIcon提示.Dispose(); // 消除提示图标
            /// 这个地方放置退出登录的操作
        }

        static void menuItemUseForVolumeControl_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        static void menuItemUseForBrightness_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #region Event handlers

        static void menuExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        static void menuBrightness(object sender, EventArgs e)
        {
            if (menuItemUseForBrightness.Checked)
            {
                menuItemUseForBrightness.Checked = false;
            }
            else
            {
                menuItemUseForVolumeControl.Checked = false;
                menuItemUseForBrightness.Checked = true;
            }
        }
        static void menuVolumeControl(object sender, EventArgs e)
        {
            if (menuItemUseForVolumeControl.Checked)
            {
                menuItemUseForVolumeControl.Checked = false;
            }
            else
            {
                menuItemUseForVolumeControl.Checked = true;
                menuItemUseForBrightness.Checked = false;
            }
        }

        static void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {
            if (menuItemUseForVolumeControl.Checked)
            {
                if (volumeDevice != null)
                    volumeDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)sbData.bPosition / 255;
            }
            if (menuItemUseForBrightness.Checked)
            {
                SetBrightness((byte)((int)sbData.bPosition * 100 / 255));
            }
        }

        #endregion

        #region static variables

        static private MMDevice volumeDevice = null;
        static SlideBar slideBar = null;

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
    }
}
