using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramStarter
{
    public partial class Form1 : Form
    {
        private List<Button> ButtonList;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Button Btntmp;
            Btntmp = new Button();
            //Btntmp. = 12;
            //Btntmp.Y = 12+ButtonList.Count*33;
            Btntmp.Height = 23;
            Btntmp.Width = 155;
            ButtonList.Add(Btntmp);
           
        }

        private void EditMode_CheckedChanged(object sender, EventArgs e)
        {
            if (EditMode.Checked == true)
                BtnAdd.Visible = true;
            else
                BtnAdd.Visible = false;
        }
    }
}
