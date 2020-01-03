using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace IncomingNotes
{
    public partial class Form1 : Form
    {
        const int DOSCUS_PORT = 41519;  // Customed port.

        private bool isDragging = false;
        int x, y;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {

            isDragging = false;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Label cur = sender as Label;
            if (cur == null) return;
            if (flowLayoutPanel1.Controls.Contains(cur))
                flowLayoutPanel1.Controls.Remove(cur);
            cur.Parent = this;

            isDragging = true;
            x = e.X; y = e.Y;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                Label l = sender as Label;
                l.Left = e.X + l.Left - x;
                l.Top = e.Y + l.Top - y;
            }
        }

        private void groupBox1_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("Hi");
        }

        BackgroundWorker bwListener = new BackgroundWorker();

        private void Form1_Load(object sender, EventArgs e)
        {
            bwListener = new BackgroundWorker();
            bwListener.DoWork += new DoWorkEventHandler(StartToListen);
            bwListener.RunWorkerAsync();
        }

        private void StartToListen(object sender, DoWorkEventArgs e)
        {
            TcpListener ListenerIPv4 = new TcpListener(IPAddress.Any, DOSCUS_PORT);
            ListenerIPv4.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = ListenerIPv4.AcceptTcpClient();
                IPAddress remoteIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                int remotePort = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
                Console.WriteLine("Connected! {0}:{1}", remoteIP, remotePort);
                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                try
                {
                    // Loop to receive all the data sent by the client.
                    while (true)
                    {

                        i = stream.Read(bytes, 0, bytes.Length);
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                        ////////////////////////////////////////
                        //Console.WriteLine("Received: {0}", data);



                        /// for test move action
                        /*
                        if (data.StartsWith("__MOVE__"))
                        {
                            string[] str = data.Split(new string[] { "X=", "Y=", "__MOVE__"}, StringSplitOptions.RemoveEmptyEntries);

                            Invoke(new Action(delegate()
                            {
                                labelDrag_Test.Left += (int)(double.Parse(str[0]) * trackBar1.Value);
                                if (labelDrag_Test.Left < 0) labelDrag_Test.Left = 0;
                                if (labelDrag_Test.Left > 300) labelDrag_Test.Left = 300;

                                if (data.Contains("Y="))
                                {
                                    labelDrag_Test.Top += (int)(double.Parse(str[1]) * 10);
                                    if (labelDrag_Test.Top < 0) labelDrag_Test.Top = 0;
                                    if (labelDrag_Test.Top > this.Height - 20) labelDrag_Test.Top = this.Height - 20;
                                }

                            }
                            )); 
                        }
                        else
                        {
                        */

                        data = data.Replace("__TEXT__", "__TEXT__++TEXT++__TEXT__");




                        if (InvokeRequired) // raw data
                        {
                            Invoke(new Action(delegate()
                            {
                                listBox1.Items.Add(data);
                            }
                            ));
                        }

                        string[] strs = data.Split(new string[] {"__TEXT__" , "__MOVE__" },
                            StringSplitOptions.RemoveEmptyEntries);



                        for (int loopi = 0; loopi < strs.Length; loopi++)
                        {
                            if (strs[loopi] == "++TEXT++")
                            {
                                string text = strs[loopi + 1];

                                Invoke(new Action(delegate()
                                {
                                    Label l = new Label();
                                    l.Text = text;
                                    flowLayoutPanel2.Controls.Add(l);
                                }
                                ));
                                byte[] retmsg = System.Text.Encoding.UTF8.GetBytes("__TEXT__RECV");

                                // Send back a response.
                                stream.Write(retmsg, 0, retmsg.Length);
                                loopi++;
                            }
                        }

                        if (data == "EXIT")
                            break;
                        if (false)
                        {
                            // Process the data sent by the client.
                            data = data.ToUpper();

                            Random ran = new Random();
                            byte[] msg = System.Text.Encoding.UTF8.GetBytes(data + ran.Next());
                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    client.Close();
                }
                // Shutdown and end connection
                client.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            labelDrag_Test.Text = textBox1.Text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxDetails.Text = listBox1.SelectedItem as string;
        }

    }
}
