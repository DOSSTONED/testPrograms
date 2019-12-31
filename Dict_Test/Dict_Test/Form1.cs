using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Trinet.Core.IO.Ntfs;

namespace Dict_Test
{
    public partial class Form1 : Form
    {
        const string zzz_IS_NEW_STREAM_NAME = "IsNew";
        const string zzz_WORDS_DIR_PATH = @"E:\Documents\Words\";
        public Form1()
        {
            InitializeComponent();
        }

        private void RefreshList()
        {
            listBoxNew.Items.Clear();
            listBoxOld.Items.Clear();

            string[] words = Directory.GetFiles(zzz_WORDS_DIR_PATH);
            //textBoxWord.AutoCompleteCustomSource.AddRange(words);
            
            
            FileInfo fsInfo = null;
            for (int i = 0; i < words.Length; i++)
            {
                fsInfo = new FileInfo(words[i]);
                if (fsInfo.AlternateDataStreamExists(zzz_IS_NEW_STREAM_NAME))
                {
                    var info = fsInfo.GetAlternateDataStream(zzz_IS_NEW_STREAM_NAME).OpenText();
                    if (info.ReadToEnd().ToUpper() == "FALSE")
                    {
                        listBoxOld.Items.Add(Path.GetFileNameWithoutExtension(words[i]));
                        info.Close();
                        continue;
                    }
                }

                listBoxNew.Items.Add(Path.GetFileNameWithoutExtension(words[i]));

            }
        }

        private void WordList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wordList = sender as ListBox;
            if (wordList == null)
                return;

            string wordFile = zzz_WORDS_DIR_PATH + (wordList.SelectedItem as string);
            if (!File.Exists(wordFile))
                return;

            FileInfo fsInfo = null;
            StreamReader wordRead = null;
            try
            {
                //wordRead = File.OpenText(wordFile);
                fsInfo = new FileInfo(wordFile);
                wordRead = fsInfo.OpenText();

                textBoxWord.Text = Path.GetFileNameWithoutExtension(wordFile);
                labelWordInCap.Text = Path.GetFileNameWithoutExtension(wordFile).ToUpper();
                //textBoxWordMeaning.Visible = false;
                //buttonWordShowMeaning.Visible = true;
                textBoxWordMeaning.Text = wordRead.ReadToEnd();

                if (fsInfo.AlternateDataStreamExists(zzz_IS_NEW_STREAM_NAME))
                {
                    var info = fsInfo.GetAlternateDataStream(zzz_IS_NEW_STREAM_NAME).OpenText();
                    if (info.ReadToEnd().ToUpper() == "FALSE")
                        checkBoxIsNew.Checked = false;
                    else
                        checkBoxIsNew.Checked = true;
                    info.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.InnerException.ToString(), ex.Source);
            }
            finally
            {
                if (wordRead != null)
                    wordRead.Close();
            }
        }

        private void buttonWordShowMeaning_Click(object sender, EventArgs e)
        {
            buttonWordShowMeaning.Visible = false;
            textBoxWordMeaning.Visible = true;
        }

        private void buttonSaveInfo_Click(object sender, EventArgs e)
        {
            string dest = zzz_WORDS_DIR_PATH + textBoxWord.Text;


            FileInfo fsInfo = null;
            try
            {
                File.Delete(dest);
                fsInfo = new FileInfo(dest);
                var infoText = fsInfo.CreateText();
                infoText.Write(textBoxWordMeaning.Text);
                infoText.Close();
                if (!checkBoxIsNew.Checked)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(false.ToString());
                    FileStream info = fsInfo.GetAlternateDataStream(zzz_IS_NEW_STREAM_NAME, FileMode.OpenOrCreate).OpenWrite();
                    info.Write(bytes, 0, bytes.Length);
                    info.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                RefreshList();
                textBoxWord.Focus();
            }
            if (sender is TextBox)
            {
                textBoxWord.Clear();
                textBoxWordMeaning.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void textBoxWordMeaning_KeyDown(object sender, KeyEventArgs e)
        {
            isCTRLPressed = e.Control;
        }
        private bool isCTRLPressed = false;
        private void textBoxWordMeaning_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                if (isCTRLPressed)
                {
                    checkBoxIsNew.Checked = true;
                    buttonSaveInfo_Click(sender, null);
                }
                e.Handled = true;
            }
            isCTRLPressed = false;
        }
    }
}
