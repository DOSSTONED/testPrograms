using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CS_ChangeIP_Route
{
    class ClassWmiIpChanger
    {
        static void SwitchToDHCP()
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;

                inPar = mo.GetMethodParameters("EnableDHCP");
                outPar = mo.InvokeMethod("EnableDHCP", inPar, null);
                break;
            }
        }

        static void SwitchToStatic()
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;

                inPar = mo.GetMethodParameters("EnableStatic");
                inPar["IPAddress"] = new string[] { "10.4.15.19" };
                inPar["SubnetMask"] = new string[] { "255.255.255.0" };
                outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                break;
            }
        }

        static void ReportIP()
        {
            Console.WriteLine("******   Current   IP   addresses:");
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;

                Console.WriteLine("{0}\n   SVC:   '{1}\n'   MAC:   [{2}]", (string)mo["Caption"],
                  (string)mo["ServiceName"], (string)mo["MACAddress"]);

                string[] addresses = (string[])mo["IPAddress"];
                string[] subnets = (string[])mo["IPSubnet"];

                Console.WriteLine("   Addresses   :");
                foreach (string sad in addresses)
                    Console.WriteLine("\t'{0}'", sad);

                Console.WriteLine("   Subnets   :");
                foreach (string sub in subnets)
                    Console.WriteLine("\t'{0}'", sub);
            }
        }
    }
}
