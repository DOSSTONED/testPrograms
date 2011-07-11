using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Ping_Multiple_Hosts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] ParseIPs(string input)
        {
            string[] ret = null;
            if (input.EndsWith(".0")) // indicates 192.168.0.0
            {
                ret = new string[254];
                for (int i = 1; i < 255; i++)
                {
                    ret[i - 1] = input.Substring(0, input.Length - 2) + "." + i.ToString();
                }
                return ret;
            }
            if (input.Contains("-"))
            {
                int last = int.Parse(input.Substring(input.LastIndexOf(".") + 1));
                int begin = 0;
                string[] nums = input.Split('.');
                foreach (string str in nums)
                {
                    if (str.Contains("-"))
                        begin = int.Parse(str.Substring(0, str.IndexOf("-")));
                }
                ret = new string[last - begin + 1];
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = nums[0] + "." + nums[1] + "." + nums[2] + "." + (i + begin).ToString();
                }
                return ret;
            }
            throw new Exception("You have input a wrong format!");
        }
        private Ping[] ping = new Ping[255];
        private int tasks = 0;

        private void buttonExit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ping.Length; i++)
            {
                if (ping[i] != null)
                    ping[i].SendAsyncCancel();
            }
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ping.Length; i++)
            {
                ping[i] = new Ping();
                ping[i].PingCompleted += new PingCompletedEventHandler(Form1_PingCompleted);
            }
        }

        void Form1_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {

                tasks--;
                if (e.Error == null)
                {
                    if (e.Reply.Status == IPStatus.Success)
                    {
                        listBoxHostsReplies.Items.Add(e.Reply.Address.ToString());
                    }

                }
                if (tasks == 0)
                {

                    if (InvokeRequired)
                    {
                        //buttonPing.Invoke(delegate(new Action() { buttonPing.Enabled = true; }), null); //buttonPing.Enabled = true;
                    }
                    else
                    {
                        toolStripStatusLabelStatus.ForeColor = Color.FromName("ControlText");
                        toolStripStatusLabelStatus.Text = "Success";
                        buttonPing.Enabled = true;
                    }
                }
            }
        }

        private void buttonPing_Click(object sender, EventArgs e)
        {
            buttonPing.Enabled = false;
            tasks = 0;
            try
            {
                switch (tabControlHosts.SelectedIndex)
                {
                    case 0:
                        tasks++;
                        Ping p = new Ping();
                        Random ran = new Random();
                        byte[] bytes = new byte[ran.Next(1, 200)];
                        ran.NextBytes(bytes);
                        PingReply pr = p.Send(textBoxOneHostDst.Text, 5000, bytes);
                        textBoxTime.Text = pr.RoundtripTime.ToString();
                        textBoxTTL.Text = pr.Options.Ttl.ToString();
                        textBoxBytes.Text = pr.Buffer.Length.ToString();
                        toolStripStatusLabelStatus.ForeColor = Color.FromName("ControlText");
                        toolStripStatusLabelStatus.Text = "Success";
                        tasks--;


                        break;

                    case 1:
                        listBoxHostsReplies.Items.Clear();
                        string[] dests = ParseIPs(textBoxHosts.Text);
                        for (int i = 0; i < dests.Length; i++)
                        {
                            tasks++;
                            ping[i].SendAsync(dests[i], 5000, listBoxHostsReplies);
                        }

                        break;
                    default:
                        throw new Exception("Wrong tabpage index!");
                }

            }
            catch (Exception ex)
            {
                toolStripStatusLabelStatus.ForeColor = Color.Red;
                toolStripStatusLabelStatus.Text = ex.Message;
            }
            finally
            {
                if (tasks == 0)
                    buttonPing.Enabled = true;
            }
        }
    }
}
