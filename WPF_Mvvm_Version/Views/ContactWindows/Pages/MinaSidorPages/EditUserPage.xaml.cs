using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.MinaSidorPages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages.MinaSidorPages
{
    public partial class EditUserPage : Page
    {
        public EditUserPage(BaseUser user, UserService userService)
        {
            InitializeComponent();
            DataContext = new EditUserPageViewModel(user, userService);
        }
    }
}
