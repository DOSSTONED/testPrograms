using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PKULogin_CommandLine
{
    class Program : ICertificatePolicy
    {
        /// <summary>
        /// Well, I have to use it again...
        /// Since NK 188 Gateway...
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="certificate"></param>
        /// <param name="request"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CheckValidationResult(ServicePoint sp,
        X509Certificate certificate, WebRequest request, int error)
        {
            return true;
        }

        static void Main(string[] args)
        {
            ServicePointManager.CertificatePolicy = new Program();

            DateTime _start = DateTime.Now;
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: this.exe free|global|disconnectall");
                return;
            }
            PKUGW GatewayLogin = new PKUGW();
            
            string result = string.Empty;
            string username = "USERNAME_DOSSTONED";
            string password = "PWDOSSTONED";
            switch (args[0].ToLower())
            {
                case "global":
                    result = GatewayLogin.Login(username, password, PKUGW.LoginType.fee);
                    break;
                case "free":
                    result = GatewayLogin.Login(username, password, PKUGW.LoginType.free);
                    break;
                case "disconnectall":
                    result = GatewayLogin.Login(username, password, PKUGW.LoginType.DisconnectAll);
                    break;
                default:
                    Console.WriteLine("Usage: this.exe free|global|disconnectall");
                    return;
            }
            if (result.IndexOf(@"<!--IPGWCLIENT_START") < 0)
            {
                Console.WriteLine("Error! Maybe wrong password or else");
            }
            else
            {
                string info = result.Substring(result.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length,
                    result.IndexOf("IPGWCLIENT_END-->") - (result.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length));
                string[] infos = info.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in infos)
                    Console.WriteLine(str);
            }
            Console.WriteLine("Connect consume:" + (DateTime.Now - _start).TotalMilliseconds.ToString() + " ms");
        }
    }
}
