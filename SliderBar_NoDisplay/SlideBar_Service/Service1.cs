using System;
using System.ServiceProcess;
using System.Management;
using CoreAudioApi;

namespace SlideBar_Service
{
    public partial class BackGroundService : ServiceBase
    {
        private ManagementEventWatcher watcher = null;
        static private MMDevice device = null;


        public BackGroundService()
        {
            InitializeComponent();
        }

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

            if (ret < 0.6)
                ret = ret / 2;
            else
                ret = (ret - 0.6f) * 7 / 4 + 0.3f;

            return ret;
        }

        static Int16 TouchInPlace = 0;
        static Int16 TouchOutPlace = 0;
        static DateTime _clock = DateTime.Now;

        private void PressMediaButton(byte place_byte, byte action_byte)
        {
            /// 基本不动，在中部点击表示暂停/播放
            /// 
            if (action_byte == 5 && Math.Abs(place_byte - 127) < 25)
            {
                WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_PLAY_PAUSE);
                TouchInPlace = 0;
                TouchOutPlace = 0;
                return;
            }

            if (action_byte == 1)    // 触摸起点
            {
                _clock = DateTime.Now;
                TouchInPlace = place_byte;
            }
            if (action_byte == 2)   //  触摸离开
            {
                TouchOutPlace = place_byte;
                
                /// 如果时间超过0.5s，那么认为这不是切换歌曲，只是调节音量
                if ((DateTime.Now - _clock).TotalMilliseconds > 500)
                    return;


                /// 从左向右划
                /// 下一首歌
                if (TouchOutPlace - TouchInPlace > 60)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_NEXT_TRACK);
                    TouchInPlace = 0;
                    TouchOutPlace = 0;
                }

                /// 从右向左划
                /// 前一首歌
                if (TouchOutPlace - TouchInPlace < -60)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_NEXT_TRACK);
                    TouchInPlace = 0;
                    TouchOutPlace = 0;
                }
            }


        }


        private void SlideBar_HandleEvent(object sender, EventArrivedEventArgs e)
        {
            //Console.WriteLine("WMIACPIEvent event occurred.");
            ManagementBaseObject mbo = e.NewEvent;
            byte[] EvtBytes = mbo["EvtBytes"] as byte[];
            if (device != null)
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = ScaleSlideBarPlaceForVolume(EvtBytes[19]);
            }
            PressMediaButton(EvtBytes[19], EvtBytes[18]);
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

                ManagementScope root_WMI = new System.Management.ManagementScope("root\\WMI");
                //SelectQuery query_WMIACPI_IO = new SelectQuery("WMIACPI_IO");

                //TESTSTRING = "WMIACPI_IO query created.";

                

                if (!WriteWMIACPI_IO(WMIACPI_IO_Status.Registration))
                {
                    throw new Exception("Write WMIACPI_IO failed.");
                }

                //TESTSTRING = "Set WMIACPI_IO successful.";

                /// Handle the event
                /// 
                WqlEventQuery query_WMIACPIEvent = new WqlEventQuery("SELECT * FROM WMIACPIEvent");
                watcher = new ManagementEventWatcher(root_WMI, query_WMIACPIEvent);
                //Console.WriteLine("Waiting for an event...");
                watcher.EventArrived += new EventArrivedEventHandler(SlideBar_HandleEvent);
                // Start listening for events
                watcher.Start();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", ex.Message + "\r\n");
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", ex.StackTrace + "\r\n");
                Stop();
            }

        }

        protected override void OnStop()
        {
            if (!WriteWMIACPI_IO(WMIACPI_IO_Status.Unregistration))
            {
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", "Unregistration failed.\r\n");
            }
            if (watcher != null)
            {
                watcher.Stop();
            }

        }
    }
}
