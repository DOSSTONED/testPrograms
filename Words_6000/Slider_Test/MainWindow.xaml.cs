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

namespace Slider_Test
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

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            
            Point p = e.GetPosition(this);
            label1.Content = p.ToString();

            double curHorPos = p.X;

            //setButtonSize(largeSize - normalSize, button1);
            //setButtonALLSize(curHorPos, 650);

        }

        void setButtonALLSize(double curHorPos, double total)
        {
            int normalSize = 64;
            int largeSize = 128;
            setButtonSize(normalSize + scale(10 * total / 650, 160 * total / 650, curHorPos) * (largeSize - normalSize), ref button1);
            setButtonSize(normalSize + scale(130 * total / 650, 280 * total / 650, curHorPos) * (largeSize - normalSize), ref button2);
            setButtonSize(normalSize + scale(250 * total / 650, 400 * total / 650, curHorPos) * (largeSize - normalSize), ref button3);
            setButtonSize(normalSize + scale(370 * total / 650, 520 * total / 650, curHorPos) * (largeSize - normalSize), ref button4);
            setButtonSize(normalSize + scale(490 * total / 650, 640 * total / 650, curHorPos) * (largeSize - normalSize), ref button5);
        }

        double scale(double start, double end, double curPlace)   // 
        {
            if (curPlace < start)
                return 0;
            if (curPlace > end)
                return 0;
            if (curPlace < start + (end - start) / 3)
                return (curPlace - start) * 3 / (end - start);
            if (curPlace > start + (end - start) * 2 / 3)
                return (1 - (curPlace - start) / (end - start)) * 3;
            return 1;
        }

        void setButtonSize(double size, ref Button btn)
        {
            double center = btn.Width / 2 + btn.Margin.Left;

            
            btn.Width = size;
            btn.Height = size;
            //btn.Margin.Left = center - size / 2;
            btn.Margin = new Thickness(center - size / 2, 0, 0, 0);
        }

        private SlideBar sb = new SlideBar(true);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sb.sbArriveEvent += new SlideBarEventHandler(sb_sbArriveEvent);
        }

        void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {
            MessageBox.Show("");
            setButtonALLSize(sbData.bPosition, 256);//throw new NotImplementedException();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sb.sbArriveEvent -= new SlideBarEventHandler(sb_sbArriveEvent);
            sb = null;
        }
    }
}
