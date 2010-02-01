using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Problem_C.Recycled_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                return;
            string[] allText = File.ReadAllLines(args[0]);
            int allCases = int.Parse(allText[0]);
            string outPath = "Problem_C.out";
            File.WriteAllText(outPath, string.Empty);
            for (int i = 1; i < allText.Length; i++)
            {
                string[] Numbers = allText[i].Split(' ');
                if (Numbers.Length == 2)
                {
                    int A = int.Parse(Numbers[0]);
                    int B = int.Parse(Numbers[1]);
                    int totalCase = 0;
                    for (int j = A; j <= B; j++)
                    {
                        int[] RecycledJ = GiveRecycleNumbers(j);
                        if (RecycledJ == null)
                        {
                            break;
                        }
                        for (int k = 0; k < RecycledJ.Length; k++)
                        {
                            if (RecycledJ[k] <= j) continue;
                            if (RecycledJ[k] > B) continue;
                            bool isRepeated = false;
                            for (int kk = k + 1; kk < RecycledJ.Length; kk++)
                            {
                                if (RecycledJ[kk] == RecycledJ[k]) isRepeated = true;
                            }
                            if (!isRepeated)
                                totalCase++;// this is not accurate since 1212-> 2121, 2121 two times.
                            //File.AppendAllText("OutDebug.txt", string.Format("A={0},B={1}, pair:({2},{3})\r\n", A, B, j, rj));
                        }
                    }
                    Console.WriteLine("Case #{0}: {1}\r\n", i, totalCase);
                    File.AppendAllText(outPath, string.Format("Case #{0}: {1}\r\n", i, totalCase));
                    
                }

            }
        }

        static int[] GiveRecycleNumbers(int num)
        {
            int totalDigits = num.ToString().Length;
            if (totalDigits == 1) return null;

            int[] ret = new int[totalDigits - 1];
            for (int i = 0; i < totalDigits - 1; i++)
            {

                ret[i] = (num * (int)Math.Pow(10, i + 1)) % (int)Math.Pow(10, totalDigits) +
                    (num * (int)Math.Pow(10, i + 1) - ((num * (int)Math.Pow(10, i + 1)) % (int)Math.Pow(10, totalDigits))) / (int)Math.Pow(10, totalDigits)
                    ;
            }
            return ret;
        }
    }
}
