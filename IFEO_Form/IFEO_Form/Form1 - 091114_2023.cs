using System;


using System.Collections.Generic;

using System.ComponentModel;
using System.Data;


using System.Drawing;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Runtime.InteropServices;

using System.Text;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IFEO_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Form1_Load(object sender, EventArgs e)
        {


            GetIFEOData(sender, e);
        }

        private void GetIFEOData(object sender, EventArgs e)
        {
            //IndexSearcher searcher = new IndexSearcher(this.AppStartPath + "\\index");
            //Query query = QueryParser.Parse(this.textBox4.Text, "contents", new StandardAnalyzer());
            ////获取搜索结果
            //Hits hit = searcher.Search(query);

            //dataGridView1.DataSource = null;


            //DataTable dt = new DataTable();
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Architecture");
            //dt.Columns.Add("Image Path");

            listBox1.Items.Clear();

            RegistryKey IFEORegx64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");

            if (IFEORegx64 != null)
            {
                foreach (string IFEOKey in IFEORegx64.GetSubKeyNames())
                {
                    RegistryKey CurIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + IFEOKey);
                    if (CurIFEOKey.GetValue("debugger", null) != null)
                    {
                        //IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                        //MessageBox.Show(CurIFEOKey.GetValue("debugger", null).ToString());//listBox1.Items.Add(IFEOlist);
                        listBox1.Items.Add(IFEOKey);
                    }
                }
            }
            IFEORegx64.Close();


            //RegistryKey IFEORegx86 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");

            //if (IFEORegx86 != null)
            //{
            //    foreach (string IFEOKey in IFEORegx86.GetSubKeyNames())
            //    {
            //        RegistryKey CurIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + IFEOKey);
            //        if (CurIFEOKey.GetValue("debugger", null) != null)
            //        {
            //            //IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
            //            //MessageBox.Show(CurIFEOKey.GetValue("debugger", null).ToString());//listBox1.Items.Add(IFEOlist);
            //            DataRow dr = dt.NewRow();
            //            dr[0] = IFEOKey;
            //            dr[1] = "X86";
            //            dr[2] = CurIFEOKey.GetValue("debugger", null);
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}
            //IFEORegx86.Close();


            //searcher.Close();
            //this.dataGridView1.DataSource = dt;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = listBox1.SelectedItem.ToString();
            RegistryKey SelectIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + textBoxName.Text);
            if (SelectIFEOKey.GetValue("Debugger", null) != null)
            {
                //IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                //MessageBox.Show(CurIFEOKey.GetValue("debugger", null).ToString());//listBox1.Items.Add(IFEOlist);

                textBoxImage.Text = SelectIFEOKey.GetValue("debugger", null).ToString();

            }
            SelectIFEOKey.Close();




        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            RegistryKey SelectIFEOKey = null;
            try
            {
                SelectIFEOKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + textBoxName.Text);

                //IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                //MessageBox.Show(CurIFEOKey.GetValue("debugger", null).ToString());//listBox1.Items.Add(IFEOlist);

                SelectIFEOKey.SetValue("Debugger", (string)textBoxImage.Text, RegistryValueKind.String);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SelectIFEOKey != null)
                    SelectIFEOKey.Close();
            }
            GetIFEOData(sender, e);
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            RegistryKey SelectIFEOKey = null;
            try
            {
                SelectIFEOKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + textBoxName.Text);

                //IFEORegItem IFEOlist = new IFEORegItem(IFEOKey, "X64", (string)CurIFEOKey.GetValue("debugger", null));
                //MessageBox.Show(CurIFEOKey.GetValue("debugger", null).ToString());//listBox1.Items.Add(IFEOlist);

                SelectIFEOKey.DeleteValue("Debugger", false);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SelectIFEOKey != null)
                    SelectIFEOKey.Close();
            }
            GetIFEOData(sender, e);
        }

    }
}
