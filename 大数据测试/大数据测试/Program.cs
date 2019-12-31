using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 大数据测试
{
    static class Members
    {
        static public string dataDest = @"D:\tmp\BigDataTest\Numbers.dat";
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            if (File.Exists(Members.dataDest))
            {
                Console.WriteLine("File: {0} exists.", Members.dataDest);
                return;
            }
            DateTime _start, _end;
            _start = DateTime.Now;
            byte[] TotalBytes = new byte[sizeof(int) * 1024 * 1024 * 128];
            Random r = new Random();
            r.NextBytes(TotalBytes);
            _end = DateTime.Now;
            Console.WriteLine(_end - _start);

            _start = DateTime.Now;
            System.IO.File.WriteAllBytes(Members.dataDest, TotalBytes);
            _end = DateTime.Now;
            Console.WriteLine(_end - _start);
            Console.ReadLine();
        }
    }
}
