using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CS_MAC
{
    class Program
    {
        static void Main(string[] args)
        {
            string mac = getRemoteMAC("192.168.0.2", "192.168.0.1").ToString();
            while (true)
            {
                if (mac.Length % 4 == 0)
                    break;
                else
                {
                    mac = "0" + mac;
                }
            }
            byte[] bt = Convert.FromBase64String(mac);
            
            for (int i = 0; i < bt.Length;i++ )
                Console.Write("{0:X}", ~bt[i]);//221.238.188.39
            Console.ReadLine();
        }
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        static private Int64 getRemoteMAC(string localIP, string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的地的ip 
            Int32 lhost = inet_addr(localIP); //本地服务器的ip 

            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                return macinfo;
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return 0;
            
        } 
        
    }
}
