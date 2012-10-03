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


namespace PKULogin
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

        static string 莫名其妙的字符串 = "|;kiDrqvfi7d$v0p5Fg72Vwbv2;|";//"%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C";

        /*

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string requestURL = @"https://its.pku.edu.cn/cas/login";

            WebClient postClient = new WebClient();
            string data =
                "username1=" + textBox_Un.Text +    //[19]
                "&password=" + passwordBox_Pb.Password +    //[37]
                "&pwd_t=密码" + //%E5%AF%86%E7%A0%81" + // “密码”的编码    //[50]
                "&fwrd=free" +    // Cernet Free    //[60]
                "&username=" + textBox_Un.Text +    //[80]
                    莫名其妙的字符串 +  //[108]
                    passwordBox_Pb.Password +   //[116]
                    莫名其妙的字符串 +  //[144]
                    "12";   //[146]


            byte[] postData = Encoding.UTF8.GetBytes(data);
            byte[] responseData = postClient.UploadData(requestURL, "POST", postData);
            requestURL = "https://its.pku.edu.cn/netportal/ipgwopen?sid=332";

            responseData = postClient.DownloadData(requestURL);
            textBox1.Text = Encoding.UTF8.GetString(responseData);
        }

        */

        private void buttonBrowserLogin_Click(object sender, RoutedEventArgs e)
        {
            string requestURL = @"https://its.pku.edu.cn/cas/login";
            string data =
                "username1=" + textBox_Un.Text +    //[19]
                "&password=" + passwordBox_Pb.Password +    //[37]
                "&pwd_t=密码" + //%E5%AF%86%E7%A0%81" + // “密码”的编码    //[50]
                "&fwrd=free" +    // Cernet Free    //[60]
                "&username=" + textBox_Un.Text +    //[80]
                    莫名其妙的字符串 +  //[108]
                    passwordBox_Pb.Password +   //[116]
                    莫名其妙的字符串 +  //[144]
                    "12";   //[146] //indicates free;
            webBrowser1.Navigate(requestURL + "?" + data);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string requestURL = @"https://its.pku.edu.cn/cas/login";
            string data =
                "username1=" + textBox_Un.Text +    //[19]
                "&password=" + passwordBox_Pb.Password +    //[37]
                "&pwd_t=密码" + //%E5%AF%86%E7%A0%81" + // “密码”的编码    //[50]
                "&fwrd=fee" +    // Global    //[60]
                "&username=" + textBox_Un.Text +    //[80]
                    莫名其妙的字符串 +  //[108]
                    passwordBox_Pb.Password +   //[116]
                    莫名其妙的字符串 +  //[144]
                    "11";   //[146] //indicates nonfree;
            webBrowser1.Navigate(requestURL + "?" + data);
        }

        private void buttonDisconnectAll_Click(object sender, RoutedEventArgs e)
        {
            string requestURL = @"https://its.pku.edu.cn/cas/login";
            string data =
                "username1=" + textBox_Un.Text +    //[19]
                "&password=" + passwordBox_Pb.Password +    //[37]
                "&pwd_t=密码" + //%E5%AF%86%E7%A0%81" + // “密码”的编码    //[50]
                "&fwrd=free" +    // Global    //[60]
                "&username=" + textBox_Un.Text +    //[80]
                    莫名其妙的字符串 +  //[108]
                    passwordBox_Pb.Password +   //[116]
                    莫名其妙的字符串 +  //[144]
                    "13";   //[146] //indicates nonfree;
            webBrowser1.Navigate(requestURL + "?" + data);
        }
    }
}
