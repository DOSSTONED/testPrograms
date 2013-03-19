using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace DES_File_Handle
{
    class Program
    {
        static BitArray GiveRandomBits()
        {
            Random ran = new Random();
            BitArray ret = new BitArray(64);
            for (int j = 0; j < ret.Length; j++)
                ret[j] = ran.Next(2) == 1;
            return ret;
        }

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Args: {FileToChange} {encrypt|decrypt} {keyfile}");
                return;
            }
            bool isDecrypt = false;

            FileInfo fi = new FileInfo(args[0]);
            if (fi.Length > 1024 * 1024 * 1024 || (fi.Length % 8) != 0)
            {
                Console.WriteLine("Input file is not formatted.");
                return;
            }
            FileInfo keyfile = new FileInfo(args[2]);
            if (keyfile.Length != 8)
            {
                Console.WriteLine("Key file is not 8 bytes.");
                return;
            }

            if (args[1].ToLower() != "decrypt" && args[1].ToLower() != "encrypt")
            {
                Console.WriteLine("Mode must be encrypt or decrypt");
                return;
            }

            Console.WriteLine("Begin doing...");

            isDecrypt = args[1].ToLower() == "decrypt";

            byte[] contentBytes = new byte[fi.Length];
            byte[] keyBytes = new byte[keyfile.Length];
            using (FileStream fs = fi.OpenRead())
            {
                fs.Read(contentBytes, 0, (int)fi.Length);
                fs.Close();
            }
            using (FileStream fs = keyfile.OpenRead())
            {
                fs.Read(keyBytes, 0, (int)keyfile.Length);
                fs.Close();
            }
            BitArray key = new BitArray(keyBytes);
            BitArray content = new BitArray(contentBytes);
            BitArray parsed = new BitArray(contentBytes);

            BitArray plain = new BitArray(64);
            BitArray cipher = new BitArray(64);
            DES des = new DES();

            for (int i = 0; i * 64 < content.Length; i++)
            {
                for (int j = 0; j < 64; j++)
                    plain[j] = content[64 * i + j];

                if (isDecrypt)
                    cipher = des.Decrypt64bit(plain, key);
                else
                    cipher = des.Encrypt64bit(plain, key);

                for (int j = 0; j < 64; j++)
                    parsed[64 * i + j] = cipher[j];
            }

            byte[] cc = new byte[parsed.Length / 8];
            parsed.CopyTo(cc, 0);
            File.WriteAllBytes(fi.FullName + args[1], cc);
            byte[] keys = new byte[8];
            key.CopyTo(keys, 0);
            Console.WriteLine("Please remember the key!!");
            Console.WriteLine(BitConverter.ToString(keys));
            Console.ReadKey();
        }
    }
}
