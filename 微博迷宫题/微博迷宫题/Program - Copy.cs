using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 微博迷宫题
{
    class Program
    {
        static void Main(string[] args)
        {
            Step init = new Step(2017, 0);
            int layer = 0;
            bool found = false;
            List<Step> allSteps = new List<Step>();
            List<Step> nextSteps = new List<Step>();
            allSteps.Add(init);
            while (layer++ < 50 && !found)
            {
                nextSteps = new List<Step>();
                foreach (Step s in allSteps)
                {
                    List<Step> tmp = getMidReverseNext(s);
                    if (tmp == null) continue;
                    if (tmp.Count == 0) continue;
                    nextSteps.AddRange(tmp);
                    if (s.currentValue == 2018)
                    {
                        found = true;
                        Console.WriteLine("Found, layer={0}, cur={1}, up={2}", layer, s.currentValue, s.upperStep);
                        break;
                    }
                }
                allSteps = nextSteps;
            }
            Console.ReadLine();
        }

        static double getNext(double ori, int next)
        {
            switch (next)
            {
                case 7:
                    return ori + 7;
                case 2:
                    return ori / 2;
                case 5:
                    return ori - 5;
                case 3:
                    return ori * 3;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        static double getMidNext(double ori_mid, int next)
        {
            switch (next)
            {
                case 7:
                    return (ori_mid + 7) / 2;
                case 2:
                    return (ori_mid / 2) + 7;
                case 5:
                    return (ori_mid - 5) * 3;
                case 3:
                    return (ori_mid * 3) - 5;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public class Step
        {
            public UInt64 currentValue;
            public int upperStep;
            public Step(UInt64 c, int s) { currentValue = c; upperStep = s; }
        }

        // from 2017 to 2018
        static List<Step> getMidReverseNext(Step s)
        {
            List<Step> ret = new List<Step>();
            switch (s.upperStep)
            {
                case 7:
                    ret.Add(new Step(2 * s.currentValue - 7,7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, 3));
                    break;
                case 2:
                    ret.Add(new Step(2 * s.currentValue - 14,2));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, 3));
                    break;
                case 5:
                    ret.Add(new Step(2 * s.currentValue - 14,2));
                    ret.Add(new Step(2 * s.currentValue - 7, 7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    //if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, 3));
                    break;
                case 3:
                    ret.Add(new Step(2 * s.currentValue - 14,2));
                    ret.Add(new Step(2 * s.currentValue - 7, 7));
                    //if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, 3));
                    break;
                case 0:// initial step;
                    ret.Add(new Step(2 * s.currentValue - 14,2));
                    ret.Add(new Step(2 * s.currentValue - 7, 7));
                    if (s.currentValue % 3 == 0) ret.Add(new Step(s.currentValue / 3 + 5, 5));
                    if (s.currentValue % 3 == 1) ret.Add(new Step((s.currentValue + 5) / 3, 3));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ret = ret.FindAll(x => x.currentValue > 0);
            return ret;
        }
    }
}
