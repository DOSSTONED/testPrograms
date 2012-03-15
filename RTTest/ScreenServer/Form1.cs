using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ScreenServer
{


    public partial class Form1 : Form
    {
        // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024*1024*3;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
        public Form1()
        {
            InitializeComponent();
        }

        TcpListener listener = new TcpListener(4002);
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 4002);
        SocketAsyncEventArgs readWriteEventArg = new SocketAsyncEventArgs();
        TcpClient tc = new TcpClient();

        private void Form1_Load(object sender, EventArgs e)
        {
            //listener.BeginReceive(new AsyncCallback(AcceptCallback), listener);
            listener.Start();
            readWriteEventArg.Completed += readWriteEventArg_Completed;
            tc = listener.AcceptTcpClient();
            tc.ReceiveBufferSize = 3 * 1024 * 1024;
            
        }

        void readWriteEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            byte[] rec = e.Buffer;
            if (InvokeRequired)
            {
                this.Invoke(new Action(delegate
                {
                    pictureBox1.Image = byteArrayToImage(rec);
                }));
            }
            else
            {
                pictureBox1.Image = byteArrayToImage(rec);
            }
        }
//        public void AcceptCallback( IAsyncResult ar)
//        {
//            // Signal the main thread to continue.
//            //allDone.Set();

//            // Get the socket that handles the client request.
//            Socket listener = (Socket)ar.AsyncState;
//            Socket handler = listener.EndAccept(ar);

//            // Create the state object.
//            StateObject state = new StateObject();
//            state.workSocket = handler;
//            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                new AsyncCallback(ReadCallback), state);
//        }

//        public void ReadCallback(IAsyncResult ar)
//        {
//            String content = String.Empty;

//            // Retrieve the state object and the handler socket
//            // from the asynchronous state object.
//            StateObject state = (StateObject)ar.AsyncState;
//            Socket handler = state.workSocket;

//            // Read data from the client socket. 
//            int bytesRead = handler.EndReceive(ar);

//            if (bytesRead > 0)
//            {
//                // There  might be more data, so store the data received so far.
//                //state.sb.Append(Encoding.ASCII.GetString(
//                //    state.buffer, 0, bytesRead));

//                if (InvokeRequired)
//                {
//                    this.Invoke(new Action(delegate {
//                    pictureBox1.Image = byteArrayToImage(state.buffer);
//                    }));
//                }
//else
//                {
//                    pictureBox1.Image = byteArrayToImage(state.buffer);
//                }

//                // Check for end-of-file tag. If it is not there, read 
//                // more data.
//                //content = state.sb.ToString();
//                //if (content.IndexOf("<EOF>") > -1)
//                //{
//                //    // All the data has been read from the 
//                //    // client. Display it on the console.
//                //    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
//                //        content.Length, content);
//                //    // Echo the data back to the client.
//                //    //Send(handler, content);
//                //}
//                //else
//                //{
//                //    // Not all data received. Get more.
//                //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                //    new AsyncCallback(ReadCallback), state);
//                //}
//            }
//        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
