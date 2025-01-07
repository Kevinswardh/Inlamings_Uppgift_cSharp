using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows
{
    /// <summary>
    /// ViewModel för ContactWindow, ansvarar för navigering och hantering av kommandon.
    /// </summary>
    public class ContactWindowViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;
        private readonly Frame _contentFrame;

        /// <summary>
        /// Konstruktor för att initiera ViewModel med användare, tjänster och navigeringsram.
        /// </summary>
        /// <param name="user">Inloggad användare.</param>
        /// <param name="userService">Tjänst för användarhantering.</param>
        /// <param name="contactService">Tjänst för kontakthantering.</param>
        /// <param name="contentFrame">Navigeringsram för sidor.</param>
        public ContactWindowViewModel(BaseUser user, UserService userService, ContactService contactService, Frame contentFrame)
        {
            _user = user;
            _userService = userService;
            _contactService = contactService;
            _contentFrame = contentFrame;

            // Initiera kommandon
            NavigateToFavoritesCommand = new RelayCommand(NavigateToFavorites);
            NavigateToContactsCommand = new RelayCommand(NavigateToContacts);
            NavigateToMyPagesCommand = new RelayCommand(NavigateToMyPages);
            LogoutCommand = new RelayCommand(Logout);
            CloseCommand = new RelayCommand(CloseApp);

            // Standardvy vid start
            NavigateToContacts();
        }

        // Kommandon för navigering och applikationskontroll
        public IRelayCommand NavigateToFavoritesCommand { get; }
        public IRelayCommand NavigateToContactsCommand { get; }
        public IRelayCommand NavigateToMyPagesCommand { get; }
        public IRelayCommand LogoutCommand { get; }
        public IRelayCommand CloseCommand { get; }

        /// <summary>
        /// Navigerar till FavoritesPage.
        /// </summary>
        private void NavigateToFavorites()
        {
            if (_user == null || _contactService == null)
            {
                MessageBox.Show("User or ContactService is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Navigera till FavoritesPage med användare och kontaktservice
            _contentFrame.Navigate(new Views.ContactWindows.Pages.FavoritesPage(_user, _contactService));
        }

        /// <summary>
        /// Navigerar till ContactsPage.
        /// </summary>
        private void NavigateToContacts()
        {
            // Navigera till ContactsPage med användare, användartjänst och kontaktservice
            _contentFrame.Navigate(new Views.ContactWindows.Pages.ContactsPage(_user, _userService, _contactService));
        }

        /// <summary>
        /// Navigerar till MinaSidorPage.
        /// </summary>
        private void NavigateToMyPages()
        {
            // Navigera till MinaSidorPage med användare och användartjänst
            _contentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPage(_user, _userService));
        }

        /// <summary>
        /// Loggar ut användaren och navigerar tillbaka till MainWindow.
        /// </summary>
        private void Logout()
        {
            // Skapa ny instans av MainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Stäng nuvarande ContactWindow
            Application.Current.Windows[0]?.Close();
        }

        /// <summary>
        /// Stänger hela applikationen.
        /// </summary>
        private void CloseApp()
        {
            Environment.Exit(0);
        }
    }
}
