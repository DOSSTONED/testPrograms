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
using Microsoft.Win32;

namespace NOMARGINAERO_TEST_WPF
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        double _top = 0;
        double _left = 0;
        const double BorderMargin = 0;

        /// <summary>
        /// My own class for IFEO
        /// </summary>

        public class IFEORegItem
        {
            private string _architecture;
            private string _imagePath;
            private string _imageName;

            public string ImageName
            {
                get { return _imageName; }
                set { _imageName = value; }
            }

            public string Architecture
            {
                get { return _architecture; }
                set { _architecture = value; }
            }

            public string ImagePath
            {
                get { return _imagePath; }
                set { _imagePath = value; }
            }

            public IFEORegItem(string name, string architecture, string imagePath)
            {
                this._imageName = name;
                this._architecture = architecture;
                this._imagePath = imagePath;
            }
        }

        /// <summary>
        /// VS generated codes
        /// </summary>
        /// 
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));



            RegistryKey IFEORegx64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            // TreeNode IFEONodex64 = new TreeNode("X64");


            if (IFEORegx64 != null)
            {
                // IFEOTree.TopNode.Nodes.Add(IFEONodex64);

                foreach (string IFEOKey in IFEORegx64.GetSubKeyNames())
                {
                    RegistryKey CurIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + IFEOKey);
                    if (CurIFEOKey.GetValue("debugger", null) != null)
                    {
                        IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                        //MessageBox.Show("Image:" + IFEOlist.ImageName + "\nPath:" + IFEOlist.ImagePath);

                        listBox1.Items.Add("--------");
                        listBox1.Items.Add(IFEOlist.ImageName);
                        listBox1.Items.Add(IFEOlist.ImagePath);
                        
                    }
                }
            }
            IFEORegx64.Close();

            //////////////////////////

            //RegistryKey IFEORegx86 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            //TreeNode IFEONodex86 = new TreeNode("X86");
            //if (IFEORegx86 != null)
            //{
            //    IFEOTree.TopNode.Nodes.Add(IFEONodex86);
            //}

            //IFEORegx86.Close();
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"a;fjiposnvzl;vn[0qrfhpo"+e.ToString()+"\n\n"+sender.ToString());
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
