using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IFEO_Test
{
    public partial class IFEO : Form
    {
        public IFEO()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            RegistryKey IFEORegx64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            TreeNode IFEONodex64 = new TreeNode("X64");
            

            if (IFEORegx64 != null)
            {
                IFEOTree.TopNode.Nodes.Add(IFEONodex64);
                
                foreach(string IFEOKey in IFEORegx64.GetSubKeyNames())
                {
                    RegistryKey CurIFEOKey=Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\"+IFEOKey);
                    if (CurIFEOKey.GetValue("debugger", null)!=null)
                    {
                        IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                        MessageBox.Show(IFEOlist.ToString());//listBox1.Items.Add(IFEOlist);
                    }
                }
            }
            IFEORegx64.Close();

            //////////////////////////

            RegistryKey IFEORegx86 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            TreeNode IFEONodex86 = new TreeNode("X86");
            if (IFEORegx86 != null)
            {
                IFEOTree.TopNode.Nodes.Add(IFEONodex86);
            }

            IFEORegx86.Close();
        }

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


        private void IFEOExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
