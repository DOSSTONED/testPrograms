using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Windows.Interop;

namespace WPF_Desktop
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private void ButtonAddProg_Click(object sender, RoutedEventArgs e)
        {

            if (File.Exists(textBoxInputPath.Text))
            {
                if (textBoxInputName.Text != "")
                {
                    Progs p = new Progs(textBoxInputPath.Text, textBoxInputName.Text);
                    
                    string path = Directory.GetCurrentDirectory() + "\\..\\Data\\FavorProg\\" + textBoxInputName.Text + ".DOSSTONED";// +p.ClickedTimes.ToString();

                    listViewProgram.Children.Add(p);

                    if (!File.Exists(path))
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(textBoxInputPath.Text);
                        }
                    }

                    textBoxInputPath.Text = "";
                    textBoxInputName.Text = "";
                }
                else
                    MessageBox.Show("缺少名称！");
            }
            else
            {
                if (textBoxInputName.Text != "" && textBoxInputPath.Text != "")
                    MessageBox.Show(new FileNotFoundException().Message);
            }
            th.Background = System.Windows.Media.Brushes.Transparent;
            stackPanel.Visibility = Visibility.Collapsed;
            th.Visibility = Visibility.Collapsed;
        }


        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            th.Visibility = Visibility.Visible;
            stackPanel.Visibility = Visibility.Visible;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
            string[] Programs = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\..\\Data\\FavorProg", "*.DOSSTONED.*");
            foreach (string program in Programs)
            {
                string sname = "";
                //uint sclick = 0;
                using (StreamReader sr = File.OpenText(program))
                {
                    sname = sr.ReadLine();
                    //sclick = Convert.ToUInt32(Path.GetExtension(program));
                }
                Progs p = new Progs(sname, System.IO.Path.GetFileNameWithoutExtension(program));
                //Label a = new Label();
                //a.Content = "111";
                //a.Foreground = System.Windows.Media.Brushes.AliceBlue;
                //p.Content = a;//
                //System.Drawing.Image icon = System.Drawing.Image.FromFile(@"E:\石道辰\Icons\ICO_6.ico");
                
                //p.Content = icon;
                //p.Width = 1000;

                //p.ClickedTimes = sclick;
                listViewProgram.Children.Add(p);
            }

        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void QuickStartAdd(Progs p)
        {

        }


        //[DllImport("user32.dll")]
        //static extern IntPtr FindWindow(string className, string windowName);
        //[DllImport("user32.dll")]
        //static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        //private void buttonChangeParent_Click(object sender, RoutedEventArgs e)
        //{
        //    IntPtr desktop = FindWindow(null, "Program Manager");
        //    IntPtr hwndOldParent = SetParent(new WindowInteropHelper(this).Handle, desktop);
        //}


        #region FlashDisk Protect

        #endregion




    }
}
