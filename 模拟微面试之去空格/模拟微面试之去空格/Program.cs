using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace 模拟微面试之去空格
{
    class Program
    {
        public static int RemoveMultipleSpaces(char[] baseStr)
        {
            //int SpaceBegin = -1, SpaceEnd = -1;   //  空格起始位置，终止位置
            //int idx = 0;    //    循环变量
            //for (idx = 0; idx < baseStr.Length; idx++)
            //{
            //    if (baseStr[idx] != ' ')
            //    {
            //        SpaceBegin = -1;  //  -1代表当前没空格
            //        continue;
            //    }
            //    if (SpaceBegin == -1) //  最开始的空格
            //    {
            //        SpaceBegin = idx;
            //    }
            //}
            //if (baseStr == null) return 0;
            int idx = 0, pointer = 0;   // 循环变量和指向缩减过的位置；
            bool isSpaceEntered = false;
            for (idx = 0; idx < baseStr.Length; )
            {
                if (baseStr[idx] != ' ')
                {
                    //if (pointer < idx)
                        baseStr[pointer++] = baseStr[idx++];
                    //idx++;
                    //pointer++;
                    isSpaceEntered = false;
                    continue;
                }

                if (isSpaceEntered) //  当前已经不是第一个空格了，所以忽略，跳至下一个
                {
                    idx++;
                    continue;
                }
                else
                {
                    isSpaceEntered = true;
                    //if (pointer < idx)
                        baseStr[pointer++] = baseStr[idx++];
                    //idx++;
                   // pointer++;
                }

            }
            return pointer;// 返回值是新字符串的长度，原字符串依然可以访问到这之后的位置。

        }

        public static int RemoveMultipleSpaces_2227969(char[] baseStr)
        {
            int r = 0, w = 0;
            char c;
            while (r < baseStr.Length)
            {
                c = baseStr[r];
                baseStr[w++] = c;
                r++;
                if (c == ' ')
                {
                    while (r < baseStr.Length && baseStr[r] == ' ')
                        r++;
                }
            }
            return w;
        }

        static char[][] TestTextx = new char[100000][];
        static void TEST(int TotalLines)
        {
            
            char[][] TestText1 = new char[TotalLines][];
            char[][] TestText2 = new char[TotalLines][];
            for (int i = 0; i < TotalLines; i++)
            {
                TestText1[i] = new char[TestTextx[i].Length];
                TestText2[i] = new char[TestTextx[i].Length];
                for (int j = 0; j < TestTextx[i].Length; j++)
                {
                    TestText1[i][j] = TestTextx[i][j];
                    TestText2[i][j] = TestTextx[i][j];
                }
            }
            Thread.Sleep(1000);
            DateTime begin = DateTime.Now; DateTime end = DateTime.Now;


            Console.WriteLine("TotalLines: " + TotalLines.ToString());

            begin = DateTime.Now;
            for (int i = 0; i < TotalLines; i++)
            {
                RemoveMultipleSpaces_2227969(TestText2[i]);
            }
            end = DateTime.Now;
            Console.Write(end - begin);
            Console.WriteLine("    RemoveMultipleSpaces_2227969");

            begin = DateTime.Now;
            for (int i = 0; i < TotalLines; i++)
            {
                RemoveMultipleSpaces(TestText1[i]);
            }
            end = DateTime.Now;
            Console.Write(end - begin);
            Console.WriteLine("    RemoveMultipleSpaces");

            

            

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // 可以产生随机的字符串，将字符串进行处理。
            Random ran = new Random();
            for (int i = 0; i < TestTextx.Length; i++)
            {
                TestTextx[i] = new char[ran.Next(1000)];
                for (int j = 0; j < TestTextx[i].Length; j++)
                    TestTextx[i][j] = (char)ran.Next(0x1C, 0x21);
            }
            for (int i = 1; i < 6; i++)
            {
                TEST((int)Math.Pow(10, i));
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            ScannersTest st = new ScannersTest();
            st.AllSpacesTest();
            st.KeepSingleSpaceTest();
            st.RemoveOneInnterSpaceBlockTest();
            st.RemoveTwoInnterSpaceBlocksTest();
            st.AllMoreSpacesTest();
            //st.NullTest();
        }
    }



    //[TestFixture]
    public class ScannersTest
    {
        //[Test]
        public void NullTest()
        {
            char[] input = null;

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(0, resultLength);

            input = new char[]{};

            resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(0, resultLength);
            Assert.AreEqual(string.Empty, new string(input, 0, resultLength));
        }

        //[Test]
        public void RemoveOneInnterSpaceBlockTest()
        {
            char[] input = "abc   def".ToCharArray();

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(7, resultLength);
            Assert.AreEqual("abc def", new string(input, 0, resultLength));
        }

        //[Test]
        public void RemoveTwoInnterSpaceBlocksTest()
        {
            char[] input = "abc def ghi".ToCharArray();

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(11, resultLength);
            Assert.AreEqual("abc def ghi", new string(input, 0, resultLength));
        }

        //[Test]
        public void KeepSingleSpaceTest()
        {
            char[] input = " a  b   d e   ".ToCharArray();

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(9, resultLength);
            Assert.AreEqual(" a b d e ", new string(input, 0, resultLength));
        }

        //[Test]
        public void AllSpacesTest()
        {
            char[] input = " ".ToCharArray();

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(1, resultLength);
            Assert.AreEqual(" ", new string(input, 0, resultLength));
        }

        //[Test]
        public void AllMoreSpacesTest()
        {
            char[] input = "     ".ToCharArray();

            int resultLength = Program.RemoveMultipleSpaces(input);

            Assert.AreEqual(1, resultLength);
            Assert.AreEqual(" ", new string(input, 0, resultLength));
        }
    }


}
