/// BY DOSSTONED
/// BUGS...
/// AND MUTILTHREADS...
/// 



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;
//using SBarHook;

namespace SlideBar_Test_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //static SlideBar sb1 = null;
        public ManagementEventWatcher watcher = null;
        //private WMIReceiveEvent wmiEvt = new WMIReceiveEvent("AutoReg");

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementScope x = new System.Management.ManagementScope("root\\WMI");

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM WMIACPIEvent");

            watcher = new ManagementEventWatcher(x, query);
            Console.WriteLine("Waiting for an event...");

            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);

            // Start listening for events
            watcher.Start();
        }

        void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject mbo = e.NewEvent;
            byte[] EvtBytes = mbo["EvtBytes"] as byte[];

            if (InvokeRequired)
            {
                /// do some delegate to change the label
                /// 
                
                Invoke(new setLablesDelegate(setLables), new object[] { labelbAction, EvtBytes[18] });
                Invoke(new setLablesDelegate(setLables), new object[] { labelbCurSpeed, EvtBytes[20] });
                Invoke(new setLablesDelegate(setLables), new object[] { labelbPosition, EvtBytes[19] });
            }
            else
            {

                setLables(labelbAction, EvtBytes[18]);
                setLables(labelbCurSpeed, EvtBytes[20]);
                setLables(labelbPosition, EvtBytes[19]);
            }
        }

        private delegate void setLablesDelegate(Label l, byte byte_to_set);
        private void setLables(Label l, byte byte_to_set)
        {
            l.Text = byte_to_set.ToString();
        }

        private void buttonBREATH_OFF_Click(object sender, EventArgs e)
        {
            if (checkBoxMethod.Checked)
                SetSlideBarStatus(0);
            else
                SetSlideBarStatus(1, WMIACPI_IO_Status.Unregistration);
        }

        private void buttonBREATH_ON_Click(object sender, EventArgs e)
        {

            if (checkBoxMethod.Checked)
                SetSlideBarStatus(1);
            else
                SetSlideBarStatus(1, WMIACPI_IO_Status.Registration);
        }

        private void buttonCONNECT_Click(object sender, EventArgs e)
        {
            if (checkBoxMethod.Checked)
                SetSlideBarStatus(16);
            else
                SetSlideBarStatus(1, WMIACPI_IO_Status.Connect);
        }

        private void buttonDISCONNECT_Click(object sender, EventArgs e)
        {
            if (checkBoxMethod.Checked)
                SetSlideBarStatus(17);
            else
                SetSlideBarStatus(1,WMIACPI_IO_Status.Disconnect);
        }

        private void buttonLED_Off_Click(object sender, EventArgs e)
        {
            if (checkBoxMethod.Checked)
                SetSlideBarStatus(32);
            else
                SetSlideBarStatus(1, WMIACPI_IO_Status.LED_OFF);
        }

        private void buttonLED_On_Click(object sender, EventArgs e)
        {
            if (checkBoxMethod.Checked)
                SetSlideBarStatus(33);
            else
                SetSlideBarStatus(1, WMIACPI_IO_Status.LED_ON);
        }

        private void SetSlideBarStatus(int a, int b) // I just write this in a single function to control instead of using SBarHook.
        {
            if (a != 1)
                return;



            PropertyDataCollection properties = null;
            System.Management.ManagementObjectSearcher mox = null;
            System.Management.ManagementObjectCollection mok = null;


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
                    mok = mox.Get();

                    foreach (System.Management.ManagementObject o in mok)
                    {
                        byte[] curBytes = o.Properties["IOBytes"].Value as byte[];
                        if (b == 32 || b == 33)
                            curBytes[84] = (byte)(b % 2);
                        if (b == 0 || b == 1)
                            curBytes[83] = (byte)(b % 2);
                        //curBytes[85] = 1;
                        //curBytes[66] = 4;
                        o.SetPropertyValue("IOBytes", curBytes);
                        o.Put();
                        break; //only work on the first object
                    }

                    //Console.WriteLine(properties["IOBytes"].Value);
                    PropertyData ioBytes = properties["IOBytes"];
                    byte[] bytes = ioBytes.Value as byte[];
                    //bytes[83] = 100;
                    //lastBytes = bytes;
                    //((byte[])ioBytes.Value)[83] = 4;
                    //((byte[])ioBytes.Value)[84] = 100;
                    //int place = -1;
                    //if (!isTheSame(bytes, out place))
                    //{
                    //    Console.WriteLine("PLACE: " + place);
                    //    Console.WriteLine(BitConverter.ToString(bytes));
                    //}
                    //if (bytes[83] < 3) break;
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

        internal enum WMIACPI_IO_Status : byte
        {
            Unregistration,
            Registration,
            Disconnect = 16,
            Connect,
            LED_OFF = 32,
            LED_ON
        }
        private bool SetSlideBarStatus(int a, WMIACPI_IO_Status status)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = (byte)numericUpDown1.Value;
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

        public bool SetSlideBarStatus(byte value)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[9] = 3;
            array[10] = 0;

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
                                                return this.SetAppRegistration(ApplicationRegistration.AP02_Reserved, false);
                                            }
                                        case 33:
                                            {
                                                return this.SetAppRegistration(ApplicationRegistration.AP02_Reserved, true);
                                            }
                                        default:
                                            {
                                                return false;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }

                    array[16] = value;
                    return this.WriteWMIACPI_IO(array);
            }

            return false;
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
        private bool SetAppRegistration(ApplicationRegistration req, bool registration)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            switch (req)
            {
                case ApplicationRegistration.AP01_QuickButton0:
                    {
                        array[8] = 9;
                        break;
                    }
                case ApplicationRegistration.AP02_Reserved:
                    {
                        array[8] = 10;
                        break;
                    }
                default:
                    {
                        if (req != ApplicationRegistration.AP07_DebugConsole)
                        {
                            return false;
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
            return WriteWMIACPI_IO(array);
        }
        private bool WriteWMIACPI_IO(byte[] ioBytes)
        {

                if (ioBytes.GetLength(0) != 128)
                {
                    return false;
                }
                SelectQuery query = new SelectQuery("WMIACPI_IO");
                ManagementScope scope = new ManagementScope("root\\WMI");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        managementObject.SetPropertyValue("IOBytes", ioBytes);
                        managementObject.Put();
                        
                        return true;
                    }
                }
                if (managementObjectSearcher != null)
                    managementObjectSearcher.Dispose();

            return false;
        }


        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is just a test GUI for Slidebar.\r\nI didn't write the eventhandler of receving the message from WMIACPI_IO, it's more like a controller.", "Enjoy");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (watcher != null)
            {
                watcher.Stop();
            }
        }

        private void buttonSet8And68_Click(object sender, EventArgs e)
        {
            byte[] array = new byte[128];
            array[0] = 1;
            array[1] = 16;
            array[8] = (byte)numericUpDown1.Value;
            array[9] = 3;
            array[10] = 0;
            array[16] = (byte)numericUpDown2.Value;

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

        }


    }
}
