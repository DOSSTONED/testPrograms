using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Notes
{
    public partial class FormNotes : Form
    {
        private string ForePath = string.Empty;


        public FormNotes()
        {
            InitializeComponent();
        }

        private void FormNotes_Load(object sender, EventArgs e)
        {
            RunProtect();
            RunRepair();
            int marginl = 100;
            int margint = 50;
            Left = SystemInformation.WorkingArea.Width - Width - marginl;
            Top = margint;
            comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " Program Started");
            // Left = SystemInformation.WorkingArea.Width - Width;
            // Top = SystemInformation.WorkingArea.Height - Height;
            string[] Notes = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\..\\Notes", "*.txt");
            foreach (string Note in Notes)
            {

                tabControl1.TabPages.Add(Path.GetFileNameWithoutExtension(Note));

            }
            if (tabControl1.TabPages.Count >= 2)
            {
                tabControl1.SelectedIndex = 1;
                tabControl1.SelectedIndex = 0;
            }
        }


        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("New Tab " + tabControl1.TabPages.Count.ToString());
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// Save the content to note
            /// 

            if (ForePath != string.Empty)
            {
                File.WriteAllText(ForePath, textBoxNote.Text, Encoding.Default);
            }

            /*
            if (ForePath != string.Empty)
            {
                StreamWriter sw = File.CreateText(ForePath);
                if (sw == null)
                {
                    MessageBox.Show(new FileLoadException().Message);
                }
                else
                {
                    sw.Write(textBoxNote.Text);
                }
                sw.Close();
            }
            */

            /// Read the note to content
            /// 

            string NotePath = Directory.GetCurrentDirectory() + "\\..\\Notes\\" + tabControl1.SelectedTab.Text + ".txt";
            if (File.Exists(NotePath))
            {
                textBoxNote.Text = File.ReadAllText(NotePath, Encoding.Default);
            }
            else
            {
                FileStream fs = File.Create(NotePath);
                fs.Close();
            }

            // Change the ForePath
            ForePath = NotePath;
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                // tabControl1.// 怎么实现获得Preview的那个标签页啊？
            }
        }





        #region FlashDisk Protector

        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x01;
        const int HTCAPTION = 0x02;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        
        const int WM_DEVICECHANGE = 0x219;
        const int WM_CREATE = 0x1;

        /// <summary>
        ///  For middle button events
        /// </summary>
        const int WM_MBUTTONDOWN = 0x207;
        const int MK_MBUTTON = 0x10;
        

        const int DBT_DEVICEARRIVAL = 0x8000;
        const int DBT_CONFIGCHANGECANCELED = 0x19;
        const int DBT_CONFIGCHANGED = 0x18;
        const int DBT_CUSTOMEVENT = 0x8006;
        const int DBT_DEVICEQUERYREMOVE = 0x8001;
        const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        const int DBT_DEVICEREMOVEPENDING = 0x8003;
        const int DBT_DEVICETYPESPECifIC = 0x8005;
        const int DBT_DEVNODES_CHANGED = 0x7;
        const int DBT_QUERYCHANGECONFIG = 0x17;
        const int DBT_USERDEFINED = 0xFFFF;

        const int BorderPixel = 10;
        bool ProtectStatus = false;
        bool RepairStatus = false;



        protected override void WndProc(ref Message m)
        {
            /// the codes below is tested for middle button press down event
            /// no WParam is needed for the click event
            /// 

            //if (m.Msg == WM_MBUTTONDOWN)// && m.WParam.ToInt32() == MK_MBUTTON)
            //{
            //    MessageBox.Show("喂，别拽我！");
            //    return;
            //}


            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    //case WM_DEVICECHANGE

                    case DBT_DEVICEARRIVAL:
                        //MessageBox.Show("Inserted");
                        //U盘插入
                        //if ProtectUDisk <> 0 {
                        //ListBox.Items.Add("FlashDisk Inserted");
                        try
                        {
                            DriveInfo[] AllDrives = DriveInfo.GetDrives();
                            string AutorunFiles;

                            foreach (DriveInfo RemoveableDevices in AllDrives)
                            {
                                if (RemoveableDevices.DriveType == DriveType.Removable && RemoveableDevices.IsReady == true && ProtectStatus)
                                {   //IO.DriveType.Fixed for harddisk
                                    //MsgBox(RemoveableDevices.ToString()-":\"+"盘已插入")


                                    AutorunFiles = RemoveableDevices.ToString() + "Autorun.inf";

                                    ////////////////////////////////////////////////
                                    //Using fs As New System.IO.FileStream(AutorunFiles, IO.FileMode.Open)
                                    //Using md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
                                    //Dim hash As Byte() = md5.ComputeHash(fs)
                                    //NotifyIconAutoRun.ShowBalloonTip(300, "Hash", BitConverter.ToString(hash), ToolTipIcon.Info)
                                    //MsgBox(BitConverter.ToString(hash))
                                    //LEnd Using
                                    //LEnd Using
                                    //////////////////////////////////////////////////////
                                    //Dim FileReader As System.IO.StreamReader

                                    //FileReader = My.Computer.FileSystem.OpenTextFileReader(AutorunFiles)
                                    //Dim FirstString As String = FileReader.ReadLine()
                                    if (File.Exists(AutorunFiles))
                                    {
                                        //    FileReader.Close()
                                        File.Delete(AutorunFiles);//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently);
                                        comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + AutorunFiles + " Deleted");
                                        //NotifyIconAutoRun.ShowBalloonTip(300, "发现威胁", "文件 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                        //else{if FirstString = ";DOSSTONED Authorised" {
                                        //    NotifyIconAutoRun.ShowBalloonTip(300, "U盘安全", RemoveableDevices.ToString.Remove(1, 2) + "盘已经通过DOSSTONED认证，可放心使用", ToolTipIcon.Info)
                                    }
                                    //FileReader.Close()

                                    if (Directory.Exists(AutorunFiles))
                                    {
                                        Directory.Delete(AutorunFiles);//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently);
                                        comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + AutorunFiles + " Deleted");
                                        // NotifyIconAutoRun.ShowBalloonTip(300, "发现异常", "目录 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                    }



                                    RecoverDir(RepairStatus, RemoveableDevices);
                                }
                                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                //if CheckBoxAddFile.Checked = true {
                                //Dim FileReader As System.IO.FileStream = System.IO.File.Create(AutorunFiles)
                                //FileReader.Write()
                                //Dim Files() As String = System.IO.Directory.GetFiles(RemoveableDevices.ToString, "*.DOSSTONED")
                                //Dim CurrentFile As String
                                //For Each CurrentFile In Files
                                //My.Computer.FileSystem.CopyFile(CurrentFile, AutorunFiles)
                                //Next

                                //}
                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    // }
                    /*
                    case DBT_CONFIGCHANGECANCELED
                    case DBT_CONFIGCHANGED
                    case DBT_CUSTOMEVENT
                    case DBT_DEVICEQUERYREMOVE
                    case DBT_DEVICEQUERYREMOVEFAILED
                    case DBT_DEVICEREMOVECOMPLETE //U盘卸载
                        //NotifyIconAutoRun.ShowBalloonTip(300, "DOSSTONED", "U盘卸载！", ToolTipIcon.Info)
                    case DBT_DEVICEREMOVEPENDING
                    case DBT_DEVICETYPESPECifIC
                    case DBT_DEVNODES_CHANGED
                    case DBT_QUERYCHANGECONFIG
                    case DBT_USERDEFINED
                    */
                }
            }

            //if (m.Msg == WM_CREATE)
            //{
            //    ListBox.Items.Add("Window Created")
            //}

            base.WndProc(ref m);
        }

        private void RecoverDir(bool status, DriveInfo RemoveableDevices)
        {

            if (status)
            {
                string[] DirectoriesInTop = Directory.GetDirectories(RemoveableDevices.ToString());
                int s = 0;
                foreach (string CurrentDirectoriesInTop in DirectoriesInTop)
                {
                    try
                    {
                        if (File.Exists(CurrentDirectoriesInTop + ".scr"))
                        {
                            File.Delete(CurrentDirectoriesInTop + ".scr");//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                            comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + CurrentDirectoriesInTop + ".scr" + " Deleted");
                            s = 1;
                        }
                        if (File.Exists(CurrentDirectoriesInTop + ".exe"))
                        {
                            File.Delete(CurrentDirectoriesInTop + ".exe");//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                            comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + CurrentDirectoriesInTop + ".exe" + " Deleted");
                            s = 1;
                        }

                        if (s == 1)
                        {

                            //SetAttr(CurrentDirectoriesInTop, FileAttribute.Normal)
                            //SetAttr(CurrentDirectoriesInTop, FileAttribute.System)
                            //ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Recovered")
                            //删除Recycled程序


                            if (File.Exists(RemoveableDevices.ToString() + "Recycled.exe"))
                            {
                                File.Delete(RemoveableDevices.ToString() + "Recycled.exe");//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + RemoveableDevices.ToString() + "Recycled.exe" + " Deleted");
                            }

                            if (CurrentDirectoriesInTop == (RemoveableDevices.ToString() + "$Recycle.bin"))
                            {
                                Directory.Delete(RemoveableDevices.ToString() + "$Recycle.bin");//, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + RemoveableDevices.ToString() + "$Recycle.bin" + " Deleted");
                            }

                            else
                            {
                                DirectoryInfo DirInf = new DirectoryInfo(CurrentDirectoriesInTop);
                                DirInf.Attributes = FileAttributes.Normal;
                                DirInf.Attributes = FileAttributes.System;
                                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " " + CurrentDirectoriesInTop + " Recoverd");
                                //Dim fs, f;
                                //fs = CreateObject("Scripting.FileSystemObject");
                                //f = fs.GetFolder(CurrentDirectoriesInTop);
                                //f.Attributes = 4;//用Attributes函数设置文件夹属性
                            }
                        }
                    }
                    //ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Detected")
                    catch (Exception ex)
                    {
                        comboBoxEvent.Items.Add(ex.ToString());
                        //MessageBox.Show(ex.ToString());
                    }



                }
            }
        }


        private void RunProtect()
        {

            if (ProtectStatus)
            {
                CheckBoxProtect.Checked = false;
                // ToolStripProtect.Checked = false;
                // StatusProtect.Text = "No Protect";
                ProtectStatus = false;
                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " Protect Disabled");
            }
            else
            {
                CheckBoxProtect.Checked = true;
                // ToolStripProtect.Checked = true;
                // StatusProtect.Text = "Protect";
                ProtectStatus = true;
                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " Protect Enabled");
            }

        }

        private void RunRepair()
        {
            if (RepairStatus)
            {
                CheckBoxRepair.Checked = false;
                // ToolStripRepair.Checked = false;
                // StatusRepair.Text = "No Repair";
                RepairStatus = false;
                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " Repair Disabled");
            }
            else
            {
                CheckBoxRepair.Checked = true;
                // ToolStripRepair.Checked = true;
                // StatusRepair.Text = "Repair";
                RepairStatus = true;
                comboBoxEvent.Items.Add(DateTime.Now.ToLongTimeString() + " Repair Enabled");
            }
        }

        private void CheckBoxProtect_CheckedChanged(object sender, EventArgs e)
        {
            RunProtect();
        }

        private void CheckBoxRepair_CheckedChanged(object sender, EventArgs e)
        {
            RunRepair();
        }

        #endregion

        private void FormNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ForePath != string.Empty)
            {
                string NotePath = Directory.GetCurrentDirectory() + "\\..\\Notes\\" + tabControl1.SelectedTab.Text + ".txt";
            
                File.WriteAllText(NotePath, textBoxNote.Text, Encoding.Default);
            }
        }



    }
}
