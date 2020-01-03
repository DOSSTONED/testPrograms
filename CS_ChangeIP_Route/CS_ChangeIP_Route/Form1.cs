using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
//using System.Web.Routing;

namespace CS_ChangeIP_Route
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            try
            {
                ManagementObjectCollection moc = mc.GetInstances(); // This step use Protected COM interface
                foreach (ManagementObject mo in moc)
                {
                    if (!(bool)mo["IPEnabled"])
                        continue;
                    string str = (string)mo["Caption"];
                    if (str.Contains("VMware"))
                        continue;
                    comboBoxAdapter1.Items.Add((string)mo["Caption"]);
                    comboBoxAdapter2.Items.Add((string)mo["Caption"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxAdapter1_IPAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAdapter1_IPAuto.Checked)
            {
                textBoxAdapter1_Gateway.Enabled = false;
                textBoxAdapter1_IPAddress.Enabled = false;
                textBoxAdapter1_Mask.Enabled = false;
            }
            else
            {
                textBoxAdapter1_Gateway.Enabled = true;
                textBoxAdapter1_IPAddress.Enabled = true;
                textBoxAdapter1_Mask.Enabled = true;
                checkBoxAdapter1_DNSAuto.Checked = false;
            }
        }

        private void checkBoxAdapter1_DNSAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAdapter1_DNSAuto.Checked)
            {
                textBoxAdapter1_DNS1.Enabled = false;
                textBoxAdapter1_DNS2.Enabled = false;
                checkBoxAdapter1_IPAuto.Checked = true;
            }
            else
            {
                textBoxAdapter1_DNS1.Enabled = true;
                textBoxAdapter1_DNS2.Enabled = true;
            }
        }

        private void comboBoxAdapter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;
                if ((string)mo["Caption"] == comboBoxAdapter1.SelectedItem.ToString())
                {
                    string[] addresses = (string[])mo["IPAddress"];
                    string[] subnets = (string[])mo["IPSubnet"];
                    string[] gateways = (string[])mo["DefaultIPGateway"];
                    string[] dnses = (string[])mo["DNSServerSearchOrder"];
                    if (dnses.Length >= 1)
                        textBoxAdapter1_DNS1.Text = dnses[0];
                    if (dnses.Length >= 2)
                        textBoxAdapter1_DNS2.Text = dnses[1];
                    if (gateways != null)
                        textBoxAdapter1_Gateway.Text = gateways[0];
                    textBoxAdapter1_IPAddress.Text = addresses[0];
                    textBoxAdapter1_Mask.Text = subnets[0];
                }
            }
        }

        private void buttonAdapter1_Apply_Click(object sender, EventArgs e)
        {
            if (IsAdmin())
            {

                ManagementBaseObject inPar = null;
                ManagementBaseObject outPar = null;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (!(bool)mo["IPEnabled"])
                        continue;
                    if ((string)mo["Caption"] == comboBoxAdapter1.SelectedItem.ToString())
                    {
                        if (checkBoxAdapter1_DNSAuto.Checked)
                        {
                            inPar = mo.GetMethodParameters("EnableDHCP");
                            outPar = mo.InvokeMethod("EnableDHCP", inPar, null);
                            break;
                        }
                        else
                        {
                            try
                            {
                                if (checkBoxAdapter1_IPAuto.Checked == false)
                                {
                                    inPar = mo.GetMethodParameters("EnableStatic");
                                    inPar["IPAddress"] = new string[] { textBoxAdapter1_IPAddress.Text };
                                    inPar["SubnetMask"] = new string[] { textBoxAdapter1_Mask.Text };
                                    outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                                }
                                else
                                {
                                    inPar = mo.GetMethodParameters("EnableDHCP");
                                    outPar = mo.InvokeMethod("EnableDHCP", inPar, null);
                                }
                                //设置网关地址
                                inPar = mo.GetMethodParameters("SetGateways");
                                inPar["DefaultIPGateway"] = new string[] { textBoxAdapter1_Gateway.Text }; // 1.网关;2.备用网关
                                outPar = mo.InvokeMethod("SetGateways", inPar, null);

                                //设置DNS
                                inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                                inPar["DNSServerSearchOrder"] = new string[] { textBoxAdapter1_DNS1.Text, textBoxAdapter1_DNS2.Text }; // 1.DNS 2.备用DNS
                                outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                                break;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Please run as Administrator.", "Request Privileges", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RestartElevated();
                }
            }
        }

        private void buttonConfig1_Click(object sender, EventArgs e)
        {
            checkBoxAdapter1_DNSAuto.Checked = true;
        }

        private void buttonConfig2_Click(object sender, EventArgs e)
        {
            checkBoxAdapter1_IPAuto.Checked = true;
            textBoxAdapter1_DNS1.Text = "202.99.104.68";
            textBoxAdapter1_DNS2.Text = "156.154.71.22";
        }

        private void buttonConfig3_Click(object sender, EventArgs e)
        {
            checkBoxAdapter1_IPAuto.Checked = false;
            textBoxAdapter1_IPAddress.Text = "192.168.1.2";
            textBoxAdapter1_Mask.Text = "255.255.255.0";
            textBoxAdapter1_Gateway.Text = "192.167.1.1";
            textBoxAdapter1_DNS1.Text = "8.8.8.8";
            textBoxAdapter1_DNS2.Text = "156.154.71.22";
        }

        private void buttonConfig4_Click(object sender, EventArgs e)
        {
            checkBoxAdapter1_IPAuto.Checked = false;
            textBoxAdapter1_IPAddress.Text = "10.4.15.19";
            textBoxAdapter1_Mask.Text = "255.255.255.0";
            textBoxAdapter1_Gateway.Text = "10.4.15.19";
            textBoxAdapter1_DNS1.Text = "8.8.8.8";
            textBoxAdapter1_DNS2.Text = "156.154.71.22";
        }

        #region Related to privileges


        static internal bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static class VistaSecurity
        {
            [DllImport("user32")]
            public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

            internal const int BCM_FIRST = 0x1600;
            internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

            /**/
            /// <summary>
            /// Add a shield icon to a button
            /// </summary>
            /// <param name="b">The button</param>
            static internal void AddShieldToButton(Button b)
            {
                b.FlatStyle = FlatStyle.System;
                SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
            }
        }

        internal static void RestartElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            try
            {
                Process p = Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return; //If cancelled, do nothing
            }
            Application.Exit();
        }


        #endregion


        private void comboBoxAdapter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;
                if ((string)mo["Caption"] == comboBoxAdapter2.SelectedItem.ToString())
                {
                    string[] addresses = (string[])mo["IPAddress"];
                    string[] subnets = (string[])mo["IPSubnet"];
                    string[] gateways = (string[])mo["DefaultIPGateway"];
                    string[] dnses = (string[])mo["DNSServerSearchOrder"];
                    if (dnses.Length >= 1)
                        textBoxAdapter2_DNS1.Text = dnses[0];
                    if (dnses.Length >= 2)
                        textBoxAdapter2_DNS2.Text = dnses[1];
                    textBoxAdapter2_Gateway.Text = gateways[0];
                    textBoxAdapter2_IPAddress.Text = addresses[0];
                    textBoxAdapter2_Mask.Text = subnets[0];
                }
            }
        }

        private void buttonDisplayRouteTable_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "route";
            p.StartInfo.Arguments = "PRINT -4";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.StandardOutputEncoding = Encoding.ASCII;
            p.Start();
            MessageBox.Show(p.StandardOutput.ReadToEnd());
            
            
        }

    
    }
}
