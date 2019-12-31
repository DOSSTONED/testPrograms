using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventLog_CS_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Start")
            {
                buttonStart.Text = "Stop";
                fileSystemWatcher1.EnableRaisingEvents = true;
            }
            else
            {
                buttonStart.Text = "Start";
                fileSystemWatcher1.EnableRaisingEvents = false;
            }
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToLongTimeString() + '\t' + e.FullPath + '\t' + e.ChangeType);
        }

        private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            listBox1.Items.Add("File: " + e.OldFullPath + " renamed to " + e.FullPath);
        }
    }
}
