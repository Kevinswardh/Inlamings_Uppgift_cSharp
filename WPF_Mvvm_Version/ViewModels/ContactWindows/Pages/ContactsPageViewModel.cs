using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;
using WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages
{
    public class ContactsPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;

        public ContactsPageViewModel(BaseUser user, UserService userService, ContactService contactService)
        {
            _user = user;
            _userService = userService;
            _contactService = contactService;

            Contacts = new ObservableCollection<ContactWithVisibility>(
                _user.Contacts.ConvertAll(contact => new ContactWithVisibility(contact))
            );

            AddContactCommand = new RelayCommand(AddContact);
            EditContactCommand = new RelayCommand<ContactWithVisibility>(EditContact);
            DeleteContactCommand = new RelayCommand<ContactWithVisibility>(DeleteContact);
            ToggleMoreInfoCommand = new RelayCommand<ContactWithVisibility>(ToggleMoreInfo);
        }

        public ObservableCollection<ContactWithVisibility> Contacts { get; }

        public ICommand AddContactCommand { get; }
        public ICommand EditContactCommand { get; }
        public ICommand DeleteContactCommand { get; }
        public ICommand ToggleMoreInfoCommand { get; }

        private void AddContact()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new AddContactPage(_user, _userService, _contactService));
        }

        private void EditContact(ContactWithVisibility contact)
        {
            if (contact == null) return;

            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new EditContactPage(contact.Contact, _user, _contactService));
        }

        private void DeleteContact(ContactWithVisibility contact)
        {
            if (contact == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete {contact.Contact.Fullname}?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Remove the contact from the ObservableCollection
                Contacts.Remove(contact);

                // Call ContactService to delete the contact from storage
                _contactService.DeleteContact(_user.Id, contact.Contact.Id);

                MessageBox.Show("Contact deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void ToggleMoreInfo(ContactWithVisibility contact)
        {
            if (contact == null) return;

            contact.IsMoreInfoVisible = !contact.IsMoreInfoVisible;
        }
    }

    // Wrapper class for contact with visibility state
    public class ContactWithVisibility : ObservableObject
    {
        public Contact Contact { get; }

        private bool _isMoreInfoVisible;
        public bool IsMoreInfoVisible
        {
            get => _isMoreInfoVisible;
            set
            {
                SetProperty(ref _isMoreInfoVisible, value);
                OnPropertyChanged(nameof(ToggleButtonText)); // Notify that ToggleButtonText has changed
            }
        }

        public string ToggleButtonText => IsMoreInfoVisible ? "Show Less" : "Show More";

        public ContactWithVisibility(Contact contact)
        {
            Contact = contact;
            _isMoreInfoVisible = false; // Default to collapsed
        }
    }

}
