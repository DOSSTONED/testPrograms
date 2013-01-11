using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 遗传算法解决TSP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Random r = new Random();
            randoms=new double[10000];
            for (int i = 0; i < 10000; i++)
                randoms[i] = r.NextDouble();
        }
        double[] randoms = null;
        int random_idx = 0;
        double giveRandom()
        {
            if (random_idx >= 10000)
            {
                for (int i = 0; i < 10000; i++)
                    randoms[i] = (new Random()).NextDouble();
                random_idx = 0;
            }
            return randoms[random_idx++];
        }
        Point[] Ps = null;
        int totalPoints = 30;
        int randomSeed = 0;
        int 种群数量 = 500;

        private void buttonGivePoints_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            Graphics g = panelDrawing.CreateGraphics();
            g.Clear(DefaultBackColor);
            Ps = new Point[totalPoints];
            for (int i = 0; i < Ps.Length; i++)
            {
                Ps[i] = new Point(r.Next(720), r.Next(720));

                g.DrawEllipse(new Pen(Brushes.Black), new Rectangle(Ps[i], new Size(5, 5)));
            }
            listBox1.Items.Clear();
        }

        private void buttonCal_Click(object sender, EventArgs e)
        {
            if (Ps == null) return;
            List<int[]> Roads = new List<int[]>();
            int totalRoadsToEvolve = 种群数量;   // first, sort them by total distance, next, abandon the last half and evolve from the first half;
            double wholeLength = 0, wholeLengthNext = double.MaxValue;  // calculate the first half + second half * 0.0001 since second half is not important.
            for (int i = 0; i < totalRoadsToEvolve; i++)
            {
                Roads.Add(new int[totalPoints]);
                //Roads[i] = new int[totalPoints];

                Roads[i] = givePermutation(totalPoints);
                //wholeLengthNext += giveTotalLength(Roads[i]);
            }

            int repeatedTimes = 0;
            int championStayed = 0;
            DateTime _start = DateTime.Now;
            while (Math.Abs(wholeLength - wholeLengthNext) > 1 || championStayed < totalPoints * 50)
            {
                wholeLength = wholeLengthNext;
                wholeLengthNext = 0;
                /// evolve the next ones
                /// 
                Roads = Roads.OrderBy(x => giveTotalLength(x)).ToList();
                int startEvolveRoad = (int)(totalRoadsToEvolve * 0.2);
                for (int j = startEvolveRoad; j < totalRoadsToEvolve; j++)
                {
                    if (giveRandom() < 0.1) Roads[j] = givePermutation(totalPoints);    // introduce the new gene
                    else
                    Roads[j] = getGeneChanged(Roads[0 + (j - startEvolveRoad) / totalPoints], (j - startEvolveRoad) % totalPoints);

                    //wholeLengthNext += giveTotalLength(Roads[j]) * 0.0001;
                }

                double firstRoadLength = giveTotalLength(Roads[0]);

                /// order
                Roads = Roads.OrderBy(x => giveTotalLength(x)).ToList();
                if (giveTotalLength(Roads[0]) != firstRoadLength)
                    championStayed = 0;
                else
                    championStayed++;

                for (int i = 0; i < totalRoadsToEvolve; i++)
                {
                    double tmp = giveTotalLength(Roads[i]);
                    wholeLengthNext += tmp * sigmoid(i, totalRoadsToEvolve);
                }

                
                if (repeatedTimes++ % 200 == 0)
                {
                Graphics g = panelDrawing.CreateGraphics();
                g.Clear(DefaultBackColor);  // clear
                // draw the dots
                for (int i = 0; i < Ps.Length; i++)
                {
                    g.DrawEllipse(new Pen(Brushes.Black), new Rectangle(Ps[i], new Size(5, 5)));
                    g.DrawString(i.ToString(), new System.Drawing.Font("Arial", 16), Brushes.Black, Ps[i]);
                    int cur = Roads[0][i];
                    g.DrawLine(new Pen(Brushes.Blue), Ps[cur], Ps[Roads[0][cur]]);
                }
                    //if (repeatedTimes > 5000) break;
                }
                
            }

            Graphics gg = panelDrawing.CreateGraphics();
            gg.Clear(DefaultBackColor);  // clear
            /// draw the dots
            for (int i = 0; i < Ps.Length; i++)
            {
                gg.DrawEllipse(new Pen(Brushes.Black), new Rectangle(Ps[i], new Size(5, 5)));
                gg.DrawString(i.ToString(), new System.Drawing.Font("Arial", 16), Brushes.Black, Ps[i]);
                int cur = Roads[0][i];
                gg.DrawLine(new Pen(Brushes.Blue), Ps[cur], Ps[Roads[0][cur]]);
            }

            int curr = 0;
            string toadd = curr.ToString();
            
            do
            {
                toadd += "->" + Roads[0][curr].ToString();
                curr = Roads[0][curr];
            } while (curr != 0);

            toadd += "\t" + giveTotalLength(Roads[0]).ToString();
            listBox1.Items.Add(toadd);

            labelRepeatTimes.Text = repeatedTimes.ToString();
            labelTime.Text = (DateTime.Now - _start).TotalMilliseconds.ToString();
        }

        double sigmoid(int cur, int total)  // cur=0 gives 1, while cur>0.5*total gives a small value
        {
            //return 2 / (1 + Math.Exp(cur * 4.0 / total));
            return 1 -cur * 1.0 / total;
        }

        int[] getGeneChanged(int[] ori)
        {
            Random r = new Random();
            //Random r = new Random();
            //if (r.NextDouble() < 0.1)
            /// for a single gene is changed
            /// such as:
            /// a->b, c->d
            /// then a->d, c->b
            /// 
            /// next is just a swap, but a fail swap!(can lead to i->i)
            /*{
                int a = r.Next(totalPoints);
                int b = ori[a];
                int c = r.Next(totalPoints);
                while (!(c != a && c != b && ori[c] != a)) c = r.Next(totalPoints);
                int d = ori[c];
                ori[a] = d;
                ori[c] = b;
                //continue;
            }
             */


            /// this one just gives another random gene
            //return givePermutation(ori.Length);

            /// so we need the real swap one!
            /// say:
            /// 0->1->2->4->5->3->6->0
            /// to:
            /// 0->1->5->4->2->3->6->0
            /// 
            return getGeneChanged(ori, (int)(r.NextDouble() * ori.Length));
        }

        int[] getGeneChanged(int[] ori, int startFixPoint)
        {
            /// so we need the real swap one!
            /// say:
            /// 0->1->2->4->5->3->6->0
            /// to:
            /// 0->1->5->4->2->3->6->0
            /// 
            Random r = new Random();
            int[] ret = new int[ori.Length];
            for (int i = 0; i < ori.Length; i++)
                ret[i] = -1;
            double ttt = r.NextDouble();
            if (ttt < 0.05)   // twist the section
            {
                //int startFixPoint = (int)(giveRandom() * ori.Length); // 1 in example, this point is not changed during this process.
                int startPoint = ori[startFixPoint];    // 2 in example, we have to find some minor changes to the path.
                int steps = 1 + (int)(r.Next(ori.Length / 2));//giveRandom() * ori.Length / 2);
                int curChanged = startPoint;
                for (int i = 0; i < steps; i++)
                {
                    int next = ori[curChanged];
                    ret[next] = curChanged;
                    curChanged = next;
                }

                ret[startFixPoint] = curChanged;
                ret[startPoint] = ori[curChanged];

            }
            else if (ttt < 0.15)
            {
                for (int i = 0; i < ori.Length; i++)
                    if (ret[i] == -1) ret[i] = ori[i];
                /// select a point, and then move the point to another place!
                /// 
                int dest = r.Next(ori.Length);
                int startPoint = ori[startFixPoint];
                while (isFourHasSame(dest, startPoint, ret[dest], ret[startPoint])) dest = r.Next(ori.Length);
                ret[startFixPoint] = ret[startPoint];
                ret[startPoint] = ret[dest];
                ret[dest] = startPoint;
            }
            else if (ttt < 0.2)
            {
                for (int i = 0; i < ori.Length; i++)
                    ret[i] = ori[i];
                /// cut the section and paste section to another
                /// 
                int sectionStartPoint = ori[startFixPoint];    // 2 in example, we have to find some minor changes to the path.
                int steps = (int)(r.Next(ori.Length / 2));//giveRandom() * ori.Length / 2);
                int curChanged = sectionStartPoint;
                for (int i = 0; i < steps; i++)
                {
                    int next = ori[curChanged];
                    curChanged = next;
                }
                int sectionEndPoint = curChanged;
                int testPastePoint = ori[curChanged];

                //steps = 1 + (int)(r.Next(ori.Length - steps - 2));  // go next and find a paste point
                //for (int i = 0; i < steps; i++)
                //{
                //    int next = ori[curChanged];
                //    curChanged = next;
                //}
                //int testPastePoint = curChanged;
                /// this works, but I want a better one
                //while (isTheyHasSame(new int[] { pastePoint, ori[pastePoint], sectionEndPoint, ori[sectionEndPoint] }))
                //    return ret; // actually do nothing

                int minPastePoint = testPastePoint;
                do
                {
                    /// recover the array
                    for (int i = 0; i < ori.Length; i++)
                        ret[i] = ori[i];
                    ret[startFixPoint] = ori[sectionEndPoint];
                    ret[testPastePoint] = sectionStartPoint;
                    ret[sectionEndPoint] = ori[testPastePoint];

                    if (giveTotalLength(ret) < giveTotalLength(ori)) minPastePoint = testPastePoint;
                    testPastePoint = ori[testPastePoint];
                } while (ori[testPastePoint] != startFixPoint);

                for (int i = 0; i < ori.Length; i++)
                    ret[i] = ori[i];
                ret[startFixPoint] = ori[sectionEndPoint];
                ret[minPastePoint] = sectionStartPoint;
                ret[sectionEndPoint] = ori[minPastePoint];

                //ret[startFixPoint] = ori[sectionEndPoint];
                //ret[testPastePoint] = sectionStartPoint;
                //ret[sectionEndPoint] = ori[testPastePoint];
            }
            
            for (int i = 0; i < ori.Length; i++)
                if (ret[i] == -1) ret[i] = ori[i];
            return ret;
        }

        bool isFourHasSame(int i, int j, int k, int l)
        {
            if (i == j) return true;
            if (i == k) return true;
            if (i == l) return true;
            if (j == k) return true;
            if (l == k) return true;
            if (l == j) return true;
            return false;
        }

        bool isTheyHasSame(int[] i)
        {
            i = i.OrderBy(x => x).ToArray();
            for (int j = 1; j < i.Length; j++)
                if (i[j - 1] == i[j]) return true;
            return false;
        }

        double giveTotalLength(int[] road)
        {
            double ret = 0;
            for (int cur = 0; cur < road.Length; cur++)
            {
                double deltaX = Ps[cur].X - Ps[road[cur]].X;
                double deltaY = Ps[cur].Y - Ps[road[cur]].Y;
                //deltaX = deltaX / 100;
                //deltaY = deltaY / 100;
                ret += Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                //cur = road[cur];
            }
            return ret;
        }

        int[] givePermutation(int totalLength)
        {
            int[] ret = new int[totalLength];
            bool[] indicator = new bool[totalLength];
            for (int i = 0; i < totalLength; i++) indicator[i] = false;
            int currentSet = 0;
            int idx = 0;
            Random r = new Random(randomSeed++);
            //r = new Random(r.Next());
            while (currentSet < totalLength)
            {

                int next = r.Next(totalLength); // get next one
                while (indicator[next] || next == idx)
                {
                    next = r.Next(totalLength); // check whether this one had reached
                    // if there is only one left, that means we have to manully assign this value
                    if (currentSet == totalLength - 1)
                    {
                        for (int j = 0; j < totalLength; j++)
                            if (!indicator[j])
                            {
                                ret[j] = 0;
                                return ret;
                            }
                    }
                }
                indicator[idx] = true;  // first occupy this position
                ret[idx] = next;
                currentSet++;
                idx = next;

            }
            return ret;
        }
    }
}
