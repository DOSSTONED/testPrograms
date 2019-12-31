using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinet.Core.IO.Ntfs;
using System.IO;

namespace NTFS_Stream_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string Path = @"E:\123.txt";
                const string StreamName = "stream1";

                //FileInfo file = new FileInfo(Path);
                //if (!file.Exists) 


                //if (!file.AlternateDataStreamExists(StreamName))
                //{
                //}

                if (!File.Exists(Path)) throw new FileNotFoundException(null, Path);

                if (!FileSystem.AlternateDataStreamExists(Path, StreamName))
                {
                    Console.WriteLine("Stream not found; creating it...");

                    AlternateDataStreamInfo data = FileSystem.GetAlternateDataStream(Path, StreamName);
                    FileStream fsWriter = data.OpenWrite();
                    fsWriter.WriteByte(65);
                    fsWriter.Close();
                }
                else
                {
                    AlternateDataStreamInfo data = FileSystem.GetAlternateDataStream(Path, StreamName);
                    FileStream fsWriter = data.OpenWrite();
                    fsWriter.WriteByte(97);
                    fsWriter.Close();
                }

                AlternateDataStreamInfo data1 = FileSystem.GetAlternateDataStream(Path, StreamName);
                
                FileStream fsReader1 = data1.OpenRead();
                byte[] buffer = new byte[100];
                fsReader1.Read(buffer, 0, Convert.ToInt32(fsReader1.Length));
                FileStream fsWriter1 = data1.OpenWrite();
                fsWriter1.WriteByte(49);
                fsWriter1.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
