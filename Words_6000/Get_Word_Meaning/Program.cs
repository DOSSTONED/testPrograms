using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Get_Word_Meaning
{
    class Program
    {
        static void Main(string[] args)
        {
            string dest = @"E:\Documents\GRE.txt";
            
            string[] words = File.ReadAllLines(dest);
            WebClient[] web = new WebClient[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                web[i] = new WebClient();
                //web.BaseAddress = @"http://dict.cn/ws.php?q=" + curWord;
                web[i].DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(web_DownloadFileCompleted);
                web[i].DownloadFileAsync(new Uri(@"http://dict.cn.sixxs.org/ws.php?q=" + words[i]), @"E:\Documents\GRE_List\" + words[i]);
                //Console.WriteLine(words[i]);
            }
            Console.ReadKey();
        }

        static void web_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine((sender as WebClient).BaseAddress);//throw new NotImplementedException();
        }
    }
}
