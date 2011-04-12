using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using System.Speech.Synthesis;

namespace WMI_TEMP_TEST
{
    static class Program
    {
        static int counting = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try { RefreshReadings(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }

            Timer t = new Timer();
            t.Interval = 10000;
            //t.Tick += new EventHandler(t_Tick);
            //t.Start();
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            Application.Run();
        }

        static void t_Tick(object sender, EventArgs e)
        {
            RefreshReadings();
            if (_currentTemperature < _criticalTripPoint * 0.8)
            {
                counting++;
                if (counting > 30)
                {
                    counting = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (_currentTemperature < _criticalTripPoint * 0.9)
                {
                    counting++;
                    if (counting > 5)
                    {
                        counting = 0;
                    }
                    else
                    {
                        return;
                    }
                }

            }
                SpeechSynthesizer speaker = new SpeechSynthesizer();
                speaker.Volume = 100;
                speaker.SpeakAsync(((int)_currentTemperature).ToString() + "!");
                //throw new NotImplementedException();
            
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


        #region research
        //http://72.14.209.104/search?q=cache:MftJ-318iVoJ:download.microsoft.com/download/5/D/6/5D6EAF2B-7DDF-476B-93DC-7CF0072878E6/SMBIOS.doc+read+smbios+table&hl=en&gl=us&ct=clnk&cd=1
        //http://www.codeguru.com/cpp/misc/samples/systeminformation/article.php/c12347/
        //public static void GetSmBiosData()
        //{
        //    string s_Query = "SELECT * FROM MSSmBios_RawSMBiosTables";
        //    string s_CIM = "root\\WMI";

        //    //byte[] buffer;
        //    //int bufferSize;

        //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(s_CIM, s_Query))
        //    {
        //        RawSMBIOSData rawData = new RawSMBIOSData();

        //        foreach (ManagementObject obj in searcher.Get())
        //        {
        //            byte[] biosObject = (byte[])obj["SMBiosData"];
        //            using (MemoryStream stream = new MemoryStream(biosObject, 0, biosObject.Length))
        //            using (BinaryReader reader = new BinaryReader(stream))
        //            {
        //                //rawData reader.ReadByte( );
        //                rawData.Used20CallingMethod = reader.ReadByte();
        //                rawData.SMBIOSMajorVersion = reader.ReadByte();
        //                rawData.SMBIOSMinorVersion = reader.ReadByte();
        //                rawData.DmiRevision = reader.ReadByte();
        //                rawData.Length = reader.ReadUInt32();
        //                rawData.SMBIOSTableData = reader.ReadBytes((int)rawData.Length);
        //            }
        //        }
        //    }
        //}
        #endregion research
        //[StructLayout(LayoutKind.Sequential)]
        //struct RawSMBIOSData
        //{
        //    internal byte Used20CallingMethod;
        //    internal byte SMBIOSMajorVersion;
        //    internal byte SMBIOSMinorVersion;
        //    internal byte DmiRevision;
        //    internal uint Length;
        //    internal byte[] SMBIOSTableData;
        //}


        static double ConvertToCelsius(string reading)
        {
            //It's recorded in 10ths of Kelvin
            return (double.Parse(reading) / 10 - 273.15);
        }

    }
}
