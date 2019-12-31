using System;

namespace 数独_Form_V1
{
    public class SudokoAlg
    {
        public void Initialise(ref int[] shudu)
        {
            for (int i = 0; i < shudu.Length; i++)
            {
                Sudoko1[i] = new int();
                Sudoko1[i] = shudu[i];
                if (shudu[i] == 0)
                {
                    CanBeTried[CanBeTried.Length] = new int();
                    CanBeTried[CanBeTried.Length] = shudu[i];
                }
            }
        }

        public int Enum(ref int[] a)
        {
            return 0;
        }

        public int AlgLv1(ref int[] a)
        {
            return 0;
        }

        public int AlgLv2(ref int[] a)
        {
            return 0;
        }



        private int[] Sudoko1;
        private int[] CanBeTried;

        private int standard = 1022;

        public int CheckSudoko(int[] Sudoko)				// 0 for not right, 1 for right
        {

            int every9number = 0;
            for (int i = 0; i < 9; i++)
            {
                every9number = 0;
                for (int j = 0; j < 9; j++)
                {
                    every9number += (int)Math.Pow(2.0, Sudoko[9 * i + j]);
                }

                if (every9number != standard)
                    return 0;
            }


            for (int i = 0; i < 9; i++)
            {
                every9number = 0;
                for (int j = 0; j < 9; j++)
                {
                    every9number += (int)Math.Pow(2.0, Sudoko[9 * j + i]);
                }

                if (every9number != standard)
                    return 0;
            }


            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    every9number = 0;
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 1]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 2]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 9]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 10]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 11]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 18]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 19]);
                    every9number += (int)Math.Pow(2.0, Sudoko[27 * i + 3 * j + 20]);

                    if (every9number != standard)
                        return 0;
                }
            }
            return 1;
        }



        void initialstand(int[] p)
        {
            for (int i = 0; i < 10; i++)
                p[i] = 0;
        }


        int JudgeSudoko(int[] sudoko) // 1 for no problem, 0 for wrong sudoko
{
	int[] stand = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[sudoko[9 * i + j]]++;
		}
		for(int k = 1; k < 10; k++)
		{
			if(stand[k] > 1)
				return 0;
		}
		
	}

	initialstand(stand);
	for(int i = 0; i < 9; i++)
	{
		initialstand(stand);
		for(int j = 0; j < 9; j++)
		{
			stand[sudoko[9 * j + i]]++;
		}
		for(int k = 1; k < 10; k++)
		{
			if(stand[k] > 1)
				return 0;
		}
	}

	initialstand(stand);
	for(int i = 0; i < 3; i++)
	{
		
		for(int j = 0; j < 3; j++)
		{
			initialstand(stand);
			stand[sudoko[27 * i + 3 * j]]++;
			stand[sudoko[27 * i + 3 * j + 1]]++;
			stand[sudoko[27 * i + 3 * j + 2]]++;
			stand[sudoko[27 * i + 3 * j + 9]]++;
			stand[sudoko[27 * i + 3 * j + 10]]++;
			stand[sudoko[27 * i + 3 * j + 11]]++;
			stand[sudoko[27 * i + 3 * j + 18]]++;
			stand[sudoko[27 * i + 3 * j + 19]]++;
			stand[sudoko[27 * i + 3 * j + 20]]++;
			for(int k = 1; k < 10; k++)
			{
				if(stand[k] > 1)
				return 0;
			}
		}
	}
	return 1;
}


       int fill1by1(int[] a, int length)
        {
            int i = 0;
            for (i = 0; i < length; i++)
                if (a[i] == 0)
                    break;
            int j = 0;
            for (j = 1; j < 10; j++)
            {
                a[i] = j;
                if (JudgeSudoko(Sudoko1) != 0)
                {
                    return 0;
                }
            }

            if (i == 0)
                return 1;
            else
            {
                a[i] = 0;
                a[i - 1] = a[i - 1] + 1;
                int k = i - 1;
                while (k >= 0 && (a[k]) > 9)
                {
                    a[k] = 0;
                    a[k - 1] = a[k - 1] + 1;
                    k--;
                }
            }
            return 0;
        }
    }
}