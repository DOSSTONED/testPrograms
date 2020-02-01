using System;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            GetIFEOData(sender, e);
        }

        private void GetIFEOData(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RegistryKey IFEOReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");

            if (IFEOReg != null)
            {
                foreach (string IFEOKey in IFEOReg.GetSubKeyNames())
                {
                    RegistryKey CurIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + IFEOKey);
                    if (CurIFEOKey.GetValue("debugger", null) != null)
                    {
                        listBox1.Items.Add(IFEOKey);
                    }
                }
            }
            IFEOReg.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = listBox1.SelectedItem.ToString();
            RegistryKey SelectIFEOKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + textBoxName.Text);
            if (SelectIFEOKey.GetValue("Debugger", null) != null)
            {
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
