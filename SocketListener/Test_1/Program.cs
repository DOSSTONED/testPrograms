//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Test_1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
//    }
//}


using System;

using System.Net;

using System.Net.Sockets;
using System.Text;










namespace Ipv6Utils
{

    class Program
    {

        private byte[] byteData = new byte[4096];

        //private const int PORT = 0;
        EndPoint remoteIp = new IPEndPoint(Dns.GetHostEntry("ip6.nku.cn").AddressList[0], 0);

        private Socket listener;



        ~Program()
        {

            listener.Close();

        }



        private void OnReceive(IAsyncResult ar)
        {

            try
            {

                int nReceived = listener.EndReceive(ar);

                string rawBytes = "";

                for (int i = 0; i < nReceived; i++)
                {

                    rawBytes += byteData[i].ToString("x2") + " ";
                    if ((i + 1) % 16 == 0)// 该行最后
                    {
                        rawBytes += "\t";
                        for (int j = i - 15; j <= i; j++)
                        {
                            if (char.IsLetterOrDigit(Convert.ToChar(byteData[j])))
                            {
                                rawBytes += (char)byteData[j];
                            }
                            else
                            {
                                rawBytes += '.';
                            }
                        }
                        rawBytes += "\r\n";
                    }
                }


                Console.WriteLine("Received something : \r\n" + rawBytes);



                byteData = new byte[4096];



                //Another call to BeginReceive so that we continue to receive the incoming packets



                listener.BeginReceiveFrom(byteData, 0, 4096, SocketFlags.None

                , ref remoteIp, new AsyncCallback(OnReceive), null);

            }

            catch (ObjectDisposedException)
            {

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

        }



        static void Main(string[] args)
        {

            try
            {

                Program p = new Program();

                p.Init();

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

            while (true) ;

        }



        public void Init()
        {

            listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Raw, ProtocolType.Unspecified); // ProtocolTypes IPv6 or Raw don't receive anything ......



            listener.Bind(new IPEndPoint(IPAddress.Parse("DOSSTONED"), 0));



            byte[] byTrue = new byte[4] { 1, 0, 0, 0 };

            byte[] byOut = new byte[4];



            int nb = listener.IOControl(IOControlCode.ReceiveAll, byTrue, byOut);



            //Start receiving the packets asynchronously


            listener.BeginReceiveFrom(byteData, 0, 4096, SocketFlags.None, ref remoteIp, new AsyncCallback(OnReceive), null);

        }
    }
}