using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DOSSTONED_FrontEnd_V0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string PLUGINS_PATH = @"E:\My Documents\Programming\Projects\Platform_Plugins\Plugins";
        static List<Assembly_Threads> ATs = new List<Assembly_Threads>();

        private void Form1_Shown(object sender, EventArgs e)
        {
            fileSystemWatcherDLL.Path = PLUGINS_PATH;
            LoadPlugins(PLUGINS_PATH);
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && (Control.ModifierKeys != Keys.Alt))
            {

                e.Cancel = true;

                this.Hide();
            }
            else
            {
                UnloadPlugins();
            }
            
            
        }

        private void fileSystemWatcherDLL_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            
            bool reloadFlag = false;
            if (System.IO.Path.GetExtension(e.FullPath).ToUpper() == ".DLL")
            {
                reloadFlag = true;
            }
            else
            {
                foreach (Assembly_Threads at in ATs)
                {
                    if (at.asm.Location == e.OldFullPath)
                    {
                        reloadFlag = true;
                        break;
                    }
                }
            }
            if (reloadFlag)
            {
                UnloadPlugins();
                LoadPlugins(PLUGINS_PATH);
            }
        }

        private void exitxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            
            Application.Exit();
        }

        private void showMainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }


    }
}
