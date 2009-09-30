using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOSSTONED_For_Personal_Use_
{
    partial class FormPop : Form
    {

        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x01;
        const int HTCAPTION = 0x02;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int WM_DEVICECHANGE = 0x219;
        const int WM_CREATE = 0x1;

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
        
        ////Public ProtectUDisk  = 0 // 1 for protected , 0 for not protected

        /*For Lesson list
        private string weekstr = "";
        private List<Lesson> Mon;
        private List<Lesson> Tue;
        private List<Lesson> Wed;
        private List<Lesson> Thu;
        private List<Lesson> Fri;
        private List<Lesson> Other;
        //Customized variables Finished
        private void SetListBoxItems(ListBox li, List<Lesson> le)
        {
            for (int i = 0; i < le.Count; i++)
                li.Items.Add(le[i].LName);
        }
        */
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
                return;
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
                                        //NotifyIconAutoRun.ShowBalloonTip(300, "发现威胁", "文件 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                        //else{if FirstString = ";DOSSTONED Authorised" {
                                        //    NotifyIconAutoRun.ShowBalloonTip(300, "U盘安全", RemoveableDevices.ToString.Remove(1, 2) + "盘已经通过DOSSTONED认证，可放心使用", ToolTipIcon.Info)
                                    }
                                    //FileReader.Close()

                                    if (Directory.Exists(AutorunFiles))
                                    {
                                        Directory.Delete(AutorunFiles);//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently);
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

        public FormPop()
        {
            InitializeComponent();
        }
       
        private void FormPop_Load(object sender, EventArgs e)
        {
            RunProtect();
            RunRepair();
            Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - Width - BorderPixel, System.Windows.Forms.SystemInformation.WorkingArea.Height - Height - BorderPixel);
            
            this.Text += DateTime.Now.DayOfWeek.ToString();
            /*
             switch (weekstr)  
             {
                 case "Monday": weekstr = "星期一"; break;
                 case "Tuesday": weekstr = "星期二"; break;
                 case "Wednesday": weekstr = "星期三"; break;
                 case "Thursday": weekstr = "星期四"; break;
                 case "Friday": weekstr = "星期五"; break;
                 case "Saturday": weekstr = "星期六"; break;
                 case "Sunday": weekstr = "星期日"; break;
             }
            
             MessageBox.Show(weekstr);
             
            SetLessonList();
            switch (weekstr)
            {
                case "Monday": weekstr = "星期一"; break;
                case "Tuesday": SetListBoxItems(listBox1, Tue);
                    break;
                case "Wednesday": weekstr = "星期三"; break;
                case "Thursday": weekstr = "星期四"; break;
                case "Friday": weekstr = "星期五"; break;
                case "Saturday":
                case "Sunday": break;
            }*/
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
                            s = 1;
                        }
                        if (File.Exists(CurrentDirectoriesInTop + ".exe"))
                        {
                            File.Delete(CurrentDirectoriesInTop + ".exe");//, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
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
                            }

                            if (CurrentDirectoriesInTop == (RemoveableDevices.ToString() + "$Recycle.bin"))
                            {
                                Directory.Delete(RemoveableDevices.ToString() + "$Recycle.bin");//, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                            }

                            else
                            {
                                DirectoryInfo DirInf = new DirectoryInfo(CurrentDirectoriesInTop);
                                DirInf.Attributes = FileAttributes.Normal;
                                DirInf.Attributes = FileAttributes.System;
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
                        listBox1.Items.Add(ex.ToString());
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
                ToolStripProtect.Checked = false;
                StatusProtect.Text = "Protection: Disabled";
                ProtectStatus = false;
            }
            else
            {
                CheckBoxProtect.Checked = true;
                ToolStripProtect.Checked = true;
                StatusProtect.Text = "Protection: Enabled";
                ProtectStatus = true;
            }

        }

        private void RunRepair()
        {
            if (RepairStatus)
            {
                CheckBoxRepair.Checked = false;
                ToolStripRepair.Checked = false;
                StatusRepair.Text = "Repair: Disabled";
                RepairStatus = false;
            }
            else
            {
                CheckBoxRepair.Checked = true;
                ToolStripRepair.Checked = true;
                StatusRepair.Text = "Repair: Enabled";
                RepairStatus = true;
            }
        }

        private void CheckBoxRepair_CheckedChanged(object sender, EventArgs e)
        {
            RunRepair();
        }

        private void CheckBoxProtect_CheckedChanged(object sender, EventArgs e)
        {
            RunProtect();
        }


        private void BtnAbt_Click(object sender, EventArgs e)
        {
            FormAbt frm = new FormAbt();
            frm.ShowDialog();
            //FormAbt.

        }

        private void NotifyIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();//处理鼠标左击事件 
            }
            //FormPop.ActiveForm.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormPop_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        /*
         * private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();   
     Brush myBrush = Brushes.Blue; //初始化字体颜色=黑色  
     this.listBox1.ItemHeight=58; //设置项高，根据具体需要设置值  
     //为每个项设置字体颜色  
     //如果不需要可以不写此switch  
      switch (e.Index)  
      {  
            case 0:  
                   myBrush = Brushes.Red;  
                  break;  
            case 1:  
                  myBrush = Brushes.Orange;  
                    break;  
            case 2:  
                   myBrush = Brushes.Purple;  
                   break;  
            case 4:  
                  myBrush = Brushes.White;  
                   break;  
       }   
       e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, myBrush,e.Bounds,null);  
      //这句好象可以不要，自己试下  
       e.DrawFocusRectangle();    
        }
        */


    }
}

