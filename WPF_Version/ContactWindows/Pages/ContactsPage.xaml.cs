using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Contacts;
using Business.Services;
using Business.Logic._1_Services.UserService;
using WPF_Version.ContactWindows.Pages.ContactPages;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Version.ContactWindows.Pages
{
    public partial class ContactsPage : Page
    {
        private readonly BaseUser _user;
        private readonly UserService _userService;
        private readonly ContactService _contactService;
        public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsPage(BaseUser user, UserService userService, ContactService contactService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            _contactService = contactService;

            // Ladda endast kontakter om ObservableCollection inte redan är bunden
            if (Contacts == null)
            {
                Contacts = new ObservableCollection<Contact>(_user.Contacts);
                DataContext = this; // Bind DataContext till sidan
            }
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to AddContactPage
            var contactWindow = Window.GetWindow(this) as ContactWindow;
            contactWindow?.ContentFrame.Navigate(new AddContactPage(_user, _userService, _contactService));
        }

        private void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = button?.CommandParameter as Contact;

            if (contact != null)
            {
                // Navigera till EditContactPage och skicka med både kontakt och användare
                var contactWindow = Window.GetWindow(this) as ContactWindow;
                contactWindow?.ContentFrame.Navigate(new EditContactPage(contact, _user, _contactService));
            }
        }

        private Point _startPoint;

        private void ContactsScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(ContactsScrollViewer);
            ContactsScrollViewer.CaptureMouse();
        }

        private void ContactsScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (ContactsScrollViewer.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(ContactsScrollViewer);
                double delta = _startPoint.Y - currentPoint.Y;

                ContactsScrollViewer.ScrollToVerticalOffset(ContactsScrollViewer.VerticalOffset + delta);
                _startPoint = currentPoint;
            }
        }

        private void ContactsScrollViewer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ContactsScrollViewer.ReleaseMouseCapture();
        }

        private void ShowMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parentStackPanel = VisualTreeHelper.GetParent(button) as StackPanel;

            if (parentStackPanel != null)
            {
                // Hitta MoreInfoPanel i samma StackPanel
                var moreInfoPanel = parentStackPanel.Children
                    .OfType<StackPanel>()
                    .FirstOrDefault(child => child.Name == "MoreInfoPanel");

                if (moreInfoPanel != null)
                {
                    if (moreInfoPanel.Visibility == Visibility.Collapsed)
                    {
                        moreInfoPanel.Visibility = Visibility.Visible;
                        button.Content = "Show Less";
                    }
                    else
                    {
                        moreInfoPanel.Visibility = Visibility.Collapsed;
                        button.Content = "Show More";
                    }
                }
            }
        }




        private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the contact from the CommandParameter
            var button = sender as Button;
            var contact = button?.CommandParameter as Contact;

            if (contact != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {contact.Fullname}?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Remove contact from ObservableCollection and user object
                    Contacts.Remove(contact);
                    _user.Contacts.Remove(contact);

                    // Update user in UserService
                    _userService.UpdateUser(_user);

                    MessageBox.Show("Contact deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
