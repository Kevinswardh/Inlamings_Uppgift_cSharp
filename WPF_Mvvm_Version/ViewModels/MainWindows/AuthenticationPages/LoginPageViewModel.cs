using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WPF_Mvvm_Version.ViewModels.MainWindows.AuthenticationPages
{
    public class LoginPageViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly ContactService _contactService;
        private readonly NavigationService _navigationService;

        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailErrorText { get; set; }
        public string PasswordErrorText { get; set; }
        public Visibility EmailErrorVisibility { get; set; } = Visibility.Collapsed;
        public Visibility PasswordErrorVisibility { get; set; } = Visibility.Collapsed;

        public ICommand LoginCommand { get; }
        public ICommand BackCommand { get; }

        public LoginPageViewModel(UserService userService, ContactService contactService, NavigationService navigationService)
        {
            _userService = userService;
            _contactService = contactService;
            _navigationService = navigationService;

            LoginCommand = new RelayCommand(OnLoginClicked);
            BackCommand = new RelayCommand(OnBackClicked);
        }

        private void OnLoginClicked()
        {
            if (!ValidateInputs())
                return;

            var user = _userService.ReadUserByEmail(Email);
            if (user != null && user.ValidatePassword(Password))
            {
                var contactWindow = new Views.ContactWindows.ContactWindow(user, _userService, _contactService);
                contactWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Ogiltig e-postadress eller lösenord.", "Inloggning misslyckades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            EmailErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText = "Ogiltig e-postadress. Kontrollera att den är korrekt.";
                EmailErrorVisibility = Visibility.Visible;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordErrorText = "Lösenord får inte vara tomt.";
                PasswordErrorVisibility = Visibility.Visible;
                isValid = false;
            }

            return isValid;
        }

        private void OnBackClicked()
        {
            if (_navigationService != null)
            {
                _navigationService.GoBack();
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
