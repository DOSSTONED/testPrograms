using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using CoreAudioApi;

namespace ClassSlideBar
{
    public class ClassSB
    {
        

        static UserControlSB UCSB = null;
        static private ManagementEventWatcher watcher = null;
        static private MMDevice device = null;


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

        public static byte[] lastBytes = new byte[128];

        private static void SlideBar_HandleEvent(object sender, EventArrivedEventArgs e)
        {
            /// Console.WriteLine("WMIACPIEvent event occurred.");
            ManagementBaseObject mbo = e.NewEvent;
            byte[] EvtBytes = mbo["EvtBytes"] as byte[];
            if (EvtBytes.Equals(lastBytes))
                return;
            else
                lastBytes = EvtBytes;
            SBMode returnStatus = SBMode.Nothing;

            if (UCSB != null)
            {

                returnStatus = UCSB.SlideBar_EventHandler(EvtBytes);

            }

            /// Special for Volume Control
            /// 
            if (returnStatus == SBMode.VolumeControl)
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = ScaleSlideBarPlaceForVolume(EvtBytes[19]);
            }
        }

        private static bool WriteWMIACPI_IO(WMIACPI_IO_Status status)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = 10;
            array[9] = 3;
            array[10] = 0;
            array[16] = (byte)status;

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

                    return true;
                }
            }
            if (managementObjectSearcher != null)
                managementObjectSearcher.Dispose();

            return false;
        }

        /// <summary>
        /// Main entry point of FrontEnd.
        /// </summary>
        /// <param name="ff">The container which gives the space of current plugin.</param>
        public static void DOSSTONED_FE_LOAD(object ff)
        {
            Panel f = ff as Panel;
            if (f == null) 
                throw new ArgumentException("Argument is not valid, this plugin requires Panel.");
            UCSB = new UserControlSB();
            //UCSB.Location = new System.Drawing.Point(0, 0);
            UCSB.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
            UCSB.Size = new System.Drawing.Size(f.Size.Width, f.Size.Height);
            if (f.InvokeRequired)
            {
                f.Invoke((MethodInvoker)delegate
                {
                    f.Controls.Add(UCSB);
                });
            }
            else
            {
                f.Controls.Add(UCSB);
            }

            



            if (!WriteWMIACPI_IO(WMIACPI_IO_Status.Registration))
            {
                throw new Exception("Write WMIACPI_IO failed.");
            }

            /// Add the event handler
            /// Start listening for events
            /// 
            ManagementScope root_WMI = new System.Management.ManagementScope("root\\WMI");
            WqlEventQuery query_WMIACPIEvent = new WqlEventQuery("SELECT * FROM WMIACPIEvent");
            watcher = new ManagementEventWatcher(root_WMI, query_WMIACPIEvent);
            watcher.EventArrived += new EventArrivedEventHandler(SlideBar_HandleEvent);
            watcher.Start();


            /// Add volume control
            /// Something unexpected happens when putting this part into the component
            /// 
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

        }

        /// <summary>
        /// Main exiting point.
        /// </summary>
        public static void DOSSTONED_FE_UNLOAD()
        {
            
            if (!WriteWMIACPI_IO(WMIACPI_IO_Status.Unregistration))
            {
                throw new CannotUnloadAppDomainException("Cannot unload SlideBar controls.");
            }
            if (watcher != null)
            {
                watcher.Stop();
            }
            
            /// There might be some exception when removing UCSB
            if (UCSB.InvokeRequired)
            {
                UCSB.Invoke((MethodInvoker)delegate
                {
                    UCSB.Parent.Controls.Remove(UCSB);
                });
            }
            else
            {
                //UCSB.Dispose();
            }
            //Thread.Sleep(1);
        }
    }
}
