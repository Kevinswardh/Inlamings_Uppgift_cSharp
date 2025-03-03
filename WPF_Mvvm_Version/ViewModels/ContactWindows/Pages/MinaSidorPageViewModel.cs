﻿using System;
using System.Windows;
using System.Windows.Input;
using Business.CoreFiles.Models.Users;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_Mvvm_Version.Views.ContactWindows;

namespace WPF_Mvvm_Version.ViewModels.ContactWindows.Pages
{
    /// <summary>
    /// ViewModel för MinaSidor-sidan som hanterar användarens information, redigering och radering.
    /// </summary>
    public class MinaSidorPageViewModel : ObservableObject
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;

        /// <summary>
        /// Konstruktor för att initiera ViewModel med användare och UserService.
        /// </summary>
        /// <param name="user">Den inloggade användaren.</param>
        /// <param name="userService">Tjänst för användarhantering.</param>
        public MinaSidorPageViewModel(BaseUser user, UserService userService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            // Initiera kommandon
            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        /// <summary>
        /// Returnerar detaljerad information om användaren som visas på sidan.
        /// </summary>
        public string UserDetails =>
            $"Name: {_user.Name}\n" +
            $"Lastname: {_user.Lastname}\n" +
            $"E-post: {_user.Email}\n" +
            $"Role: {_user.Role}";

        /// <summary>
        /// Kommando för att redigera användaren.
        /// </summary>
        public ICommand EditUserCommand { get; }

        /// <summary>
        /// Kommando för att radera användaren.
        /// </summary>
        public ICommand DeleteUserCommand { get; }

        /// <summary>
        /// Navigerar till sidan för att redigera användarens information.
        /// </summary>
        private void EditUser()
        {
            // Hämta det aktuella ContactWindow och navigera till EditUserPage
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
            contactWindow?.ContentFrame.Navigate(new Views.ContactWindows.Pages.MinaSidorPages.EditUserPage(_user, _userService));
        }

        /// <summary>
        /// Raderar användaren efter bekräftelse och navigerar tillbaka till MainWindow.
        /// </summary>
        private void DeleteUser()
        {
            // Visa en dialogruta för att bekräfta radering
            var result = MessageBox.Show(
                $"Är du säker på att du vill radera användaren {_user.Fullname}?",
                "Bekräfta Radering",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Försök att radera användaren via UserService
                    _userService.DeleteUser(_user);

                    // Visa ett bekräftelsemeddelande
                    MessageBox.Show("Användaren har raderats.", "Radering lyckades", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Öppna MainWindow
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    });

                    // Stäng ContactWindow
                    var contactWindow = Application.Current.Windows.OfType<ContactWindow>().FirstOrDefault();
                    contactWindow?.Close();
                }
                catch (Exception ex)
                {
                    // Hantera fel vid radering
                    MessageBox.Show($"Ett fel uppstod vid radering: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
