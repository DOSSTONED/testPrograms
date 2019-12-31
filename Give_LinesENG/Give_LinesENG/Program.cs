using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Give_LinesENG
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"E:\My Documents\Desktop\template.txt");
            string[] ENGlines = new string[lines.Length / 5];
            int curPlace = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 2)
                {
                    if ((lines[i][0] >= 'A' && lines[i][0] <= 'z') || (lines[i][1] >= 'A' && lines[i][1] <= 'z'))
                    {
                        ENGlines[curPlace++] = lines[i];
                    }
                }
            }

            File.WriteAllLines(@"E:\My Documents\Desktop\template_1.txt", ENGlines);
        }
    }
}
