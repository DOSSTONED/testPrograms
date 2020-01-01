using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNC
{
    public partial class Form1 : Form
    {
        const string PASS = "012345678abcdeABCDEFGHIJKLMNfghijklmnUVWXYZxyzuvwopqrstOPQRST9";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxOri_TextChanged(object sender, EventArgs e)
        {
            labelIndex.Text = string.Empty;
            if ((sender as TextBox).Text.Length > 0)
                foreach (char c in (sender as TextBox).Text)
                    labelIndex.Text += PASS.IndexOf(c).ToString() + ", ";
        }

        private void textBoxOri_TextChanged_1(object sender, EventArgs e)
        {
            labelOri.Text = string.Empty;
            if ((sender as TextBox).Text.Length > 0)
                foreach (char c in (sender as TextBox).Text)
                    labelOri.Text += PASS.IndexOf(c).ToString() + ", ";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = this.EnCode(textBox1.Text);
        }

        string EnCode(String str)
        {
            string cas_str = "9012345678abcdeABCDEFGHIJKLMNfghijklmnUVWXYZxyzuvwopqrstOPQRST";
            int[] cas_str_buffer = new int[16];
            int cas_esi = 37;
            char[] src_str = new char[128], dec_str = new char[128];
            int k = 0;
            int cas_eax, cas_edx;
            //div_t x;
            uint i, j;
            cas_str_buffer[15] = 25;
            cas_str_buffer[14] = 35;
            cas_str_buffer[13] = 182;
            cas_str_buffer[12] = 236;
            cas_str_buffer[11] = 43;
            cas_str_buffer[10] = 41;
            cas_str_buffer[9] = 53;
            cas_str_buffer[8] = 18;
            cas_str_buffer[7] = 226;
            cas_str_buffer[6] = 215;
            cas_str_buffer[5] = 24;
            cas_str_buffer[4] = 117;
            cas_str_buffer[3] = 35;
            cas_str_buffer[2] = 201;
            cas_str_buffer[1] = 52;
            cas_str_buffer[0] = 17;
            src_str = str.ToCharArray();
            for (i = 0; i < src_str.Length; i++)
            {
                for (j = 0; j < cas_str.Length; j++)
                {

                    if (src_str[i] == cas_str[(int)j])
                    {
                        //if (i < 16)
                        //    cas_eax = cas_str_buffer[i];
                        //else
                        //{
                        //    x = div(i, 16);
                        //    cas_eax = cas_str_buffer[i % 16];
                        //}

                        cas_eax = cas_str_buffer[i % 16];
                        cas_edx = cas_esi + cas_esi * 2;
                        cas_eax = cas_eax ^ cas_edx;
                        cas_eax = cas_eax ^ k;
                        cas_eax = cas_eax + (int)j;
                        //x = div(cas_eax,62);
                        dec_str[i] = cas_str[cas_eax % 62];
                        cas_edx = cas_eax % 62;
                        cas_esi = cas_esi ^ (cas_edx + 9433);
                        break;
                    }
                }
                if (dec_str[i] == 0)
                    dec_str[i] = src_str[i];
                k = k + 5;
            }
            //str = dec_str.ToString();
            byte[] ret = new byte[dec_str.Length];
            for (int ii = 0; ii < ret.Length; ii++)
                ret[ii] = (byte)dec_str[ii];
            str = Encoding.Default.GetString(ret);
            return str;
        }

    }
}
