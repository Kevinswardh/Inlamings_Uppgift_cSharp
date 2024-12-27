using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Helpers;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;

namespace WPF_Version.ContactWindows.Pages.ContactPages
{
    public partial class AddFavoritePage : Page
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;

        public AddFavoritePage(BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _contactService = contactService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Skapa en ny favoritkontakt baserat på input
            var favoriteContact = new FavoriteContact
            {
                Name = NameTextBox.Text,
                Lastname = LastnameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Address = AddressTextBox.Text,
                Email = EmailTextBox.Text,
                Notes = NotesTextBox.Text,
                Id = GuidGenerator.GenerateGuid()
            };

            // Lägg till favoriten i användarens favoritlista och spara via ContactService
            _contactService.AddFavorite(_user.Id, favoriteContact);

            MessageBox.Show("Favorite added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till FavoritesPage
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera tillbaka till FavoritesPage utan att spara
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }
    }
}
