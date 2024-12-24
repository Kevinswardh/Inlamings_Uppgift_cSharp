using Business.CoreFiles.Models.Users;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Version.ContactWindows.Pages.ContactPages;
using WPF_Version.ContactWindows.Pages.MinaSidorPages;

namespace WPF_Version.ContactWindows.Pages
{
    public partial class MinaSidorPage : Page
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        public MinaSidorPage(BaseUser user, UserService userService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            // Visa användarinformation i gränssnittet
            NameTextBlock.Text = $"Name: {_user.Name}";
            LastNameTextBlock.Text = $"Lastname: {_user.Lastname}";
            UserEmailTextBlock.Text = $"E-post: {_user.Email}";
            UserRoleTextBlock.Text = $"Role: {_user.Role}";
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new EditUserPage(_user, _userService));
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Bekräfta radering
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

                    // Skapa och visa huvudfönstret (MainWindow)
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    });
                    // Stäng kontaktfönstret
                    var contactWindow = Window.GetWindow(this) as ContactWindow;
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
