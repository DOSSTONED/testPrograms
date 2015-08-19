using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CorruptFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"T:\Downloads\Music\世界十大背景音乐无损\He's A Pirate - Klaus Badelt.flac";
            var bytes = File.ReadAllBytes(filePath);
            bytes[bytes.Length - 1] = 0;
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
