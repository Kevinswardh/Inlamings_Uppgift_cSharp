using Business.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Version.Pages.AuthenticationPages
{
    public partial class RegisterPage : Page
    {
        private readonly UserService _userService;
        public RegisterPage(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Validera fälten
            if (!ValidateInputs(out string name, out string lastname, out string email, out string password, out string role))
            {
                return; // Avbryt om valideringen misslyckas
            }

            // Kontrollera om e-post redan existerar
            if (_userService.ReadUserByEmail(email) != null)
            {
                MessageBox.Show("E-postadressen är redan registrerad. Vänligen ange en annan e-postadress.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Skapa användaren
            _userService.CreateUser(name, lastname, email, password, role);


            // Navigera tillbaka till HomePage
            NavigationService.Navigate(new HomePage.HomePage());
        }
        private bool ValidateInputs(out string name, out string lastname, out string email, out string password, out string role)
        {
            name = NameTextBox.Text?.Trim();
            lastname = LastnameTextBox.Text?.Trim();
            email = EmailTextBox.Text?.Trim();
            password = PasswordBox.Password?.Trim();
            role = RoleComboBox.Text?.Trim();

            bool isValid = true;

            // Återställ felmeddelanden
            NameErrorText.Visibility = Visibility.Collapsed;
            LastnameErrorText.Visibility = Visibility.Collapsed;
            EmailErrorText.Visibility = Visibility.Collapsed;
            PasswordErrorText.Visibility = Visibility.Collapsed;
            RoleErrorText.Visibility = Visibility.Collapsed;

            // Validera namn
            if (string.IsNullOrWhiteSpace(name) || name.Length > 50 || !Regex.IsMatch(name, @"^[\p{L}\s'-]+$"))
            {
                NameErrorText.Text = "Förnamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                NameErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Validera efternamn
            if (string.IsNullOrWhiteSpace(lastname) || lastname.Length > 50 || !Regex.IsMatch(lastname, @"^[\p{L}\s'-]+$"))
            {
                LastnameErrorText.Text = "Efternamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                LastnameErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Validera e-post
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText.Text = "Ogiltig e-postadress. Kontrollera att den är korrekt.";
                EmailErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Validera lösenord
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                PasswordErrorText.Text = "Lösenordet måste vara minst 6 tecken långt.";
                PasswordErrorText.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Validera roll
            role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(); // Hämta valt värde från ComboBox
            if (string.IsNullOrWhiteSpace(role))
            {
                RoleErrorText.Text = "Roll måste anges.";
                RoleErrorText.Visibility = Visibility.Visible;
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
