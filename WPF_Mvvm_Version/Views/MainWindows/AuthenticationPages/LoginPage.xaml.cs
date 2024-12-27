using System.Windows.Controls;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.MainWindows.AuthenticationPages;

namespace WPF_Mvvm_Version.Views.MainWindows.AuthenticationPages
{
    public partial class LoginPage : Page
    {
        public LoginPage(UserService userService, ContactService contactService)
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel(userService, contactService);
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }
    }
}
