using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
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
            int idx = 0, pointer = 0;   // 循环变量和指向缩减过的位置；
            bool isSpaceEntered = false;
            for (idx = 0; idx < baseStr.Length; )
            {
                if (baseStr[idx] != ' ')
                {
                    if (pointer < idx)
                        baseStr[pointer] = baseStr[idx];
                    idx++;
                    pointer++;
                    isSpaceEntered = false;
                    continue;
                }
                else
                {
                    if (isSpaceEntered) //  当前已经不是第一个空格了，所以忽略，跳至下一个
                    {
                        idx++;
                        continue;
                    }
                    else
                    {
                        isSpaceEntered = true;
                        if (pointer < idx)
                            baseStr[pointer] = baseStr[idx];
                        idx++;
                        pointer++;
                    }
                }
            }
            return pointer;// 返回值是新字符串的长度
        }

        static void Main(string[] args)
        {
            ScannersTest st = new ScannersTest();
            st.AllSpacesTest();
            st.KeepSingleSpaceTest();
            st.RemoveOneInnterSpaceBlockTest();
            st.RemoveTwoInnterSpaceBlocksTest();
            st.AllMoreSpacesTest();
        }
    }



    //[TestFixture]
    public class ScannersTest
    {
        //[Test]
        public void RemoveOneInnterSpaceBlockTest()
        {
            char[] input = "abc def".ToCharArray();

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
            char[] input = " a b d e ".ToCharArray();

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
