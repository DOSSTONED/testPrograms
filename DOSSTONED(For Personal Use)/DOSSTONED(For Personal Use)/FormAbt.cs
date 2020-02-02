using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOSSTONED_For_Personal_Use_
{
    public partial class FormAbt : Form
    {
        public FormAbt()
        {
            InitializeComponent();
        }

        private void BtnFrmAbtOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormAbt_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();
        }

    }
}
