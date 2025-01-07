using System;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;
using WPF_Mvvm_Version.Views.ContactWindows.Pages;
using WPF_Mvvm_Version.Views.ContactWindows.Pages.ContactPages;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.ContactPages
{
    /// <summary>
    /// ViewModel för redigering av kontakter i EditContactPage.
    /// </summary>
    public class EditContactPageViewModel : ObservableObject
    {
        private readonly ContactService _contactService; // Tjänst för att hantera kontakter.
        private readonly BaseUser _user; // Representerar den aktuella användaren.
        private readonly Contact _contact; // Kontakt som ska redigeras.

        /// <summary>
        /// Initierar ViewModel med en kontakt, användare och ContactService.
        /// </summary>
        /// <param name="contact">Kontakt som ska redigeras.</param>
        /// <param name="user">Den aktuella användaren.</param>
        /// <param name="contactService">Tjänst för hantering av kontakter.</param>
        public EditContactPageViewModel(Contact contact, BaseUser user, ContactService contactService)
        {
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));

            // Initiera egenskaper med kontaktens nuvarande data
            Name = contact.Name;
            Lastname = contact.Lastname;
            PhoneNumber = contact.PhoneNumber;
            Address = contact.Address;
            Email = contact.Email;
            Notes = contact.Notes;

            // Initiera kommandon
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindbara egenskaper kopplade till UI
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        // Kommandon
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Sparar ändringarna till kontakten och navigerar tillbaka till ContactsPage.
        /// </summary>
        private void Save()
        {
            // Uppdatera kontaktens data
            _contact.Name = Name;
            _contact.Lastname = Lastname;
            _contact.PhoneNumber = PhoneNumber;
            _contact.Address = Address;
            _contact.Email = Email;
            _contact.Notes = Notes;

            // Uppdatera kontakten via ContactService
            _contactService.UpdateContact(_user.Id, _contact);

            // Visa bekräftelsemeddelande
            MessageBox.Show("Contact updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till ContactsPage
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, null, _contactService));
        }

        /// <summary>
        /// Avbryter redigeringen och navigerar tillbaka till ContactsPage utan att spara.
        /// </summary>
        private void Cancel()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new ContactsPage(_user, null, _contactService));
        }
    }
}
