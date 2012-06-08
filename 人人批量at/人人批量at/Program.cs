using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 人人批量at
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class BatchAt
    {

        // 把一段可能含有unicode编码的字符串换成字符串
        public static String unistrToString(String unistr)
        {
            const int UNICODE_LEN = 4;
            StringBuilder sb = new StringBuilder();
            int val = 0;
            for (int i = 0; i < unistr.Length; i++)
            {
                char ch = unistr[i];
                if (ch == '\\')
                {
                    if (i < unistr.Length - 1 && unistr[i + 1] == 'u')
                    {
                        if (unistr.Length - 2 - i >= UNICODE_LEN)
                        {
                            val = 0;
                            for (int j = 1; j <= UNICODE_LEN; j++)
                            {
                                char nch = unistr[i + 1 + j];
                                if (Char.IsDigit(nch))
                                    val = val * 16 + (nch - '0');
                                else if (Char.IsLetter(nch))
                                {
                                    nch = Char.ToLower(nch);
                                    val = val * 16 + (nch - 'a') + 10;
                                }

                            }
                            sb.Append((char)val);
                            i += (UNICODE_LEN + 1);
                        }
                    }
                    else
                        sb.Append(ch);
                }
                else
                    sb.Append(ch);
            }
            return sb.ToString();
        }
        static void batchAt(String infile, String groupName)
        {
            try
            {

                //BufferedReader br = getbr(infile);
                String line = File.ReadAllText(infile);//br.readLine();

                List<String[]> mats = null;
                MatchCollection mc = Regex.Matches(line, "\"id\":(\\d+).*?\"name\":\"([^\"]*)\".*?\"groups\":\\[([^\\]]*)\\]");
                int cnt = 0;
                foreach (Match m in mc)
                //{
                //}
                //foreach (String[] mat in mats)
                {
                    string[] mat = m.Result("").Split(new string[] { "\"id\":", "\"name\":", "\"groups\":" }, StringSplitOptions.RemoveEmptyEntries);
                    //mats
                    String id = mat[1].Trim();
                    String name = unistrToString(mat[2].Trim());
                    String groups = unistrToString(mat[3].Trim());
                    if (groups.Contains(groupName))
                    {
                        Console.Write(" @" + name + "(" + id + ") ");
                        cnt++;
                        if (cnt % 10 == 0)
                            Console.WriteLine();
                    }
                }
                //br.close();
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }
        }
        public static void main(String[] args)
        {
            batchAt("friends.txt", "分组名称");
        }
    }
}
