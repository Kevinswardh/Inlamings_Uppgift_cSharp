using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WPF_Mvvm_Version.Views.MainWindows.AuthenticationPages;

namespace WPF_Mvvm_Version.ViewModels.MainWindows.HomePage
{
    public class HomePageViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly UserService _userService;

        public HomePageViewModel()
        {
            _navigationService = App.GetNavigationService();
            _userService = App.GetUserService();

            RegisterCommand = new RelayCommand(NavigateToRegisterPage);
            LoginCommand = new RelayCommand(NavigateToLoginPage); // Add LoginCommand
        }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; } // Add LoginCommand property

        private void NavigateToRegisterPage()
        {
            if (_navigationService != null)
            {
                var registerPage = new RegisterPage(_userService, _navigationService);
                _navigationService.Navigate(registerPage);
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateToLoginPage()
        {
            if (_navigationService != null)
            {
                var loginPage = new LoginPage(_userService, App.GetContactService());
                _navigationService.Navigate(loginPage);
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
