using System;
using System.Windows;
using System.Configuration;

namespace WPF_DOSSTONED
{
    class ClassFavorProg : System.Windows.Controls.Button
    {

        private string Path;
        // private string Tooltip;

        public ClassFavorProg()
        {
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            Path = "";
            ClickedTimes = 0;
        }

        public int ClickedTimes;

        /*
        ~ClassFavorProg()
        {
            MessageBox.Show("Dispose");
        }
        */
        public void SetPath(string p)
        {
            Path = p;
        }

        public string GetPath()
        {
            return Path;
        }

        public void BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (System.IO.File.Exists(Path))
                System.Diagnostics.Process.Start(Path);
                ClickedTimes++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }


    // Define a custom section.
    public sealed class SaveProgSection : ConfigurationSection
    {
        public SaveProgSection()
        {

        }

        [ConfigurationProperty("programPath", DefaultValue = "defaultpath")]
        [StringValidator( MinLength = 1, MaxLength = 600)]
        public String ProgramPath
        {
            get
            {
                return (String)this["programPath"];
            }
            set
            {
                this["programPath"] = value;
            }
        }

        [ConfigurationProperty("programName", DefaultValue = "defaultname")]
        [StringValidator(InvalidCharacters = "*\"\\|/<>:", MinLength = 1, MaxLength = 600)]
        public String ProgramName
        {
            get
            {
                return (String)this["programName"];
            }
            set
            {
                this["programName"] = value;
            }
        }

        [ConfigurationProperty("clickedTimes", DefaultValue = 0)]
        [IntegerValidator(MinValue=0)]   

        public int ClickedTimes
        {
            get
            {
                return (int)this["clickedTimes"];
            }
            set
            {
                this["clickedTimes"] = value;
            }
        }
        /*
          
        [ConfigurationProperty("maxIdleTime", DefaultValue = "1:30:30")]
        public TimeSpan MaxIdleTime
        {
            get
            {
                return (TimeSpan)this["maxIdleTime"];
            }
            set
            {
                this["maxIdleTime"] = value;
            }
        }

        
        [ConfigurationProperty("permission", DefaultValue = Permissions.Read)]
        public enum Permissions
        {
            FullControl = 0,
            Modify = 1,
            ReadExecute = 2,
            Read = 3,
            Write = 4,
            SpecialPermissions = 5
        }
        public Permissions Permission
        {
            get
            {
                return (Permissions)this["permission"];
            }

            set
            {
                this["permission"] = value;
            }

        }
        */
    }
}
