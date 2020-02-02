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
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace PKULogin_WebRequest
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

        PKUGW GatewayLogin = new PKUGW();

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            labelTime.Content = "Trying to Login...";
            Thread t = new Thread(new ParameterizedThreadStart(NetworkThread));
            groupBoxControls.IsEnabled = false;
            t.Start(sender);

            //textBlock1.Text = result;
            //webBrowser1.NavigateToStream(new MemoryStream(Encoding.UTF8.GetBytes(result)));
        }

static bool FilterOdd(int i)
{
    return i % 2 == 1;
}

        void NetworkThread(object sender)
        {
            DateTime _start = DateTime.Now;
            string result = string.Empty;
            string loginType = string.Empty;
            string username = string.Empty;
            string password = string.Empty;
            Button b = sender as Button;

            Dispatcher.Invoke(new Action(() =>
                {
                    loginType = b.Content as string;
                    username = textBox_Un.Text;
                    password = passwordBox_Pb.Password;
                }));

            try
            {
                switch (loginType)
                {
                    case "Global":
                        result = GatewayLogin.Login(username, password, PKUGW.LoginType.fee);
                        break;
                    case "Free Login":
                        result = GatewayLogin.Login(username, password, PKUGW.LoginType.free);
                        break;
                    case "Disconnect All":
                        result = GatewayLogin.Login(username, password, PKUGW.LoginType.DisconnectAll);
                        break;
                    default:
                        return;
                }

                string info = result.Substring(result.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length,
                    result.IndexOf("IPGWCLIENT_END-->") - (result.IndexOf(@"<!--IPGWCLIENT_START") + ("<!--IPGWCLIENT_START").Length));
                string[] infos = info.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Dispatcher.Invoke(new Action(() =>
                {
                    listBox1.Items.Clear();
                    foreach (string inf in infos)
                    {
                        //inf.Split('=');
                        //GridViewColumnCollection gvcc = new GridViewColumnCollection();
                        //GridViewColumn gvc0 = new GridViewColumn();
                        //gvc0.Header = inf.Split('=')[0];
                        //gvcc.Add(gvc0);
                        //GridViewColumn gvc1 = new GridViewColumn();
                        //gvc1.Header = inf.Split('=')[1];
                        //gvcc.Add(gvc1);
                        //GridViewRowPresenter gvrp = new GridViewRowPresenter();
                        ////gvrp.Content=
                        //gvrp.Content = gvcc;

                        //gvrp.Height = 20;
                        //listBox1.Items.Add(gvrp);
                        listBox1.Items.Add(inf);
                    }
                    labelTime.Content = (DateTime.Now - _start).TotalMilliseconds.ToString() + " ms";
                    labelError.Content = string.Empty;
                }));
            }
            catch (WebException we)
            {
                Dispatcher.Invoke(new Action(() =>
                    {
                        labelError.Content = we.Message;
                        labelTime.Content = string.Empty;
                    }));
            }
            finally
            {
                Dispatcher.Invoke(new Action(() =>
                       {
                           groupBoxControls.IsEnabled = true;
                       }));
            }
        }



        //private void buttonFreeLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    DateTime _start = DateTime.Now;
        //    string result =string.Empty;
        //    WebProxy proxy = new WebProxy();
        //    if (ProxyGrid.IsEnabled)
        //    {
        //        proxy.Address = new Uri(textBoxProxyAddress.Text + ":" + textBoxProxyPort.Text);
        //    }
        //    Button b = sender as Button;
        //    if (b.Content.ToString().Contains("Global"))
        //    {
        //        switch (
        //                MessageBox.Show(
        //                "You select global access, this may be charged!!!\r\n" +
        //                "If you want to continue, press YES.\r\n" +
        //                "If you just want to connect free, press NO.\r\n" +
        //                "If you want to cancel login procedure, press CANCEL.",
        //                "Warning",
        //                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning
        //                )
        //            )
        //        {
        //            case MessageBoxResult.Yes:
        //                result = GatewayLogin.Login(textBox_Un.Text, passwordBox_Pb.Password, PKUGW.LoginType.fee, proxy);
        //                break;
        //            case MessageBoxResult.No:
        //                result = GatewayLogin.Login(textBox_Un.Text, passwordBox_Pb.Password, PKUGW.LoginType.free);
        //                break;
        //            default:
        //                return;
        //        }

        //    }
        //    else
        //        result = GatewayLogin.Login(textBox_Un.Text, passwordBox_Pb.Password, PKUGW.LoginType.free, proxy);
        //    //webBrowser1.NavigateToStream(new MemoryStream(Encoding.UTF8.GetBytes(result)));
        //    //MessageBox.Show((DateTime.Now - _start).TotalMilliseconds.ToString() + " ms");
        //    labelTime.Content = (DateTime.Now - _start).TotalMilliseconds.ToString() + " ms";
        //}

        //private void buttonDisconnectAll_Click(object sender, RoutedEventArgs e)
        //{
        //    string result = GatewayLogin.Login(textBox_Un.Text, passwordBox_Pb.Password, PKUGW.LoginType.DisconnectAll);
        //    //webBrowser1.NavigateToStream(new MemoryStream(Encoding.UTF8.GetBytes(result)));
        //}


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox_Un.Text = Properties.Settings.Default.Username;
            passwordBox_Pb.Password = Properties.Settings.Default.Password;
            int[] numbers = { 10, 23, 423, 56, 8, -23, 5, 21, 5 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Username = textBox_Un.Text;
            Properties.Settings.Default.Password = passwordBox_Pb.Password;
            Properties.Settings.Default.Save();
        }

    }
}
