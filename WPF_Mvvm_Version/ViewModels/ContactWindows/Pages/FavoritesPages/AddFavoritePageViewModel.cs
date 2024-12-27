using Business.CoreFiles.Helpers;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using WPF_Mvvm_Version.Views.ContactWindows;
using WPF_Mvvm_Version.Views.ContactWindows.Pages;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.FavoritesPages
{
    public class AddFavoritePageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;

        public AddFavoritePageViewModel(BaseUser user, ContactService contactService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));

            SaveCommand = new RelayCommand(SaveFavorite);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindable properties for inputs
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private void SaveFavorite()
        {
            var favoriteContact = new FavoriteContact
            {
                Name = Name,
                Lastname = Lastname,
                PhoneNumber = PhoneNumber,
                Address = Address,
                Email = Email,
                Notes = Notes,
                Id = GuidGenerator.GenerateGuid()
            };

            _contactService.AddFavorite(_user.Id, favoriteContact);

            MessageBox.Show("Favorite added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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
