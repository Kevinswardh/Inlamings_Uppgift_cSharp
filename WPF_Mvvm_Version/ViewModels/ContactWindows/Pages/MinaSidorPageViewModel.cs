using System;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Users;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages
{
    public class MinaSidorPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;

        public MinaSidorPageViewModel(BaseUser user, UserService userService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        public string UserDetails =>
            $"Name: {_user.Name}\n" +
            $"Lastname: {_user.Lastname}\n" +
            $"E-post: {_user.Email}\n" +
            $"Role: {_user.Role}";

        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        private void EditUser()
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPages.EditUserPage(_user, _userService));
        }

        private void DeleteUser()
        {
            var result = MessageBox.Show(
                $"Är du säker på att du vill radera användaren {_user.Fullname}?",
                "Bekräfta Radering",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Radera användaren via UserService
                    _userService.DeleteUser(_user);

                    // Visa ett meddelande om att användaren raderades
                    MessageBox.Show("Användaren har raderats.", "Radering lyckades", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Öppna MainWindow
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    });

                    // Stäng det aktuella fönstret
                    var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
                    contactWindow?.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ett fel uppstod vid radering: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
