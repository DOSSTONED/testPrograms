using System;
using System.Collections.Generic;

namespace 微博迷宫题
{
    class Program
    {
        static void Main(string[] args)
        {
            /// 开始节点与最终节点
            Step init = new Step(2017, 0);
            Step finall = new Step(0, 0);
            /// 当前搜索层数
            int layer = 0;
            /// 是否发现符合要求的结果
            bool found = false;
            /// 当前所有可能走的步骤
            List<Step> allSteps = new List<Step>();
            /// 下一轮所有可能走的步骤
            List<Step> nextSteps = new List<Step>();
            /// 添加开始节点
            allSteps.Add(init);

            while (layer++ < 50 && !found)
            {
                nextSteps = new List<Step>();
                /// 对于当前可走步骤，逐个找下一步
                foreach (Step s in allSteps)
                {
                    List<Step> tmp = getMidReverseNext(s);
                    if (tmp == null) continue;
                    if (tmp.Count == 0) continue;
                    nextSteps.AddRange(tmp);
                    /// 目标找到
                    if (s.currentValue == 2018)
                    {
                        found = true;
                        finall = s;
                        Console.WriteLine("Found, layer={0}, cur={1}", layer, s.currentValue);
                        break;
                    }
                }
                allSteps = nextSteps;
                //Console.WriteLine("Layer = {0}", layer);
            }

            UInt64 cur = finall.currentValue;
            /// 反向找上一层的数据
            for (int i = 0; i <= finall.upperStep.Count - 1; i++)
            {
                cur = getMidRevNext(cur, finall.upperStep[i]);
                //Console.WriteLine("upper = {0}, value = {1}", finall.upperStep[i], cur);
            }
            Console.WriteLine("Reversed output finished, next is to show how to solve the original problem.");
            
            /// 从头开始找，并逐步列出步骤            
            for (int i = finall.upperStep.Count - 1; i >= 0; i--)
            {
                cur = (UInt64)getMidNext(cur, finall.upperStep[i]);
                Console.WriteLine("Next = {0}, value = {1}", finall.upperStep[i], cur);
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// 正向给出下一步的数据
        /// </summary>
        /// <param name="ori_mid">原始值</param>
        /// <param name="next">寻路方向</param>
        /// <returns>下一步的值</returns>
        static UInt64 getMidNext(UInt64 ori_mid, int next)
        {
            switch (next)
            {
                case 7:
                    Console.WriteLine("({0} + 7) / 2", ori_mid);
                    return (ori_mid + 7) / 2;
                case 2:
                    Console.WriteLine("({0} / 2) + 7", ori_mid);
                    return (ori_mid / 2) + 7;
                case 5:
                    Console.WriteLine("({0} - 5) * 3", ori_mid);
                    return (ori_mid - 5) * 3;
                case 3:
                    Console.WriteLine("({0} * 3) - 5", ori_mid);
                    return (ori_mid * 3) - 5;
                case 0:
                    return ori_mid;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 反向给出上一步的顺序
        /// </summary>
        /// <param name="x">初始值</param>
        /// <param name="next">寻路方向</param>
        /// <returns>上一步的值</returns>
        static UInt64 getMidRevNext(UInt64 x, int next)
        {
            switch (next)
            {
                case 7:
                    //Console.WriteLine("(2 * {0}) - 7", x);
                    return 2 * x - 7;
                case 2:
                    //Console.WriteLine("2 * ({0} - 7)", x);
                    return 2 * x - 14;
                case 5:
                    //Console.WriteLine("({0} / 3) + 5", x);
                    return x / 3 + 5;
                case 3:
                    //Console.WriteLine("({0} + 5) / 3", x);
                    return (x + 5) / 3;
                case 0:
                    return x;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 表示步骤的类
        /// </summary>
        public class Step
        {
            /// <summary>
            /// 走到这一步为止当前值
            /// </summary>
            public UInt64 currentValue;
            /// <summary>
            /// 走的步骤
            /// </summary>
            public List<int> upperStep = new List<int>();

            /// <summary>
            /// 初始化一个步骤（在第一步的时候用到，其他不用）
            /// </summary>
            /// <param name="c">当前值</param>
            /// <param name="s">方向值</param>
            public Step(UInt64 c, int s) { currentValue = c; upperStep.Add(s); }
            
            /// <summary>
            /// 由某一步走下一步
            /// </summary>
            /// <param name="c">当前值</param>
            /// <param name="s">当前步骤</param>
            /// <param name="next">下一步的方向</param>
            public Step(UInt64 c, Step s, int next)
            {
                currentValue = c;
                upperStep = new List<int>();
                for (int i = 0; i < s.upperStep.Count; i++)
                    upperStep.Add(s.upperStep[i]);
                upperStep.Add(next);
            }
        }

        /// <summary>
        /// 反向给出下一步可走的列表（是从2017到2018的方向）
        /// </summary>
        /// <param name="s">当前步骤</param>
        /// <returns>当前步骤可以走到的所有下一步骤</returns>
        static List<Step> getMidReverseNext(Step s)
        {
            List<Step> ret = new List<Step>();
            switch (s.upperStep[s.upperStep.Count - 1])
            {
                case 7:
                    ret.Add(new Step(2 * s.currentValue - 7, s, 7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, s, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, s, 3));
                    break;
                case 2:
                    ret.Add(new Step(2 * s.currentValue - 14, s, 2));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, s, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, s, 3));
                    break;
                case 5:
                    ret.Add(new Step(2 * s.currentValue - 14, s, 2));
                    ret.Add(new Step(2 * s.currentValue - 7, s, 7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, s, 5));
                    //if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, s, 3));
                    break;
                case 3:
                    ret.Add(new Step(2 * s.currentValue - 14, s, 2));
                    ret.Add(new Step(2 * s.currentValue - 7, s, 7));
                    //if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, s, 3));
                    break;
                case 0:// initial step;
                    ret.Add(new Step(2 * s.currentValue - 14, s, 2));
                    ret.Add(new Step(2 * s.currentValue - 7, s, 7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, s, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, s, 3));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ret = ret.FindAll(x => x.currentValue > 0);
            return ret;
        }
    }
}
