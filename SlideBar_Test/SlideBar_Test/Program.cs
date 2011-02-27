///
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/*
0-3	3
4-40	36
41-75	34
76-113	37
114-150	36
151-185	34
186-220	34
221-253	32
254-255	2

*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBarHook;
using System.Runtime.InteropServices;
//using CoreAudioApi;

namespace SlideBar_Test
{
    //[Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //public interface IAudioEndpointVolume
    //{
    //    [PreserveSig]
    //    int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);
    //}



    class Program
    {
        //static private MMDevice device = null;
        
        static SlideBar sb1 = null;
        static void Main(string[] args)
        {
            //SlideBar.SlideBarData sbData = new SlideBar.SlideBarData();
            //SlideBar sb = new SlideBar(false);
            //sb.DebugInfoEvent += new DebugInfoHandler(sb_DebugInfoEvent);
            //sb.sbArriveEvent += new SlideBarEventHandler(sb_sbArriveEvent);
Console.WriteLine("initialising...");
            
            sb1 = new SlideBar(new SlideBarEventCallback(sb_sbArriveEvent), true);
            sb1.DebugInfoEvent+=new DebugInfoHandler(sb1_DebugInfoEvent);
            Console.WriteLine("initialised.");
            
            /// control the breath effect of the 2 lights
            /// BREATH_OFF
            //sb1.SetSlideBarStatus(1, 0);
            /// BREATH_ON
            //sb1.SetSlideBarStatus(1, 1);

            /// CONNECT
            //sb1.SetSlideBarStatus(1, 16);
            /// DISCONNECT
            //sb1.SetSlideBarStatus(1, 17);

            /// LED_ON
            //sb1.SetSlideBarStatus(1, 32);
            /// LED_OFF
            //sb1.SetSlideBarStatus(1, 33);


            //MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            //device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            //Console.WriteLine(device.AudioEndpointVolume.MasterVolumeLevelScalar);

                Console.ReadKey();
        }
        

        static void sb1_DebugInfoEvent(string dbgInfo)
        {
            Console.WriteLine(dbgInfo);//throw new NotImplementedException();
        }

        static void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {

            //if (device != null)
                //device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)sbData.bPosition / 255;

            Console.WriteLine("bAction:\t" + sbData.bAction.ToString());
            Console.WriteLine("bCurSpeed:\t" + sbData.bCurrentSpeed);
            Console.WriteLine("bPosition:\t" + sbData.bPosition.ToString());
            Win32.CopySlideBarDataStruct st;

            //_AudioEndpointVolume.SetMasterVolumeLevel((float)sbData.bPosition / 255, Guid.Empty);
            
            //SetBrightness((byte)((int)sbData.bPosition * 100 / 255));
        }

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
