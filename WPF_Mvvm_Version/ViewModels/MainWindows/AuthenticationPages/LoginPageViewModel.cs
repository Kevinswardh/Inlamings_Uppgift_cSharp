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
    /// <summary>
    /// ViewModel för inloggningssidan som hanterar användarens inmatning, validering och navigering.
    /// </summary>
    public class LoginPageViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly ContactService _contactService;
        private readonly NavigationService _navigationService;

        /// <summary>
        /// E-post som användaren matar in.
        /// </summary>
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        /// <summary>
        /// Lösenord som användaren matar in.
        /// </summary>
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        /// <summary>
        /// Felmeddelande för e-post.
        /// </summary>
        private string _emailErrorText;
        public string EmailErrorText
        {
            get => _emailErrorText;
            set => SetProperty(ref _emailErrorText, value);
        }

        /// <summary>
        /// Felmeddelande för lösenord.
        /// </summary>
        private string _passwordErrorText;
        public string PasswordErrorText
        {
            get => _passwordErrorText;
            set => SetProperty(ref _passwordErrorText, value);
        }

        /// <summary>
        /// Synlighet för e-postfel.
        /// </summary>
        private Visibility _emailErrorVisibility = Visibility.Collapsed;
        public Visibility EmailErrorVisibility
        {
            get => _emailErrorVisibility;
            set => SetProperty(ref _emailErrorVisibility, value);
        }

        /// <summary>
        /// Synlighet för lösenordsfel.
        /// </summary>
        private Visibility _passwordErrorVisibility = Visibility.Collapsed;
        public Visibility PasswordErrorVisibility
        {
            get => _passwordErrorVisibility;
            set => SetProperty(ref _passwordErrorVisibility, value);
        }

        /// <summary>
        /// Kommando för inloggning.
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Kommando för att gå tillbaka till föregående sida.
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Konstruktor för LoginPageViewModel.
        /// </summary>
        /// <param name="userService">Tjänst för användarhantering.</param>
        /// <param name="contactService">Tjänst för kontakthantering.</param>
        /// <param name="navigationService">Navigeringstjänst.</param>
        public LoginPageViewModel(UserService userService, ContactService contactService, NavigationService navigationService)
        {
            _userService = userService;
            _contactService = contactService;
            _navigationService = navigationService;

            // Initiera kommandon
            LoginCommand = new RelayCommand(OnLoginClicked);
            BackCommand = new RelayCommand(OnBackClicked);
        }

        /// <summary>
        /// Hanterar inloggning när användaren klickar på "Login".
        /// </summary>
        private void OnLoginClicked()
        {
            if (!ValidateInputs())
                return;

            // Kontrollera om användaren finns och lösenordet är korrekt.
            var user = _userService.ReadUserByEmail(Email);
            if (user != null && user.ValidatePassword(Password))
            {
                // Öppna huvudfönstret och stäng nuvarande fönster.
                var contactWindow = new Views.ContactWindows.ContactWindow(user, _userService, _contactService);
                contactWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                // Visa felmeddelande om inloggning misslyckas.
                MessageBox.Show("Account doesn't exist.", "Login failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validerar användarens inmatning för e-post och lösenord.
        /// </summary>
        /// <returns>True om all inmatning är giltig, annars False.</returns>
        private bool ValidateInputs()
        {
            // Återställ felmeddelanden
            EmailErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;

            bool isValid = true;

            // Validera e-post
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

            // Validera lösenord
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordErrorText = "Please enter password.";
                PasswordErrorVisibility = Visibility.Visible;
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Navigerar tillbaka till föregående sida.
        /// </summary>
        private void OnBackClicked()
        {
            _navigationService?.GoBack();
        }
    }
}
