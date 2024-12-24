using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;

namespace WPF_Version.ContactWindows.Pages.ContactPages
{
    public partial class EditContactPage : Page
    {
        private readonly Contact _contact;
        private readonly ContactService _contactService;
        private readonly BaseUser _user;

        public EditContactPage(Contact contact, BaseUser user, ContactService contactService)
        {
            InitializeComponent();
            _contact = contact;
            _contactService = contactService;
            _user = user;

            // Fyll i formulärfälten med kontaktens nuvarande information
            NameTextBox.Text = _contact.Name;
            LastNameTextBox.Text = _contact.Lastname;
            PhoneNumberTextBox.Text = _contact.PhoneNumber;
            AddressTextBox.Text = _contact.Address;
            EmailTextBox.Text = _contact.Email;
            NotesTextBox.Text = _contact.Notes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Uppdatera kontaktobjektet
            _contact.Name = NameTextBox.Text;
            _contact.Lastname = LastNameTextBox.Text;
            _contact.PhoneNumber = PhoneNumberTextBox.Text;
            _contact.Address = AddressTextBox.Text;
            _contact.Email = EmailTextBox.Text;
            _contact.Notes = NotesTextBox.Text;

            // Uppdatera kontakt i ContactService
            _contactService.UpdateContact(_user.Id, _contact);

            // Bekräftelsemeddelande
            MessageBox.Show("Contact updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till ContactsPage
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, null, _contactService));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera tillbaka till ContactsPage utan att spara
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, null, _contactService));
        }
    }


}
