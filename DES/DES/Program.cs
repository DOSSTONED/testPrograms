﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    class Program
    {
        //初始置换表IP  64
        readonly int[] IP_Table = new int[]{  57,49,41,33,25,17,9,1,  
                                     59,51,43,35,27,19,11,3,  
                                     61,53,45,37,29,21,13,5,  
                                     63,55,47,39,31,23,15,7,  
                                     56,48,40,32,24,16,8,0,  
                                     58,50,42,34,26,18,10,2,  
                                     60,52,44,36,28,20,12,4,  
                                     62,54,46,38,30,22,14,6};
        //逆初始置换表IP^-1  64
        readonly int[] IP_1_Table = new int[]{
            39,7,47,15,55,23,63,31,  
               38,6,46,14,54,22,62,30,  
               37,5,45,13,53,21,61,29,  
               36,4,44,12,52,20,60,28,  
               35,3,43,11,51,19,59,27,  
               34,2,42,10,50,18,58,26,  
               33,1,41,9,49,17,57,25,  
               32,0,40,8,48,16,56,24};

        //扩充置换表E  48
        readonly int[] E_Table = new int[]{31, 0, 1, 2, 3, 4,  
                      3,  4, 5, 6, 7, 8,  
                      7,  8,9,10,11,12,  
                      11,12,13,14,15,16,  
                      15,16,17,18,19,20,  
                      19,20,21,22,23,24,  
                      23,24,25,26,27,28,  
                      27,28,29,30,31, 0};

        //置换函数P  32
        readonly int[] P_Table = new int[]{15,6,19,20,28,11,27,16,  
                      0,14,22,25,4,17,30,9,  
                      1,7,23,13,31,26,2,8,  
                      18,12,29,5,21,10,3,24};

        //S盒  8x4x16
        readonly int[, ,] S_Table = new int[,,]//S1  
                {{{14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},  
                  {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},  
                    {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},  
                    {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}},  
                    //S2  
                  {{15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},  
                  {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},  
                  {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},  
                  {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}},  
                  //S3  
                  {{10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},  
                  {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},  
                    {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},  
                  {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}},  
                  //S4  
                  {{7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},  
                  {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},  
                  {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},  
                  {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}},  
                  //S5  
                  {{2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},  
                  {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},  
                  {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},  
                  {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}},  
                  //S6  
                  {{12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},  
                  {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},  
                  {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},  
                  {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}},  
                  //S7  
                  {{4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},  
                  {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},  
                  {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},  
                  {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}},  
                  //S8  
                  {{13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},  
                  {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},  
                  {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},  
                  {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}}};
        //置换选择1  56
        readonly int[] PC_1 = new int[]{56,48,40,32,24,16,8,  
                  0,57,49,41,33,25,17,  
                  9,1,58,50,42,34,26,  
                  18,10,2,59,51,43,35,  
                  62,54,46,38,30,22,14,  
                  6,61,53,45,37,29,21,  
                  13,5,60,52,44,36,28,  
                  20,12,4,27,19,11,3};

        //置换选择2  48
        readonly int[] PC_2 = new int[]{13,16,10,23,0,4,2,27,  
                  14,5,20,9,22,18,11,3,  
                  25,7,15,6,26,19,12,1,  
                  40,51,30,36,46,54,29,39,  
                  50,44,32,46,43,48,38,55,  
                  33,52,45,41,49,35,28,31};

        //对左移次数的规定  16
        readonly int[] MOVE_TIMES = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };


        BitArray 密钥变换(BitArray key)
        {
            BitArray ret = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                ret[i] = key[PC_1[i]];
            }
            return ret;
        }

        BitArray[] 给出16个子密钥(BitArray key56)
        {
            BitArray[] resultInternal = new BitArray[16];
            resultInternal[0] = 循环左移操作(key56, MOVE_TIMES[0]);

            for (int i = 1; i < 16; i++)
                resultInternal[i] = 循环左移操作(resultInternal[i - 1], MOVE_TIMES[i]);

            BitArray[] ret = new BitArray[16];
            ////做置换
            //for (int i = 0; i < 16; i++)
            //{
            //    ret[i] = new BitArray(48);
            //    for (int j = 0; j < 48; j++)
            //    {
            //        ret[i][j] = resultInternal[i][PC_2[j]];
            //    }
            //}

            return resultInternal;
        }

        BitArray 循环左移操作(BitArray bits56, int move)
        {
            if (bits56.Length != 56) throw new ArgumentOutOfRangeException();
            bool head0 = bits56[0];
            bool head28 = bits56[28];

            if (move < 1) return bits56;

            /// 一次左移
            BitArray ret = new BitArray(bits56.Length);
            for (int i = 1; i < bits56.Length; i++)
                ret[i - 1] = bits56[i];
            ret[27] = head0;
            ret[55] = head28;

            if (move > 1)// 再次移位
                return 循环左移操作(ret, move - 1);
            else
                return ret;
        }

        BitArray 从56位密钥变48位子密钥(BitArray key56)
        {
            BitArray ret = new BitArray(48);
            if (key56.Length != 56) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < 48; i++)
                ret[i] = key56[PC_2[i]];
            return ret;
        }

        // [0] is Left, [1] is Right
        BitArray[] 明文变换(BitArray plain)
        {
            BitArray wholeArray = new BitArray(plain.Length);
            if (wholeArray.Length != 64) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < wholeArray.Length; i++)
                wholeArray[i] = plain[IP_Table[i]];
            BitArray[] ret = new BitArray[2];
            ret[0] = new BitArray(32);
            ret[1] = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                ret[0][i] = wholeArray[i];
                ret[1][i] = wholeArray[32 + i];
            }
            return ret;
        }

        BitArray 加密右边32变48(BitArray R32)
        {
            BitArray ret = new BitArray(48);
            if (R32.Length != 32) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < 48; i++)
                ret[i] = R32[E_Table[i]];
            return ret;
        }

        BitArray[] 右边32变48之后和K做异或(BitArray e3248, BitArray key48)
        {
            if (e3248.Length != key48.Length) throw new ArgumentOutOfRangeException();
            if (e3248.Length != 48) throw new ArgumentOutOfRangeException();
            BitArray cpy = new BitArray(e3248);
            BitArray int48 = cpy.Xor(key48);
            BitArray[] ret = new BitArray[8];
            for (int i = 0; i < 8; i++)
            {
                ret[i] = new BitArray(6);
                for (int j = 0; j < 6; j++)
                    ret[i][j] = int48[i * 6 + j];
            }
            return ret;
        }

        BitArray 做S变换(BitArray[] B)
        {
            if (B.Length != 8) throw new ArgumentOutOfRangeException();
            BitArray ret = new BitArray(32);
            int[] 每个盒子的结果 = new int[8];
            for (int i = 0; i < 8; i++)
            {
                int placeRow = (B[i][0] ? 2 : 0) + (B[i][5] ? 1 : 0);
                int placeCol = (B[i][1] ? 8 : 0) + (B[i][2] ? 4 : 0) + (B[i][3] ? 2 : 0) + (B[i][4] ? 1 : 0);
                int result = S_Table[i, placeRow, placeCol];
                ret[4 * i] = ((result >> 3) % 2) == 1;
                ret[4 * i + 1] = ((result >> 2) % 2) == 1;
                ret[4 * i + 2] = ((result >> 1) % 2) == 1;
                ret[4 * i + 3] = (result % 2) == 1;
            }

            return ret;
        }

        BitArray S变换后的变换与异或(BitArray B32, BitArray L32)
        {
            BitArray ba = new BitArray(32);
            if (B32.Length != 32) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < 32; i++)
                ba[i] = B32[P_Table[i]];
            return ba.Xor(L32);
        }

        BitArray Encrypt(BitArray plain, BitArray key, bool 加密)
        {

            BitArray[] keys16With56bits = 给出16个子密钥(密钥变换(key));
            //keys16With56bits.Reverse();
            BitArray[] keys16With48bits = new BitArray[16];
            for (int i = 0; i < 16; i++)
            {
                keys16With48bits[i] = 从56位密钥变48位子密钥(keys16With56bits[i]);
            }
            if (!加密)
            {
                BitArray tmp;
                tmp = keys16With48bits[0];
                for (int i = 0; i < 8; i++)
                {
                    tmp = keys16With48bits[i];
                    keys16With48bits[i] = keys16With48bits[15 - i];
                    keys16With48bits[15 - i] = tmp;
                }
                //keys16With48bits.Reverse();
            }
            BitArray[] plainBits = 明文变换(plain);
            BitArray[] L = new BitArray[17];
            BitArray[] R = new BitArray[17];
            L[0] = plainBits[0];
            R[0] = plainBits[1];
            int Round = 16;
            for (int i = 0; i < Round; i++)
            {
                BitArray Schanged = 做S变换(右边32变48之后和K做异或(加密右边32变48(R[i]), keys16With48bits[i]));
                //if (i > 0)
                //    L[i] = R[i - 1];
                //if (i < 15)
                R[i + 1] = S变换后的变换与异或(Schanged, L[i]);
                L[i + 1] = R[i];
                //R[i + 1] = S变换后的变换与异或(做S变换(右边32变48之后和K做异或(加密右边32变48(R[i]), keys16With48bits[i])), L[i]);
            }

            BitArray ret_1 = new BitArray(64);
            for (int i = 0; i < 32; i++)
            {
                /// 这里应该交换！！！！
                ret_1[32 + i] = L[Round][i];
                ret_1[i] = R[Round][i];
            }

            BitArray ret = new BitArray(64);
            for (int i = 0; i < 64; i++)
                ret[i] = ret_1[IP_1_Table[i]];

            return ret;
        }

        BitArray EncryptAndDisplay64bit(BitArray plain, BitArray key, bool 加密, bool display)
        {
            BitArray ret = Encrypt(plain, key, 加密);
            if (display)
            {
                DisplayBits(ret);
            }
            return ret;
        }

        static void DisplayBits(BitArray ba)
        {
            byte[] b = new byte[(ba.Length + 7) / 8];
            ba.CopyTo(b, 0);
            Console.WriteLine(BitConverter.ToString(b));
        }

        BitArray Encrypt64bit(BitArray plain, BitArray key)
        {
            return EncryptAndDisplay64bit(plain, key, true, false);
        }

        BitArray Decrypt64bit(BitArray plain, BitArray key)
        {
            return EncryptAndDisplay64bit(plain, key, false, false);
        }
        BitArray Decrypt64bit(BitArray plain, BitArray key, bool display)
        {
            return EncryptAndDisplay64bit(plain, key, false, display);
        }

        BitArray RoundTo64Bits(BitArray inputBits)
        {
            BitArray ret = new BitArray(((inputBits.Length + 63) / 64) * 64);
            for (int i = 0; i < ret.Length; i++)
                if (i < inputBits.Length) ret[i] = inputBits[i];
                else ret[i] = false;
            return ret;
        }

        static void Main(string[] args)
        {
            BitArray Keys = new BitArray(new int[] { 0x12345678, 0x76543210 });

            BitArray plainBit = new BitArray(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 });
            BitArray ciphered = new BitArray(64);

            byte[] p = new byte[8];
            byte[] K = new byte[8];
            plainBit.CopyTo(p, 0);
            Console.WriteLine("Original bits: " + BitConverter.ToString(p));
            Keys.CopyTo(K, 0);
            Console.WriteLine("Keys: " + BitConverter.ToString(K));

            Program pro = new Program();

            pro.EncryptAndDisplay64bit(pro.EncryptAndDisplay64bit(plainBit, Keys, true, true), Keys, true, true);

            Console.Write("Change Key: ");
            Keys[7] = !Keys[7];
            Keys.CopyTo(K, 0);
            Console.WriteLine(BitConverter.ToString(K));
            pro.EncryptAndDisplay64bit(pro.EncryptAndDisplay64bit(plainBit, Keys, true, true), Keys, true, true);

            Console.WriteLine("测试完毕，更改最后密钥的一位不影响加密效果。");

            #region ECB
            {
                Console.WriteLine("下面测试ECB");
                Random ran = new Random();
                BitArray[] ECBplain = new BitArray[3];
                BitArray[] ECBcipher = new BitArray[3];
                for (int i = 0; i < ECBplain.Length; i++)
                {
                    ECBplain[i] = GiveRandomBits();
                    DisplayBits(ECBplain[i]);
                    ECBcipher[i] = pro.Encrypt64bit(ECBplain[i], Keys);

                }
                Console.WriteLine();
                for (int i = 0; i < ECBplain.Length; i++) DisplayBits(pro.Decrypt64bit(ECBcipher[i], Keys));

                Console.WriteLine("ECB加密解密测试完毕，可以解密。");
            }
            #endregion

            #region CBC
            {
                Console.WriteLine("下面测试CBC");

                BitArray CBCIV = GiveRandomBits();
                BitArray[] CBCp = new BitArray[3];
                BitArray[] CBCc = new BitArray[3];
                for (int i = 0; i < CBCp.Length; i++)
                {
                    CBCp[i] = GiveRandomBits();
                    if (i > 0)
                        CBCc[i] = pro.Encrypt64bit((new BitArray(CBCp[i])).Xor(CBCc[i - 1]), Keys);
                    else
                        CBCc[i] = pro.Encrypt64bit((new BitArray(CBCIV)).Xor(CBCp[i]), Keys);

                    DisplayBits(CBCp[i]);
                }
                Console.WriteLine();

                for (int i = 0; i < CBCc.Length; i++)
                {
                    if (i > 0)
                        CBCp[i] = (pro.Decrypt64bit(CBCc[i], Keys)).Xor(CBCc[i - 1]);
                    else
                        CBCp[i] = (pro.Decrypt64bit(CBCc[i], Keys)).Xor(CBCIV);

                    DisplayBits(CBCp[i]);
                }

                Console.WriteLine("CBC加密解密测试完毕，可以解密。");
            }
            #endregion


            #region CFB
            {
                Console.WriteLine("下面测试CFB");
                BitArray IV = GiveRandomBits();
                BitArray[] CFBp = new BitArray[3];
                BitArray[] CFBc = new BitArray[3];

                for (int i = 0; i < CFBp.Length; i++)
                {
                    CFBp[i] = GiveRandomBits();
                    if (i > 0)
                        CFBc[i] = pro.Encrypt64bit(CFBc[i - 1], Keys).Xor(CFBp[i]);
                    else
                        CFBc[i] = pro.Encrypt64bit(IV, Keys).Xor(CFBp[i]);
                    DisplayBits(CFBp[i]);
                }

                Console.WriteLine();
                for (int i = 0; i < CFBc.Length; i++)
                {
                    if (i > 0)
                        CFBp[i] = pro.Encrypt64bit(CFBc[i - 1], Keys).Xor(CFBp[i]);
                    else
                        CFBp[i] = pro.Encrypt64bit(IV, Keys).Xor(CFBc[i]);
                    DisplayBits(CFBp[i]);
                }


                Console.WriteLine("CFB加密解密测试完毕，可以解密。这里是两次加密而不是一次加密一次解密");
            }
            #endregion

            #region OFB
            {
                Console.WriteLine("下面测试OFB");

                BitArray IV = GiveRandomBits();
                BitArray[] OFBp = new BitArray[3];
                BitArray[] OFBc = new BitArray[3];
                BitArray CFBo = GiveRandomBits();
                for (int i = 0; i < OFBp.Length; i++)
                {
                    OFBp[i] = GiveRandomBits();
                    if (i > 0)
                    {
                        OFBc[i] = pro.Encrypt64bit(CFBo, Keys).Xor(OFBp[i]);
                        CFBo = pro.Encrypt64bit(CFBo, Keys);
                    }
                    else
                    {
                        OFBc[i] = pro.Encrypt64bit(IV, Keys).Xor(OFBp[i]);
                        CFBo = pro.Encrypt64bit(IV, Keys);
                    }
                    DisplayBits(OFBp[i]);
                }
                Console.WriteLine();

                for (int i = 0; i < OFBc.Length; i++)
                {
                    if (i > 0)
                    {
                        OFBp[i] = pro.Encrypt64bit(CFBo, Keys).Xor(OFBc[i]);
                        CFBo = pro.Encrypt64bit(CFBo, Keys);
                    }
                    else
                    {
                        OFBp[i] = pro.Encrypt64bit(IV, Keys).Xor(OFBc[i]);
                        CFBo = pro.Encrypt64bit(IV, Keys);
                    }

                    DisplayBits(OFBp[i]);
                }

                Console.WriteLine("OFB加密解密测试完毕，可以解密。");
            }
            #endregion

            Console.ReadKey();
        }

        static Random ran = new Random();
        static BitArray GiveRandomBits()
        {

            BitArray ret = new BitArray(64);
            for (int j = 0; j < ret.Length; j++)
                ret[j] = ran.Next(2) == 1;
            return ret;
        }
    }
}
