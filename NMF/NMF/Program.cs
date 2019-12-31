using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 

namespace NMF
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 4)
            {
                Console.WriteLine("Usage: NMF.exe matrix-input-file matrix-row matrix-col matrix-rank");
                Console.WriteLine("matrix = (r * ra) * (ra * c)");
                return;
            }

            string FILE = args[0];//"E:\\TEMP\\star.dat";//
            string argsROW = args[1];//"100";//
            string argsCOL = args[2];//"86";//
            string argsRANK = args[3];//"2";//


            string contents = File.ReadAllText(FILE);
            string[] numbers = System.Text.RegularExpressions.Regex.Split(contents, "\\s+");

            int row = int.Parse(argsROW);
            int col = int.Parse(argsCOL);
            int rank = int.Parse(argsRANK);
            double[] Original = new double[row * col];
            for (int i = 0; i < row * col; i++)
            {
                Original[i] = double.Parse(numbers[i]);
            }

            /// initialise the B, H, BH
            /// use the iterate method to get close to Original with BH;
            double[] B = new double[row * rank], H = new double[rank * col];
            double[] BH = new double[row * col];

            initialise(ref B, ref H);
            /// update BH
            BH = M1xM2(B, H, row, rank, col);

            int iterate = 1000;
            while ((iterate-- > 0) && distanceBetweenM1AndM2(BH, Original, Original.Length) > 0.0001)
            {
                //
                updateBH(ref BH, ref B, ref H, Original, row, rank, col);
            }

            Console.WriteLine(distanceBetweenM1AndM2(BH, Original, Original.Length));

            printM(Original, row, col);Console.WriteLine();
            Console.WriteLine("BH");
            printM(BH, row, col);Console.WriteLine();
            Console.WriteLine("B");
            printM(B, row, rank);Console.WriteLine();
            Console.WriteLine("H");
            printM(H, rank, col);
            Console.ReadKey();
        }

        /// <summary>
        /// Initialise the B, H
        /// </summary>
        /// <param name="B">Matrix B, ref para</param>
        /// <param name="H">Matrix H, ref para</param>
        static void initialise(ref double[] B, ref double[] H)
        {
            int i = 0;
            for (i = 0; i < B.Length; i++)
            {
                B[i] = 1;
            }
            for (i = 0; i < H.Length; i++)
            {
                H[i] = 10;
            }
        }

        /// <summary>
        /// Update BH, which gives BH = B * H
        /// </summary>
        /// <param name="BH">BH, ref para</param>
        /// <param name="B">B, ref para</param>
        /// <param name="H">H, ref para</param>
        /// <param name="original">Origianl Matrix (used to mesaure distance)</param>
        /// <param name="B_m">B's rows</param>
        /// <param name="B_n">B's cols, also H's rows</param>
        /// <param name="H_n">H's cols</param>
        static void updateBH(ref double[] BH, ref double[] B, ref double[] H, double[] original, int B_m, int B_n, int H_n)
        {
            for (int j = 0; j < B_n; j++)
            {
                /// update BH
                BH = M1xM2(B, H, B_m, B_n, H_n);

                /// update a row of H
                /// 
                for (int k = 0; k < H_n; k++)
                {
                    double temp1 = 0.0;
                    double temp2 = 0.0;
                    for (int i = 0; i < B_m; i++)
                        temp1 += (B[i * B_n + j] * original[i * H_n + k] / BH[i * H_n + k]);
                    for (int i = 0; i < B_m; i++)
                        temp2 += B[i * B_n + j];
                    H[j * H_n + k] = H[j * H_n + k] * temp1 / temp2;
                }

                /// update BH
                BH = M1xM2(B, H, B_m, B_n, H_n);

                /// update a row of B
                /// 
                for (int i = 0; i < B_m; i++)
                {
                    double temp1 = 0.0;
                    double temp2 = 0.0;
                    for (int k = 0; k < H_n; k++)
                        temp1 += (H[j * H_n + k] * original[i * H_n + k] / BH[i * H_n + k]);
                    for (int ii = 0; ii < H_n; ii++)
                        temp2 += H[j * H_n + ii];
                    B[i * B_n + j] = B[i * B_n + j] * temp1 / temp2;
                }
            }
        }

        /// <summary>
        /// Give the distance of M1 and M2, use the log function to estimate
        /// </summary>
        /// <param name="M1">The first matrix</param>
        /// <param name="M2">The second matrix</param>
        /// <param name="Elements">number of elements in matrix</param>
        /// <returns>the distance</returns>
        static double distanceBetweenM1AndM2(double[] M1, double[] M2, int Elements)
        {
            double ret = 0;
            /// This is just a one version of norm of a matrix M1 - M2
            //for (int i = 0; i < Elements; i++)
            //    ret += Math.Abs(M1[i] * Math.Log(M1[i] / M2[i]) - M1[i] + M2[i]);
            for (int i = 0; i < Elements; i++)
                ret += Math.Abs(M2[i] - M1[i]);
            return ret;
        }

        /// <summary>
        /// Print a Matrix
        /// </summary>
        /// <param name="M">Matrix to print</param>
        /// <param name="M_m">The number of it's rows</param>
        /// <param name="M_n">The number of it's cols</param>
        static void printM(double[] M, int M_m, int M_n)
        {
            
            for (int i = 0; i < M_m; i++)
            {
                for (int j = 0; j < M_n; j++)
                {
                    Console.Write(M[i * M_n + j].ToString() + "\t");
                }
                Console.WriteLine();
               
            }
        }

        /// <summary>
        /// Give the multiply of M1 and M2
        /// </summary>
        /// <param name="M1">the first matrix(m * r)</param>
        /// <param name="M2">the second matrix(r * n)</param>
        /// <param name="M1_m">M1's rows</param>
        /// <param name="M1_r">M1's cols, also M2's rows</param>
        /// <param name="M2_n">M2's cols</param>
        /// <returns>M1*M2(NOT M2*M1)</returns>
        static double[] M1xM2(double[] M1, double[] M2, int M1_m, int M1_r, int M2_n)
        {
            double[] ret = new double[M1_m * M2_n];
            for (int i = 0; i < M1_m; i++)
            {
                for (int j = 0; j < M2_n; j++)
                {
                    ret[i * M2_n + j] = 0;
                    for (int k = 0; k < M1_r; k++)
                    {
                        ret[i * M2_n + j] += M1[i * M1_r + k] * M2[k * M2_n + j];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Give the transform of M
        /// </summary>
        /// <param name="M">The matrix to transform</param>
        /// <param name="M_m">The number of it's rows</param>
        /// <param name="M_n">The number of it's cols</param>
        /// <returns>Transformed M(M')</returns>
        static double[] MT(double[] M, int M_m, int M_n)
        {
            double[] ret = new double[M_m * M_n];
            for (int i = 0; i < M_m; i++)
            {
                for (int j = 0; j < M_n; j++)
                {
                    ret[i * M_n + j] = M[j * M_n + i];
                }
            }
            return ret;
        }

    }
}
