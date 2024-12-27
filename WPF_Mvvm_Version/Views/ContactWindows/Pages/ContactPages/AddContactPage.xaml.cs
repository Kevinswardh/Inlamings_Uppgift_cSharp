using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.ContactPages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages
{
    public partial class AddContactPage : Page
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;

        public AddContactPage(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            _contactService = contactService;

            // Sätt datakontroll
            DataContext = new AddContactPageViewModel(user, userService, contactService);
        }
    }
}
