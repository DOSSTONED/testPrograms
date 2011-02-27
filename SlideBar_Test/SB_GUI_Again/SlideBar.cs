using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SBarHook
{
	public class SlideBar : InterfaceBase
	{
		private enum Event : byte
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
		private enum ServiceKey : byte
		{
			NoServiceKey,
			VolumeChanged,
			VolumeUp,
			VolumeDown,
			Mute,
			PlayPause,
			Stop,
			Previous,
			Next,
			LockKeys = 64,
			QuickKey1 = 128,
			QuickKey2,
			QuickKey3,
			QuickKey4
		}
		private enum Action : byte
		{
			NoAction,
			Update,
			Read,
			Write
		}
		private enum Notification : byte
		{
			NoNotification,
			Notification
		}
		private enum InputBufferDataIndex : byte
		{
			PasswordID,
			Password,
			Event = 8,
			Action,
			Notification,
			DataLength,
			Data = 16
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
		public enum SlideBarDataAddress : byte
		{
			SlideBarLightSwitch = 1
		}
		public struct SlideBarData
		{
			public byte bAction;
			public byte bPosition;
			public byte bCurrentSpeed;
		}
		private readonly string WMIACPINamespace = "root\\wmi";
		private readonly string WMIACPIEventClass = "WMIACPIEvent";
		private readonly string WMIACPIIOClass = "WMIACPI_IO";
		private readonly byte DeviceUnsupported = 255;
		private readonly byte IOBytesLength = 128;
		private readonly byte DataBlock1inIOByteOffset = 64;
		private bool bAutoAppReg;
		private ManagementEventWatcher watcher_acpi;
		private SlideBarEventCallback sbEventCallback;
		private IntPtr handle = IntPtr.Zero;
		private SlideBar.SlideBarData sbData = default(SlideBar.SlideBarData);
		public event SlideBarEventHandler sbArriveEvent
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.sbArriveEvent +=value;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.sbArriveEvent -= value;
			}
		}
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
		~SlideBar()
		{
			this.sbEventCallback = null;
			this.handle = IntPtr.Zero;
			this.StopAllEventWatcher();
		}
		private byte[] ReadWMIACPI_IO()
		{
			byte[] result = null;
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
						result = (byte[])managementObject["IOBytes"];
						return result;
					}
				}
                if (managementObjectSearcher != null)
                    managementObjectSearcher.Dispose();
			}
			catch (ManagementException arg_69_0)
			{
				ManagementException ex = arg_69_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_75_0)
			{
				Exception ex2 = arg_75_0;
				base.DebugInfoTrace(ex2);
			}
			//return null;
			return result;
		}
		private bool WriteWMIACPI_IO(byte[] ioBytes)
		{
			bool result = false;
			try
			{
				if (ioBytes.GetLength(0) != (int)this.IOBytesLength)
				{
					result = false;
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
						result = true;
						return result;
					}
				}
                if (managementObjectSearcher != null)
                    managementObjectSearcher.Dispose();
			}
			catch (ManagementException arg_84_0)
			{
				ManagementException ex = arg_84_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_90_0)
			{
				Exception ex2 = arg_90_0;
				base.DebugInfoTrace(ex2);
			}
			//return false;
			return result;
		}
		private bool SetAppRegistration(SlideBar.ApplicationRegistration req, bool registration)
		{
			bool result = false;
			try
			{
				byte[] array = new byte[(int)this.IOBytesLength];
				array[0] = 1;
				array[1] = 16;
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
			catch (ManagementException arg_6C_0)
			{
				ManagementException ex = arg_6C_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_76_0)
			{
				Exception ex2 = arg_76_0;
				base.DebugInfoTrace(ex2);
			}
			//return false;
			return result;
		}
		private void StartAllEventWatcher()
		{
			if (this.bAutoAppReg)
			{
				this.SetAppRegistration(SlideBar.ApplicationRegistration.AP02_Reserved, true);
			}
			this.MonitorWMIACPIEvent();
		}
		private void StopAllEventWatcher()
		{
			try
			{
				if (this.bAutoAppReg)
				{
					this.SetAppRegistration(SlideBar.ApplicationRegistration.AP02_Reserved, false);
				}
				if (this.watcher_acpi != null)
				{
					this.watcher_acpi.Stop();
				}
			}
			catch (ManagementException arg_26_0)
			{
				ManagementException ex = arg_26_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_30_0)
			{
				Exception ex2 = arg_30_0;
				base.DebugInfoTrace(ex2);
			}
		}
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
            catch (ManagementException arg_49_0)
            {
                ManagementException ex = arg_49_0;
                base.DebugInfoTrace(ex);
            }
            catch (Exception arg_53_0)
            {
                Exception ex2 = arg_53_0;
                base.DebugInfoTrace(ex2);
            }
		}
		private void HandleWMIACPIEvent(object sender, EventArrivedEventArgs e)
		{
			try
			{
				if (((byte[])e.NewEvent["evtBytes"])[0] == 1 && ((byte[])e.NewEvent["evtBytes"])[1] == 42)
				{
					this.sbData.bAction = ((byte[])e.NewEvent["evtBytes"])[18];
					this.sbData.bPosition = ((byte[])e.NewEvent["evtBytes"])[19];
					this.sbData.bCurrentSpeed = ((byte[])e.NewEvent["evtBytes"])[20];
                    //if (this.sbArriveEvent != null)
                    //{
                    //    this.sbArriveEvent(this.sbData);
                    //}
					if (this.sbEventCallback != null)
					{
						this.sbEventCallback(this.sbData);
					}
					if (this.handle != IntPtr.Zero)
					{
						Win32.CopySlideBarDataStruct copySlideBarDataStruct;
						copySlideBarDataStruct.dwData = (IntPtr)0;
						copySlideBarDataStruct.cbData = Marshal.SizeOf(typeof(SlideBar.SlideBarData));
						copySlideBarDataStruct.lpData = this.sbData;
						IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Win32.CopySlideBarDataStruct)));
						Marshal.StructureToPtr(copySlideBarDataStruct, intPtr, true);
						Win32.SendMessage(this.handle, 1025u, IntPtr.Zero, intPtr);
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
			catch (ManagementException arg_159_0)
			{
				ManagementException ex = arg_159_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_163_0)
			{
				Exception ex2 = arg_163_0;
				base.DebugInfoTrace(ex2);
			}
		}
		private bool CheckIfActionMatched(SlideBar.SlideBarAction action)
		{
			return this.sbData.bAction == (byte)action;
		}
		public bool SlideBarOn(ref int position)
		{
			position = (int)this.sbData.bPosition;
			return this.CheckIfActionMatched(SlideBar.SlideBarAction.On);
		}
		public bool SlideBarLeave(ref int position)
		{
			position = (int)this.sbData.bPosition;
			return this.CheckIfActionMatched(SlideBar.SlideBarAction.Leave);
		}
		public bool SlideBarMove(ref int position)
		{
			position = (int)this.sbData.bPosition;
			return this.CheckIfActionMatched(SlideBar.SlideBarAction.Move);
		}
		public bool SlideBarHover(ref int position)
		{
			position = (int)this.sbData.bPosition;
			return this.CheckIfActionMatched(SlideBar.SlideBarAction.Hover);
		}
		public bool SlideBarClick(ref int position)
		{
			position = (int)this.sbData.bPosition;
			return this.CheckIfActionMatched(SlideBar.SlideBarAction.Click);
		}
		public void SlideBarCurrentSpeed(ref int speed)
		{
			speed = (int)this.sbData.bCurrentSpeed;
		}
		public void GetSlideBarMaxPixels(ref int maxPixels)
		{
			maxPixels = 256;
		}
		public bool GetSlideBarStatus(byte address, ref byte value)
		{
			bool result = false;
			try
			{
				value = this.DeviceUnsupported;
				byte[] array = this.ReadWMIACPI_IO();
				if (array.Length != (int)this.IOBytesLength || array[(int)this.DataBlock1inIOByteOffset] != 1)
				{
					result = false;
					return result;
				}
				if (address == 1)
				{
					if (21 + this.DataBlock1inIOByteOffset + 1 > this.IOBytesLength)
					{
						result = false;
						return result;
					}
					value = array[(int)(21 + this.DataBlock1inIOByteOffset)];
					result = true;
					return result;
				}
			}
			catch (ManagementException arg_5F_0)
			{
				ManagementException ex = arg_5F_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_69_0)
			{
				Exception ex2 = arg_69_0;
				base.DebugInfoTrace(ex2);
			}
			//return false;
			return result;
		}
		public bool SetSlideBarStatus(byte address, byte value)
		{
			bool result = false;
			try
			{
				byte[] array = new byte[(int)this.IOBytesLength];
				array[0] = 1;
				array[1] = 16;
				array[9] = 3;
				array[10] = 0;
				if (address == 1)
				{
					array[8] = 43;
					switch (value)
					{
						case 0:
						case 1:
						{
							break;
						}
						default:
						{
							switch (value)
							{
								case 16:
								case 17:
								{
									break;
								}
								default:
								{
									switch (value)
									{
										case 32:
										{
											result = this.SetAppRegistration(SlideBar.ApplicationRegistration.AP02_Reserved, false);
											return result;
										}
										case 33:
										{
											result = this.SetAppRegistration(SlideBar.ApplicationRegistration.AP02_Reserved, true);
											return result;
										}
										default:
										{
											result = false;
											return result;
										}
									}
									break;
								}
							}
							break;
						}
					}
					array[16] = value;
					result = this.WriteWMIACPI_IO(array);
					return result;
				}
				result = false;
				return result;
			}
			catch (ManagementException arg_91_0)
			{
				ManagementException ex = arg_91_0;
				base.DebugInfoTrace(ex);
			}
			catch (Exception arg_9B_0)
			{
				Exception ex2 = arg_9B_0;
				base.DebugInfoTrace(ex2);
			}
			//return false;
			return result;
		}


        public bool SetSlideBarStatusREWRITE(byte address, byte value)
        {
            bool result = false;
            try
            {
                byte[] array = new byte[(int)this.IOBytesLength];
                array[0] = 1;
                array[1] = 16;
                array[9] = 3;
                array[10] = 0;
                if (address == 1)
                {
                    array[8] = value;
                    array[16] = 33;
                    result = this.WriteWMIACPI_IO(array);
                    return result;
                }
                result = false;
                return result;
            }
            catch (ManagementException arg_91_0)
            {
                ManagementException ex = arg_91_0;
                base.DebugInfoTrace(ex);
            }
            catch (Exception arg_9B_0)
            {
                Exception ex2 = arg_9B_0;
                base.DebugInfoTrace(ex2);
            }
            //return false;
            return result;
        }
	}
}
