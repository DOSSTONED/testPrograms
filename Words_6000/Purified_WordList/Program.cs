using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Purified_WordList
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] wordLines = File.ReadAllLines(@"E:\Documents\GRE.htm");
            string[] words = new string[wordLines.Length];
            int totalLineNumber = 0;

            for (int i = 0; i < wordLines.Length; i++)
            {
                if (wordLines[i].Contains("3g.dict.cn"))
                {
                    words[totalLineNumber] = wordLines[i].Substring(wordLines[i].IndexOf("s.php?q=") + 8);
                    words[totalLineNumber] = words[totalLineNumber].Substring(0, words[totalLineNumber].IndexOf('\"'));
                    if (words[totalLineNumber].Contains("20"))
                    {
                        continue;
                    }
                    totalLineNumber++;
                    continue;
                }
                //wordLines[i] = string.Empty;
            }
            string[] writtenWords = new string[totalLineNumber];
            for (int i = 0; i < totalLineNumber; i++)
            {
                writtenWords[i] = words[i];
            }
            
            File.WriteAllLines(@"E:\Documents\GRE.txt", writtenWords);
        }
    }
}
