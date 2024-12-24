using System.Windows.Controls;
using System.Windows;
using WPF_Version.Pages.AuthenticationPages;

namespace WPF_Version.HomePage
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage(App.GetUserService(), App.GetContactService()));

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera till RegisterPage och skicka med UserService
            NavigationService.Navigate(new RegisterPage(App.GetUserService()));
        }

    }
}
