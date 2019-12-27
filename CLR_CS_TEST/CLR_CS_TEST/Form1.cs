using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CLR_CS_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object sa=new Shell.Application;//=new Application();
            //    sa = new-object -com Shell.Application
    sa.Namespace(17).ParseName("I:").InvokeVerb("Eject")
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sa);
        }
    }
}
