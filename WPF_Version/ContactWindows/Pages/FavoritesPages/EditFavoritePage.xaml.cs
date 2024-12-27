using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;

namespace WPF_Version.ContactWindows.Pages.FavoritesPages
{
    public partial class EditFavoritePage : Page
    {
        private readonly FavoriteContact _favorite;
        private readonly ContactService _contactService;
        private readonly BaseUser _user;

        public EditFavoritePage(FavoriteContact favorite, BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            _favorite = favorite;
            _contactService = contactService;
            _user = user;

            // Fyll i formulärfälten med favoritens nuvarande information
            NameTextBox.Text = _favorite.Name;
            LastNameTextBox.Text = _favorite.Lastname;
            PhoneNumberTextBox.Text = _favorite.PhoneNumber;
            AddressTextBox.Text = _favorite.Address;
            EmailTextBox.Text = _favorite.Email;
            NotesTextBox.Text = _favorite.Notes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Uppdatera favoritobjektet
            _favorite.Name = NameTextBox.Text;
            _favorite.Lastname = LastNameTextBox.Text;
            _favorite.PhoneNumber = PhoneNumberTextBox.Text;
            _favorite.Address = AddressTextBox.Text;
            _favorite.Email = EmailTextBox.Text;
            _favorite.Notes = NotesTextBox.Text;

            // Uppdatera favorit i ContactService
            _contactService.UpdateFavorite(_user.Id, _favorite);

            // Bekräftelsemeddelande
            MessageBox.Show("Favorite updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

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
