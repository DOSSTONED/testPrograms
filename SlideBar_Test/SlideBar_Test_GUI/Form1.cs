using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //sb1 = new SlideBar(true);
        }


        /*
        void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {


            labelbAction.Text = sbData.bAction.ToString();
            labelbCurSpeed.Text = sbData.bCurrentSpeed.ToString();
            labelbPosition.Text = sbData.bPosition.ToString();
            
            //_AudioEndpointVolume.SetMasterVolumeLevel((float)sbData.bPosition / 255, Guid.Empty);

            //SetBrightness((byte)((int)sbData.bPosition * 100 / 255));
        }
         */

        private void buttonBREATH_OFF_Click(object sender, EventArgs e)
        {
            //sb1.
            SetSlideBarStatus(1, 0);
        }

        private void buttonBREATH_ON_Click(object sender, EventArgs e)
        {
            //sb1.
            SetSlideBarStatus(1, 1);
        }

        private void buttonCONNECT_Click(object sender, EventArgs e)
        {
            //sb1.SetSlideBarStatus(1, 16);
        }

        private void buttonDISCONNECT_Click(object sender, EventArgs e)
        {
            //sb1.SetSlideBarStatus(1, 17);
        }

        private void buttonLED_Off_Click(object sender, EventArgs e)
        {
            //sb1.
            SetSlideBarStatus(1, 32);
        }

        private void buttonLED_On_Click(object sender, EventArgs e)
        {
            //sb1.
            SetSlideBarStatus(1, 33);
        }

        private void SetSlideBarStatus(int a, int b)
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
                        //properties = o.Properties;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //PropertyDataCollection properties = null;
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


                mok = mox.Get();

                foreach (System.Management.ManagementObject o in mok)
                {
                    byte[] curBytes = o.Properties["IOBytes"].Value as byte[];
                    curBytes[81] = 100;
                    o.SetPropertyValue("IOBytes", curBytes);
                    //properties = o.Properties;
                    //o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness }); //note the reversed order - won't work otherwise!
                    break; //only work on the first object
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


    }
}
