using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashPlayer_UsingActiveX_CS
{
    public partial class Form1 : Form
    {
        private string flashMoviePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string moviePath)
        {
            flashMoviePath = moviePath;
            
            
            InitializeComponent();
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("An application using Flash ActiveX!\nEnjoy!\nBy DOSSTONED","About this program",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (flashMoviePath != string.Empty)
                axShockwaveFlash1.Movie = flashMoviePath;
            //axShockwaveFlash1.AllowNetworking = "all";
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
