using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RAS;

namespace RasDial
{
    class Program
    {
        static void Main(string[] args)
        {
            RAS.RasManager rm = new RasManager();
            rm.EntryName = "rwpppoe";
            rm.Password = "41519";
            rm.UserName = "3:\r\nbqUt9oqDn";
            int a = rm.Connect();
            //Console.WriteLine("Return message = {0}", a);
            if (a == 0)
                Console.WriteLine("连接成功，按任意键退出");
            else
                Console.WriteLine("连接失败，错误代码{0}，按任意键退出", a);
            Console.ReadKey();
        }
    }
}
