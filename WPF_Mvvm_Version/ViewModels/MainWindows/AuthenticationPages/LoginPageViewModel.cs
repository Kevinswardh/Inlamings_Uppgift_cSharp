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

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _emailErrorText;
        public string EmailErrorText
        {
            get => _emailErrorText;
            set => SetProperty(ref _emailErrorText, value);
        }

        private string _passwordErrorText;
        public string PasswordErrorText
        {
            get => _passwordErrorText;
            set => SetProperty(ref _passwordErrorText, value);
        }

        private Visibility _emailErrorVisibility = Visibility.Collapsed;
        public Visibility EmailErrorVisibility
        {
            get => _emailErrorVisibility;
            set => SetProperty(ref _emailErrorVisibility, value);
        }

        private Visibility _passwordErrorVisibility = Visibility.Collapsed;
        public Visibility PasswordErrorVisibility
        {
            get => _passwordErrorVisibility;
            set => SetProperty(ref _passwordErrorVisibility, value);
        }

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
                MessageBox.Show("Account doesn't exist.", "Login failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            EmailErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailErrorText = "Please enter email.";
                EmailErrorVisibility = Visibility.Visible;
                isValid = false;
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText = "Email måste vara korrekt format.";
                EmailErrorVisibility = Visibility.Visible;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordErrorText = "Please enter password.";
                PasswordErrorVisibility = Visibility.Visible;
                isValid = false;
            }

            return isValid;
        }

        private void OnBackClicked()
        {
            _navigationService?.GoBack();
        }
    }
}
