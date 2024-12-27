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
    public class EditUserPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;

        public EditUserPageViewModel(BaseUser user, UserService userService)
        {
            _user = user;
            _userService = userService;

            // Initialize properties
            Name = _user.Name;
            Lastname = _user.Lastname;
            Email = _user.Email;

            // Initialize commands
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Bindable properties
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private void Save()
        {
            // Update user details
            _user.Name = Name;
            _user.Lastname = Lastname;
            _user.Email = Email;

            // Save to UserService
            _userService.UpdateUser(_user);

            MessageBox.Show("User information updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigate back to MinaSidorPage
            NavigateToMinaSidor();
        }

        private void Cancel()
        {
            // Navigate back to MinaSidorPage without saving
            NavigateToMinaSidor();
        }

        private void NavigateToMinaSidor()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPage(_user, _userService));
        }
    }
}
