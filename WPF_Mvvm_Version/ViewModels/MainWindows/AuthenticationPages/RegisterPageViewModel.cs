using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WPF_Mvvm_Version.ViewModels.MainWindows.AuthenticationPages
{
    public class RegisterPageViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly NavigationService _navigationService;

        // Properties för UI-bindning
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value); // Uppdaterar värdet och skickar PropertyChanged
        }
        private string password;
        public string Role { get; set; }

        // Felmeddelanden
        public string NameErrorText { get; set; }
        public string LastnameErrorText { get; set; }
        public string EmailErrorText { get; set; }
        public string PasswordErrorText { get; set; }
        public string RoleErrorText { get; set; }

        // Kommandon
        public ICommand CreateCommand { get; }
        public ICommand BackCommand { get; }

        public RegisterPageViewModel(UserService userService, NavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            CreateCommand = new RelayCommand(OnCreateClicked);
            BackCommand = new RelayCommand(OnBackClicked);
        }


        private void OnCreateClicked()
        {
            if (!ValidateInputs())
            {
                return; // Avbryt om valideringen misslyckas
            }

            if (_userService.ReadUserByEmail(Email) != null)
            {
                MessageBox.Show("E-postadressen är redan registrerad. Vänligen ange en annan e-postadress.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _userService.CreateUser(Name, Lastname, Email, Password, Role);

          

            // Navigera tillbaka till HomePage
            if (_navigationService != null)
            {
                _navigationService.Navigate(new Views.HomePage.HomePage());
            }
            else
            {
                MessageBox.Show("NavigationService är null! Kontrollera att det har initierats korrekt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            NameErrorText = string.Empty;
            LastnameErrorText = string.Empty;
            EmailErrorText = string.Empty;
            PasswordErrorText = string.Empty;
            RoleErrorText = string.Empty;
            
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Name) || Name.Length > 50 || !Regex.IsMatch(Name, @"^[\p{L}\s'-]+$"))
            {
                NameErrorText = "Förnamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Lastname) || Lastname.Length > 50 || !Regex.IsMatch(Lastname, @"^[\p{L}\s'-]+$"))
            {
                LastnameErrorText = "Efternamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                EmailErrorText = "Ogiltig e-postadress. Kontrollera att den är korrekt.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                PasswordErrorText = "Lösenordet måste vara minst 6 tecken långt.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Role))
            {
                RoleErrorText = "Roll måste anges.";
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
