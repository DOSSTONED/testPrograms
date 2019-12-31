using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Give_Base4_Dis3_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            
            Timer t = new Timer();
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Interval = 5000;
            t.Start();
            p.main();
        }



        static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Currently is: {0}, {1}%", MAT_F, (float)MAT_F / 430467.21);
            //throw new NotImplementedException();
        }
        /*
找出一组6位4进制编码，使得码字中至少距离为3
首先列出所有12位2进制编码，找出其中码字距离为3，4，5，6的序列
之后逐个筛选。
3位->6位（4进制）
或者8位二进制->12位


#include <stdio.h>
*/

//Specialised since Mb is I8 + M8x4
//1x8->1x12
        int[] MatrixMul(int[] Ma, int[] Mb, int ii, int jj, int kk)	// Ma is ii*jj, Mb is jj*kk, so ret is ii*kk
        {
            int[] ret = new int[ii * kk];
            //Array.Copy(Ma, ret, Ma.Length);
            int i = 0, j = 0, k = 0, sum = 0;
            for (i = 0; i < ii; i++)
            {
                for (j = 0; j < kk; j++)
                {
                    sum = 0;
                    for (k = 0; k < jj; k++)
                    {
                        //Console.WriteLine("sum = %d\n", sum);
                        sum = sum ^ (Ma[i * jj + k] & Mb[k * kk + j]);
                    }
                    ret[i * kk + j] = sum % 2;
                }
            }
            return ret;
        }

        int[] TransformFromInt7(int a)	// Transform a to a 4-base digits, 3 bits
        {
            int[] ret = new int[7];
            ret[0] = a % 2;
            ret[1] = (a >> 1) % 2;
            ret[2] = (a >> 2) % 2;
            ret[3] = (a >> 3) % 2;
            ret[4] = (a >> 4) % 2;
            ret[5] = (a >> 5) % 2;
            ret[6] = (a >> 6) % 2;
            
            return ret;
        }

        int[] TransformFrom1x12To1x6(int[] a)	// byte[] -> int[]
        {
            int[] ret = new int[6];
            ret[0] = a[1] * 2 + a[0];
            ret[1] = a[3] * 2 + a[2];
            ret[2] = a[5] * 2 + a[4];
            ret[3] = a[7] * 2 + a[6];
            ret[4] = a[9] * 2 + a[8];
            ret[5] = a[11] * 2 + a[10];
            return ret;
        }

        void Output(int[] Mat, int length)	// Output the matrix
        {
            for (int i = 0; i < length; i++)
                Console.WriteLine("{0}\t", Mat[i]);
            Console.WriteLine("\n");
        }

        // tmp->2x2 matrix
        int[] Translate(int tmp)
        {
            int[] ret = null;
            switch (tmp)
            {
                case 0:
                    ret = new int[4]{
                        1,0,
                        1,0};
                    break;
                case 1:
                    ret = new int[4]{
                        0,1,
                        1,0};
                    break;
                case 2:
                    ret = new int[4]{
                        1,1,
                        1,0};
                    break;
                case 3:
                    ret = new int[4]{
                        1,0,
                        0,1};
                    break;
                case 4:
                    ret = new int[4]{
                        0,1,
                        0,1};
                    break;
                case 5:
                    ret = new int[4]{
                        1,1,
                        0,1};
                    break;
                case 6:
                    ret = new int[4]{
                        1,0,
                        1,1};
                    break;
                case 7:
                    ret = new int[4]{
                        0,1,
                        1,1};
                    break;
                case 8:
                    ret = new int[4]{
                        1,1,
                        1,1};
                    break;

            }
            return ret;
        }

        void MatReform_OLD(int a, ref int[] mat)
        {
            int tmp = 0, i = 0;
            tmp = a % 9;
            mat[24 * i + 8] = Translate(tmp)[0];
            mat[24 * i + 9] = Translate(tmp)[1];
            mat[24 * i + 20] = Translate(tmp)[2];
            mat[24 * i + 21] = Translate(tmp)[3];
            tmp = (a / 9) % 9;
            mat[24 * i + 10] = Translate(tmp)[0];
            mat[24 * i + 11] = Translate(tmp)[1];
            mat[24 * i + 22] = Translate(tmp)[2];
            mat[24 * i + 23] = Translate(tmp)[3];
            i++;

            tmp = (a / 81) % 9;
            mat[24 * i + 8] = Translate(tmp)[0];
            mat[24 * i + 9] = Translate(tmp)[1];
            mat[24 * i + 20] = Translate(tmp)[2];
            mat[24 * i + 21] = Translate(tmp)[3];
            tmp = (a / 729) % 9;
            mat[24 * i + 10] = Translate(tmp)[0];
            mat[24 * i + 11] = Translate(tmp)[1];
            mat[24 * i + 22] = Translate(tmp)[2];
            mat[24 * i + 23] = Translate(tmp)[3];
            i++;


            tmp = (a / 6561) % 9;
            mat[24 * i + 8] = Translate(tmp)[0];
            mat[24 * i + 9] = Translate(tmp)[1];
            mat[24 * i + 20] = Translate(tmp)[2];
            mat[24 * i + 21] = Translate(tmp)[3];
            tmp = (a / 59049) % 9;
            mat[24 * i + 10] = Translate(tmp)[0];
            mat[24 * i + 11] = Translate(tmp)[1];
            mat[24 * i + 22] = Translate(tmp)[2];
            mat[24 * i + 23] = Translate(tmp)[3];
            i++;

            tmp = (a / 531441) % 9;
            mat[24 * i + 8] = Translate(tmp)[0];
            mat[24 * i + 9] = Translate(tmp)[1];
            mat[24 * i + 20] = Translate(tmp)[2];
            mat[24 * i + 21] = Translate(tmp)[3];
            tmp = (a / 4782969) % 9;
            mat[24 * i + 10] = Translate(tmp)[0];
            mat[24 * i + 11] = Translate(tmp)[1];
            mat[24 * i + 22] = Translate(tmp)[2];
            mat[24 * i + 23] = Translate(tmp)[3];
        }
        static int MAT_F = 0;

        int main()
        {
            int[] TransMat = new int[7 * 12]{
1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0,
0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0,
0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1,
0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0,
0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1,
0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1,
0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0
//0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0
};
            int[] TransInt = null;//{0, 0, 0};
            int[] TotalRes = new int[128 * 6];
            int i = 0, j = 0, k = 0;
            bool flag = false;
            Random ran = new Random();
            
            while (true)
            {
                for (i = 0; i < 7; i++)
                {
                    for (j = 7; j < 12; j++)
                    {
                        TransMat[12 * i + j] = ran.Next(2);
                    }
                }


                flag = false;

                for (i = 0; i < 128; i++)
                {
                    TransInt = new int[7];
                    TransInt = TransformFromInt7(i);
                    int[] ret12 = new int[12];
                    ret12 = MatrixMul(TransInt, TransMat, 1, 7, 12);

                    //int[] ret = new int[6];
                    //ret = TransformFrom1x12To1x6(ret12);
                    for (j = 0; j < 6; j++)
                        TotalRes[i * 6 + j] = ret12[2 * j] + 2 * ret12[2 * j + 1];
                    //Output(ret, 6);
                }

                for (i = 0; i < 128; i++)
                {
                    for (j = i + 1; j < 128; j++)
                    {
                        int diff = 0;
                        for (k = 0; k < 6; k++)
                        {
                            if (TotalRes[6 * i + k] != TotalRes[6 * j + k])
                                diff++;
                        }
                        if (diff < 3)
                        {
                            flag = true;

                            //for (k = 0; k < 6; k++)
                            //{
                            //    Console.WriteLine("{0}, {1}", TotalRes[6 * i + k], TotalRes[6 * j + k]);
                            //}
                            break;
                        }
                    }
                    if (flag) break;
                }
                if (!flag)
                {
                    for (i = 0; i < 7; i++)
                    {
                        for (j = 0; j < 12; j++)
                            Console.Write("{0} ", TransMat[12 * i + j]);
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n\n");

                    for (i = 0; i < 128; i++)
                    {
                        for (k = 0; k < 6; k++)
                            Console.Write("{0} ", TotalRes[6 * i + k]);
                        Console.WriteLine();
                    }

                    return 0;
                }
            }

            return 0;
        }




    }
}
