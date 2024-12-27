using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using System.Windows;
using System.Windows.Navigation; // För Frame-navigation
using WPF_Version.ContactWindows.Pages;

namespace WPF_Version
{
    public partial class ContactWindow : Window
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;

        public ContactWindow(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            _contactService = contactService;

            // Sätt fönstrets titel
            Title = $"Welcome, {_user.Fullname}";

            // Navigera till ContactsPage som standardvy
            ContentFrame.Navigate(new ContactsPage(_user, _userService, _contactService));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Gå tillbaka till inloggningsfönstret
            var loginWindow = new MainWindow(); // Förutsatt att MainWindow hanterar login
            loginWindow.Show();

            // Stäng nuvarande fönster
            this.Close();
        }

        private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void NavigateToFavorites(object sender, RoutedEventArgs e)
        {
            // Navigera till FavoritesPage med användardata och tjänster
            ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }


        private void NavigateToContacts(object sender, RoutedEventArgs e)
        {
            // Navigera till ContactsPage
            ContentFrame.Navigate(new ContactsPage(_user, _userService, _contactService));
        }

        private void NavigateToMyPages(object sender, RoutedEventArgs e)
        {
            // Navigera till MinaSidorPage
            ContentFrame.Navigate(new MinaSidorPage(_user, _userService));
        }
    }
}
