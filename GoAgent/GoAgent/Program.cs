using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace GoAgent
{
    enum AccessMethod
    {
        google_cn,
        google_hk,
        google_ipv6
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string AccessMethod = "google_cn";
            AccessMethod AM = AccessMethod.google_ipv6;
            IPAddress ip = IPAddress.Any;
            string iniFilePath = string.Empty, exeFilePath = string.Empty;
            bool changeRepo = false, changeIP = false;
            try
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("dir = {0}", dir);
                if (File.Exists(dir + @"local\goagent.exe"))
                {
                    iniFilePath = dir + @"local\proxy.ini";
                    exeFilePath = dir + @"local\goagent.exe";
                }
                if (File.Exists(dir + @"goagent\local\goagent.exe"))
                {
                    iniFilePath = dir + @"goagent\local\proxy.ini";
                    exeFilePath = dir + @"goagent\local\goagent.exe";
                }
                if (exeFilePath == string.Empty)// not set yet
                    throw new FileNotFoundException("Cannot find goagent main executable. Press any key to exit.");

                switch (args.Length)
                {
                    case 2:
                        if (args[1].ToLower() == "changeip")
                        {
                            changeIP = true;
                            int AvailableIP = 0;
                            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

                            for (int i = 0; i < ips.Length; i++)
                            {
                                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                                {
                                    ip = ips[i];
                                    Console.WriteLine("{0}: {1}", i, ips[i]);
                                    ++AvailableIP;
                                }
                            }
                            
                            if (AvailableIP > 1)    // multiple IPv4 address detected
                            {
                                Console.WriteLine("Please select the address to listen.");
                                int select = Console.ReadKey().KeyChar - '0';
                                if (select > ips.Length || ips[select].AddressFamily != AddressFamily.InterNetwork)
                                {
                                    Console.WriteLine("No such option, exit."); return;
                                }
                                ip = ips[select];
                            }
                        }
                        else
                        {
                            try
                            {
                                ip = IPAddress.Parse(args[1]);
                                changeIP = true;
                            }
                            catch
                            {
                                Console.WriteLine("This is not a correct IP, exit.");
                                return;
                            }
                        }
                        goto case 1;
                    case 1:
                        if ((args[0].ToLower() == "cn") || (args[0].ToLower() == "ipv4"))
                            AM = AccessMethod.google_cn;
                        if (args[0].ToLower() == "ipv6" || args[0].ToLower() == "v6")
                            AM = AccessMethod.google_ipv6;
                        if (args[0].ToLower() == "hk")
                            AM = AccessMethod.google_hk;
                        changeRepo = true;
                        if (args[0].ToLower() == "default") // use the default option
                            changeRepo = false;
                        break;
                    case 0:
                        break;
                    default:
                        return;
                }
                if (changeRepo||changeIP)
                {
                    string[] iniFile = File.ReadAllLines(iniFilePath);
                    for (int i = 0; i < iniFile.Length; i++)
                    {
                        if (changeRepo)
                        {
                            if (iniFile[i].Contains("profile = "))
                            {
                                iniFile[i] = "profile = " + AM.ToString();
                                Console.WriteLine("Changed appspot to {0}.", AM.ToString());
                                continue;
                            }
                        }
                        if (changeIP)
                        {
                            if (iniFile[i].StartsWith("ip = "))
                            {
                                iniFile[i] = "ip = " + ip.ToString();
                                Console.WriteLine("Changed listen ip to {0}.", ip.ToString());
                                continue;
                            }
                        }
                        /// 1.8.5 version doesn't need this part any more.
                        //if (iniFile[i].Contains("hosts = "))
                        //{
                        //    iniFile[i] = "hosts = " + AccessMethod;
                        //    Console.WriteLine("Changed hosts to {0}.", AccessMethod);
                        //    continue;
                        //}
                    }
                    File.WriteAllLines(iniFilePath, iniFile);
                }

                if (Process.GetProcessesByName("Goagent").Length > 1)
                {
                    Console.WriteLine("Already started.");
                    return;
                }
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(exeFilePath);
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                
            }
            catch (IOException ioe)
            {
                Console.WriteLine("iniFilePath = {0}", iniFilePath);
                Console.WriteLine("exeFilePath = {0}", exeFilePath);
                Console.WriteLine(ioe.Message);
                Console.WriteLine(ioe.StackTrace);
                Console.ReadKey();
            }

            //Console.ReadKey();
        }
    }
}
