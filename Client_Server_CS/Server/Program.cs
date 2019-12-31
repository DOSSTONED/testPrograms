using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 port = 41519;
            TcpListener ListenerIPv4 = new TcpListener(IPAddress.Any, port);
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
                Console.WriteLine("Connected!{2}, {0}:{1}", is_NK_internal_network(remoteIP) ? "NK school network" : "Not NK school network", remoteIP, remotePort);
                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

                // Shutdown and end connection
                client.Close();
            }
        }

        static bool is_NK_internal_network(IPAddress ip)
        {
            byte[] IPv6Scope = new byte[] { 0x20, 0x01, 0x2, 0x50, 0x4, 0x01 };

            /*
            byte[][] IPv4Scope = new byte[][] {
                new byte[] { 202, 113, 16, 0 },
                new byte[] { 202, 113, 224, 0 },
                new byte[] { 222, 30, 32, 0 },
                new byte[] { 10, 0, 0, 0 }
                };
            16  = 0001000
            224 = 1110000
             
             */

            byte[] ipBytes = ip.GetAddressBytes();
            switch (ip.AddressFamily)
            {
                case AddressFamily.InterNetwork:    // get bytes should be byte[4]
                    
                    if (ipBytes.Length == 4)
                    {
                        if (ipBytes[0] == 10)
                            return true;
                        if ((ipBytes[0] == 222) && (ipBytes[1] == 30) && ((ipBytes[2] & 240) == 32))
                            return true;
                        if ((ipBytes[0] == 202) && (ipBytes[1] == 113))
                        {
                            switch (ipBytes[2] & 240)
                            {
                                case 16:
                                case 224:
                                    return true;
                            }
                        }
                    }
                    break;
                case AddressFamily.InterNetworkV6:  // get bytes should be byte[16]
                    if (ipBytes.Length == 16)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (IPv6Scope[i] != ipBytes[i])
                                return false;
                            
                        }
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
