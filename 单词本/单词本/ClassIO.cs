using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trinet.Core.IO.Ntfs;

namespace 单词本
{
    class Word
    {
        private string _word;
        private string _meaning;
        private double _familiarity;
        private long _reviewTimes;

        public Word(string word, string meaning, double familiarity,long reviewedTimes)
        {
            _word = word;
            _meaning = meaning;
            _familiarity = familiarity;
            _reviewTimes = reviewedTimes;

        }

        public Word(string word, string meaning, double familiarity)
        {
            _word = word;
            _meaning = meaning;
            _familiarity = familiarity;
            _reviewTimes = 1;
        }

        public Word(string word, string meaning)
        {
            _word = word;
            _meaning = meaning;
            _familiarity = 0;
            _reviewTimes = 1;
        }

        public Word(string word)
        {
            _word = word;
            _meaning = string.Empty;
            _familiarity = 0;
            _reviewTimes = 1;
        }

        public string WordName
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }

        public string Meaning
        {
            get
            {
                return _meaning;
            }
            set
            {
                _meaning = value;
            }
        }

        public double Familiarity
        {
            get
            {
                return _familiarity;
            }
            set
            {
                _familiarity = value;
            }
        }

        public long ReviewedTimes
        {
            get
            {
                return _reviewTimes;//throw new System.NotImplementedException();
            }
            set
            {
                _reviewTimes = value;
            }
        }
    }
    static class ClassIO
    {
        static public List<Word> words = null;

        static public void LoadWords()
        {
            words = new List<Word>();
            string[] wordFiles = Directory.GetFiles(WorkingDir);
            foreach (string wordSingle in wordFiles)
            {
                string curMeaning = File.ReadAllText(wordSingle);
                string curName = Path.GetFileNameWithoutExtension(wordSingle);
                double famili = 0;
                long review = 1;
                FileInfo fInfo = new FileInfo(wordSingle);
                if (fInfo.AlternateDataStreamExists(StreamNameFamili))
                {
                    StreamReader sr = fInfo.GetAlternateDataStream(StreamNameFamili).OpenText();
                    famili = Convert.ToDouble(sr.ReadToEnd());
                }
                if (fInfo.AlternateDataStreamExists(StreamNameReviewedTimes))
                {
                    StreamReader sr = fInfo.GetAlternateDataStream(StreamNameReviewedTimes).OpenText();
                    review = Convert.ToInt64(sr.ReadToEnd());
                }

                words.Add(new Word(curName, curMeaning, famili, review));
            }
        }

        public static void WriteWordInfo(Word newWord)
        {
            FileInfo fInfo = new FileInfo(WorkingDir + newWord.WordName);
            if (fInfo.AlternateDataStreamExists(StreamNameFamili))
                fInfo.DeleteAlternateDataStream(StreamNameFamili);
            if (fInfo.AlternateDataStreamExists(StreamNameReviewedTimes))
                fInfo.DeleteAlternateDataStream(StreamNameReviewedTimes);

            FileStream fsFamili = fInfo.GetAlternateDataStream(StreamNameFamili, FileMode.Create).OpenWrite();
            byte[] bytesFamili = Encoding.ASCII.GetBytes(newWord.Familiarity.ToString());
            fsFamili.Write(bytesFamili, 0, bytesFamili.Length);
            fsFamili.Close();

            FileStream fsReview = fInfo.GetAlternateDataStream(StreamNameReviewedTimes, FileMode.Create).OpenWrite();
            byte[] bytesReview = Encoding.ASCII.GetBytes(newWord.ReviewedTimes.ToString());
            fsReview.Write(bytesReview, 0, bytesReview.Length);
            fsReview.Close();


        }

        private const string WorkingDir = @"E:\Documents\Words\";
        private const string StreamNameFamili = @"Familiarity";
        private const string StreamNameReviewedTimes = @"ReviewedTimes";
        //public static void Loa
    }

}
