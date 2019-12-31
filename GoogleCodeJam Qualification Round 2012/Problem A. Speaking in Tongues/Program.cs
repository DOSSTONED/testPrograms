using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Problem_A.Speaking_in_Tongues
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                return;
            string[] allText = File.ReadAllLines(args[0]);
            int TotalCases = int.Parse(allText[0]);
            string outPath = "Problem_A.out";
            File.WriteAllText(outPath, string.Empty);
            for (int i = 1; i <= TotalCases; i++)
            {
                Console.WriteLine("Case #{0}: {1}", i, Decipher(allText[i]));
                File.AppendAllText(outPath, string.Format("Case #{0}: {1}\r\n", i, Decipher(allText[i])));
            }
        }

        static string Decipher(string ciphered)
        {
            string str = string.Empty;
            for (int i = 0; i < ciphered.Length; i++)
            {
                str += Decipher(ciphered[i]);
            }
            return str;
        }

        static char Decipher(char ciphered)
        {
            switch (ciphered)
            {
                case ('a'): return 'y';
                case ('n'): return 'b';
                case ('f'): return 'c';
                case ('i'): return 'd';
                case ('c'): return 'e';
                case ('w'): return 'f';
                case ('l'): return 'g';
                case ('b'): return 'h';
                case ('k'): return 'i';
                case ('u'): return 'j';
                case ('o'): return 'k';
                case ('m'): return 'l';
                case ('x'): return 'm';
                case ('s'): return 'n';
                case ('e'): return 'o';
                case ('v'): return 'p';
                case ('q'): return 'z';///
                case ('p'): return 'r';
                case ('d'): return 's';
                case ('r'): return 't';
                case ('j'): return 'u';
                case ('g'): return 'v';
                case ('t'): return 'w';
                case ('h'): return 'x';
                case ('y'): return 'a';
                case ('z'): return 'q';///
                default: return ciphered;
            }
        }
    }
}
