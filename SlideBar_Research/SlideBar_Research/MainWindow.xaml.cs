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
using SBarHook;

namespace SlideBar_Research
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

        static SlideBar sbController = new SlideBar(true);
        static bool directionLeft2Right = true;
        static byte formerPlace = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sbController.sbArriveEvent += new SlideBarEventHandler(sbController_sbArriveEvent);
        }

        void sbController_sbArriveEvent(SlideBar.SlideBarData sbData)
        {
            directionLeft2Right = sbData.bPosition >= formerPlace;
            formerPlace = sbData.bPosition;
            
        }
    }
}
