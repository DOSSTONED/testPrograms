using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fill_With_Little_Logic
{
    class Program
    {

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

        static void Main(string[] args)
        {
            string[] sudokus = Directory.GetFiles(@"E:\My Documents\Programming\Projects\数独\Sudokus", "*.hard");
            foreach (string curSudoku in sudokus)
            {
                int[] s_base = new int[81];
                int[] base_bak = new int[81];


                s_base = LoadBaseFromFile(curSudoku);
                bool test = CheckSudoko(s_base);
                base_bak = s_base.Clone() as int[];
                
                    base_bak = TryToSolve(s_base);
                    

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

        private int[] s_base = new int[81];
        private int[] base_bak = new int[81];

        class stepInfo
        {
            public int filledPlace = 0;
            public int filledNumber = 0;
            public bool isTry = false;

            public stepInfo(int place, int number, bool istry)
            {
                filledNumber = number;
                filledPlace = place;
                isTry = istry;
            }
        }

        static public int[] getCurrentAvaliable(int[] sudokuBase, int curPosition)
        {
            if (sudokuBase.Length != 81)
                return null;
            if (curPosition > 80 || curPosition < 0)
                return null;
            bool[] positionAvaliable = new bool[10] { false, true, true, true, true, true, true, true, true, true };
            int totalTrue = 0;

            int col = curPosition % 9, row = (curPosition - curPosition % 9) / 9;
            int blockCol = (col - col % 3) / 3, blockRow = (row - row % 3) / 3;
            for (int i = 0; i < 9; i++)
            {
                if (sudokuBase[col + 9 * i] != 0)
                    positionAvaliable[sudokuBase[col + 9 * i]] = false;
                if (sudokuBase[i + 9 * row] != 0)
                    positionAvaliable[sudokuBase[i + 9 * row]] = false;
                if (sudokuBase[27 * blockRow + blockCol + i % 3 + (i - i % 3) * 3] != 0)
                    positionAvaliable[sudokuBase[27 * blockRow + blockCol + i % 3 + (i - i % 3) * 3]] = false;
            }

            for (int i = 1; i < 10; i++)
            {
                if (positionAvaliable[i])
                    totalTrue++;
            }
            if (totalTrue <= 0)
                return null;
            int[] ret = new int[totalTrue];
            for (int i = 0; i < totalTrue; i++)
                ret[i] = getThePlaceIndex(positionAvaliable, i);
            return ret;
        }

        internal static int getThePlaceIndex(bool[] bo, int whichone)
        {
            int cur = whichone + 1;
            for (int i = 0; i < bo.Length; i++)
            {
                if (bo[i])
                    cur--;
                if (cur == 0)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Load the file content
        /// </summary>
        /// <param name="filepath">File path</param>
        /// <returns>the sudoko base</returns>
        static int[] LoadBaseFromFile(string filepath)
        {
            int curPosition = 0;
            int[] s_base = new int[81];
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
                return null;
            }
            finally
            {

            }
            return s_base;
        }

        public static bool VerifySudoko(int[] sudokuBase)
        {
            if (sudokuBase.Length != 81)
                return false;
            for (int i = 0; i < 81; i++)
            {
                if (sudokuBase[i] > 9)
                    return false;
            }


            /// Check Row
            /// 
            for (int i = 0; i < 9; i++)
            {
                int checksum = 0;
                for (int j = 0; j < 9; j++)
                    checksum += (int)Math.Pow(2, sudokuBase[9 * i + j]);
                if (checksum != 1022)
                    return false;
            }
            /// Check Col
            /// 
            for (int i = 0; i < 9; i++)
            {
                int checksum = 0;
                for (int j = 0; j < 9; j++)
                    checksum += (int)Math.Pow(2, sudokuBase[9 * j + i]);
                if (checksum != 1022)
                    return false;
            }
            /// Check square
            /// 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int[] cur = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
                        if (cur[k] != 1)
                            return false;
                }
            }
            return true;
        }

        static int[] TryToSolve(int[] s_base_in)
        {
            List<stepInfo> steps = new List<stepInfo>();
            int[] baseCopy = s_base_in.Clone() as int[];
            int minAva = 9, minIndex = 0;

            while (true)
            {
                /// print baseCopy
                /// 
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (j == 3 || j == 6)
                            Console.Write("|");
                        Console.Write(baseCopy[9 * i + j]);
                    }
                    Console.WriteLine();
                    if (i == 2 || i == 5)
                        Console.WriteLine("-----------");
                }
                Console.WriteLine();



                for (int i = 0; i < baseCopy.Length; i++)
                {
                    int[] cur = getCurrentAvaliable(baseCopy, i);
                    if (cur == null)
                    {
                        minAva = 0;
                    }
                    else
                    {
                        if (cur.Length < minAva)
                        {
                            minAva = cur.Length;
                            minIndex = i;
                        }
                    }
                }
                if (minAva == 0)// something wrong, must trackback
                {
                    for (int i = steps.Count - 1; i >= 0; i--)
                    {
                        if (!steps[i].isTry)// current is not a try step, just roll back
                        {
                            baseCopy[steps[i].filledPlace] = 0;
                            steps.RemoveAt(i);
                        }
                        else
                        {
                            baseCopy[steps[i].filledPlace] = 0;
                            int[] arrays = getCurrentAvaliable(baseCopy, steps[i].filledPlace);
                            int find_the_next = 0;
                            for (int loopi = 1; loopi < arrays.Length; loopi++)
                            {
                                if (arrays[loopi] > steps[loopi].filledNumber)
                                {
                                    find_the_next = arrays[loopi];
                                    break;
                                }
                            }
                            if (find_the_next != 0)
                            {
                                baseCopy[steps[i].filledPlace] = find_the_next;
                                steps[i].filledNumber = find_the_next;
                                break;
                            }
                            else// cannot find the next, must go back again
                            {
                                baseCopy[steps[i].filledPlace] = 0;
                                steps.RemoveAt(i);
                                continue;
                            }
                            
                        }
                    }
                }
                if (minAva == 1)
                {
                    baseCopy[minIndex] = getCurrentAvaliable(baseCopy, minIndex)[0];
                    steps.Add(new stepInfo(minIndex, baseCopy[minIndex], false));
                }
                if (minAva > 1)
                {
                    baseCopy[minIndex] = getCurrentAvaliable(baseCopy, minIndex)[0];
                    steps.Add(new stepInfo(minIndex, baseCopy[minIndex], true));
                }


                if (VerifySudoko(baseCopy))
                    break;
            }


            return baseCopy;
        }
    }
}
