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
    public class FavoritesPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;

        public FavoritesPageViewModel(BaseUser user, ContactService contactService)
        {
            _user = user;
            _contactService = contactService;

            // Initialize the Favorites collection with visibility state
            Favorites = new ObservableCollection<FavoriteContactWithVisibility>(
                _contactService.GetAllFavorites(_user.Id)
                .ConvertAll(favorite => new FavoriteContactWithVisibility(favorite))
            );

            // Initialize commands
            AddFavoriteCommand = new RelayCommand(AddFavorite);
            EditFavoriteCommand = new RelayCommand<FavoriteContactWithVisibility>(EditFavorite);
            DeleteFavoriteCommand = new RelayCommand<FavoriteContactWithVisibility>(DeleteFavorite);
            ToggleMoreInfoCommand = new RelayCommand<FavoriteContactWithVisibility>(ToggleMoreInfo);
        }

        // Collection of favorites with visibility state
        public ObservableCollection<FavoriteContactWithVisibility> Favorites { get; }

        // Commands for interacting with the view
        public ICommand AddFavoriteCommand { get; }
        public ICommand EditFavoriteCommand { get; }
        public ICommand DeleteFavoriteCommand { get; }
        public ICommand ToggleMoreInfoCommand { get; }

        private void AddFavorite()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new AddFavoritePage(_user, _contactService));
        }

        private void EditFavorite(FavoriteContactWithVisibility favorite)
        {
            if (favorite == null) return;

            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new EditFavoritePage(favorite.Favorite, _user, _contactService));
        }


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
                Favorites.Remove(favorite);
                _contactService.RemoveFavorite(_user.Id, favorite.Favorite.Id);
                MessageBox.Show("Favorite deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ToggleMoreInfo(FavoriteContactWithVisibility favorite)
        {
            if (favorite == null) return;

            favorite.IsMoreInfoVisible = !favorite.IsMoreInfoVisible;
        }
    }

    // Wrapper class for favorite contacts with visibility state
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
                OnPropertyChanged(nameof(ToggleButtonText)); // Notify that ToggleButtonText has changed
            }
        }

        public string ToggleButtonText => IsMoreInfoVisible ? "Show Less" : "Show More";

        public FavoriteContactWithVisibility(FavoriteContact favorite)
        {
            Favorite = favorite;
            _isMoreInfoVisible = false; // Default to collapsed
        }
    }

}
