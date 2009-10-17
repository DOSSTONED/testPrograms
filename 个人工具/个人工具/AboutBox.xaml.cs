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
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace 个人工具
{
    /// <summary>
    /// AboutBox.xaml 的交互逻辑
    /// </summary>
    
    public partial class AboutBox : Window
    {
        //private bool neverRendered = true;
        private double _top = 10, _left = 10;//, _right = 200, _bottom = 200;
        
        public const int WM_MOVING = 0x0216;
        public const int WM_MOVE = 0x0003;
        public const ulong BorderMargin = 5;

        public AboutBox()
        {
            InitializeComponent();

            this.Title = "About Me";//String.Format("关于 {0} {0}", AssemblyTitle);
            this.labelProductName.Content = AssemblyProduct;
            this.labelVersion.Content = String.Format("版本 {0} {0}", AssemblyVersion);
            this.labelCopyright.Content = AssemblyCopyright;
            this.labelCompanyName.Content = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            System.Media.SystemSounds.Asterisk.Play();
            this.SourceInitialized += new EventHandler(AboutBox_SourceInitialized);
            /*
            labelProductName.Margin.Top = imageDOSSTONED.Height;
            labelVersion.Margin.Top = imageDOSSTONED.Height;
            labelCopyrighte.Margin.Top = imageDOSSTONED.Height;
            labelCompanyName.Margin.Top = imageDOSSTONED.Height;
            textBoxDescription.Margin.Top = imageDOSSTONED.Height;
             */
            DragThumb.DragDelta += OnMove;
        }


        private void OnMove(object s, DragDeltaEventArgs e)
        {

            _left = Left;
            _top = Top;
            _left += (int)e.HorizontalChange;
            _top += (int)e.VerticalChange;
            
            if (_top < BorderMargin)
            {
                _top = BorderMargin;
            }
            if (_left < BorderMargin)
            {
                _left = BorderMargin;
            }
            if (_top > SystemParameters.WorkArea.Height - Height - BorderMargin)
            {
                _top = SystemParameters.WorkArea.Height - Height - BorderMargin;
            }
            if (_left > SystemParameters.WorkArea.Width - Width - BorderMargin)
            {
                _left = SystemParameters.WorkArea.Width - Width - BorderMargin;
            }
            this.Left = _left;
            this.Top = _top;
        }

        void AboutBox_SourceInitialized(object sender, EventArgs e)
        {
            //base.OnSourceInitialized(e);
            //HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            //if (hwndSource != null)
            //{
            //    hwndSource.AddHook(new HwndSourceHook(this.WndProc));
            //}
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }
        /// <summary>
        /// It seems that "OnContentRendered" function doesn't function at all. At least, if the function is disabled, Aero effects can also displayed.
        /// </summary>
        /// <param name="e"></param>
       /*
        protected override void OnContentRendered(EventArgs e)
        {
            if (this.neverRendered)
            {
                // The window takes the size of its content because SizeToContent
                // is set to WidthAndHeight in the markup. We then allow
                // it to be set by the user, and have the content take the size
                // of the window.
                this.SizeToContent = SizeToContent.Manual;

                FrameworkElement root = this.Content as FrameworkElement;
                if (root != null)
                {
                    root.Width = double.NaN;
                    root.Height = double.NaN;
                }

                this.neverRendered = false;
            }

            base.OnContentRendered(e);
        }
        */

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        //protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    switch (msg)
        //    {
        //        case WM_MOVING:
        //            {
        //                handled = true;
        //                break;
        //            }
        //        case WM_MOVE:
        //            {
        //                handled = true;
        //                break;
        //            }
        //    }
        //    return IntPtr.Zero;
        //}




        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
