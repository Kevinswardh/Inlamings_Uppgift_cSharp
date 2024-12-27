using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages
{
    public partial class MinaSidorPage : Page
    {
        public MinaSidorPage(BaseUser user, UserService userService)
        {
            InitializeComponent();
            // Sätt ViewModel som DataContext
            DataContext = new MinaSidorPageViewModel(user, userService);
        }
    }
}
