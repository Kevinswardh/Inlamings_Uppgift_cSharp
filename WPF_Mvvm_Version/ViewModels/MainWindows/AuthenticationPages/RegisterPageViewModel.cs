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
    /// ViewModel för registreringssidan som hanterar användarens inmatning och validering.
    /// </summary>
    public class RegisterPageViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly NavigationService _navigationService;

        // Bindings för UI
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        // Property för lösenord med notifiering
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string password;

        public string Role { get; set; }

        // Felmeddelanden för validering
        public string NameErrorText { get; set; }
        public string LastnameErrorText { get; set; }
        public string EmailErrorText { get; set; }
        public string PasswordErrorText { get; set; }
        public string RoleErrorText { get; set; }

        // Kommandon för användarinteraktion
        public ICommand CreateCommand { get; }
        public ICommand BackCommand { get; }

        /// <summary>
        /// Konstruktor för att initiera tjänster och kommandon.
        /// </summary>
        public RegisterPageViewModel(UserService userService, NavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            // Initiera kommandon
            CreateCommand = new RelayCommand(OnCreateClicked);
            BackCommand = new RelayCommand(OnBackClicked);
        }

        /// <summary>
        /// Metod som körs när användaren klickar på "Skapa".
        /// Validerar inmatning och skapar en ny användare om allt är korrekt.
        /// </summary>
        private void OnCreateClicked()
        {
            if (!ValidateInputs())
            {
                return; // Avbryt om valideringen misslyckas
            }

            // Kontrollera om e-postadressen redan finns
            if (_userService.ReadUserByEmail(Email) != null)
            {
                MessageBox.Show("E-postadressen är redan registrerad. Vänligen ange en annan e-postadress.",
                                "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Skapa användare
            _userService.CreateUser(Name, Lastname, Email, Password, Role);

            // Navigera tillbaka till startsidan
            if (_navigationService != null)
            {
                _navigationService.Navigate(new Views.HomePage.HomePage());
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.",
                                "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validerar användarens inmatning och sätter felmeddelanden om något är ogiltigt.
        /// </summary>
        /// <returns>True om alla inmatningar är giltiga, annars False.</returns>
        private bool ValidateInputs()
        {
            // Återställ felmeddelanden
            NameErrorText = string.Empty;
            LastnameErrorText = string.Empty;
            EmailErrorText = string.Empty;
            PasswordErrorText = string.Empty;
            RoleErrorText = string.Empty;

            bool isValid = true;

            // Validera namn
            if (string.IsNullOrWhiteSpace(Name) || Name.Length > 50 || !Regex.IsMatch(Name, @"^[\p{L}\s'-]+$"))
            {
                NameErrorText = "Förnamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                isValid = false;
            }

            // Validera efternamn
            if (string.IsNullOrWhiteSpace(Lastname) || Lastname.Length > 50 || !Regex.IsMatch(Lastname, @"^[\p{L}\s'-]+$"))
            {
                LastnameErrorText = "Efternamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                isValid = false;
            }

            // Validera e-post
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText = "Ogiltig e-postadress. Kontrollera att den är korrekt.";
                isValid = false;
            }

            // Validera lösenord
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                PasswordErrorText = "Lösenordet måste vara minst 6 tecken långt.";
                isValid = false;
            }

            // Validera roll
            if (string.IsNullOrWhiteSpace(Role))
            {
                RoleErrorText = "Roll måste anges.";
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Navigerar tillbaka till föregående sida.
        /// </summary>
        private void OnBackClicked()
        {
            if (_navigationService != null)
            {
                _navigationService.GoBack();
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.",
                                "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
