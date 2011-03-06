using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Management;
using CoreAudioApi;
using SBarHook;
using System.IO;
using Microsoft.Win32;

namespace DOSSTONED_BG_Service
{
    public partial class Main : ServiceBase
    {
        static private SlideBar SB = null;
        static private MMDevice device = null;
        //static private string TempPath = Path.GetTempPath();

        #region SlideBar handlers

        /// <summary>
        /// Scale the volume (0-255) to (0-1)
        /// Map 153 to 0.3, other is linear transform.
        /// </summary>
        /// <param name="volume">Volume wanna set. 255 is max.</param>
        /// <returns>Scaled volume.</returns>
        public static float ScaleSlideBarPlaceForVolume(byte volume)
        {
            float ret = 0;
            ret = (float)volume / 255; // no scale

            //if (ret < 0.6)
            //    ret = ret / 2;
            //else
            //    ret = (ret - 0.6f) * 7 / 4 + 0.3f;

            if (ret < 0.1)
                ret = 0;
            else
                ret = (ret - 0.1f) * 10 / 9;

            return ret;
        }

        private void SlideBar_HandleEvent(SlideBar.SlideBarData sbData)
        {
            //Console.WriteLine("WMIACPIEvent event occurred.");
            //byte[] EvtBytes = e.NewEvent["EvtBytes"] as byte[];

            if (sbData.bEvent == SlideBar.Event.ServiceKey)
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
                /// add the reset of touch screen
                /// 
                DisableTouchScreen(false);
            }
            else
                if (device != null)
                {
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = ScaleSlideBarPlaceForVolume(sbData.bPosition);
                }
        }

        #endregion

        public Main()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //string TESTSTRING = string.Empty;
            try
            {
                ///SlideBar settings
                ///
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

                //TESTSTRING = "MMDevice initialised.";

                SB = new SlideBar(SlideBar_HandleEvent, true);

                DisableTouchScreen(false);
                //Record_Brightness.Entrance();
                SystemEvents.PowerModeChanged += OnPowerChange;
            }
            catch (Exception ex)
            {
                string LogPath = Directory.GetCurrentDirectory() + "\\DOSSTONED_BG.txt";
                System.IO.File.AppendAllText(LogPath,
                    DateTime.Now + "\r\n"
                    + ex.Message + "\r\n"
                    + ex.StackTrace + "\r\n"
                    + "\r\n");
                Stop();
            }

        }

        void OnPowerChange(Object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    DisableTouchScreen(false);
                    break;
            }
        }

        void DisableTouchScreen(bool disableIt)
        {
            // Since I found a 2.0.0.0 version of usbmini driver, so just return it.
            // as the driver did not calibrated well, always recognise left as right
            // stop using this driver. use the unsigned usbmini instead.
            //return;


            try
            {
                DisableHardware.DisableDevice(
                              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
                              !disableIt);
                System.Threading.Thread.Sleep(500);
                DisableHardware.DisableDevice(
                      n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
                      disableIt);
            }
            catch (Exception ex)
            {
                string LogPath = Directory.GetCurrentDirectory() + "\\DOSSTONED_BG.txt";
                System.IO.File.AppendAllText(LogPath,
                    DateTime.Now + "\r\n"
                    + ex.Message + "\r\n"
                    + ex.StackTrace + "\r\n"
                    + "\r\n");
            }
        }

        protected override void OnStop()
        {
            if (SB != null)
                SB.StopAllEventWatcher();
        }
    }
}
