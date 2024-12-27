using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Version.ContactWindows.Pages.ContactPages;
using WPF_Version.ContactWindows.Pages.FavoritesPages;

namespace WPF_Version.ContactWindows.Pages
{
    public partial class FavoritesPage : Page
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;
        public ObservableCollection<FavoriteContact> Favorites { get; set; }

        public FavoritesPage(BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _contactService = contactService;

            // Ladda favoriter
            if (Favorites == null)
            {
                Favorites = new ObservableCollection<FavoriteContact>(_contactService.GetAllFavorites(_user.Id));
                DataContext = this; // Bind DataContext till sidan
            }
        }
        private void AddFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new AddFavoritePage(_user, _contactService));
        }

        private void EditFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var favorite = button?.CommandParameter as FavoriteContact;

            if (favorite != null)
            {
                // Navigera till EditFavoritePage
                var contactWindow = Window.GetWindow(this) as ContactWindow;
                contactWindow?.ContentFrame.Navigate(new EditFavoritePage(favorite, _user, _contactService));
            }
        }

        private void ShowMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parentStackPanel = VisualTreeHelper.GetParent(button) as StackPanel;

            if (parentStackPanel != null)
            {
                // Hitta MoreInfoPanel i samma StackPanel
                var moreInfoPanel = parentStackPanel.Children
                    .OfType<StackPanel>()
                    .FirstOrDefault(child => child.Name == "MoreInfoPanel");

                if (moreInfoPanel != null)
                {
                    if (moreInfoPanel.Visibility == Visibility.Collapsed)
                    {
                        moreInfoPanel.Visibility = Visibility.Visible;
                        button.Content = "Show Less";
                    }
                    else
                    {
                        moreInfoPanel.Visibility = Visibility.Collapsed;
                        button.Content = "Show More";
                    }
                }
            }
        }

        private void DeleteFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var favorite = button?.CommandParameter as FavoriteContact;

            if (favorite != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {favorite.Fullname}?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Favorites.Remove(favorite);
                    _contactService.RemoveFavorite(_user.Id, favorite.Id);
                    MessageBox.Show("Favorite deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
