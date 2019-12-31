using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace 恢复文件夹
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ClassWin6Privileges.IsAdmin())
            {
                ClassWin6Privileges.RestartElevated();
            }
            else
            {
                //string[] vbsfiles;
                StringBuilder vbsfiles = new StringBuilder();


                #region Recovery the directories

                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        string[] dirs = Directory.GetDirectories(drive.RootDirectory.ToString());
                        for (int i = 0; i < Directory.GetFiles(drive.RootDirectory.ToString(), "*.vbs", SearchOption.TopDirectoryOnly).Length; i++)
                            vbsfiles.Append(Directory.GetFiles(drive.RootDirectory.ToString(), "*.vbs", SearchOption.TopDirectoryOnly)[i]);
                        foreach (string dir in dirs)
                        {
                            DirectoryInfo dirinf = new DirectoryInfo(dir);
                            if (dirinf.Name == "Boot" || dirinf.Name == "PerfLogs" || dirinf.Name == "Recovery" || dirinf.Name == "Recycled" || dirinf.Name == "System Volume Information")
                                continue;
                            try
                            {
                                dirinf.Attributes -= FileAttributes.System;
                                dirinf.Attributes = FileAttributes.Normal;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }


                #endregion

                #region Repair Register

                try
                {

                    Registry.LocalMachine.SetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL\\CheckedValue", 1, RegistryValueKind.DWord);
                    Registry.LocalMachine.SetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL\\DefaultValue", 2, RegistryValueKind.DWord);

                    Registry.LocalMachine.SetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\NOHIDDEN\\CheckedValue", 1, RegistryValueKind.DWord);
                    Registry.LocalMachine.SetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\NOHIDDEN\\DefaultValue", 2, RegistryValueKind.DWord);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion


                #region Query Whether delete the .vbs files or not


                if (vbsfiles.Length != 0)
                    if (DialogResult.Yes == MessageBox.Show("Delete the vbs files?\n" + vbsfiles.ToString(), "Found vbs files!", MessageBoxButtons.YesNo))
                    {
                        try
                        {
                            for (int i = 0; i < vbsfiles.Length; i++)
                                File.Delete(vbsfiles[i].ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }



                #endregion
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!ClassWin6Privileges.IsAdmin())
                ClassWin6Privileges.VistaSecurity.AddShieldToButton(button1);
            
        }
    }
}
