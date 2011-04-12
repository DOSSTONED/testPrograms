using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WMI_TEMP_CON
{
    class Program
    {
        static double ConvertToCelsius(string reading)
        {
            //It's recorded in 10ths of Kelvin
            return (double.Parse(reading) / 10 - 273.15);

        }
        static double _currentTemperature;
        static double _criticalTripPoint;
        static double _thermalStamp;

        internal static void RefreshReadings()
        {
            string s_Query = "SELECT * FROM MSAcpi_ThermalZoneTemperature";
            string s_CIM = "root\\WMI";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(s_CIM, s_Query))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    _currentTemperature = ConvertToCelsius(obj["CurrentTemperature"].ToString());
                    _criticalTripPoint = ConvertToCelsius(obj["CriticalTripPoint"].ToString());
                    _thermalStamp = double.Parse(obj["ThermalStamp"].ToString());
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            RefreshReadings();
            Console.Write(_currentTemperature);
            Console.ReadKey();
        }
    }
}
