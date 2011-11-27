/*
 * MODIFIED by DOSSTONED
20111128    小修小改了一下，源自1.0.0.6版本的SBarHook.dll

 * TODO：
 * 添加函数，使得用户可以控制灯光效果以及是否connect 20111129 DONE!
 * 20111129: Simplified the class, removed some unused enums and vars.
*/
using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SBarHook
{
    
    public delegate void SlideBarEventCallback(SlideBar.SlideBarData sbData);
    public delegate void SlideBarEventHandler(SlideBar.SlideBarData sbData);

    public class SlideBar
    {
        #region Data structure

        public enum Event : byte
        {
            NoEvent,
            AP00 = 8,
            AP01,
            AP02,
            AP03,
            AP04,
            AP05,
            AP06,
            AP07,
            Brightness = 24,
            ServiceKey,
            BT = 32,
            WLAN,
            WL3G,
            WIMAX,
            GlobalSwitch,
            Touchpad = 40,
            SilentMode,
            SlideBar,
            SlideBarLightSwitch
        }
        private enum ApplicationRegistration : byte
        {
            AP00_OSD,
            AP01_QuickButton0,
            AP02_Reserved,
            AP03_Reserved,
            AP04_Reserved,
            AP05_Reserved,
            AP06_WMIMonitor,
            AP07_DebugConsole
        }
        private enum DataBlock : byte
        {
            Buffer1 = 1,
            Buffer2
        }
        private enum Buffer1DataIndex : byte
        {
            DataBlock,
            Event,
            EventRequest0,
            Brightness,
            ServiceKey,
            BT = 8,
            WLAN,
            WL3G,
            WIMAX,
            GlobalSwitch,
            Touchpad = 16,
            SilentMode,
            SlideBar0,
            SlideBar1,
            SlideBar2,
            SlideBarLightSwitch,
            ProjectDefine0 = 32,
            ProjectDefine1,
            ProjectDefine2,
            ProjectDefine3,
            ProjectDefine4,
            ProjectDefine5,
            ProjectDefine6,
            ProjectDefine7
        }
        public enum SlideBarSettings : byte
        {
            BREATH_OFF,
            BREATH_ON,
            LED_OFF = 16,
            LED_ON,
            DISCONNECT = 32,
            CONNECT
        }
        public enum SlideBarAction : byte
        {
            On = 1,
            Leave,
            Move,
            Hover,
            Click
        }
        public struct SlideBarData
        {
            /// <summary>
            /// Current slidebar action.
            /// </summary>
            public SlideBarAction bAction;
            /// <summary>
            /// Current touch position.
            /// </summary>
            public byte bPosition;
            /// <summary>
            /// Current moving speed.
            /// </summary>
            public byte bCurrentSpeed;
            /// <summary>
            /// evtBytes[1]. This is the mode, 42 for slidebar, 25 for 一键影音
            /// </summary>
            public Event bEvent;
            /// <summary>
            /// evtBytes[2]. Indicates whether slidebar is enabled.
            /// </summary>
            public byte bEnabled;
            /// <summary>
            /// evtBytes[21]. Indicates whether slidebar breath effect is enabled.
            /// </summary>
            public byte bBreathEffect;
            /// <summary>
            /// evtBytes[22]. Show the light.
            /// </summary>
            public byte bLightEffect;

            /// <summary>
            /// This is raw EvtBytes[64].
            /// </summary>
            public byte[] RawBytes;
        }

        #endregion


        #region Variables

        /// <summary>
        /// root\wmi
        /// </summary>
        private readonly string WMIACPINamespace = "root\\wmi";
        /// <summary>
        /// WMIACPIEvent
        /// </summary>
        private readonly string WMIACPIEventClass = "WMIACPIEvent";
        /// <summary>
        /// WMIACPI_IO
        /// </summary>
        private readonly string WMIACPIIOClass = "WMIACPI_IO";
        /// <summary>
        /// Total IOBytes length, is 128.
        /// </summary>
        private readonly byte IOBytesLength = 128;
        /// <summary>
        /// Whether auto enabled the slidebar.
        /// </summary>
        private bool bAutoAppReg;
        /// <summary>
        /// Watcher of WMIACPI class. Listening to the event.
        /// </summary>
        private ManagementEventWatcher watcher_acpi;
        /// <summary>
        /// Event callback functions.
        /// </summary>
        private SlideBarEventCallback sbEventCallback;
        /// <summary>
        /// Maybe the handle of current COM object.
        /// </summary>
        private IntPtr handle = IntPtr.Zero;
        /// <summary>
        /// Datas that are passed when an event occured.
        /// </summary>
        private SlideBar.SlideBarData sbData = default(SlideBar.SlideBarData);
        /// <summary>
        /// The event is raised when touching the slidebar.
        /// </summary>
        public event SlideBarEventHandler sbArriveEvent;

        #endregion


        #region Constructors and destructor

        public SlideBar(bool bAutoAppReg)
        {
            this.bAutoAppReg = bAutoAppReg;
            this.StartAllEventWatcher();
        }
        public SlideBar(SlideBarEventCallback callback, bool bAutoAppReg)
        {
            this.sbEventCallback = callback;
            this.bAutoAppReg = bAutoAppReg;
            this.StartAllEventWatcher();
        }
        public SlideBar(IntPtr handle, bool bAutoAppReg)
        {
            this.handle = handle;
            this.bAutoAppReg = bAutoAppReg;
            this.StartAllEventWatcher();
        }
        /// <summary>
        /// Remove all event watcher when disposing current object.
        /// </summary>
        ~SlideBar()
        {
            this.sbEventCallback = null;
            this.handle = IntPtr.Zero;
            this.StopAllEventWatcher();
        }

        #endregion

        /// <summary>
        /// Read byte[] IOBytes in WMPACPI_IO class
        /// </summary>
        /// <returns>WMPACPI_IO["IOBytes"]</returns>
        private byte[] ReadWMIACPI_IO()
        {
            try
            {
                ManagementScope scope = new ManagementScope(this.WMIACPINamespace);
                SelectQuery query = new SelectQuery(this.WMIACPIIOClass);
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        return (byte[])managementObject["IOBytes"];
                    }
                }
            }
            catch (ManagementException)
            {
            }
            return null;
        }

        /// <summary>
        /// Write the bytes into WMIACPI_IO
        /// changed to public due to debug.
        /// </summary>
        /// <param name="ioBytes">Raw bytes, length = 128</param>
        /// <returns>True if success.</returns>
        public bool WriteWMIACPI_IO(byte[] ioBytes)
        {
            try
            {
                if (ioBytes.GetLength(0) != (int)this.IOBytesLength)
                {
                    bool result = false;
                    return result;
                }
                SelectQuery query = new SelectQuery(this.WMIACPIIOClass);
                ManagementScope scope = new ManagementScope(this.WMIACPINamespace);
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        managementObject.SetPropertyValue("IOBytes", ioBytes);
                        managementObject.Put();
                        bool result = true;
                        return result;
                    }
                }
            }
            catch (ManagementException)
            {
            }
            return false;
        }

        /// <summary>
        /// Register the event.
        /// </summary>
        /// <param name="req">Registration type.</param>
        /// <param name="registration">Enable the light or not.</param>
        /// <returns>True if success.</returns>
        private bool SetAppRegistration(SlideBar.ApplicationRegistration req, bool registration)
        {
            try
            {
                byte[] array = new byte[(int)this.IOBytesLength];
                array[0] = 1;
                array[1] = 16;
                bool result;
                switch (req)
                {
                    case SlideBar.ApplicationRegistration.AP01_QuickButton0:
                        {
                            array[8] = 9;
                            break;
                        }
                    case SlideBar.ApplicationRegistration.AP02_Reserved:
                        {
                            array[8] = 10;
                            break;
                        }
                    default:
                        {
                            if (req != SlideBar.ApplicationRegistration.AP07_DebugConsole)
                            {
                                result = false;
                                return result;
                            }
                            array[8] = 15;
                            break;
                        }
                }
                array[9] = 3;
                array[10] = 0;
                if (registration)
                {
                    array[16] = 1;
                }
                else
                {
                    array[16] = 0;
                }
                result = this.WriteWMIACPI_IO(array);
                return result;
            }
            catch (ManagementException)
            {
            }
            return false;
        }

        /// <summary>
        /// Start the event watcher, always used in constructors.
        /// </summary>
        private void StartAllEventWatcher()
        {
            if (this.bAutoAppReg)
            {
                this.SetAppRegistration(SlideBar.ApplicationRegistration.AP02_Reserved, true);
            }
            this.MonitorWMIACPIEvent();
        }

        /// <summary>
        /// I changed this from private to public, due to some COM release problem.
        /// </summary>
        public void StopAllEventWatcher()
        {
            if (this.watcher_acpi != null)
            {
                this.watcher_acpi.Stop();
            }
        }

        /// <summary>
        /// Start monitoring the WMIACPIEvent.
        /// </summary>
        private void MonitorWMIACPIEvent()
        {
            try
            {
                WqlEventQuery query = new WqlEventQuery(this.WMIACPIEventClass);
                ManagementScope scope = new ManagementScope(this.WMIACPINamespace);
                this.watcher_acpi = new ManagementEventWatcher(scope, query);
                this.watcher_acpi.EventArrived += new EventArrivedEventHandler(this.HandleWMIACPIEvent);
                this.watcher_acpi.Start();
            }
            catch (ManagementException)
            {
            }
        }
        private void HandleWMIACPIEvent(object sender, EventArrivedEventArgs e)
        {
            try
            {
                byte[] input = (byte[])e.NewEvent["evtBytes"];
                this.sbData.bEnabled = input[2];
                this.sbData.bBreathEffect = input[21];
                this.sbData.bLightEffect = input[22];
                this.sbData.RawBytes = input;
                if (input[0] == 1 && input[1] == (byte)Event.SlideBar)
                {
                    this.sbData.bAction = (SlideBarAction)input[18];
                    this.sbData.bPosition = input[19];
                    this.sbData.bCurrentSpeed = input[20];
                    this.sbData.bEvent = Event.SlideBar;
                    if (this.sbArriveEvent != null)
                    {
                        this.sbArriveEvent(this.sbData);
                    }
                    if (this.sbEventCallback != null)
                    {
                        this.sbEventCallback(this.sbData);
                    }
                }
                ////////////////////////////////////////////////////////////
                // This is made by DOSSTONED, added the response of 一键影音
                else if (input[0] == 1 && input[1] == (byte)Event.ServiceKey)
                {
                    this.sbData.bAction = 0;
                    this.sbData.bPosition = 0;
                    this.sbData.bCurrentSpeed = 0;
                    this.sbData.bEvent = Event.ServiceKey;
                    if (this.sbArriveEvent != null)
                    {
                        this.sbArriveEvent(this.sbData);
                    }
                    if (this.sbEventCallback != null)
                    {
                        this.sbEventCallback(this.sbData);
                    }
                }
                else
                    throw new ArgumentException(
                        string.Format("evtBytes[0] = {0}\nevtBytes[1] = {1}",
                                input[0], 
                                input[1]));
                //
                ////////////////////////////////////////////////////////////
            }
            catch (ManagementException)
            {
            }
        }


        /// <summary>
        /// Set the slidebar status. Just like SetAppRegistration.
        /// This function is also written by DOSSTONED
        /// </summary>
        /// <param name="evt">Event to set.</param>
        /// <param name="enabled">Whether this event is enabled.</param>
        public byte[] SetSlideBarStatus(Event evt, bool enabled)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = (byte)evt;
            array[9] = 3;
            array[10] = 0;
            array[16] = (byte)(enabled ? 1 : 0);
            array[68] = (byte)(enabled ? 1 : 0);

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
            return array;
        }

        /// <summary>
        /// Gives the whole WMIACPI_IO["IOBytes"] object.
        /// </summary>
        /// <returns>IOBytes[128]</returns>
        public byte[] GetSlideBarStatus()
        {
            return this.ReadWMIACPI_IO();
        }
    }
}
