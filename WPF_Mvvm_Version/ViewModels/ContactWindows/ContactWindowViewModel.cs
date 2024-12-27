using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows
{
    public class ContactWindowViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;
        private readonly Frame _contentFrame;

        public ContactWindowViewModel(BaseUser user, UserService userService, ContactService contactService, Frame contentFrame)
        {
            _user = user;
            _userService = userService;
            _contactService = contactService;
            _contentFrame = contentFrame;

            NavigateToFavoritesCommand = new RelayCommand(NavigateToFavorites);
            NavigateToContactsCommand = new RelayCommand(NavigateToContacts);
            NavigateToMyPagesCommand = new RelayCommand(NavigateToMyPages);
            LogoutCommand = new RelayCommand(Logout);
            CloseCommand = new RelayCommand(CloseApp);

            // Set default view
            NavigateToContacts();
        }

        public IRelayCommand NavigateToFavoritesCommand { get; }
        public IRelayCommand NavigateToContactsCommand { get; }
        public IRelayCommand NavigateToMyPagesCommand { get; }
        public IRelayCommand LogoutCommand { get; }
        public IRelayCommand CloseCommand { get; }

        private void NavigateToFavorites()
        {
            if (_user == null || _contactService == null)
            {
                MessageBox.Show("User or ContactService is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _contentFrame.Navigate(new Views.ContactWindows.Pages.FavoritesPage(_user, _contactService));
        }


        private void NavigateToContacts()
        {
            _contentFrame.Navigate(new Views.ContactWindows.Pages.ContactsPage(_user, _userService, _contactService));
        }

        private void NavigateToMyPages()
        {
            _contentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPage(_user, _userService));
        }

        private void Logout()
        {
            // Go back to MainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Close ContactWindow
            Application.Current.Windows[0]?.Close();
        }

        private void CloseApp()
        {
            // Exit the entire application
            System.Environment.Exit(0);
        }
    }
}
