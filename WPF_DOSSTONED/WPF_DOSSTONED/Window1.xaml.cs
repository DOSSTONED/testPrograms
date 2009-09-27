using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Win32;

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;

*/

namespace WPF_DOSSTONED
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// 定义常量
        /// </summary>
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

        const int BorderPixel = 6;
        bool ProtectStatus = false;
        bool RepairStatus = false;

        /// <summary>
        /// 以下定义函数
        /// </summary>
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DayLesson.Header = DateTime.Now.DayOfWeek.ToString();
            DisplayList(DateTime.Now.DayOfWeek.ToString());
            RunProtect();
            RunRepair();
            //Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - Width - BorderPixel, System.Windows.Forms.SystemInformation.WorkingArea.Height - Height - BorderPixel);
            //weekstr = DateTime.Now.DayOfWeek.ToString();
            //this.Text += "(" + weekstr + ")";
            this.Left = SystemParameters.WorkArea.Width - Width - BorderPixel;
            this.Top = SystemParameters.WorkArea.Height - Height - BorderPixel;
            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));


            //////////////////////////
            RegistryKey IFEORegx64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            
            if (IFEORegx64 != null)
            {
                foreach (string IFEOKey in IFEORegx64.GetSubKeyNames())
                {
                    RegistryKey CurIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + IFEOKey);
                    if (CurIFEOKey.GetValue("debugger", null) != null)
                    {
                        {
                            ClassIFEORegItem IFEOlist = new ClassIFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                            //MessageBox.Show(IFEOlist.ToString());//listBox1.Items.Add(IFEOlist);
                            listBox1.Items.Add(IFEOlist);
                        }
                    }
                }
            }
            IFEORegx64.Close();

        }

        private void LessonUnitAdd(ref ClassLessonUnit clu, ref ListBox lbx)
        {
            lbx.Items.Add(clu);
            clu.Focusable = false;
            //clu.IsHitTestVisible = false;
            clu.IsTabStop = false;
            clu.HorizontalAlignment = HorizontalAlignment.Stretch;
            clu.Margin = new Thickness(0, 2, 2, 2);

        }

        private void DisplayList(string DayOfWeek)
        {
            ClassLessonUnit L1 = new ClassLessonUnit();
            ClassLessonUnit L2 = new ClassLessonUnit();
            ClassLessonUnit L3 = new ClassLessonUnit();
            ClassLessonUnit L4 = new ClassLessonUnit();
            ClassLessonUnit L5 = new ClassLessonUnit();
            ClassLessonUnit L6 = new ClassLessonUnit();


            switch (DayOfWeek)
            {
                case "Monday":
                    L1.setClassLessonUnit("数学", "地点", "教师", 1, 2);
                    L1.setClassLessonUnit("数学", "地点", "教师", 3, 4);
                    L3.setClassLessonUnit("密码", "地点", "教师", 5, 7, "蹭课");
                    LessonUnitAdd(ref L1, ref listBoxLesson);
                    LessonUnitAdd(ref L2, ref listBoxLesson);
                    LessonUnitAdd(ref L3, ref listBoxLesson);
                    break;

                case "Tuesday":
                    L1.setClassLessonUnit("抽代", "地点", "教师", 3, 4);//, listBoxLesson.Width - 8
                    L2.setClassLessonUnit("其他", "地点", "教师", 7, 8);
                    LessonUnitAdd(ref L1, ref listBoxLesson);
                    LessonUnitAdd(ref L2, ref listBoxLesson);

                    break;

                case "Wednesday":
                    L1.setClassLessonUnit("体育", "地点", "教师", 3, 4);
                    L2.setClassLessonUnit("数学", "地点", "教师", 5, 7);
                    L3.setClassLessonUnit("数学", "地点", "教师", 11, 12, "上机");
                    LessonUnitAdd(ref L1, ref listBoxLesson);
                    LessonUnitAdd(ref L2, ref listBoxLesson);
                    LessonUnitAdd(ref L3, ref listBoxLesson);
                    break;

                case "Thursday":
                    L1.setClassLessonUnit("其他", "地点", "教师", 1, 2, "听力");
                    L2.setClassLessonUnit("抽代", "地点", "教师", 1, 2, "习题、主讲");
                    L3.setClassLessonUnit("其他", "地点", "教师", 5, 6);
                    L4.setClassLessonUnit("超导", "地点", "教师", 7, 8);
                    L5.setClassLessonUnit("其他", "地点", "教师", 9, 10);
                    LessonUnitAdd(ref L1, ref listBoxLesson);
                    LessonUnitAdd(ref L2, ref listBoxLesson);
                    LessonUnitAdd(ref L3, ref listBoxLesson);
                    LessonUnitAdd(ref L4, ref listBoxLesson);
                    LessonUnitAdd(ref L5, ref listBoxLesson);
                    break;

                case "Friday":
                    L1.setClassLessonUnit("其他", "地点", "教师", 3, 4);
                    L2.setClassLessonUnit("数学", "地点", "教师", 5, 6, "习题");
                    LessonUnitAdd(ref L1, ref listBoxLesson);
                    LessonUnitAdd(ref L2, ref listBoxLesson);
                    break;

                //case "Saturday":  break;
                //case "Sunday": break;
                default:
                    ListBoxItem def = new ListBoxItem();
                    def.Content = "No lesson";
                    def.HorizontalAlignment = HorizontalAlignment.Center;
                    def.IsHitTestVisible = false;
                    listBoxLesson.Items.Add(def);
                    break;

            }
            listBoxLesson.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            //for (int i = 1; i < listBoxLesson.Items.Count; i++)
            //{
            //    listBoxLesson.Items[2];
            //}
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.WndProc));
            }
        }
        /// <summary>
        /// 原Form的WndProc,在这里相当于自定义，使用virtual是为了继承
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCLBUTTONDOWN && wParam.ToInt32() == HTCAPTION)
                handled = true;
            if (msg == WM_DEVICECHANGE)
            {
                switch (wParam.ToInt32())
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

            return IntPtr.Zero;
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
                        //ListBox.Items.Add(ex.ToString());
                        MessageBox.Show(ex.ToString());
                    }



                }
            }
        }

        private void RunProtect()
        {

            if (ProtectStatus)
            {


                CheckBoxProtect.IsChecked = false;
                //ToolStripProtect.Checked = false;
                //StatusProtect.Text = "Protection: Disabled";
                labelProtectStatus.Content = "Protect Status: False";

                ProtectStatus = false;
            }
            else
            {
                CheckBoxProtect.IsChecked = true;
                //ToolStripProtect.Checked = true;
                //StatusProtect.Text = "Protection: Enabled";

                labelProtectStatus.Content = "Protect Status: True";
                ProtectStatus = true;
            }

        }

        private void RunRepair()
        {
            if (RepairStatus)
            {
                CheckBoxRepair.IsChecked = false;
                //ToolStripRepair.Checked = false;
                //StatusRepair.Text = "Repair: Disabled";
                labelRecoverStatus.Content = "Recover Status: False";
                RepairStatus = false;
            }
            else
            {
                CheckBoxRepair.IsChecked = true;
                //ToolStripRepair.Checked = true;
                //StatusRepair.Text = "Repair: Enabled";
                labelRecoverStatus.Content = "Recover Status: True";
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

        private void BtnAbt_Click(object sender, RoutedEventArgs e)
        {
            //WindowAbt winabt = new WindowAbt();
            AboutBox winabt = new AboutBox();
            winabt.ShowDialog();
        }

        private void BtnAddProg_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPath.Text == "") return;
            ClassFavorProg cls1 = new ClassFavorProg();
            cls1.HorizontalAlignment = HorizontalAlignment.Stretch;
            cls1.Margin = new Thickness(6, 2, 8, 2);
            cls1.SetPath(textBoxPath.Text);
            cls1.Content = Path.GetFileNameWithoutExtension(textBoxPath.Text);
            ListBoxProg.Items.SortDescriptions.Add(new SortDescription("ClickedTimes", ListSortDirection.Descending));


            if (System.IO.File.Exists(cls1.GetPath()))
            {
                ListBoxProg.Items.Add(cls1);

                //////////////////////// Save the Configuration

                SaveFavorProgSection(cls1.GetPath(),(string)cls1.Content,cls1.ClickedTimes);

                /////////////////////

                textBoxPath.Text = "";
            }
            else
            {
                FileNotFoundException ex = new FileNotFoundException();
                MessageBox.Show(ex.Message);

            }
        }

        private void BtnRmvProg_Click(object sender, RoutedEventArgs e)
        {
            while (ListBoxProg.SelectedItem != null)
                ListBoxProg.Items.Remove(ListBoxProg.SelectedItem);

        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Application.Current.Shutdown();
                //e.Handled = true;

            }
        }

        static void SaveFavorProgSection(string SFPS_path, string SFPS_name, int SFPS_click)
        {
            try
            {
                // Get the current configuration file.
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Create the section entry  
                // in <configSections> and the 
                // related target section in <configuration>.
                /*
                if (config.Sections["SaveProgSection"] == null)
                {
                    saveProgSection = new SaveProgSection();

                    config.Sections.Add("SaveProgSection", saveProgSection);
                    saveProgSection.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Full);

                    MessageBox.Show("Section name: {0} created", saveProgSection.SectionInformation.Name);

                }
                */
                SaveProgSection saveProgSection = new SaveProgSection();
                saveProgSection.ProgramPath = SFPS_path;
                saveProgSection.ProgramName = SFPS_name;
                saveProgSection.ClickedTimes = SFPS_click;
                config.Sections.Add("SaveProgSection", saveProgSection);
                saveProgSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);
            }
            catch (ConfigurationErrorsException err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        static void SaveFavorProgSection(string SFPS_path, string SFPS_name, int SFPS_click,bool isdelete)
        {
            if (isdelete == true)
            {
                try
                {
                    // Get the current configuration file.
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                }
                catch (ConfigurationErrorsException err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl1.SelectedItem == FavorProg)
            {
                //SaveFavorProgSection();
                e.Handled = true;
            }
        }


    }

}
