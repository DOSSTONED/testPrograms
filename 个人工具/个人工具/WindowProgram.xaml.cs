using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace 个人工具
{
    /// <summary>
    /// WindowProgram.xaml 的交互逻辑
    /// </summary>
    public partial class WindowProgram : Window
    {
        private double _top = 10, _left = 10, BorderMargin = 0;
        
        /// <summary>
        /// 
        /// </summary>

        public WindowProgram()
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
                    p.Width = 89;
                    p.Height = 33;
                    p.Background = Brushes.Transparent;
                    p.Content = textBoxInputName.Text;
                    string path = Directory.GetCurrentDirectory() + "\\Data\\FavorProg\\" + textBoxInputName.Text + ".DOSSTONED";// +p.ClickedTimes.ToString();

                    listView1.Items.Add(p);

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
            th.Background = Brushes.Transparent;
            stackPanel.Visibility = Visibility.Collapsed;
            th.Visibility = Visibility.Collapsed;
        }



        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            th.Visibility = Visibility.Visible;
            stackPanel.Visibility = Visibility.Visible;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            const int BorderPixel = 0;
            this.Left = SystemParameters.WorkArea.Width - Width - BorderPixel;
            this.Top = SystemParameters.WorkArea.Height - Height - BorderPixel;
            string[] Programs = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\FavorProg", "*.DOSSTONED.*");
            foreach (string program in Programs)
            {
                string sname = "";
                //uint sclick = 0;
                using (StreamReader sr = File.OpenText(program))
                {
                    sname = sr.ReadLine();
                    //sclick = Convert.ToUInt32(Path.GetExtension(program));
                }
                Progs p = new Progs(sname, Path.GetFileNameWithoutExtension(program));
                p.Width = 89;
                p.Height = 33;
                p.Background = Brushes.Transparent;
                //p.ClickedTimes = sclick;
                listView1.Items.Add(p);
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //string[] Programs = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\FavorProg", "*.DOSSTONED");
            //foreach (string program in Programs)
            //{
            //    string sname = "";
            //    uint sclick = 0;
            //    using (StreamReader sr = File.OpenText(program))
            //    {
            //        sname = sr.ReadLine();
            //        sclick = Convert.ToUInt32(sr.ReadLine());
            //    }
            //}
        }


        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        private void buttonChangeParent_Click(object sender, RoutedEventArgs e)
        {
            IntPtr desktop = FindWindow(null, "Program Manager");
            IntPtr hwndOldParent = SetParent(new WindowInteropHelper(this).Handle, desktop);
        }




        private void DragThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            _left = Left;
            _top = Top;
            _left += (int)e.HorizontalChange;
            _top += (int)e.VerticalChange;

            if (_top < BorderMargin)
            {
                _top = BorderMargin;
            }
            if (_left < BorderMargin)
            {
                _left = BorderMargin;
            }
            if (_top > SystemParameters.WorkArea.Height - Height - BorderMargin)
            {
                _top = SystemParameters.WorkArea.Height - Height - BorderMargin;
            }
            if (_left > SystemParameters.WorkArea.Width - Width - BorderMargin)
            {
                _left = SystemParameters.WorkArea.Width - Width - BorderMargin;
            }
            this.Left = _left;
            this.Top = _top;
        }

        


    }
}
