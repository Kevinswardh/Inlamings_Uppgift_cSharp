using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.ContactPages
{
    public class AddContactPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;
        private readonly UserService _userService;

        public AddContactPageViewModel(BaseUser user, UserService userService, ContactService contactService)
        {
            _user = user;
            _contactService = contactService;
            _userService = userService;

            Contact = new Contact();
            SaveCommand = new RelayCommand(SaveContact);
            CancelCommand = new RelayCommand(Cancel);
        }

        public Contact Contact { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private void SaveContact()
        {
            // Lägg till kontakten i användarens kontaktlista och spara
            Contact.Id = Guid.NewGuid().ToString();
            _contactService.CreateContact(_user.Id, Contact);
            _user.Contacts.Add(Contact);
            _userService.UpdateUser(_user);

            MessageBox.Show("Contact added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till ContactsPage
            NavigateToContactsPage();
        }

        private void Cancel()
        {
            // Navigera tillbaka till ContactsPage utan att spara
            NavigateToContactsPage();
        }

        private void NavigateToContactsPage()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.ContactsPage(_user, _userService, _contactService));
        }
    }
}
