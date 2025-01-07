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
    /// <summary>
    /// ViewModel för ContactsPage. Hanterar visning och interaktion med användarens kontakter.
    /// </summary>
    public class ContactsPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;

        /// <summary>
        /// Initierar ContactsPageViewModel med användare och tjänster.
        /// </summary>
        /// <param name="user">Inloggad användare.</param>
        /// <param name="userService">Tjänst för användarhantering.</param>
        /// <param name="contactService">Tjänst för kontakthantering.</param>
        public ContactsPageViewModel(BaseUser user, UserService userService, ContactService contactService)
        {
            _user = user;
            _userService = userService;
            _contactService = contactService;

            // Initiera listan med kontakter och deras synlighetstillstånd
            Contacts = new ObservableCollection<ContactWithVisibility>(
                _user.Contacts.ConvertAll(contact => new ContactWithVisibility(contact))
            );

            // Initiera kommandon
            AddContactCommand = new RelayCommand(AddContact);
            EditContactCommand = new RelayCommand<ContactWithVisibility>(EditContact);
            DeleteContactCommand = new RelayCommand<ContactWithVisibility>(DeleteContact);
            ToggleMoreInfoCommand = new RelayCommand<ContactWithVisibility>(ToggleMoreInfo);
        }

        /// <summary>
        /// Lista över kontakter med synlighetstillstånd.
        /// </summary>
        public ObservableCollection<ContactWithVisibility> Contacts { get; }

        /// <summary>
        /// Kommandon för att hantera kontakter.
        /// </summary>
        public ICommand AddContactCommand { get; }
        public ICommand EditContactCommand { get; }
        public ICommand DeleteContactCommand { get; }
        public ICommand ToggleMoreInfoCommand { get; }

        /// <summary>
        /// Navigerar till sidan för att lägga till en ny kontakt.
        /// </summary>
        private void AddContact()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new AddContactPage(_user, _userService, _contactService));
        }

        /// <summary>
        /// Navigerar till sidan för att redigera en kontakt.
        /// </summary>
        /// <param name="contact">Kontakt som ska redigeras.</param>
        private void EditContact(ContactWithVisibility contact)
        {
            if (contact == null) return;

            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new EditContactPage(contact.Contact, _user, _contactService));
        }

        /// <summary>
        /// Tar bort en kontakt efter användarens bekräftelse.
        /// </summary>
        /// <param name="contact">Kontakt som ska tas bort.</param>
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
                // Tar bort kontakten från ObservableCollection
                Contacts.Remove(contact);

                // Använder ContactService för att ta bort kontakten från lagringen
                _contactService.DeleteContact(_user.Id, contact.Contact.Id);

                MessageBox.Show("Contact deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Växlar synligheten av ytterligare information om en kontakt.
        /// </summary>
        /// <param name="contact">Kontakt vars information ska växlas.</param>
        private void ToggleMoreInfo(ContactWithVisibility contact)
        {
            if (contact == null) return;

            contact.IsMoreInfoVisible = !contact.IsMoreInfoVisible; // Växlar synlighetstillståndet
        }
    }

    /// <summary>
    /// Wrapper-klass för kontakt med synlighetstillstånd.
    /// </summary>
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
                OnPropertyChanged(nameof(ToggleButtonText)); // Informera att ToggleButtonText har ändrats
            }
        }

        /// <summary>
        /// Text för knappen som växlar visning av ytterligare information.
        /// </summary>
        public string ToggleButtonText => IsMoreInfoVisible ? "Show Less" : "Show More";

        /// <summary>
        /// Initierar en kontakt med standardinställningar för synlighet.
        /// </summary>
        /// <param name="contact">Kontakt som ska visas.</param>
        public ContactWithVisibility(Contact contact)
        {
            Contact = contact;
            _isMoreInfoVisible = false; // Standard är att informationen är dold
        }
    }
}
