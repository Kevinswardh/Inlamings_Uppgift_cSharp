using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService;
using Business.Services;
using System.Windows;
using WPF_Mvvm_Version.ViewModels.ContactWindows;

namespace WPF_Mvvm_Version.Views.ContactWindows
{
    public partial class ContactWindow : Window
    {
        public ContactWindow(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();
            DataContext = new ContactWindowViewModel(user, userService, contactService, ContentFrame);
        }

        private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
