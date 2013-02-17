using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace BatchSSH
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 11; i < 255; i++)
            {
                Console.WriteLine("------------------------------------");
                
                try
                {
                    var connectionInfo = new KeyboardInteractiveConnectionInfo("162.105.209." + i.ToString(), 61022, "pi");
                    connectionInfo.AuthenticationPrompt += delegate(object sender, AuthenticationPromptEventArgs e)
                    {
                        foreach (var prompt in e.Prompts)
                        {
                            if (prompt.Request.ToLower().Contains("password"))
                            {
                                prompt.Response = "password";
                            }
                        }
                    };
                    SshClient s = new SshClient(connectionInfo);

                    s.Connect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("IP: " + i.ToString());
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("------------------------------------");
            }
        }
    }
}
