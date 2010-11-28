using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Org.Mentalis.Proxy.Http;

namespace PortProxy_46
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }


        HttpListener lis;
        private HttpListener Lis
        {
            get
            { 
                return lis; 
            }
            set 
            {
                if (lis != null)
                {
                    lis.Dispose();
                }
                lis = value;

            }
        }



        private void buttonStartProxy_Click(object sender, EventArgs e)
        {
            //System.Net.IPAddress[] test = System.Net.Dns.GetHostEntry("10.88.1.253").AddressList;
            if (buttonStartProxy.Text == "Start Proxy")
            {
                int por = 0;
                //string port = textBoxPort.Text;
                try
                {
                    por = Convert.ToInt32(numericUpDownPort.Value);//Convert.ToInt32(port);
                    lis = new HttpListener(por);
                   
                    lis.Start();
                    buttonStartProxy.Text = "Stop Proxy";
                    numericUpDownPort.Enabled = false;


                }
                catch(Exception ex)
                {
                    MessageBox.Show("Port number wrong!\n"+ex.Message);
                }
            }
            else
            {
                buttonStartProxy.Text = "Start Proxy";
                numericUpDownPort.Enabled = true;

                if (lis != null)
                {
                    lis.Dispose();
                }
            }
        }
    }
}
