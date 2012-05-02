using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using DialOnly.Class;
//using Microsoft.Phone.Tasks;

namespace DialOnly
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void buttonNum_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b == null) return;
            PageTitle.Text += b.Content as string;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            if (PageTitle.Text.Length < 1) return;
            PageTitle.Text = PageTitle.Text.Substring(0, PageTitle.Text.Length - 1);
        }

        private void buttonCall_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask pct = new PhoneCallTask();
            //pct.DisplayName = "Test";
            pct.PhoneNumber = PageTitle.Text;
            pct.Show();
            
        }

        private void buttonMe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks for use this dialer, made by DOSSTONED", "Hi~", MessageBoxButton.OK);
        }
    }
}