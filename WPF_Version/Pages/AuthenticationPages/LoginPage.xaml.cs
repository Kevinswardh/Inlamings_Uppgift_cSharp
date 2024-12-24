using Business.Logic._1_Services.UserService;
using Business.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Version.Pages.AuthenticationPages
{
    public partial class LoginPage : Page
    {
        private readonly UserService _userService;
        private readonly ContactService _contactService;

        public LoginPage(UserService userService, ContactService contactService)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out string email, out string password))
                return;

            // Kontrollera användare via UserService
            var user = _userService.ReadUserByEmail(email);
            if (user != null && user.ValidatePassword(password))
            {
                // Inloggning lyckades, öppna ContactWindow
                var contactWindow = new ContactWindow(user, _userService, _contactService);
                contactWindow.Show();

                // Stäng nuvarande fönster (om det är huvudfönstret)
                Window.GetWindow(this)?.Close();
            }
            else
            {
                // Visa felmeddelande om inloggning misslyckades
                MessageBox.Show("Ogiltig e-postadress eller lösenord.", "Inloggning misslyckades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs(out string email, out string password)
        {
            email = EmailTextBox.Text?.Trim().ToLower();
            password = PasswordBox.Password?.Trim();

            EmailErrorText.Visibility = Visibility.Collapsed;
            PasswordErrorText.Visibility = Visibility.Collapsed;

            bool isValid = true;

            // Validera e-post
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText.Text = "Ogiltig e-postadress. Kontrollera att den är korrekt.";
                EmailErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Validera lösenord
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordErrorText.Text = "Lösenord får inte vara tomt.";
                PasswordErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            return isValid;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage.HomePage());
        }
    }
}
