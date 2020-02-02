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
            if (args.Length > 2 || args.Length < 1)
            {
                Usage();
                return;
            }
            PKUGW GatewayLogin = new PKUGW();

            string result = string.Empty;
            string username = "USERNAME_DOSSTONED";
            string password = "PWDOSSTONED";
            PKUGW.LoginType lt = PKUGW.LoginType.noopen;
            string settings = string.Empty;
            bool forceLogin = false;

            if (args.Length == 1)
            {
                settings = args[0].ToLower();
                forceLogin = false;
            }
            else if (args.Length == 2)
            {
                if (args[0].ToLower() == "force")
                {
                    settings = args[1].ToLower();
                    forceLogin = true;
                }
                else if (args[1].ToLower() == "force")
                {
                    settings = args[0].ToLower();
                    forceLogin = true;
                }
                else
                    settings = string.Empty;    // indicates error
            }
            else
            {
                throw new Exception("How can you run here? It is illogic!");
            }
            switch (settings)
            {
                case "openall":
                case "global":
                    lt = PKUGW.LoginType.fee;
                    break;
                case "open":
                case "login":
                case "free":
                    lt = PKUGW.LoginType.free;
                    break;
                case "disconnectall":
                    lt = PKUGW.LoginType.DisconnectAll;
                    break;
                default:
                    Usage();
                    return;
            }


            while (true)
            {
                if (forceLogin)
                    result = GatewayLogin.Login(username, password, PKUGW.LoginType.DisconnectAll);
                else
                    result = GatewayLogin.Login(username, password, lt);
                ParseStrings(result);
                Console.WriteLine("Connect consume:" + (DateTime.Now - _start).TotalMilliseconds.ToString() + " ms");
                forceLogin = !forceLogin;
                if (forceLogin)
                    break;
            }
        }

        static void ParseStrings(string input)
        {
            if (input.IndexOf(@"<!--IPGWCLIENT_START") < 0)
            {
                Console.WriteLine("Error! Maybe wrong password or else");
            }
            else
            {
                string info = input.Substring(input.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length,
                        input.IndexOf("IPGWCLIENT_END-->") - (input.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length));
                string[] infos = info.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in infos)
                    Console.WriteLine(str);
            }
        }

        static void Usage()
        {
            Console.WriteLine("Usage: this.exe [option] [force]");
            Console.WriteLine(
                "option:\n"+
                "free/open/login:   open the gateway with cernet only websites.\n"+
                "global/openall:    open to reach further international networks.\n"+
                "disconnectall:     disconnect the current account all connections(may affect other machines.)\n");
        }
    }
}
