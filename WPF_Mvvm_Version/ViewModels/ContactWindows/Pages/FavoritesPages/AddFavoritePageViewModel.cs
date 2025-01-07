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
    /// <summary>
    /// ViewModel för att hantera logiken bakom sidan för att lägga till en ny favoritkontakt (AddFavoritePage).
    /// </summary>
    public class AddFavoritePageViewModel : ObservableObject
    {
        private readonly BaseUser _user; // Den aktuella användaren
        private readonly ContactService _contactService; // Tjänst för att hantera kontakter

        /// <summary>
        /// Initierar ViewModel med användare och ContactService.
        /// </summary>
        /// <param name="user">Den aktuella användaren.</param>
        /// <param name="contactService">Tjänst för kontaktoperationer.</param>
        /// <exception cref="ArgumentNullException">Kastas om user eller contactService är null.</exception>
        public AddFavoritePageViewModel(BaseUser user, ContactService contactService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));

            // Initiera kommandon
            SaveCommand = new RelayCommand(SaveFavorite);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindbara egenskaper kopplade till UI
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        // Kommandon kopplade till knappar
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }

        /// <summary>
        /// Sparar en ny favoritkontakt och navigerar tillbaka till FavoritesPage.
        /// </summary>
        private void SaveFavorite()
        {
            // Skapa en ny favoritkontakt med inmatade värden
            var favoriteContact = new FavoriteContact
            {
                Name = Name,
                Lastname = Lastname,
                PhoneNumber = PhoneNumber,
                Address = Address,
                Email = Email,
                Notes = Notes,
                Id = GuidGenerator.GenerateGuid() // Genererar unikt ID
            };

            // Lägg till favoriten i ContactService
            _contactService.AddFavorite(_user.Id, favoriteContact);

            // Visa bekräftelsemeddelande
            MessageBox.Show("Favorite added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till FavoritesPage
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }

        /// <summary>
        /// Avbryter operationen och navigerar tillbaka till FavoritesPage utan att spara.
        /// </summary>
        private void Cancel()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }
    }
}
