using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace IFEO_Detector_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process startcalc = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "calc.exe";
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            startcalc.StartInfo = info;

            bool res = startcalc.Start();
            if (startcalc.ProcessName.ToLower() != "calc")
            {
                labelStatus.Text = "The Calc has been hijacked!!";
                labelDescription.Text = string.Empty;
                System.Media.SystemSounds.Hand.Play();
                if (startcalc.HasExited)
                    return;
            }
            else
            {
                labelStatus.Text = "It seems no problem with Calc";
            }
            labelDescription.Text = "Waiting for Calc exit...";
            startcalc.WaitForExit();
            labelDescription.Text = "Calc exited.";

        }
    }
}
