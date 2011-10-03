using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ClassDr
{
    public partial class UserControlDr : UserControl
    {
        public UserControlDr()
        {
            InitializeComponent();
        }

        private void button重置_Click(object sender, EventArgs e)
        {
            textBoxUsrName.Text = string.Empty;
            maskedTextBoxUsrPas.Text = string.Empty;
        }
    }
}
