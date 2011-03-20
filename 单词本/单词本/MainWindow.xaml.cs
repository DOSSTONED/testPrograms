using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace 单词本
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonReload_Click(object sender, RoutedEventArgs e)
        {
            ClassIO.LoadWords();
            curWords = ClassIO.words;
        }

        private List<Word> curWords = new List<Word>();
        private Word curWord = null;
        private int curPlace = 0;

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (curWords == null)
            {
                MessageBox.Show("Not loaded words");
            }
            else
            {
                Random ran = new Random();
                curPlace = ran.Next(curWords.Count);
                loadWord(curWords[curPlace]);
            }
        }

        private void loadWord(Word curword)
        {
            curWord = curword;
            textBoxWord.Text = curword.WordName;
            textBlockWORD.Text = curword.WordName.ToUpper();
            //textBlockMeaning.Text = curword.Meaning;
        }

        private void textBoxMeaning_MouseLeave(object sender, MouseEventArgs e)
        {
            textBoxMeaning.Text = string.Empty;
        }

        private void textBoxMeaning_MouseEnter(object sender, MouseEventArgs e)
        {
            if (curWord == null)
                return;
            textBoxMeaning.Text = curWord.Meaning;
        }

        private void buttonLevels_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if ((btn.Content as string).Contains('0'))
                {
                    updateTheWrod(0);
                    //return;
                }
                if ((btn.Content as string).Contains('1'))
                {
                    updateTheWrod(1);
                    //return;
                }
                if ((btn.Content as string).Contains('2'))
                {
                    updateTheWrod(2);
                    //return;
                }
                if ((btn.Content as string).Contains('3'))
                {
                    updateTheWrod(3);
                    //return;
                }
                if ((btn.Content as string).Contains('4'))
                {
                    updateTheWrod(4);
                    //return;
                }
                buttonNext_Click(null, null);
            }
        }

        private void updateTheWrod(int level)
        {
            double newFamili = 0;
            long newReview = 1;

            newFamili = curWords[curPlace].Familiarity;
            newReview = curWords[curPlace].ReviewedTimes;

            double sum = newFamili * newReview + level;
            newReview++;
            newFamili = sum / newReview;

            curWords[curPlace].Familiarity = newFamili;
            curWords[curPlace].ReviewedTimes = newReview;
            ClassIO.words[curPlace].Familiarity = newFamili;
            ClassIO.words[curPlace].ReviewedTimes = newReview;

            ClassIO.WriteWordInfo(curWords[curPlace]);
        }

    }
}
