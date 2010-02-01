using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Problem_B.Dancing_With_the_Googlers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                return;
            string[] allText = File.ReadAllLines(args[0]);
            int TotalCases = int.Parse(allText[0]);
            string outPath = "Problem_B.out";
            File.WriteAllText(outPath, string.Empty);
            for (int i = 1; i <= TotalCases; i++)
            {
                string[] strs = allText[i].Split(' ');
                int Number = int.Parse(strs[0]);
                int Stars = int.Parse(strs[1]);
                int p = int.Parse(strs[2]);
                int total = 0;
                int canUseStar = 0;
                for (int j = 0; j < Number; j++)
                {
                    int CurrentPerson = int.Parse(strs[j + 3]);
                    if( (CurrentPerson < 29 )&&( CurrentPerson > 1))//28=8+10+10,can use star
                        canUseStar++;
                    if (CurrentPerson >= 3 * p - 2)
                    {
                        total++;

                        continue;
                    }
                    if (CurrentPerson < 3 * p - 4) continue;

                    if (3 * p - 4 > 0)
                    {
                        if (Stars > 0)
                        {
                            total++; Stars--; canUseStar--;// must use star to gain more totals
                        }
                    }
                    else// 3p-4<0, that is, p=0 or p=1
                    // this time, CurrentPerson < 3 * p - 2
                    // which means currentPerson==0 or 1
                    {
                        if (CurrentPerson == 0)
                            if (p == 0)
                            { total++; continue; }
                            else continue;
                        total++;//CurrentPerson==1, p==0or1 both are ok
                    }

                }
                if (Stars != 0 && canUseStar < Stars)
                {
                    Console.WriteLine(string.Format("Case #{0}: Stars are not all used.", i));
                    File.AppendAllText(outPath, string.Format("Case #{0}: 0\r\n", i));
                }
                else
                {
                    Console.WriteLine(string.Format("Case #{0}: {1}", i, total));
                    File.AppendAllText(outPath, string.Format("Case #{0}: {1}\r\n", i, total));
                }
            }
        }
    }
}
