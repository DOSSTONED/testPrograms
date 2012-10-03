using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PKUGW;
using System.IO;
using System.Text;

namespace PKUGW_Helper
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        PKUGW_WP GatewayLogin = new PKUGW_WP();

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = DateTime.Now;
            try
            {
                string result = string.Empty;
                Button b = sender as Button;

                result = GatewayLogin.Login(TextBlockUsername.Text, PasswordBox1.Password, PKUGW_WP.LoginType.free);
                textBlock1.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show((DateTime.Now - _start).TotalMilliseconds.ToString() + " ms");
        }

        private void ButtonDisconnectAll_Click(object sender, RoutedEventArgs e)
        {
            HttpAdapter ha = new HttpAdapter();
            ha.Destination = "https://its.pku.edu.cn/cas/login";
            ha.cc = _cookie;
            ha.ProcessHttp(PKUGW_WP.paraCombination("USERNAME_DOSSTONED", "PWDOSSTONED", PKUGW_WP.莫名其妙的字符串, PKUGW_WP.LoginType.free), 20);

            ha.Destination = "https://its.pku.edu.cn/netportal/ipgwcloseall";
            ha.ProcessHttp("sid=348");

        }
        CookieContainer _cookie = new CookieContainer();

     

        //private void ButtonDisconnectAll_Click(object sender, RoutedEventArgs e)
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/cas/login");
        //    request.CookieContainer = new CookieContainer();
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //        //("https://its.pku.edu.cn/netportal/ipgwcloseall?sid=465");
        //    request.BeginGetResponse(new AsyncCallback(ReadWebRequestCallback), request); 

        //}
        //// STEP4 STEP4 STEP4
        //private void ReadWebRequestCallback(IAsyncResult callbackResult)
        //{
        //    HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
        //    HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(callbackResult);

        //    using (StreamReader httpwebStreamReader = new StreamReader(myResponse.GetResponseStream()))
        //    {
        //        string results = httpwebStreamReader.ReadToEnd();
        //        //TextBlockResults.Text = results; //-- on another thread!
        //        Dispatcher.BeginInvoke(() => textBlock1.Text = results);
        //    }
        //    myResponse.Close();
        //}
    }
}