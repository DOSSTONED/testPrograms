using System;
using System.Collections.Generic;
using System.Text;
using Powerplan_AutoChange;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr activeGuidPtr = IntPtr.Zero;
            //uint res = PowerScheme.PowerGetActiveScheme(IntPtr.Zero, ref activeGuidPtr);
            //if (res == 0)
            //{
                Guid VideoSettingGuid = new Guid();
                UInt32 index = 0;
                UInt32 BufferSize = (UInt32)Marshal.SizeOf(typeof(Guid));
                while (0 == PowerScheme.PowerEnumerate(
                    IntPtr.Zero, activeGuidPtr, new Guid("7516b95f-f776-4464-8c53-06167f40cc99"), 18, index, ref VideoSettingGuid, ref BufferSize))
                {
                    Console.Write(VideoSettingGuid.ToString() + ": ");

                    UInt32 size = 1024;
                    IntPtr temp = Marshal.AllocHGlobal(1024);
                    IntPtr type = IntPtr.Zero;

                    PowerScheme.PowerReadACValue(IntPtr.Zero, activeGuidPtr, IntPtr.Zero, VideoSettingGuid, ref type, ref temp, ref size);

                    Console.Write(Marshal.PtrToStringUni(temp));
                    Marshal.FreeHGlobal(temp);
                    index++;
                }
            //}

                Console.ReadKey();



        }
    }
}
