using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using System.Windows.Controls;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.FavoritesPages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages
{
    public partial class AddFavoritePage : Page
    {
        public AddFavoritePage(BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            DataContext = new AddFavoritePageViewModel(user, contactService);
        }
    }
}
