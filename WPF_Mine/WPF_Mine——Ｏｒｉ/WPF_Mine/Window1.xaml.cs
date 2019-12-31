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
using System.Timers;
using System.Windows.Threading;

namespace WPF_Mine
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Sweeper : Window
    {
        int SpentTimes = 0;
        Timer CountTime = new Timer(1000);
        SweepButton[] Mines = new SweepButton[100];
        int TotalNumber = -1;

        public Sweeper()
        {


            for (int i = 0; i < 100; i++)
            {
                Mines[i] = new SweepButton();
            }


            InitializeComponent();

            // Create the filed

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Mines[10 * i + j].Margin = new Thickness(21 * i, 21 * j, 0, 0);
                    gridBombZone.Children.Add(Mines[10 * i + j]);
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelBomb.Content = "Bombs : 0";
            CountTime.Elapsed += new ElapsedEventHandler(CountTime_Elapsed);
            AeroGlass.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double _left = Left;
            double _top = Top;
            _left += (int)e.HorizontalChange;
            _top += (int)e.VerticalChange;
            this.Left = _left;
            this.Top = _top;
        }

        private void buttonStartNew_Click(object sender, RoutedEventArgs e)
        {

            CountTime.Stop();
            SpentTimes = 0;
            labelSpendTime.Content = "0";



            ///////////////////
            // for design the mines
            ///////////////////


            // Set the bombs complete
            Random random = new Random();
            TotalNumber = random.Next(5, 10);
            int SetNumber = 0;
            labelBomb.Content = "Bombs : " + TotalNumber.ToString();
            for (int i = 0; i < 100; i++)
            {
                if (random.Next(1, 100) > 80 && SetNumber < TotalNumber)
                {
                    Mines[i].Bombs = 9;
                    SetNumber++;
                }
                else
                {
                    Mines[i].Bombs = 0;
                }
                Mines[i].IsEnabled = true;
                Mines[i].Content = "";
                Mines[i].SetPositon(i % 10, (i - i % 10) / 10);

            }

            // Set the numbers that indicates the bombs around

            for (int i = 0; i < 100; i++)
            {
                if (Mines[i].Bombs != 9)
                {
                    int j = i % 10;
                    int k = (i - j) / 10; // Bombs[k][j] , 10*k+j
                    Mines[i].Bombs = 0;
                    int numbers = 0;
                    for (int temp1 = -1; temp1 < 2; temp1++)
                    {
                        for (int temp2 = -1; temp2 < 2; temp2++)
                        {
                            if (k + temp1 >= 0 && k + temp1 <= 9)
                            {
                                if (j + temp2 >= 0 && j + temp2 <= 9)
                                {
                                    if (Mines[10 * (k + temp1) + j + temp2].Bombs == 9)
                                        numbers++;
                                }
                            }
                        }
                    }
                    Mines[i].Bombs = numbers;
                }
            }

            ///////////////////
            // 
            ///////////////////

            CountTime.Start();
        }

        void CountTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                SpentTimes++;
                labelSpendTime.Content = SpentTimes.ToString();

            }));
        }

        class SweepButton : System.Windows.Controls.Button
        {
            private int _bombs;// 9 for this button is a bomb!!

            private int _coloum;
            private int _row;
            private bool _positionSet;

            public void SetPositon(int col, int row)
            {
                if (!_positionSet)
                {
                    _positionSet = true;
                    _coloum = col;
                    _row = row;
                }
            }

            public int GetColoum()
            {
                return _coloum;
            }

            public int GetRow()
            {
                return _row;
            }

            public int Bombs
            {
                get
                {
                    return _bombs;
                }
                set
                {
                    _bombs = value;
                }
            }

            public SweepButton()
            {
                Width = 21;
                Height = 21;
                Bombs = 0;
                this.HorizontalAlignment = HorizontalAlignment.Left;
                this.VerticalAlignment = VerticalAlignment.Top;
                this.HorizontalContentAlignment = HorizontalAlignment.Center;
                this.VerticalContentAlignment = VerticalAlignment.Center;
                this.Click += new RoutedEventHandler(SweepButton_Click);
                this.MouseRightButtonDown += new MouseButtonEventHandler(SweepButton_MouseRightButtonDown);
                _row = 0;
                _coloum = 0;
                _positionSet = false;
            }

            void SweepButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
            {
                if ((string)this.Content != "")
                    this.Content = "";
                else
                    this.Content = "*";
            }

            void SweepButton_Click(object sender, RoutedEventArgs e)
            {
                if ((string)this.Content != "*")
                {
                    this.IsEnabled = false;
                    if (this.Bombs == 9)
                    {
                        this.Content = "#";

                        MessageBox.Show("You are dead!!");
                    }

                    this.Content = this.Bombs;
                    if (this.Bombs == 0)
                    {
                        this.Content = "";
                        for (int temp1 = -1; temp1 < 2; temp1++)
                        {
                            for (int temp2 = -1; temp2 < 2; temp2++)
                            {
                                if (_coloum + temp1 >= 0 && _coloum + temp1 <= 9)
                                {
                                    if (_row + temp2 >= 0 && _row + temp2 <= 9)
                                    {
                                        Mines[10 * (_coloum + temp1) + _row + temp2].SweepButton_Click(sender, e);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }



}
