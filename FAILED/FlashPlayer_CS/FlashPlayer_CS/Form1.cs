using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlashAccessibility;
using ShockwaveFlashObjects;

namespace FlashPlayer_CS
{
    public partial class Form1 : Form
    {
        ShockwaveFlashClass flashPlyr = new ShockwaveFlashClass();
        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flashPlyr.Menu = true;
            //flashPlyr.AllowFullScreen = false;
            //flashPlyr.AllowNetworking = false;
            flashPlyr.TPlay(@"E:\Game\flash\Super-Mario-Crossover.swf");
            MessageBox.Show(flashPlyr.FlashVersion().ToString());
        }

  
    }
}
