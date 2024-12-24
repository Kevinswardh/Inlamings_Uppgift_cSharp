using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Helpers;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Business.Logic._1_Services.UserService;
using Business.Services;

namespace WPF_Version.ContactWindows.Pages.ContactPages
{
    public partial class AddContactPage : Page
    {
        private readonly BaseUser _user;
        private readonly ContactService _contactService;
        private readonly UserService _userService;

        public AddContactPage(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _contactService = contactService;
            _userService = userService;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Skapa en ny kontakt baserat på input
            var contact = new Contact
            {
                Name = NameTextBox.Text,
                Lastname = LastnameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Address = AddressTextBox.Text,
                Email = EmailTextBox.Text,
                Notes = NotesTextBox.Text,
                Id = GuidGenerator.GenerateGuid()
            };

            // Lägg till kontakten i användarens kontaktlista och spara via ContactService
            _contactService.CreateContact(_user.Id, contact);
            _user.Contacts.Add(contact); // Uppdatera BaseUser-kontakter
            _userService.UpdateUser(_user); // Spara användaren

            MessageBox.Show("Contact added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till ContactsPage
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, _userService, _contactService));
        }





        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera tillbaka till ContactsPage utan att spara
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, _userService, _contactService));
        }

    }
}
