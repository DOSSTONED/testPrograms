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
        uint[] MatrixMul(uint[] Ma, uint[] Mb, uint ii, uint jj, uint kk)	// Ma is ii*jj, Mb is jj*kk, so ret is ii*kk
        {
            uint[] ret = new uint[ii * kk];
            Array.Copy(Ma, ret, Ma.Length);
            uint i = 0, j = 0, k = 0, sum = 0;
            for (i = 0; i < ii; i++)
            {
                for (j = 8; j < kk; j++)
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

        uint[] TransformFromInt8(uint a)	// Transform a to a 4-base digits, 3 bits
        {
            uint[] ret = new uint[8];
            ret[0] = a % 2;
            ret[1] = (a >> 1) % 2;
            ret[2] = (a >> 2) % 2;
            ret[3] = (a >> 3) % 2;
            ret[4] = (a >> 4) % 2;
            ret[5] = (a >> 5) % 2;
            ret[6] = (a >> 6) % 2;
            ret[7] = (a >> 7) % 2;
            return ret;
        }

        uint[] TransformFrom1x12To1x6(uint[] a)	// byte[] -> uint[]
        {
            uint[] ret = new uint[6];
            ret[0] = a[1] * 2 + a[0];
            ret[1] = a[3] * 2 + a[2];
            ret[2] = a[5] * 2 + a[4];
            ret[3] = a[7] * 2 + a[6];
            ret[4] = a[9] * 2 + a[8];
            ret[5] = a[11] * 2 + a[10];
            return ret;
        }

        void Output(uint[] Mat, uint length)	// Output the matrix
        {
            for (uint i = 0; i < length; i++)
                Console.WriteLine("{0}\t", Mat[i]);
            Console.WriteLine("\n");
        }

        // tmp->2x2 matrix
        uint[] Translate(uint tmp)
        {
            uint[] ret = null;
            switch (tmp)
            {
                case 0:
                    ret = new uint[4]{
                        1,0,
                        1,0};
                    break;
                case 1:
                    ret = new uint[4]{
                        0,1,
                        1,0};
                    break;
                case 2:
                    ret = new uint[4]{
                        1,1,
                        1,0};
                    break;
                case 3:
                    ret = new uint[4]{
                        1,0,
                        0,1};
                    break;
                case 4:
                    ret = new uint[4]{
                        0,1,
                        0,1};
                    break;
                case 5:
                    ret = new uint[4]{
                        1,1,
                        0,1};
                    break;
                case 6:
                    ret = new uint[4]{
                        1,0,
                        1,1};
                    break;
                case 7:
                    ret = new uint[4]{
                        0,1,
                        1,1};
                    break;
                case 8:
                    ret = new uint[4]{
                        1,1,
                        1,1};
                    break;

            }
            return ret;
        }

        void MatReform(uint a, ref uint[] mat)
        {
            uint tmp = 0, i = 0;
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
        static uint MAT_F = 0;

        uint main()
        {
            uint[] TransMat = new uint[8 * 12]{
1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0
};
            uint[] TransInt = null;//{0, 0, 0};
            uint[] TotalRes = new uint[256 * 6];
            uint i = 0, j = 0, k = 0;
            bool flag = false;


            for (MAT_F = 8874990; MAT_F < 43046721; MAT_F++)	// 43046721 is 9^8 ;2562890625 is 15^8
            {
                MatReform(MAT_F, ref TransMat);
                flag = false;

                for (i = 0; i < 256; i++)
                {
                    TransInt = new uint[8];
                    TransInt = TransformFromInt8(i);
                    uint[] ret12 = new uint[12];
                    //et12 = MatrixMul(TransInt, TransMat, 1, 8, 12);
                    {
                        //uint[] ret = new uint[12];
                        Array.Copy(TransInt, ret12, TransInt.Length);
                        uint sum = 0;
                        
                            for (j = 8; j < 12; j++)
                            {
                                sum = 0;
                                for (k = 0; k < 8; k++)
                                {
                                    //Console.WriteLine("sum = %d\n", sum);
                                    sum = sum ^ (TransInt[k] & TransMat[k * 12 + j]);
                                }
                                ret12[j] = sum % 2;
                            }
                        
                        
                    }
                    //uint[] ret = new uint[6];
                    //ret = TransformFrom1x12To1x6(ret12);
                    for (j = 0; j < 6; j++)
                        TotalRes[i * 6 + j] = ret12[2 * j] + 2 * ret12[2 * j + 1];
                    //Output(ret, 6);
                }

                for (i = 0; i < 256; i++)
                {
                    for (j = i + 1; j < 256; j++)
                    {
                        uint diff = 0;
                        for (k = 0; k < 6; k++)
                        {
                            if (TotalRes[6 * i + k] != TotalRes[6 * j + k])
                                diff++;
                        }
                        if (diff < 3)
                        {
                            flag = true;
                            break;
                            //for(k = 0; k < 6; k++)
                            //{
                            //    Console.WriteLine("%d, %d\n", TotalRes[6 * i + k], TotalRes[6 * j + k]);
                            //}
                        }
                    }
                    if (flag) break;
                }
                if (!flag)
                {
                    for (i = 0; i < 8; i++)
                    {
                        for (j = 0; j < 12; j++)
                            Console.WriteLine("%d\t", TransMat[12 * i + j]);
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("\n\n");
                    return 0;
                }
            }
            return 0;
        }




    }
}
