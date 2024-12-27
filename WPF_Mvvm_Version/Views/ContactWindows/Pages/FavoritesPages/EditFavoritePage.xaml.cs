using System.Windows.Controls;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.FavoritesPages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages.FavoritesPages
{
    public partial class EditFavoritePage : Page
    {
        public EditFavoritePage(FavoriteContact favorite, BaseUser user, ContactService contactService)
        {
            InitializeComponent();

            // Set the DataContext to the ViewModel
            DataContext = new EditFavoritePageViewModel(favorite, user, contactService);
        }
    }
}
