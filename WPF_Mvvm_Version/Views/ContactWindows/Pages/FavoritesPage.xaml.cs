using System.Windows.Controls;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages;
using Business.CoreFiles.Models.Users;
using Business.Services;
using Business.Logic._1_Services.UserService;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages
{
    public partial class FavoritesPage : Page
    {
        public FavoritesPage(BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            DataContext = new FavoritesPageViewModel(user, contactService);
        }

    }
}
