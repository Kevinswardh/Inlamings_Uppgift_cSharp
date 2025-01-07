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
    /// <summary>
    /// ViewModel för att hantera logiken bakom sidan för att lägga till en ny kontakt (AddContactPage).
    /// </summary>
    public class AddContactPageViewModel : ObservableObject
    {
        private readonly BaseUser _user; // Representerar den aktuella användaren.
        private readonly ContactService _contactService; // Tjänst för kontaktoperationer.
        private readonly UserService _userService; // Tjänst för användarhantering.

        /// <summary>
        /// Konstruktor som initialiserar ViewModel med nödvändiga beroenden.
        /// </summary>
        /// <param name="user">Den aktuella användaren.</param>
        /// <param name="userService">Tjänst för att hantera användare.</param>
        /// <param name="contactService">Tjänst för att hantera kontakter.</param>
        public AddContactPageViewModel(BaseUser user, UserService userService, ContactService contactService)
        {
            _user = user;
            _contactService = contactService;
            _userService = userService;

            // Initiera kontakt och kommandon
            Contact = new Contact();
            SaveCommand = new RelayCommand(SaveContact);
            CancelCommand = new RelayCommand(Cancel);
        }

        /// <summary>
        /// Den kontakt som användaren håller på att skapa.
        /// </summary>
        public Contact Contact { get; set; }

        // Kommandon för att spara eller avbryta kontakt-skapandet
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Sparar den nya kontakten och uppdaterar användarens kontaktlista.
        /// </summary>
        private void SaveContact()
        {
            // Generera unikt ID för kontakten
            Contact.Id = Guid.NewGuid().ToString();

            // Lägg till kontakten i ContactService
            _contactService.CreateContact(_user.Id, Contact);

            // Uppdatera användarens kontaktlista och spara användaren
            _user.Contacts.Add(Contact);
            _userService.UpdateUser(_user);

            // Bekräftelsemeddelande
            MessageBox.Show("Contact added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till ContactsPage
            NavigateToContactsPage();
        }

        /// <summary>
        /// Avbryter operationen och navigerar tillbaka till ContactsPage.
        /// </summary>
        private void Cancel()
        {
            NavigateToContactsPage();
        }

        /// <summary>
        /// Navigerar tillbaka till ContactsPage.
        /// </summary>
        private void NavigateToContactsPage()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.ContactsPage(_user, _userService, _contactService));
        }
    }
}
