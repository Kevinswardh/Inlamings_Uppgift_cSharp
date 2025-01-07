using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WPF_Mvvm_Version.Views.MainWindows.AuthenticationPages;

namespace WPF_Mvvm_Version.ViewModels.MainWindows.HomePage
{
    /// <summary>
    /// ViewModel för HomePage som hanterar navigering till autentiseringssidor.
    /// </summary>
    public class HomePageViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly UserService _userService;

        /// <summary>
        /// Initierar en ny instans av HomePageViewModel.
        /// </summary>
        public HomePageViewModel()
        {
            // Hämtar NavigationService och UserService från App-klassen.
            _navigationService = App.GetNavigationService();
            _userService = App.GetUserService();

            // Initiera kommandon
            RegisterCommand = new RelayCommand(NavigateToRegisterPage);
            LoginCommand = new RelayCommand(NavigateToLoginPage);
        }

        /// <summary>
        /// Kommando för att navigera till registreringssidan.
        /// </summary>
        public ICommand RegisterCommand { get; }

        /// <summary>
        /// Kommando för att navigera till inloggningssidan.
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Navigerar till registreringssidan.
        /// </summary>
        private void NavigateToRegisterPage()
        {
            if (_navigationService != null)
            {
                // Skapar en instans av RegisterPage och navigerar dit.
                var registerPage = new RegisterPage(_userService, _navigationService);
                _navigationService.Navigate(registerPage);
            }
            else
            {
                // Visar ett felmeddelande om NavigationService inte är initierad.
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Navigerar till inloggningssidan.
        /// </summary>
        private void NavigateToLoginPage()
        {
            if (_navigationService != null)
            {
                // Skapar en instans av LoginPage och navigerar dit.
                var loginPage = new LoginPage(_userService, App.GetContactService(), _navigationService);
                _navigationService.Navigate(loginPage);
            }
            else
            {
                // Visar ett felmeddelande om NavigationService inte är initierad.
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
