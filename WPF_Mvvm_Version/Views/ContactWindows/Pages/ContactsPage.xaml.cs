using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages
{
    public partial class ContactsPage : Page
    {
        public ContactsPage(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();

            // Set DataContext to ViewModel
            DataContext = new ContactsPageViewModel(user, userService, contactService);
        }
    }
}
