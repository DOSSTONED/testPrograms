using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerate_Possible_Scores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] curTotalScore = new int[] { 20, 12, 11, 10, 9, 0, 0, 0 };
            int remain = 16;
            for (int i6 = 9; i6 >= 2; i6--)
            {
                for (int i7 = i6; i7 >= 2; i7--)
                {
                    for (int i8 = i7; i8 >= 2; i8--)
                    {
                        if (i6 + i7 + i8 == remain)
                        {
                            curTotalScore[6 - 1] = i6;
                            curTotalScore[7 - 1] = i7;
                            curTotalScore[8 - 1] = i8;

                            Situation s = new Situation(8, 2);
                            s.Score = new int[,]
                            { { 10,10},
                                {0,0 },
                                {0,0 },
                                {0,0 },
                                {0,0 },
                                {0,0 },
                                {0,0 },
                                {0,0 } };

                            s.ScoreOccupied = new bool[,]
                            {   {true,true },
                                {false,false },
                                {false,false },
                                {false,false },
                                {false,false },
                                {false,false },
                                {false,false },
                                {false,false }
                            };
                            s.Desired = curTotalScore.ToArray();
                            s.remainings.AddRange(new int[] { 8, 8, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1 });

                            Queue<Situation> allS = new Queue<Situation>();
                            allS.Enqueue(s);
                            while (allS.Count > 0)
                            {
                                Situation curS = allS.Dequeue();
                                var nextS = curS.FillOneElement();
                                foreach (var next in nextS)
                                {
                                    switch (next.Check())
                                    {
                                        case FillResults.Success:
                                            Console.WriteLine(next);
                                            continue;
                                        case FillResults.Not_full:
                                            allS.Enqueue(next);
                                            break;
                                        default:
                                            throw new NotImplementedException();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Finished.");
            Console.ReadKey();
                
        }
    }

    enum FillResults
    {
        Sum_is_not_correct,
        Success,
        Not_full
    }

    class Situation
    {
        public int row;
        public int column;
        public int[,] Score;
        public bool[,] ScoreOccupied;
        public int[] Desired;
        public Situation()
        {
            row = 8;
            column = 2;
            Score = new int[row, column];
            ScoreOccupied = new bool[row, column];
            Desired = new int[row];
        }

        public Situation(int r,int c)
        {
            row = r;
            column = c;
            Score = new int[row, column];
            ScoreOccupied = new bool[row, column];
            Desired = new int[row];
        }

        public Situation(Situation s)
        {
            row = s.row;
            column = s.column;
            Score = new int[row, column];
            ScoreOccupied = new bool[row, column];
            Desired = new int[row];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Score[i, j] = s.Score[i, j];
                    ScoreOccupied[i, j] = s.ScoreOccupied[i, j];
                }
                Desired[i] = s.Desired[i];
            }
            remainings = s.remainings.ToList();
        }

        public override string ToString()
        {
            switch (Check())
            {
                case FillResults.Not_full:
                    Console.WriteLine("Not full.");
                    break;
                case FillResults.Success:
                    for(int i=0;i< row;i++)
                    {
                        Console.Write("{0}\t", Desired[i]);

                        for (int j = 0; j < column; j++)
                        {
                            Console.Write("{0}\t", Score[i, j]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("-----------");
                    break;
                case FillResults.Sum_is_not_correct:
                    return "Sum is not correct";
                default:
                    return "Default encountered.";
            }
            return "Function END.";
        }

        public List<int> remainings = new List<int>();

        public Situation[] FillOneElement()
        {
            List<Situation> allSits = new List<Situation>();
            for (int k = 0; k < remainings.Count; k++)
            {
                Situation newS = new Situation(this);
                bool filled = false;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (!ScoreOccupied[i, j])
                        {
                            newS.Score[i, j] = remainings[k];
                            newS.remainings.Remove(remainings[k]);
                            newS.ScoreOccupied[i, j] = true;
                            filled = true;
                            break;
                        }
                    }
                    if (filled) break;
                }
                if (newS.Check() != FillResults.Sum_is_not_correct) allSits.Add(newS);
            }


            return allSits.ToArray();
        }

        public FillResults Check()
        {
            for (int i = 0; i < row; i++)
            {
                int sum = 0;
                for (int j = 0; j < column; j++)
                {
                    if (!ScoreOccupied[i, j]) return FillResults.Not_full;
                    sum += Score[i, j];
                }

                if (sum != Desired[i]) return FillResults.Sum_is_not_correct;
            }

            return FillResults.Success;
        }
    }
}
