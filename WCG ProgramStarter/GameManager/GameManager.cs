using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameManager
{
    public partial class GameManager : Form
    {
        public GameManager()
        {
            InitializeComponent();
        }

        private void GameManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show(e.CloseReason.ToString());
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
