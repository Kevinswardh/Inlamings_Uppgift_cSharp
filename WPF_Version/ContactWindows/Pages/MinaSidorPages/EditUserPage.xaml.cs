using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;

namespace WPF_Version.ContactWindows.Pages.MinaSidorPages
{
    public partial class EditUserPage : Page
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;

        public EditUserPage(BaseUser user, UserService userService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;

            // Fyll i fälten med användarens nuvarande information
            NameTextBox.Text = _user.Name;
            LastNameTextBox.Text = _user.Lastname;
            EmailTextBox.Text = _user.Email;
          // Om användaren har telefonnummer
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Uppdatera användarens information
            _user.Name = NameTextBox.Text;
            _user.Lastname = LastNameTextBox.Text;
            _user.Email = EmailTextBox.Text;
          

            // Uppdatera i UserService
            _userService.UpdateUser(_user);

            // Bekräftelsemeddelande
            MessageBox.Show("User information updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigera tillbaka till Mina Sidor eller annan relevant sida
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new MinaSidorPage(_user, _userService));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera tillbaka till Mina Sidor utan att spara
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new MinaSidorPage(_user, _userService));
        }
    }
}
