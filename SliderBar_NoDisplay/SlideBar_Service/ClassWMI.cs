using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace SlideBar_Service
{

    partial class BackGroundService
    {

        internal enum WMIACPI_IO_Status : byte
        {
            Unregistration,
            Registration,
            Disconnect = 16,
            Connect,
            LED_OFF = 32,
            LED_ON
        }

        private bool WriteWMIACPI_IO(WMIACPI_IO_Status status)
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

        ///// Set the initial state
        ///// 

        //ManagementObjectSearcher mox = new ManagementObjectSearcher(root_WMI, query_WMIACPI_IO);
        //ManagementObjectCollection mok = mox.Get();

        //TESTSTRING = "Query WMIACPI_IO successful.";

        //foreach (ManagementObject o in mok)
        //{
        //    byte[] curBytes = o.Properties["IOBytes"].Value as byte[];
        //    curBytes[84] = 1;
        //    curBytes[83] = 1;
        //    o.SetPropertyValue("IOBytes", curBytes);

        //    

        //    break; //only work on the first object
        //}
    }
}
