using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 电影字幕文件重命名
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void buttonRename_Click(object sender, EventArgs e)
        {
            listBoxMovie.Items.Clear();
            listBoxTitle.Items.Clear();
            string[] strmovies = Directory.GetFiles(@"E:\Film\HowIMetYourMotherS3", "*.avi");
            foreach (string strmovie in strmovies)
            {
                listBoxMovie.Items.Add(Path.GetFileNameWithoutExtension(strmovie));
            }
            string[] strtitles = Directory.GetFiles(@"E:\Film\HowIMetYourMotherS3", "*.srt");
            foreach (string strtitle in strtitles)
            {
                listBoxTitle.Items.Add(Path.GetFileNameWithoutExtension(strtitle));
            }


            if (DialogResult.Cancel == MessageBox.Show("Continue?", "Confirm", MessageBoxButtons.OKCancel))
                MessageBox.Show("cancelled");
            else
            {
                for (int i = 0; i < strmovies.Length; i++)
                {
                    File.Move(strtitles[i], @"E:\Film\HowIMetYourMotherS3\" + Path.GetFileNameWithoutExtension(strmovies[i]) + ".srt");
                }
            }
            

        }
    }
}
