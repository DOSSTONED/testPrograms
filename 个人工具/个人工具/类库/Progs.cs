using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace 个人工具
{
    //[Serializable]
    class Progs:System.Windows.Controls.Button
    {
        public Progs(string path, string name)
        {
            
            //Name
            Content = name;
            ImagePath = path;
            
            this.Content = name;
            Margin = new System.Windows.Thickness(3);
            this.Click += new System.Windows.RoutedEventHandler(this.Progs_ClickThread);
        }

        void Progs_ClickThread(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Progs_Click));
            t.Start();
        }

        void Progs_Click()
        {
            try
            {
                
                System.Diagnostics.Process.Start(ImagePath);
                ClickedTimes++;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private string _path;
        private string _name;
        private uint _clicktimes;
        public uint ClickedTimes
        {
            get
            {
                return _clicktimes;
            }
            set
            {
                _clicktimes = value;
            }
        }

        public string ImagePath
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

    }
}
