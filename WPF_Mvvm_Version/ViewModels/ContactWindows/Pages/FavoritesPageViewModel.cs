using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;
using WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages;
using WPF_Mvvm_Version.Views.ContactWindows.Pages.FavoritesPages;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages
{
    /// <summary>
    /// ViewModel för FavoritesPage. Hanterar visning och interaktion med favoriter.
    /// </summary>
    public class FavoritesPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;

        /// <summary>
        /// Initierar ViewModel för FavoritesPage.
        /// </summary>
        /// <param name="user">Inloggad användare.</param>
        /// <param name="contactService">Tjänst för kontakthantering.</param>
        public FavoritesPageViewModel(BaseUser user, ContactService contactService)
        {
            _user = user;
            _contactService = contactService;

            // Initierar listan av favoriter med deras synlighetstillstånd
            Favorites = new ObservableCollection<FavoriteContactWithVisibility>(
                _contactService.GetAllFavorites(_user.Id)
                .ConvertAll(favorite => new FavoriteContactWithVisibility(favorite))
            );

            // Initiera kommandon
            AddFavoriteCommand = new RelayCommand(AddFavorite);
            EditFavoriteCommand = new RelayCommand<FavoriteContactWithVisibility>(EditFavorite);
            DeleteFavoriteCommand = new RelayCommand<FavoriteContactWithVisibility>(DeleteFavorite);
            ToggleMoreInfoCommand = new RelayCommand<FavoriteContactWithVisibility>(ToggleMoreInfo);
        }

        /// <summary>
        /// Lista över favoriter med synlighetstillstånd.
        /// </summary>
        public ObservableCollection<FavoriteContactWithVisibility> Favorites { get; }

        /// <summary>
        /// Kommandon för att hantera favoriter.
        /// </summary>
        public ICommand AddFavoriteCommand { get; }
        public ICommand EditFavoriteCommand { get; }
        public ICommand DeleteFavoriteCommand { get; }
        public ICommand ToggleMoreInfoCommand { get; }

        /// <summary>
        /// Navigerar till sidan för att lägga till en ny favorit.
        /// </summary>
        private void AddFavorite()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new AddFavoritePage(_user, _contactService));
        }

        /// <summary>
        /// Navigerar till sidan för att redigera en favorit.
        /// </summary>
        /// <param name="favorite">Favorit som ska redigeras.</param>
        private void EditFavorite(FavoriteContactWithVisibility favorite)
        {
            if (favorite == null) return;

            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new EditFavoritePage(favorite.Favorite, _user, _contactService));
        }

        /// <summary>
        /// Tar bort en favorit efter att användaren har bekräftat.
        /// </summary>
        /// <param name="favorite">Favorit som ska tas bort.</param>
        private void DeleteFavorite(FavoriteContactWithVisibility favorite)
        {
            if (favorite == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete {favorite.Favorite.Fullname}?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Favorites.Remove(favorite); // Tar bort favoriten från listan
                _contactService.RemoveFavorite(_user.Id, favorite.Favorite.Id); // Tar bort favoriten från tjänsten
                MessageBox.Show("Favorite deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Växlar visningen av ytterligare information om en favorit.
        /// </summary>
        /// <param name="favorite">Favorit vars information ska växlas.</param>
        private void ToggleMoreInfo(FavoriteContactWithVisibility favorite)
        {
            if (favorite == null) return;

            favorite.IsMoreInfoVisible = !favorite.IsMoreInfoVisible; // Växla synligheten
        }
    }

    /// <summary>
    /// Wrapper-klass för favoritkontakter som inkluderar synlighetstillstånd.
    /// </summary>
    public class FavoriteContactWithVisibility : ObservableObject
    {
        public FavoriteContact Favorite { get; }

        private bool _isMoreInfoVisible;
        public bool IsMoreInfoVisible
        {
            get => _isMoreInfoVisible;
            set
            {
                SetProperty(ref _isMoreInfoVisible, value);
                OnPropertyChanged(nameof(ToggleButtonText)); // Informera att ToggleButtonText har ändrats
            }
        }

        /// <summary>
        /// Text för knappen som växlar visningen av ytterligare information.
        /// </summary>
        public string ToggleButtonText => IsMoreInfoVisible ? "Show Less" : "Show More";

        /// <summary>
        /// Konstruktor för att initiera en favorit med standardvärden.
        /// </summary>
        /// <param name="favorite">Favoritkontakt.</param>
        public FavoriteContactWithVisibility(FavoriteContact favorite)
        {
            Favorite = favorite;
            _isMoreInfoVisible = false; // Standardvärde är dold information
        }
    }
}
