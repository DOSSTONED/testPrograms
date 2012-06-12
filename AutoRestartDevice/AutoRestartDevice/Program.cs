using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AutoRestartDevice
{

    class Program
    {
        static void Main(string[] args)
        {
            SystemEvents.PowerModeChanged += OnPowerChange;
            //int i = 5;
            //DateTime _start = DateTime.Now, _end = DateTime.Now;
            //double total = 0;
            //DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              false);
            //while (i-- > 0)
            //{
            //    _start = DateTime.Now;
            //    DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              true);
            //    _end = DateTime.Now;
            //    total += (_end - _start).TotalSeconds;
            //    DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              false);
            //}
            //Console.WriteLine("Disable consumes {0}s.", total / 5);

            //total = 0;
            //i = 5;
            //DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              true);
            //while (i-- > 0)
            //{
            //    _start = DateTime.Now;
            //    DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              false);
            //    _end = DateTime.Now;
            //    total += (_end - _start).TotalSeconds;
            //    DisableHardware.DisableDevice(
            //              n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
            //              true);
            //}
            //Console.WriteLine("Enable consumes {0}s.", total / 5);
            
            /// 总计耗时关闭设备0.2秒，开启设备0.27秒

            Thread.Sleep(int.MaxValue);
        }

        static void OnPowerChange(Object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    DisableHardware.DisableDevice(
                          n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
                          true);
                    //Thread.Sleep(1000);
                    DisableHardware.DisableDevice(
                          n => n.ToUpperInvariant().Contains(@"USB\VID_0EEF&PID_0001&REV_0100"),
                          false);
                    break;
            }
        }

    }
}
