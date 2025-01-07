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
    /// <summary>
    /// ViewModel för redigering av favoritkontakter i EditFavoritePage.
    /// </summary>
    public class EditFavoritePageViewModel : ObservableObject
    {
        private readonly ContactService _contactService; // Hanterar kontaktoperationer.
        private readonly BaseUser _user; // Representerar den aktuella användaren.
        private readonly FavoriteContact _favorite; // Representerar den valda favoriten.

        /// <summary>
        /// Initierar ViewModel med en favorit, användare och ContactService.
        /// </summary>
        /// <param name="favorite">Favoritkontakt som ska redigeras.</param>
        /// <param name="user">Den aktuella användaren.</param>
        /// <param name="contactService">Tjänst för hantering av kontakter.</param>
        public EditFavoritePageViewModel(FavoriteContact favorite, BaseUser user, ContactService contactService)
        {
            _favorite = favorite ?? throw new ArgumentNullException(nameof(favorite));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));

            // Initiera egenskaper med favoritens nuvarande data
            Name = favorite.Name;
            Lastname = favorite.Lastname;
            PhoneNumber = favorite.PhoneNumber;
            Address = favorite.Address;
            Email = favorite.Email;
            Notes = favorite.Notes;

            // Initiera kommandon
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindbara egenskaper kopplade till UI
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        // Kommandon
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Sparar ändringarna till favoritkontakten och navigerar tillbaka till FavoritesPage.
        /// </summary>
        private void Save()
        {
            // Uppdatera favoritens data
            _favorite.Name = Name;
            _favorite.Lastname = Lastname;
            _favorite.PhoneNumber = PhoneNumber;
            _favorite.Address = Address;
            _favorite.Email = Email;
            _favorite.Notes = Notes;

            // Uppdatera favoritkontakten via ContactService
            _contactService.UpdateFavorite(_user.Id, _favorite);

            // Visa bekräftelsemeddelande
            MessageBox.Show("Favorite updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till FavoritesPage
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }

        /// <summary>
        /// Avbryter redigeringen och navigerar tillbaka till FavoritesPage utan att spara.
        /// </summary>
        private void Cancel()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new FavoritesPage(_user, _contactService));
        }
    }
}
