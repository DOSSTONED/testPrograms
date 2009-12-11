using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Trans_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeXMLComponent();
            _Location = this.Location;
            _Size = this.Size;
        }

        Size _Size = new Size(200, 300);
        Point _Location = new Point(0, 0);
        Point _MouseLocation = new Point(0, 0);
        bool IsClipped = false;
        int BorderMargin = 20;


        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(150, 142);
            _Location = Location;
            _Location.Y = SystemInformation.WorkingArea.Height - Height - 0;
            _Location.X = (int)(SystemInformation.WorkingArea.Width * 0.62);
            IsClipped = true;
            Location = _Location;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            TransferFiles(files);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            Form1_MouseEnter(sender, e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _Location = this.DesktopLocation;
            _MouseLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            /*
             * 
            if (e.Button == MouseButtons.Left)
            {
                _Location.X = _Location.X - (_MouseLocation.X - e.X);
                _Location.Y = _Location.Y - (_MouseLocation.Y - e.Y);

                IsClipped = false;
                if (_Location.X < BorderMargin)
                {
                    _Location.X = 0;
                    //IsClipped = true;
                }
                if (_Location.Y < BorderMargin)
                {
                    _Location.Y = 0;
                    //IsClipped = true;
                }
                if (_Location.Y > SystemInformation.WorkingArea.Height - Height - BorderMargin)
                {
                    _Location.Y = SystemInformation.WorkingArea.Height - Height - 0;
                    IsClipped = true;
                }
                if (_Location.X > SystemInformation.WorkingArea.Width - Width - BorderMargin)
                {
                    _Location.X = SystemInformation.WorkingArea.Width - Width - 0;
                    //IsClipped = true;
                }

                DesktopLocation = _Location;
            }
            
             * 
             * 
            */

            if (e.Button == MouseButtons.Left)
            {
                _Location.X = _Location.X - (_MouseLocation.X - e.X);
                IsClipped = true;

                if (_Location.X < BorderMargin)
                {
                    _Location.X = 0;
                    //IsClipped = true;
                }
                if (_Location.X > SystemInformation.WorkingArea.Width - Width - BorderMargin)
                {
                    _Location.X = SystemInformation.WorkingArea.Width - Width - 0;
                    //IsClipped = true;
                }

                DesktopLocation = _Location;
            }

        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if (IsClipped)
            {
                _Location = DesktopLocation;
                {
                    Height = 10;
                    _Location.Y = SystemInformation.WorkingArea.Height - Height - 0;

                }
                DesktopLocation = _Location;
            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            if (IsClipped)
            {
                _Location = DesktopLocation;
                this.Size = _Size;// why (150,142) changed to (158,142)???
                {
                    _Location.Y = SystemInformation.WorkingArea.Height - Height - 0;

                }
                DesktopLocation = _Location;
            }
        }



        #region Related to Transfer


        void TransferFiles(string[] s)
        {
            if (s.Length == 1)
            {
                string ShellProgram = string.Empty;
                switch (Path.GetExtension(s[0]))
                {
                    case ".rm":
                    case ".rmvb":
                        ShellProgram = @"E:\TOOLS\mplayerc.exe";
                        break;
                    case ".scm":
                    case ".scx":
                        //ShellProgram = @"F:\Game\Brood\staredit.exe";
                        break;
                    default:
                        break;
                }

                if (ShellProgram != string.Empty)
                {
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.FileName = ShellProgram;
                    p.Arguments = s[0];
                    System.Diagnostics.Process.Start(p);
                }

                /// The next transfer is recongnized by the file name, not extension
                /// 
            }
        }

        void GenerateButtons(string[] s)
        {
        }


        void RemoveButtons()
        {
        }

        #endregion


        #region Related to XML

        /// <summary>
        /// Variables declaration
        /// </summary>
        /// 
        XmlDocument config = new XmlDocument();
        bool configCanRead = false;

        /// <summary>
        /// Functions
        /// </summary>
        /// 
        void InitializeXMLComponent()
        {
            try
            {
                config.Load(Directory.GetCurrentDirectory() + "\\Config.xml");
                configCanRead = true;
            }
            catch
            {
                configCanRead = false;
                // MessageBox.Show(Directory.GetCurrentDirectory());
                Create_Xml();//如果加载失败就，创建一个新文档（第一次运行程序的时候生成配置文档）
                if (configCanRead)
                {
                    config.Load(Directory.GetCurrentDirectory() + "\\Config.xml");
                }
                else
                    MessageBox.Show("Failed to create config file.");
            }
        }

        private void Create_Xml()
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlelem;

            xmldoc = new XmlDocument();
            
            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "FileType", "TransFileType");
            xmldoc.AppendChild(xmlnode);


            try
            {
                xmldoc.Save(Directory.GetCurrentDirectory() + "\\Config.xml");
                configCanRead = true;
            }
            catch (Exception ex)
            {
                configCanRead = false;
                MessageBox.Show(ex.Message);
            }
        }



        #endregion
    }
}
