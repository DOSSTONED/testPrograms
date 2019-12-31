using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 找出中位数
{
    static class Members
    {
        static public string dataDest = @"D:\tmp\BigDataTest\Numbers.dat";
    }

    class Program
    {
        static byte[] AllBytes;
        static int[] AllNumbers;
        static void Main(string[] args)
        {
            Program p = new Program();
            DateTime _start = DateTime.Now;
            AllBytes = File.ReadAllBytes(Members.dataDest);
            Console.WriteLine("Read {0} bytes in {1} ms.", AllBytes.Length, (DateTime.Now - _start).Milliseconds);
            
            //_start = DateTime.Now;
            AllNumbers = p.ParseBytes(AllBytes);
            AllBytes = new byte[1]; // free the byte array.
            Console.WriteLine("Parsing bytes into ints at {0} ms.", (DateTime.Now - _start).Milliseconds);
            
            //_start = DateTime.Now;
            Console.WriteLine("The number in the middle is:{0}.", p.给出中位数(AllNumbers));
            Console.WriteLine("Total Time: {0} ms.", (DateTime.Now - _start).Milliseconds);
        }

        int[] ParseBytes(byte[] bytes)
        {
            int[] ret = new int[(bytes.Length + 3) / 4];
            for (int i = 0; i < bytes.Length; i = i + 4)
            {
                ret[i / 4] = ((int)bytes[i] << 24) + ((int)bytes[i + 1] << 16) + ((int)bytes[i + 2] << 8) + ((int)bytes[i + 3]);
            }
            return ret;
        }

        int 给出中位数(int[] input)
        {
            DateTime _start = DateTime.Now;
            int ret = 0;
            int numbersLessThanMiddle = 0;
            int groupedNumbers = 0;
            int[] sorted = new int[65536];// since maxint-minint=4294967295, hence we need 65536 to sort them.
            //分组，每65536个数算一组，也就是说前2字节一样的归为一类
            for (int i = 0; i < sorted.Length; i++) sorted[i] = 0;
            for (int i = 0; i < input.Length; i++) sorted[(input[i] / 65536) + 32768]++;
            Console.WriteLine("Init Time: {0} ms.", (DateTime.Now - _start).Milliseconds);
            {
                int i = 0;
                while (numbersLessThanMiddle + sorted[i] < input.Length / 2)
                {
                    numbersLessThanMiddle += sorted[i++];
                }//找到中位数在哪个组
                //ret = 65536 * i;    //
                ret = 65536 * (i - 32768);//这是中位数所在范围，ret~ret+65535;
                numbersLessThanMiddle = sorted[i];
            }
            Console.WriteLine("Grouped Time: {0} ms.", (DateTime.Now - _start).Milliseconds);

            for (int i = 0; i < sorted.Length; i++) sorted[i] = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < ret) continue;
                if (input[i] > ret + 65535) continue;
                sorted[input[i] - ret]++;
            }
            Console.WriteLine("Reinit Time: {0} ms.", (DateTime.Now - _start).Milliseconds);
            {
                groupedNumbers = 0;
                int i = 0;
                while (groupedNumbers + sorted[i] < numbersLessThanMiddle / 2)
                {
                    groupedNumbers += sorted[i++];
                }
                //找到中位数在哪个组
                //ret = 65536 * i;    //
                ret = ret + i;
            }
            Console.WriteLine("Function Time: {0} ms.", (DateTime.Now - _start).Milliseconds);
            return ret;
        }
    }
}
