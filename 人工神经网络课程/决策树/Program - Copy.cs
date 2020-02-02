using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 决策树
{
    class Program
    {

        static void Main(string[] args)
        {
            /// 样本们
            /// 
            Sample[] Samples = new Sample[]{
                new Sample(Category.w1,new char[]{'A','E','H','K','M'}),
                new Sample(Category.w1,new char[]{'B','E','I','L','M'}),
                new Sample(Category.w1,new char[]{'A','G','I','L','N'}),
                new Sample(Category.w1,new char[]{'B','G','H','K','M'}),
                new Sample(Category.w1,new char[]{'A','G','I','L','M'}),
                new Sample(Category.w2,new char[]{'B','F','I', 'L', 'M'}),
                new Sample(Category.w2,new char[]{'B','F', 'J', 'L', 'N'}),
                new Sample(Category.w2,new char[]{'B','E' ,'I', 'L', 'N'}),
                new Sample(Category.w2,new char[]{'C','G' ,'J', 'K', 'N' }),
                new Sample(Category.w2,new char[]{'C','G', 'J','L', 'M' }),
                new Sample(Category.w2,new char[]{'D','G', 'J', 'K', 'M'}),
                new Sample(Category.w2,new char[]{'B','G', 'I', 'L', 'M'})
            };

            Sample toClassify1 = new Sample(Category.not_decided, new char[] { 'B', 'G', 'I', 'K', 'N' });
            Sample toClassify2 = new Sample(Category.not_decided, new char[] { 'C', 'E', 'J', 'L', 'M' });

            /// 头节点，顶点
            /// 
            DNode head = new DNode();
            /// 待验证的特征
            /// 
            head.remainFeatures.AddRange(new char[] { 'A', 'E', 'H', 'K', 'M' });
            /// 添加样本
            /// 
            head.allSamples = Samples;
            /// 那些待验证的节点的集合
            /// 
            List<DNode> toBeRelaxed = new List<DNode>();
            /// 添加头节点到该集合中
            /// 
            toBeRelaxed.Add(head);
            /// 集合不空就要逐个验证
            /// 
            while (toBeRelaxed.Count != 0)
            {
                /// 取出第一个节点
                /// 
                DNode cur = toBeRelaxed[0];
                toBeRelaxed.RemoveAt(0);
                /// 获取子节点
                /// 
                cur.children = cur.getNext();
                /// 如果子节点非空，那就添加到待验证的集合中
                /// 
                if (cur.children != null)
                {
                    if (cur.children.Count != 0)
                        toBeRelaxed.AddRange(cur.children);
                }
            }
            /// 输出分类
            /// 
            print(head, string.Empty);

            Console.WriteLine();
            Console.WriteLine(toClassify1.features);
            print(head, string.Empty, toClassify1);
            Console.WriteLine(toClassify2.features);
            print(head, string.Empty, toClassify2);
            Console.ReadKey();

        }

        /// <summary>
        /// 递归方式输出类别
        /// </summary>
        /// <param name="d">当前节点</param>
        /// <param name="str">前面已经分类的信息</param>
        static void print(DNode d,string str)
        {
            //Console.Write(d.currentSubFeature);
            if ((d.children == null) || (d.children.Count == 0))
            {
                Console.Write(str);
                Console.Write(" = {0}", d.currentCategory);
                Console.WriteLine();
                return;
            }
            foreach (DNode dd in d.children)
            {
                print(dd, str + dd.currentSubFeature);
            }
        }

        /// <summary>
        /// 和print一样，只不过是在遍历的时候附带了测试是否为所给样本对应的类别。
        /// </summary>
        /// <param name="d">当前节点</param>
        /// <param name="str">前面已经分类的信息</param>
        /// <param name="s">待测样本</param>
        static void print(DNode d, string str, Sample s)
        {
            //Console.Write(d.currentSubFeature);
            if ((d.children == null) || (d.children.Count == 0))
            {
                Console.Write(str);
                Console.Write(" = {0}", d.currentCategory);
                Console.WriteLine();
                return;
            }
            bool printed = false;
            foreach (DNode dd in d.children)
            {
                if (s.features.Contains(dd.currentSubFeature))
                {
                    printed = true;
                    print(dd, str + dd.currentSubFeature, s);
                    return;
                }
            }
            if (printed == false)
            {
                Console.Write(str);
                Console.Write(" = {0}", d.currentCategory);
                Console.WriteLine();
                return;
            }
        }


        //static double info(double[] D)
        //{
        //    double ret = 0;
        //    double total = D.Sum();
        //    foreach (double d in D)
        //    {
        //        ret += d / total * Math.Log(d / total, 2);
        //    }
        //    return -ret;
        //}
    }

    /// <summary>
    /// 类别
    /// </summary>
    enum Category
    {
        /// <summary>
        /// 第一类
        /// </summary>
        w1,
        /// <summary>
        /// 第二类
        /// </summary>
        w2,
        /// <summary>
        /// 没决定
        /// </summary>
        not_decided, 
        /// <summary>
        /// 无法决定，是因为它的样本包含两类内容
        /// </summary>
        cannot_decide
    }

    class Sample
    {
        /// <summary>
        /// 本样本的类别
        /// </summary>
        public Category cat;
        /// <summary>
        /// 本样本的特征
        /// </summary>
        public char[] features;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="c">类别</param>
        /// <param name="fs">所有特征</param>
        public Sample(Category c, char[] fs)
        {
            cat = c; features = fs;
        }
    }

    class DNode
    {
        /// <summary>
        /// 当前节点所有特征
        /// </summary>
        public Sample[] allSamples;
        /// <summary>
        /// 当前主特征
        /// </summary>
        public char currentMainFeature;
        /// <summary>
        /// 当前子特征
        /// </summary>
        public char currentSubFeature;
        /// <summary>
        /// 当前类别
        /// </summary>
        public Category currentCategory=Category.not_decided;
        /// <summary>
        /// 所有儿子
        /// </summary>
        public List<DNode> children=new List<DNode>();
        /// <summary>
        /// 剩下没分类的特征
        /// </summary>
        public List<char> remainFeatures=new List<char>();

        //static double I(double i1, double i2)
        //{
        //    return -(i1/(i1+i2)*Math.Log(i1/(i1+i2),2)+i2/(i1+i2)*Math.Log(i2/(i1+i2),2));
        //}

        /// <summary>
        /// 对应于那个\sum{P_i * log(P_i)}，计算信息熵
        /// </summary>
        /// <param name="D">每个特征别对应的实例个数</param>
        /// <returns>熵</returns>
        static double info(double[] D)
        {
            double ret = 0;
            double total = D.Sum();
            foreach (double d in D)
            {
                if (d > 0)
                    ret += d / total * Math.Log(d / total, 2);
            }
            return -ret;
        }

        /// <summary>
        /// 获取当前节点的子节点的可能取值
        /// </summary>
        /// <returns>子节点们</returns>
        public List<DNode> getNext()
        {
            /// 如果他们都在同一类，那就没必要再分特征了
            if ((allSamples.Count(x => x.cat == Category.w1) == allSamples.Length) ||
                 (allSamples.Count(x => x.cat == Category.w2) == allSamples.Length))
            {
                this.currentCategory = allSamples[0].cat;
                return null;
            }
            /// 没有儿子，说明分类到此为止
            if (remainFeatures.Count == 0)
                return null;    /// this one is end;
                                /// 

            /// 找到最小的信息熵，它对应的所在分类
            char minFeature = '\0';
            double minInfo = double.MaxValue;
            double maxInfo = double.MinValue;
            foreach (char feature in remainFeatures)
            {
                /// get all the info
                /// 
                double curinfo = giveInfoByFeature(feature);
                //if (maxInfo < curinfo)
                //{
                //    maxInfo = curinfo;
                //    minFeature = feature;
                //}
                if (minInfo > curinfo)
                {
                    minInfo = curinfo;
                    minFeature = feature;
                }
            }
            /// 根据当前特征，获取其都有哪些子特征，然后利用他们来构造儿子们的类别
            /// 
            currentMainFeature = minFeature;
            remainFeatures.Remove(currentMainFeature);
            
            List<DNode> ret = new List<DNode>();
            /// 给每个儿子赋值
            /// 
            foreach (char c in giveAvailableFeature(minFeature))
            {
                DNode d = new DNode();
                d.remainFeatures = new List<char>(remainFeatures);
                d.currentSubFeature = c;
                d.allSamples = allSamples.Where((x) => x.features.Contains(c)).ToArray();
                if (d.allSamples.Length == 0) continue;
                ret.Add(d);
            }
            return ret;

            
        }
        
        /// <summary>
        /// 给出所有子特征
        /// </summary>
        /// <param name="fea">主特征</param>
        /// <returns>所有子特征</returns>
        List<char> giveAvailableFeature(char fea)
        {
            List<char> ret = new List<char>();
            switch (fea)
            {
                case 'A':
                    ret.AddRange(new char[] { 'A', 'B', 'C', 'D' });
                    break;
                case 'E':
                    ret.AddRange(new char[] { 'E', 'F', 'G' });
                    break;
                case 'H':
                    ret.AddRange(new char[] { 'H', 'I', 'J' });
                    break;
                case 'K':
                    ret.AddRange(new char[] { 'K', 'L' });
                    break;
                case 'M':
                    ret.AddRange(new char[] { 'M', 'N' });
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 通过某个主特征来给出对应的信息熵不纯度
        /// </summary>
        /// <param name="fea">主特征</param>
        /// <returns>熵不纯度</returns>
        double giveInfoByFeature(char fea)
        {
            double ret = 0;
            switch (fea)
            {
                case 'A':
                    ret = info(new double[]{
                    allSamples.Count(x => x.features.Contains('A')),
                    allSamples.Count(x => x.features.Contains('B')),
                    allSamples.Count(x => x.features.Contains('C')),
                    allSamples.Count(x => x.features.Contains('D'))
                    });
                    break;
                case 'E':
                    ret = info(new double[]{
                    allSamples.Count(x => x.features.Contains('E')),
                    allSamples.Count(x => x.features.Contains('F')),
                    allSamples.Count(x => x.features.Contains('G'))
                    });
                    break;
                case 'H':
                    ret = info(new double[]{
                    allSamples.Count(x => x.features.Contains('H')),
                    allSamples.Count(x => x.features.Contains('I')),
                    allSamples.Count(x => x.features.Contains('J'))
                    });
                    break;
                case 'K':
                    ret = info(new double[]{
                    allSamples.Count(x => x.features.Contains('K')),
                    allSamples.Count(x => x.features.Contains('L'))
                    });
                    break;
                case 'M':
                    ret = info(new double[]{
                    allSamples.Count(x => x.features.Contains('M')),
                    allSamples.Count(x => x.features.Contains('N'))
                    });
                    break;
            }
            return ret;
        }
    }
}
