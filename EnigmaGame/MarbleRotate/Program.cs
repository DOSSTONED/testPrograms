using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarbleRotate
{
    class Program
    {
        static void Main(string[] args)
        {

            MarbleTable mt = new MarbleTable();
            
            Queue<MarbleTable> relax = new Queue<MarbleTable>();
            relax.Enqueue(mt);
            while (relax.Count > 0)
            {
                MarbleTable de = relax.Dequeue();
                if(MarbleTable.isTarget(de))
                {
                    Console.WriteLine(de.CurrentSequence);
                    Console.WriteLine("Finished");
                    break;
                }
                relax.Enqueue(de.Click4());
                relax.Enqueue(de.Click5());
                relax.Enqueue(de.Click8());
                relax.Enqueue(de.Click9());
                relax.Enqueue(de.Click10());
                relax.Enqueue(de.Click13());
                relax.Enqueue(de.Click14());
            }

            Console.ReadKey();
        }
    }


    enum MarbleColor
    {
        Grey,
        Yellow,
        Red
    }

    class MarbleTable
    {
        MarbleColor[] table = new MarbleColor[19] {
            MarbleColor.Grey,
            MarbleColor.Yellow,
            MarbleColor.Grey,

            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,

            MarbleColor.Yellow,
            MarbleColor.Red,
            MarbleColor.Yellow,
            MarbleColor.Yellow,
            MarbleColor.Yellow,

             MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,

             MarbleColor.Yellow,
            MarbleColor.Grey,
            MarbleColor.Grey
        };




        public string CurrentSequence = string.Empty;

        public static bool isTarget(MarbleTable t1)
        {
            MarbleColor[] TargetTable =
            {
            MarbleColor.Grey,
            MarbleColor.Yellow,
            MarbleColor.Grey,

            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,

            MarbleColor.Yellow,
            MarbleColor.Yellow,
            MarbleColor.Red,
            MarbleColor.Yellow,
            MarbleColor.Yellow,

             MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,
            MarbleColor.Grey,

             MarbleColor.Grey,
            MarbleColor.Yellow,
            MarbleColor.Grey
            };
            for (int i = 0; i < 19; i++)
                if (t1.table[i] != TargetTable[i]) return false;

            return true;
        }

        public MarbleTable Click4()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[0];
            // 0-3-8-9-5-1
            ret.table[0] = ret.table[1];
            ret.table[1] = ret.table[5];
            ret.table[5] = ret.table[9];
            ret.table[9] = ret.table[8];
            ret.table[8] = ret.table[3];
            ret.table[3] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click4--";
            return ret;
        }

        public MarbleTable Click5()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[1];
            // 1-4-9-10-6-2
            ret.table[1] = ret.table[2];
            ret.table[2] = ret.table[6];
            ret.table[6] = ret.table[10];
            ret.table[10] = ret.table[9];
            ret.table[9] = ret.table[4];
            ret.table[4] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click5--";
            return ret;
        }

        public MarbleTable Click8()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[3];
            // 3-7-12-13-9-4
            ret.table[3] = ret.table[4];
            ret.table[4] = ret.table[9];
            ret.table[9] = ret.table[13];
            ret.table[13] = ret.table[12];
            ret.table[12] = ret.table[7];
            ret.table[7] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click8--";
            return ret;
        }

        public MarbleTable Click9()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[4];
            // 4-8-13-14-10-5
            ret.table[4] = ret.table[5];
            ret.table[5] = ret.table[10];
            ret.table[10] = ret.table[14];
            ret.table[14] = ret.table[13];
            ret.table[13] = ret.table[8];
            ret.table[8] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click9--";
            return ret;
        }

        public MarbleTable Click10()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[5];
            // 5-9-14-15-11-6
            ret.table[5] = ret.table[6];
            ret.table[6] = ret.table[11];
            ret.table[11] = ret.table[15];
            ret.table[15] = ret.table[14];
            ret.table[14] = ret.table[9];
            ret.table[9] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click10--";
            return ret;
        }

        public MarbleTable Click13()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[8];
            // 8-12-16-17-14-9
            ret.table[8] = ret.table[9];
            ret.table[9] = ret.table[14];
            ret.table[14] = ret.table[17];
            ret.table[17] = ret.table[16];
            ret.table[16] = ret.table[12];
            ret.table[12] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click13--";
            return ret;
        }

        public MarbleTable Click14()
        {
            MarbleTable ret = new MarbleTable();
            ret.table = this.table.ToArray();
            MarbleColor tmp = ret.table[9];
            // 9-13-17-18-15-10
            ret.table[9] = ret.table[10];
            ret.table[10] = ret.table[15];
            ret.table[15] = ret.table[18];
            ret.table[18] = ret.table[17];
            ret.table[17] = ret.table[13];
            ret.table[13] = tmp;

            ret.CurrentSequence = this.CurrentSequence + "Click14--";
            return ret;
        }
    }
}
