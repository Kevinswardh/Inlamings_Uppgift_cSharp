using System.Windows.Controls;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.ContactPages;

namespace WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages
{
    public partial class EditContactPage : Page
    {
        public EditContactPage(Contact contact, BaseUser user, ContactService contactService)
        {
            InitializeComponent();

            // Sätt DataContext till en instans av EditContactPageViewModel
            DataContext = new EditContactPageViewModel(contact, user, contactService);
        }
    }
}
