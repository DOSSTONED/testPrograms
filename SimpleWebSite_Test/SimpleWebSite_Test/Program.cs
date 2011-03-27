using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SimpleWebSite_Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = Properties.Resources.GRE.Split('\n');

            string Headers = "<html><head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF8\">\n<title>GRE词汇表</title>\n</head>\n<body>\n";
            string Enders = "</body></html>";
            Random ran = new Random();
            int beginLine = 0, endLine = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("<body>"))
                    beginLine = i + 1;
                if (lines[i].Contains("</body>"))
                    endLine = i - 1;
            }


                if (!HttpListener.IsSupported)
                {
                    Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                    return;
                }

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            listener.Prefixes.Add("http://10.10.10.10:41519/");
            listener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                bool isReadyToQuit = false;

                string responseString = "<!DOCTYPE html PUBLIC \"-//WAPFORUM//DTD XHTML Mobile 1.0//EN\" \"http://www.wapforum.org/DTD/xhtml-mobile10.dtd\">\r\n <HTML><BODY> Hello world!</BODY></HTML>";

                if (request.RawUrl.ToLower() == @"/quit")
                {
                    responseString = "<HTML><BODY> Bye!</BODY></HTML>";
                    isReadyToQuit = true;
                }
                else
                {
                    responseString = Headers;
                    //string[] readyToShow = new string[100];
                    for (int i = 0; i < 100; i++)
                    {

                        string curLine = lines[ran.Next(beginLine, endLine)];
                        while (curLine == "")
                        {
                            curLine = lines[ran.Next(beginLine, endLine)];
                        }
                        //readyToShow[i] = curLine;
                        responseString += curLine + "\r\n";

                    }
                    responseString += Enders;

                }

                
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                Console.WriteLine(request.RawUrl);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                if (isReadyToQuit)
                {
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
            }
            Console.WriteLine("quited.");
            listener.Stop();
            
        }
    }
}
