using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 文件替换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddSubDirFilesToListbox(string directorypath, ListBox lstbox)
        {
            if (Directory.Exists(directorypath))
            {
                foreach (string subdir in Directory.GetDirectories(directorypath))
                {
                    AddSubDirFilesToListbox(subdir, lstbox);
                }
            }
            foreach (string file in Directory.GetFiles(directorypath))
            {
                lstbox.Items.Add(file);
            }
        }

        private void listBoxFrom_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] dirs = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string dir in dirs)
                {
                    if (Directory.Exists(dir))
                    {
                        AddSubDirFilesToListbox(dir, listBoxFrom);
                    }
                }
            }
            
        }

        private void listBoxFrom_DragEnter(object sender, DragEventArgs e)
        {
            // change the drag cursor to show valid data ready

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            } 

        }

        private void listBoxTo_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] dirs = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string dir in dirs)
                {
                    if (Directory.Exists(dir))
                    {
                        AddSubDirFilesToListbox(dir, listBoxTo);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxFrom.Items.Count == listBoxTo.Items.Count)
            {
                for (int i = 0; i < listBoxTo.Items.Count; i++)
                {
                    File.Copy(listBoxFrom.Items[i].ToString(), listBoxTo.Items[i].ToString(), true);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxFrom.Items.Clear();
            listBoxTo.Items.Clear();
        }


    }
}
