using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;
using WPF_Mvvm_Version.Views.ContactWindows.Pages;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.FavoritesPages
{
    public class EditFavoritePageViewModel : ObservableObject
    {
        private readonly ContactService _contactService;
        private readonly BaseUser _user;
        private readonly FavoriteContact _favorite;

        public EditFavoritePageViewModel(FavoriteContact favorite, BaseUser user, ContactService contactService)
        {
            _favorite = favorite;
            _user = user;
            _contactService = contactService;

            // Initialize fields
            Name = favorite.Name;
            Lastname = favorite.Lastname;
            PhoneNumber = favorite.PhoneNumber;
            Address = favorite.Address;
            Email = favorite.Email;
            Notes = favorite.Notes;

            // Initialize commands
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Properties bound to the UI
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private void Save()
        {
            // Update favorite object
            _favorite.Name = Name;
            _favorite.Lastname = Lastname;
            _favorite.PhoneNumber = PhoneNumber;
            _favorite.Address = Address;
            _favorite.Email = Email;
            _favorite.Notes = Notes;

            // Update favorite in the ContactService
            _contactService.UpdateFavorite(_user.Id, _favorite);

            // Confirmation message
            MessageBox.Show("Favorite updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigate back to FavoritesPage
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }

        private void Cancel()
        {
            // Navigate back to FavoritesPage without saving
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }
    }
}
