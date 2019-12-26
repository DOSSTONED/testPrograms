using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AppPath_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAppPathsData(sender, e);
            VistaSecurity.AddShieldToButton(buttonGetPrivilege);
            if (!IsAdmin())
            {
                buttonApply.Enabled = false;
                buttonClear.Enabled = false;
                groupBox1.Text = "User Mode";
                
                //MessageBox.Show("The program is now run under user mode, you only have rights to read the values.");
            }
            else
            {
                buttonGetPrivilege.Visible = false;
                groupBox1.Text = "Administrator Mode";
            }
        }

        private void GetAppPathsData(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RegistryKey AppPathsReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths");

            if (AppPathsReg != null)
            {
                foreach (string AppPathsKey in AppPathsReg.GetSubKeyNames())
                {
                    RegistryKey CurAppPathsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + AppPathsKey);
                    if (((string)CurAppPathsKey.GetValue(null) != string.Empty) && (CurAppPathsKey.GetValue(null) != null))
                    {
                        listBox1.Items.Add(AppPathsKey);
                    }
                }
            }
            if (AppPathsReg != null)
                AppPathsReg.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = listBox1.SelectedItem.ToString();
            RegistryKey SelectAppPathsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + textBoxName.Text);
            if (SelectAppPathsKey.GetValue(null) != null)
            {
                textBoxImage.Text = SelectAppPathsKey.GetValue(null).ToString();
            }
            if (SelectAppPathsKey != null)
                SelectAppPathsKey.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            RegistryKey SelectAppPathsKey = null;
            try
            {
                SelectAppPathsKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + textBoxName.Text);
                SelectAppPathsKey.SetValue(null, (string)textBoxImage.Text, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SelectAppPathsKey != null)
                    SelectAppPathsKey.Close();
            }
            GetAppPathsData(sender, e);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            RegistryKey SelectAppPathsKey = null;
            try
            {
                SelectAppPathsKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + textBoxName.Text);
                SelectAppPathsKey.SetValue(null, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SelectAppPathsKey != null)
                    SelectAppPathsKey.Close();
            }
            GetAppPathsData(sender, e);
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

        private void buttonGetPrivilege_Click(object sender, EventArgs e)
        {
            
            RestartElevated();
        }
    }
}
