using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WMI_Test
{
    class Program
    {
       static byte[] lastBytes = new byte[128];
       static void Main(string[] args)
       {
           Console.SetWindowSize(Console.LargestWindowWidth - 5, Console.LargestWindowHeight - 1);
           PropertyDataCollection properties = null;
           System.Management.ManagementObjectSearcher mox = null;
           System.Management.ManagementObjectCollection mok = null;
           int repeatTimes = 0;

           try
           {
               //define scope (namespace)
               System.Management.ManagementScope x = new System.Management.ManagementScope("root\\WMI");

               //define query
               System.Management.SelectQuery q = new System.Management.SelectQuery("WMIACPI_IO");

               //output current brightness
               mox = new System.Management.ManagementObjectSearcher(x, q);

               mok = mox.Get();
               while (true)
               {
                   

                   using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = mox.Get().GetEnumerator())
                   {
                       if (enumerator.MoveNext())
                       {
                           ManagementObject managementObject = (ManagementObject)enumerator.Current;
                           
                           ConsoleKeyInfo ckey = Console.ReadKey();
                           if (ckey.Key == ConsoleKey.C) break;
                           if (ckey.Key == ConsoleKey.F)
                           {
                               managementObject.SetPropertyValue("Active", false);
                           }
                           else
                           {
                               if (ckey.Key == ConsoleKey.T)
                               {
                                   managementObject.SetPropertyValue("Active", true);
                               }
                               else
                               {
                                   Console.WriteLine((bool)managementObject["Active"]);
                               }
                           }
                           managementObject.Put();
                       }
                   }
                   
                   
               }

               while (true)
               {
                   System.Threading.Thread.Sleep(200);
                   mok = mox.Get();


                   foreach (System.Management.ManagementObject o in mok)
                   {
                       properties = o.Properties;
                       //o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness }); //note the reversed order - won't work otherwise!
                       break; //only work on the first object
                   }

                   //Console.WriteLine(properties["IOBytes"].Value);
                   PropertyData ioBytes = properties["IOBytes"];
                   byte[] bytes = ioBytes.Value as byte[];
                   //bytes[83] = 100;
                   //lastBytes = bytes;
                   //((byte[])ioBytes.Value)[83] = 4;
                   //((byte[])ioBytes.Value)[84] = 100;
                   int place = -1;
                   if (!isTheSame(bytes, out place))
                   {
                       if (++repeatTimes >= 10)
                       {
                           repeatTimes = 0;
                           Console.Clear();
                       }
                       string message =
                           "PLACE: " + place + "\r\n"
                           + BitConverter.ToString(bytes);

                       Console.WriteLine(message);



                   }
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.StackTrace);
               Console.WriteLine(ex.Message);
               Console.WriteLine(ex.Source);
           }
           finally
           {

               if (mox != null)
                   mox.Dispose();
               if (mok != null)
                   mok.Dispose();
           }
       }

        private static bool isTheSame(byte[] Byte_IN, out int place)
        {
            place = -1;
            for (int i = 0; i < Byte_IN.Length; i++)
                if (Byte_IN[i] != lastBytes[i])
                {
                    place = i;
                    //if (i == 83 || i == 84 || i == 85) continue;
                    lastBytes = Byte_IN;
                    return false;
                }
            
            return true;
        }


        internal enum WMIACPI_IO_Status : byte
        {
            Unregistration,
            Registration,
            Disconnect = 16,
            Connect,
            LED_OFF = 32,
            LED_ON
        }

        private static bool WriteWMIACPI_IO(byte place8, WMIACPI_IO_Status status)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = place8;
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
    }
}
