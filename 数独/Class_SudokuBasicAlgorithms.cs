using System;

public static class SudokoAlgorithms
{
    public static int[] getCurrentAvaliable(int[] sudokuBase, int curPosition)
    {
        if (sudokuBase.Length != 81)
            return null;
        if (curPosition > 80 || curPosition < 0)
            return null;
        bool[] positionAvaliable = new bool[10];
        int totalTrue = 0;



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

    internal int getThePlaceIndex(bool[] bo, int whichone)
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
    /// Check whether there is a conflict
    /// </summary>
    /// <returns>false indicates the duplicate numbers</returns>
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

    public static bool VerifySudoko(int[] sudokoBase)
}
