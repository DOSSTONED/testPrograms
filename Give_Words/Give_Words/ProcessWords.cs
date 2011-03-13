using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Text.RegularExpressions;
using System.Xml;

namespace Give_Words
{
    static class ProcessWords
    {
        static string DirectoryPath = @"E:\SRT\OUT\";
        /// <summary>
        /// return the title files
        /// </summary>
        /// <param name="path">From the directory</param>
        internal static string[] _GetTitles(string path)
        {
            return Directory.GetFiles(path, "*.srt", SearchOption.AllDirectories);//throw new System.NotImplementedException();
        }

        /// <summary>
        /// return every line in the spercific file
        /// </summary>
        /// <param name="filePath">the file that contain the lines</param>
        internal static string[] _GetLines(string filePath)
        {
            //StreamReader sr = new StreamReader(filePath);
            //char[] seperater = new char[] { '\n', '\r' };
            //string[] strs = sr.ReadToEnd().Split(seperater, StringSplitOptions.RemoveEmptyEntries);
            //for (int i = 0; i < strs.Length; i++)
            //{
            //    strs[i] = strs[i].Trim();
            //}
            //return strs;//throw new System.NotImplementedException();
            return File.ReadAllLines(filePath);
        }

        /*
        /// <summary>
        /// return the split words using regex
        /// Abandoned at 20110327
        /// </summary>
        /// <param name="line">a line of sentence</param>
        internal static string[] _GetWordsRegex(string line)
        {
            if (line == string.Empty || line.Length < 2)
                return null;
            else
            {
                string cur = line.ToLower();
                return Regex.Split(line, @"\W+");
            }//throw new System.NotImplementedException();
        }
         */

         
        /// <summary>
        /// write words to files
        /// </summary>
        /// <param name="path">the directory path contains srt files</param>
        public static void WriteIt(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", ""));
            XmlNode mainNode = xmlDoc.CreateNode(XmlNodeType.Element, "WORDS", "");
            //mainNode.SelectSingleNode("//word[@Name='eng']");
            

            string[] files = _GetTitles(path);
            string curTimeLine = string.Empty;
            int index_files, index_lines, index_words;
            for (index_files = 0; index_files < files.Length; index_files++)
            {
                string[] lines = _GetLines(files[index_files]);
                for (index_lines = 0; index_lines < lines.Length; index_lines++)
                {
                    if (lines[index_lines].Contains(@"-->"))
                    {
                        curTimeLine = lines[index_lines];
                        continue;
                    }
                    string[] words = _GetWordsOwn(lines[index_lines]);
                    if (words == null) continue;
                    for (index_words = 0; index_words < words.Length; index_words++)
                    {
                        if (words[index_words].Length < 2)
                            continue;
                        //if (lines[index_lines].Length < 2) continue;
                        string content = "\r\n" + files[index_files] + "\r\n" + curTimeLine + "\r\n" + lines[index_lines] + "\r\n";



                        /// next is used by normal writting method, direct write to files.
                        /// abandoned at 20110327
                        /// 
                        /*
                        if (words[index_words] == "con") continue;
                        File.AppendAllText(DirectoryPath + words[index_words], content);
                         */
                        //mainNode.AppendChild(xmlDoc.CreateNode(

                        /// get if there exists a same name node
                        /// 

                        XmlNode node = xmlDoc.SelectSingleNode(@"WORDS/Word[@Name='" + words[index_words] + "']");
                        if (node == null)
                        {

                            XmlElement curNode = xmlDoc.CreateElement("Word");

                            curNode.SetAttribute("Name", words[index_words]);
                            curNode.SetAttribute("Familiarity", "0");
                            curNode.SetAttribute("LastReviewTime", DateTime.UtcNow.ToUniversalTime().ToString());

                            curNode.InnerText += content;

                            mainNode.AppendChild(curNode);
                        }
                        else
                        {
                            node.InnerText += content;
                        }
                    }
                   
                }
            }

            xmlDoc.AppendChild(mainNode);

            using (XmlWriter xw = XmlWriter.Create(DirectoryPath + "Configure.xml"))
            {
                xmlDoc.WriteTo(xw);
                xw.Close();
            }
            //throw new System.NotImplementedException();
        }


        /*
        /// <summary>
        /// whether the string is a word
        /// </summary>
        /// <param name="str">a string to judge</param>
        public static bool _IsWord(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i]<0x60||str[i]>0x7a)
                    return false;//throw new System.NotImplementedException();
                
            }
            return true;
        }

         */

        /// <summary>
        /// return the split words use own method
        /// </summary>
        /// <param name="line">a line of sentence</param>
        internal static string[] _GetWordsOwn(string line)
        {
            string curLine = line.ToUpper();
            List<string> words = new List<string>();
            int wordStart = 0, wordFinish = 0;
            bool curCharIsLetter = false;
            for (int i = 0; i < curLine.Length; i++)
            {
                if (((curLine[i] >= 'A') && (curLine[i] <= 'Z')))//(curLine[i] == '\'') ||  || ((curLine[i] > 'A' - 1) && (curLine[i] < 'Z' + 1)) || (curLine[i] == '\''))
                {
                    if (!curCharIsLetter)
                    {
                        curCharIsLetter = true;
                        wordStart = i;
                        continue;
                    }
                    continue;
                }
                if (curCharIsLetter)
                {
                    curCharIsLetter = false;
                    wordFinish = i - 1;
                    words.Add(curLine.Substring(wordStart, wordFinish - wordStart + 1));
                    continue;
                }
            }




            if (words.Count < 1)
                return null;
            else
            {
                string[] retWords = new string[words.Count];
                for (int i = 0; i < words.Count; i++)
                {
                    retWords[i] = words[i];
                }
                return retWords;
            }
        }
    }
}
