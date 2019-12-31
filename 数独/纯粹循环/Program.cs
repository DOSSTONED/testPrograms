using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


///
/// 这个效率很低，比较适合解决我能解决的那种简单级别。。。
/// 而且如果空过多，效率低下，必须采用先按规则填写再调用这个算法
/// 
namespace 纯粹循环
{
    class Program
    {
        static int[] s_base = new int[81];
        static int[] base_bak = new int[81];
        static int curPlace = 0;
        static bool isReverse = false;
        static void Main(string[] args)
        {
            

            string[] sudokus = Directory.GetFiles(@"E:\My Documents\Programming\Projects\数独\Sudokus", "*.txt");
            foreach (string curSudoku in sudokus)
            {
                s_base = new int[81];
                base_bak = new int[81];
                curPlace = 0;
                isReverse = false;


                LoadBaseFromFile(curSudoku);
                bool test = CheckSudoko(s_base);
                base_bak = s_base.Clone() as int[];
                while (true)
                {
                    TryToSolve();
                    if (CheckSudoko(s_base) && curPlace > 80)
                        break;

                    if (args.Length == 1 && args[0].ToLower() == "nodisplay")
                    {
                    }
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (j == 3 || j == 6)
                                    Console.Write("|");
                                Console.Write(s_base[9 * i + j]);
                            }
                            Console.WriteLine();
                            if (i == 2 || i == 5)
                                Console.WriteLine("-----------");
                        }
                        Console.WriteLine();
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (j == 3 || j == 6)
                            Console.Write("|");
                        Console.Write(s_base[9 * i + j]);
                    }
                    Console.WriteLine();
                    if (i == 2 || i == 5)
                        Console.WriteLine("-----------");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadKey();
            }
            Console.ReadKey();
        }
        public static bool CheckSudoko(int[] sudokuBase)
        {
            if (sudokuBase.Length != 81)
                return false;
            for (int i = 0; i < 81; i++)
            {
                if (sudokuBase[i] > 9)
                    return false;
            }
            int[] cur = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            /// Check Row
            /// 
            for (int i = 0; i < 9; i++)
            {
                cur = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int j = 0; j < 9; j++)
                    if (sudokuBase[9 * i + j] != 0)
                        cur[sudokuBase[9 * i + j]]++;
                for (int k = 1; k < 10; k++)
                    if (cur[k] > 1)
                        return false;
            }
            /// Check Col
            /// 
            for (int i = 0; i < 9; i++)
            {
                cur = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int j = 0; j < 9; j++)
                    if (sudokuBase[9 * j + i] != 0)
                        cur[sudokuBase[9 * j + i]]++;
                for (int k = 1; k < 10; k++)
                    if (cur[k] > 1)
                        return false;
            }
            /// Check square
            /// 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cur = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    cur[sudokuBase[27 * i + 3 * j]]++;
                    cur[sudokuBase[27 * i + 3 * j + 1]]++;
                    cur[sudokuBase[27 * i + 3 * j + 2]]++;
                    cur[sudokuBase[27 * i + 3 * j + 9]]++;
                    cur[sudokuBase[27 * i + 3 * j + 10]]++;
                    cur[sudokuBase[27 * i + 3 * j + 11]]++;
                    cur[sudokuBase[27 * i + 3 * j + 18]]++;
                    cur[sudokuBase[27 * i + 3 * j + 19]]++;
                    cur[sudokuBase[27 * i + 3 * j + 20]]++;
                    for (int k = 1; k < 10; k++)
                        if (cur[k] > 1)
                            return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Load the file content
        /// </summary>
        /// <param name="filepath">File path</param>
        /// <returns>true if load success</returns>
        static bool LoadBaseFromFile(string filepath)
        {
            int curPosition = 0;
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Open);
                while (fs.Position < fs.Length)
                {
                    int curByte = fs.ReadByte();
                    if (curByte >= 0x30 && curByte <= 0x39)
                    {
                        s_base[curPosition++] = curByte - 0x30;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {

            }
            return true;
        }

        /// <summary>
        /// To solve
        /// </summary>
        /// <param name="curPlace">The place which will be filled</param>
        /// <returns>If false returned, then it must be traceback to modify something.</returns>
        static void TryToSolve()
        {
            if (base_bak[curPlace] != 0)
            {
                if (isReverse)
                    curPlace--;
                else
                    curPlace++;
                return;
            }
            if (!isReverse)// normal fill
            {
                int i = 0;
                for (i = 1; i < 10; i++)
                {
                    s_base[curPlace] = i;
                    if (CheckSudoko(s_base)) break;
                }
                if (i > 9)// something wrong, this place cannot fill with any number, so ,go back
                {
                    isReverse = true;
                    if (base_bak[curPlace] == 0)
                        s_base[curPlace] = 0;
                    curPlace--;
                    return;
                }
                curPlace++;
            }
            else // something wrong, is going back
            {
                int i = 0;
                for (i = s_base[curPlace] + 1; i < 10; i++)
                {
                    s_base[curPlace] = i;
                    if (CheckSudoko(s_base))
                    {
                        isReverse = false;
                        curPlace++;
                        return;
                    }
                }
                if (i > 9)
                {
                    isReverse = true;
                    if (base_bak[curPlace] == 0)
                        s_base[curPlace] = 0;
                    curPlace--;
                    return;
                }
            }
        }

    }
}
