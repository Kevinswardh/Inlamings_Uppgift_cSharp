using System;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Users;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages.MinaSidorPages
{
    /// <summary>
    /// ViewModel för redigering av användaruppgifter på EditUserPage.
    /// </summary>
    public class EditUserPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;

        /// <summary>
        /// Initierar EditUserPageViewModel med en användare och UserService.
        /// </summary>
        /// <param name="user">Den användare vars information ska redigeras.</param>
        /// <param name="userService">Tjänst för att hantera användare.</param>
        public EditUserPageViewModel(BaseUser user, UserService userService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            // Initiera bindbara egenskaper med användarens nuvarande information
            Name = _user.Name;
            Lastname = _user.Lastname;
            Email = _user.Email;

            // Initiera kommandon
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindbara egenskaper
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value); // Uppdaterar värdet och triggar PropertyChanged
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value); // Uppdaterar värdet och triggar PropertyChanged
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value); // Uppdaterar värdet och triggar PropertyChanged
        }

        // Kommandon
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Sparar de uppdaterade användaruppgifterna och navigerar tillbaka till MinaSidorPage.
        /// </summary>
        private void Save()
        {
            // Uppdatera användarens information
            _user.Name = Name;
            _user.Lastname = Lastname;
            _user.Email = Email;

            // Spara uppdateringen via UserService
            _userService.UpdateUser(_user);

            MessageBox.Show("User information updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till MinaSidorPage
            NavigateToMinaSidor();
        }

        /// <summary>
        /// Avbryter redigeringen och navigerar tillbaka till MinaSidorPage utan att spara.
        /// </summary>
        private void Cancel()
        {
            NavigateToMinaSidor();
        }

        /// <summary>
        /// Navigerar till MinaSidorPage.
        /// </summary>
        private void NavigateToMinaSidor()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPage(_user, _userService));
        }
    }
}
