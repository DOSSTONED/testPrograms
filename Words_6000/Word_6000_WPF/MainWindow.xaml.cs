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
using System.Threading;
//using System.Net;

namespace Word_6000_WPF
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
        private List<string> wordList = new List<string>();
        //private WebClient web = null;
        private string dest = @"E:\Documents\GRE.txt";
        private List<string> wordsMeaning = null;

        /// If you wanna accelerate the speed of finding the element in the array, maybe a GetWordDouble is needed.

        private int getCurWordPos(string in_word)
        {

            return wordList.IndexOf(in_word);
        }
        private string getCurWordMeaning(string in_word)
        {
            string downStr = string.Empty;
            try
            {
                downStr = File.ReadAllText(@"E:\Documents\GRE_List\" + in_word, Encoding.GetEncoding("GBK"));
            
                downStr = downStr.Substring(downStr.IndexOf(@"<def>") + 5);
                downStr = downStr.Substring(0, downStr.IndexOf(@"</def>"));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(downStr, "getCurWordMeaning Error " + in_word);
            }
            return downStr;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));

            //Thread updateWordListT = new Thread(updateWordList);

            updateWordList();


            //updateWordListT.Start();
            ////updateWordListT.Join();
            ////while (!updateWordListT.IsAlive) ;
            //updateWordListT.Join();

            progressBar1.Visibility = Visibility.Collapsed;
            thumbLoading.Visibility = Visibility.Collapsed;
            //Button btn = new Button();
            //btn.Width = 32;
            //btn.Height = 32;
            //btn.Click += new RoutedEventHandler(wordButton_Click);
            //listViewWords.Items.Add(btn);
        }

        private void updateWordList()
        {
            
            string[] words = File.ReadAllLines(dest);
            wordsMeaning = new List<string>();
            try
            {
                progressBar1.Maximum = words.Length;
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == string.Empty | words[i] == null) continue;
                    wordList.Add(words[i]);
                    wordsMeaning.Add(string.Empty);


                    //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    //    new Action(

                    //delegate()
                    //{
                    Button btn = new Button();
                    btn.Click += new RoutedEventHandler(wordButton_Click);
                    btn.MouseEnter += new MouseEventHandler(wordButton_MouseEnter);
                    btn.MouseLeave += new MouseEventHandler(wordButton_MouseLeave);


                    btn.Width = 120;
                    btn.Height = 32;
                    btn.Content = words[i];
                    wordsMeaning[i] = getCurWordMeaning(words[i]);


                    listViewWords.Children.Add(btn);
                    //}
                    //)
                    progressBar1.Value = i;
                    //Thread.Sleep(1);

                    //);



                    //if (web == null)
                    //{
                    //    web = new WebClient();
                    //    //web.DownloadDataCompleted += new DownloadDataCompletedEventHandler(web_DownloadDataCompleted);
                    //}
                    ////web.BaseAddress = @"http://dict.cn/ws.php?q=" + curWord;

                    //byte[] wordDownByte = web.DownloadData(new Uri(@"http://dict.cn.sixxs.org/ws.php?q=" + words[i]));
                    //string wordDownMeaning = Encoding.GetEncoding("GBK").GetString(wordDownByte);
                    //string wordMeaning = wordDownMeaning.Substring(wordDownMeaning.IndexOf(@"<def>") + 5);
                    //wordMeaning = wordMeaning.Substring(0, wordMeaning.IndexOf(@"</def>"));
                    //wordsMeaning[i] = wordMeaning;


                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.StackTrace, wordsMeaning.Last());
            }

        }

        void wordButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button curBtn = sender as Button;
            if (curBtn == null)
                return;//throw new NotImplementedException();
            string curWord = curBtn.Content.ToString();
            curWord = curWord.Substring(0, curWord.IndexOf("\r\n"));
            curBtn.Content = curWord;
            curBtn.Width = 120;
            curBtn.Height = 32;
        }

        void wordButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button curBtn = sender as Button;
            if (curBtn == null)
                return;
            string curWord = curBtn.Content.ToString();
            // int place = getCurWordPos(curWord);
            curBtn.Content = curWord + "\r\n" + getCurWordMeaning(curWord);
            curBtn.Width = 220;
            curBtn.Height = 128;
            //throw new NotImplementedException();
        }

        void wordButton_Click(object sender, RoutedEventArgs e)
        {
            Button curBtn = sender as Button;
            if (curBtn == null)
                return;
            string curWord = curBtn.Content.ToString();

            //try
            //{
            //    if (web == null)
            //    {
            //        web = new WebClient();
            //        //web.DownloadDataCompleted += new DownloadDataCompletedEventHandler(web_DownloadDataCompleted);
            //    }
            //    //web.BaseAddress = @"http://dict.cn/ws.php?q=" + curWord;
            //    if (wordsMeaning[getCurWordPos(curWord)] == null)
            //    {
            //        byte[] wordDownByte = web.DownloadData(new Uri(@"http://dict.cn.sixxs.org/ws.php?q=" + curWord));
            //        string wordDownMeaning = Encoding.GetEncoding("GBK").GetString(wordDownByte);
            //        string wordMeaning = wordDownMeaning.Substring(wordDownMeaning.IndexOf(@"<def>") + 5);
            //        wordMeaning = wordMeaning.Substring(0, wordMeaning.IndexOf(@"</def>"));
            //        wordsMeaning[getCurWordPos(curWord)] = wordMeaning;
            //    }

            if (MessageBox.ShowMsgBox(wordsMeaning[getCurWordPos(curWord)], curWord) == MessageBoxResult.Yes)
            {
                int place = getCurWordPos(curWord);
                wordList.Remove(curBtn.Content.ToString());
                wordsMeaning.RemoveAt(place);
                listViewWords.Children.Remove(curBtn);
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //MessageBoxResult res = MessageBox.Show();
            //MessageBox.Show(sender.GetType().ToString());//throw new NotImplementedException();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] str = wordList.ToArray();
            if (str.Length > 2000)
                File.WriteAllLines(dest, str);
        }

    }
}
