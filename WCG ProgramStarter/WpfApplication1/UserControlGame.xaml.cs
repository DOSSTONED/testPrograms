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

namespace GameManager_WPF
{
    /// <summary>
    /// Interaction logic for UserControlGame.xaml
    /// </summary>
    public partial class UserControlGame : UserControl
    {
        public string GameName;
        public string GameTarget;
        public string GameIcon;
        public string GameArgs;

        public UserControlGame()
        {
            InitializeComponent();
        }

        public UserControlGame(GameInfo gi)
        {
            InitializeComponent();
            GameArgs = gi.Args;
            GameIcon = gi.Icon;
            GameName = gi.Name;
            GameTarget = gi.Target;

            LabelGameName.Content = gi.Name;
        }
    }
}
