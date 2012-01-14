using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoko
{
    enum SudokoStatus
    {
        /// <summary>
        /// Have found a solution.
        /// </summary>
        End,
        /// <summary>
        /// Have some children, can go deeper.
        /// </summary>
        HasChildren,
        /// <summary>
        /// This has no solution, dead sudoko.
        /// </summary>
        Failed,
        /// <summary>
        /// Can fill.
        /// </summary>
        CanFill
    }

    class Sudoko_Try
    {
        //public bool isFilled = false;
        public List<Sudoko_Try> AllStatus = new List<Sudoko_Try>();
        public List<Sudoko_Try> Next = new List<Sudoko_Try>();
        public Sudoko_Try Parent = null;
        public int[,] bases = new int[81, 10];
        public void Input(string sudokos)
        {
            string[] strs = sudokos.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length != 81) throw new FormatException("Input base is not 81 digits.");
            for (int i = 0; i < strs.Length; i++)
            {
                bases[i, 0] = int.Parse(strs[i]);
            }
        }
        public string Output()
        {
            string ret = string.Empty;
            for (int i = 0; i < bases.Length; i++)
            {
                ret += bases[i,0].ToString();
                if ((i + 1) % 9 == 0)
                {
                    ret += "\r\n";
                }
                else
                {
                    ret += " ";
                }
            }
            return ret;
        }

        static SudokoStatus FillCurrent(Sudoko_Try st)
    {
        int row = 0, col = 0;
        int[] counter = new int[10];
        

            /// Fill line
        for (int i = 0; i < counter.Length; i++)
            counter[i] = 0;
            for(row=0;row<9;row++)
                for (col = 0; col < 9; col++)
                {
                    counter[st.bases[9 * row + col,0]]++;
                }
            // check
            for (int i = 1; i < counter.Length; i++)
                if (counter[i] > 1) return SudokoStatus.Failed;



    }

        static SudokoStatus GetNext(Sudoko_Try st, out Sudoko_Try next)   
        {
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}
