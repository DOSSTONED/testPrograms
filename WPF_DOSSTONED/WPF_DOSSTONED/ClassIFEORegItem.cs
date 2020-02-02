using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_DOSSTONED
{
    public class ClassIFEORegItem
    {
        private string _architecture;
        private string _imagePath;
        private string _imageName;

        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        public string Architecture
        {
            get { return _architecture; }
            set { _architecture = value; }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public ClassIFEORegItem(string name, string architecture, string imagePath)
        {
            this._imageName = name;
            this._architecture = architecture;
            this._imagePath = imagePath;
            
        }
    }


}
