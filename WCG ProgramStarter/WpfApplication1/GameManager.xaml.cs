using System;
using System.Windows;
using System.Collections.Generic;

namespace GameManager_WPF
{
    public struct GameInfo
    {
        public string Name;
        public string Icon;
        public string Args;
        public string Target;
    }

    /// <summary>
    /// Interaction logic for GameManager.xaml
    /// </summary>
    public partial class GameManager : Window
    {
        SBarHook.SlideBar SB = null;
        List<GameInfo> Games = new List<GameInfo>();

        private int SBInputPos;

        public GameManager()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SB = new SBarHook.SlideBar(SlideBarHandler, true);
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
            RefreshGameList();
        }

        private void SlideBarHandler(SBarHook.SlideBar.SlideBarData sbData)
        {
            if (sbData.bEvent == SBarHook.SlideBar.Event.ServiceKey)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    this.Show();
                    this.Activate();
                    this.Focus();
                }), null);
                return;
            }
            if (sbData.bAction == SBarHook.SlideBar.SlideBarAction.On)
            {
                SBInputPos = sbData.bPosition;
            }

            Dispatcher.Invoke(new Action(delegate
            {
                
            }), null
                );
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (SB != null)
                SB.StopAllEventWatcher(false);
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonGameCancel_Click(object sender, RoutedEventArgs e)
        {
            PageGameSetting.Visibility = Visibility.Hidden;
        }

        private void buttonGameApply_Click(object sender, RoutedEventArgs e)
        {
            GameInfo newGame = new GameInfo();
            newGame.Args = textBoxTargetArguments.Text;
            newGame.Icon = textBoxIconPath.Text;
            newGame.Name = textBoxGameName.Text;
            newGame.Target = textBoxTargetPath.Text;
            Games.Add(newGame);
        }

        private void RefreshGameList()
        {
            PageGames.Visibility = Visibility.Hidden;
            PageGameSetting.Visibility = Visibility.Hidden;
            PageLoading.Visibility = Visibility.Visible;
            if (Games.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < Games.Count; i++)
                {
                    UserControlGame ucg = new UserControlGame(Games[i]);
                    PanelGames.Children.Add(ucg);
                }
            }
            PageLoading.Visibility = Visibility.Hidden;
            PageGames.Visibility = Visibility.Visible;
        }
    }
}
