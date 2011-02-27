using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SB_GUI_Again
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                    break; 
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

        SBarHook.SlideBar sb1 = new SBarHook.SlideBar(true);

        private void buttonBREATH_ON_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 1);
        }

        private void buttonBREATH_OFF_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 0);
        }

        private void buttonDISCONNECT_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 17);
        }

        private void buttonCONNECT_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 16);
        }

        private void buttonLED_OFF_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 33);
        }

        private void buttonLED_ON_Click(object sender, EventArgs e)
        {
            sb1.SetSlideBarStatus(1, 32);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                sb1.SetSlideBarStatusREWRITE(1, (byte)i);
            }
        }
    }
}
