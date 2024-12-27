using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.MainWindows.AuthenticationPages;

namespace WPF_Mvvm_Version.Views.MainWindows.AuthenticationPages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage(UserService userService, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RegisterPageViewModel(userService, navigationService);
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterPageViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }

    }



}
