using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Speech;
using System.Speech.Synthesis;

namespace SimpleWebSite_GUI_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] lines = File.ReadAllLines(@"E:\Documents\GRE.htm");
        int beginLine = 0, endLine = 0;
        int curLineNumber = 0;
        int totalWords = 0, ReviewedWords = 0, KnownWords = 0;
        bool[] LineIndicator;
        BackgroundWorker bgWorker = new BackgroundWorker();
        SpeechSynthesizer speech = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (speech != null)
            {
                speech.Rate = -3;
                speech.Volume = 100;
            }
            
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            
            
            
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("<body>"))
                    beginLine = i + 1;
                if (lines[i].Contains("</body>"))
                    endLine = i - 1;
            }


            LineIndicator = new bool[lines.Length];
            for (int i = 0; i < LineIndicator.Length; i++)
            {
                LineIndicator[i] = false;
                
            }

            for (int i = beginLine; i <= endLine; i++)
            {
                if (lines[i] != string.Empty)
                    totalWords++;

            }

            toolStripStatusLabelTotalWords.Text = "Total Words: " + totalWords.ToString();

            //buttonRead_Click(sender, e);
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            //checkBoxShowWebBroswer.Checked = false;

            Random ran = new Random();

            /// give the word, might be proper again~
            curLineNumber = ran.Next(beginLine, endLine);
            string curLine = lines[curLineNumber];

            while (curLine == "" || LineIndicator[curLineNumber])
            {

                curLineNumber = ran.Next(beginLine, endLine);
                curLine = lines[curLineNumber];
                if (!LineIndicator[curLineNumber])
                    break;

            }
            LineIndicator[curLineNumber] = true;

            string beforeString = curLine.Substring(curLine.IndexOf("s.php?q="));

            string curWord = beforeString.Substring(8, beforeString.IndexOf("\">") - 8);

            SetWord(curWord);
            if (speech != null)
            {
                speech.SpeakAsync(curWord);
            }

            if (bgWorker.IsBusy)
                bgWorker.CancelAsync();
            bgWorker.RunWorkerAsync();


            toolStripStatusLabelCurReviewed.Text = "Reviewed this session: " + ReviewedWords.ToString();
            ReviewedWords++;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(@"E:\Documents\GRE.htm", lines);
        }

        private void SetWord(string word)
        {
            label1.Text = word.ToLower();
            label2.Text = word.ToUpper();



            //int totalNumber = 0;
            //for (int i = 0; i < LineIndicator.Length; i++)
            //    if (LineIndicator[i])
            //        totalNumber++;
            //MessageBox.Show(totalNumber.ToString());
        }

        private void buttonKnow_Click(object sender, EventArgs e)
        {
            lines[curLineNumber] = string.Empty;
            KnownWords++;
            toolStripStatusLabelCurKnows.Text = "Known words: " + KnownWords.ToString();
            buttonRead_Click(sender, e);
        }

        private void buttonDontKnow_Click(object sender, EventArgs e)
        {
            buttonRead_Click(sender, e);
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string curLine = lines[curLineNumber];
            string curURL = curLine.Substring(curLine.IndexOf("http://3g.dict.cn/s.php?q="));
            curURL = curURL.Substring(0, curURL.IndexOf("\">"));
            if (checkBoxUseIPv6Proxy.Checked)
                curURL = curURL.Replace(".cn", ".cn.sixxs.org");

            webBrowser1.Navigate(curURL);
            //row new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines(@"E:\Documents\GRE.htm", lines);
        }

        private void checkBoxShowWB_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Visible = checkBoxShowWB.Checked;
            labelExplanation.Visible = checkBoxShowWB.Checked;
        }

        private void checkBoxShowWB_MouseEnter(object sender, EventArgs e)
        {
            checkBoxShowWB.Checked = !checkBoxShowWB.Checked;
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine();
            HtmlElement cur = webBrowser1.Document.Body;
            if (cur != null)
            {
                if (cur.Children != null)
                {
                    if (cur.Children.Count > 4)
                    {
                        labelExplanation.Text = cur.Children[4].InnerText;
                    }

                }
            }
        }

        private void checkBoxSound_CheckedChanged(object sender, EventArgs e)
        {
            if (speech == null && checkBoxSound.Checked)
            {
                speech = new SpeechSynthesizer();
                speech.Rate = -2;
            }
            else
            {
                speech = null;
            }
        }

        private double getWordDouble(string word)
        {
            char[] chars = word.ToLower().ToCharArray();
            double res = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] <= 0x60 && chars[i] > 0x7A)
                    return 0;
                res += (chars[i] - 0x61) * Math.Pow(26, -i);
            }
            return res;
        }



    }
}
