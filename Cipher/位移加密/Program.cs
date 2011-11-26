using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 位移加密
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            for (int k = 0; k < 26; k++)
                for (int i = 0; i < 26; i++)
                {
                    //i(105)->o(111), x(120)->c(99)
                    if ((((105 - 97) * i + k) % 26 == 111 % 26) && (((120 - 97) * i + k) % 26 == 99 % 26))
                    {
                        for (int j = 0; j < input.Length; j++)
                            Console.Write((char)(97 + ((input[j] - 97) * i + k) % 26));
                        Console.WriteLine();
                    }
                }
            Console.ReadKey();
        }
    }
}
