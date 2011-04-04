using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DoubleKiller_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] filesPaths = Directory.GetFiles(@"D:\Program\test", "*.*", SearchOption.AllDirectories);
            FileInfo[] filesInfos = new FileInfo[filesPaths.Length];
            listBox1.Items.AddRange(filesPaths);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] dirs = Directory.GetDirectories(@"D:\Program\test", "*", SearchOption.AllDirectories);

            listBox1.Items.AddRange(dirs);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] dirInfos = new string[listBox1.Items.Count];
            for (int i = 0; i < dirInfos.Length; i++)
            {
                dirInfos[i] = listBox1.Items[i] as string;
                if (dirInfos[i] != null)
                {
                    if (Directory.GetDirectories(dirInfos[i]) == null && Directory.GetFiles(dirInfos[i]) == null)
                        Directory.Delete(dirInfos[i]);
                }
            }
            
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] fileInfos = new string[listBox1.Items.Count];
            for (int i = 0; i < fileInfos.Length; i++)
            {
                fileInfos[i] = listBox1.Items[i] as string;
                if (fileInfos[i] != null)
                {
                }
            }


            // Create a delicious data source.
            string[] fruits = { "cherry", "apple", "blueberry" };

            // Query for ascending sort.
            IEnumerable<string> sortAscendingQuery =
                from fruit in fruits
                orderby fruit //"ascending" is default
                select fruit;

            // Query for descending sort.
            IEnumerable<string> sortDescendingQuery =
                from w in fruits
                orderby w.Length descending
                select w;

            // Execute the query.
            Console.WriteLine("Ascending:");
            foreach (string s in sortAscendingQuery)
            {
                listBox1.Items.Add(s);//Console.WriteLine(s);
            }

            // Execute the query.
            Console.WriteLine(Environment.NewLine + "Descending:");
            foreach (string s in sortDescendingQuery)
            {
                listBox1.Items.Add(s);//Console.WriteLine(s);
            }

        }
    }
}
