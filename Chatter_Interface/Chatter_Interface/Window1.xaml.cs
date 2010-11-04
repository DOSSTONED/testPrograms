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


namespace Chatter_Interface
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        const string msgSentByMyself = "myself";

        private bool add_Contents(string str, string type)
        {
            Label labelTitle = new Label();
            TextBox content = new TextBox();
            Label labelContent = new Label();


            if (type == msgSentByMyself)
            {
                labelTitle.Foreground = Brushes.Green;
                labelTitle.Content = "My self";

                DockPanel.SetDock(labelTitle, Dock.Top);
                dockPanelMsg.Children.Add(labelTitle);



                /*
                content.Foreground = Brushes.Black;
                content.Background = Brushes.Transparent;
                content.Text = str;
                DockPanel.SetDock(content, Dock.Top);
                dockPanelMsg.Children.Add(content);
                */



                labelContent.Foreground = Brushes.Black;
                labelContent.Content = str;

                DockPanel.SetDock(labelContent, Dock.Top);
                dockPanelMsg.Children.Add(labelContent);

            }
            return false;
        }

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1, -1, -1, -1));
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double tmpLeft = Left, tmpTop = Top;
            tmpLeft += e.HorizontalChange;
            tmpTop += e.VerticalChange;

            /// must put codes here to deal with some effects on moving the window

            if (tmpLeft + Width > SystemParameters.WorkArea.Right)
                tmpLeft = SystemParameters.WorkArea.Right - Width;
            if (tmpLeft < SystemParameters.WorkArea.Left)
                tmpLeft = SystemParameters.WorkArea.Left;
            if (tmpTop < SystemParameters.WorkArea.Top)
                tmpTop = SystemParameters.WorkArea.Top;
            if (tmpTop + Height > SystemParameters.WorkArea.Bottom)
                tmpTop = SystemParameters.WorkArea.Bottom - Width;



            Left = tmpLeft;
            Top = tmpTop;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            add_Contents(textBoxSend.Text, msgSentByMyself);


        }
    }
}
