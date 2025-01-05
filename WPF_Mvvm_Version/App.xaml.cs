using System.Windows;
using System.IO;
using System.Windows.Navigation;
using Business.Services;
using Business.Logic._2_Repositories;
using Business.CoreFiles.Factory;
using Business.Logic._1_Services.UserService;

namespace WPF_Mvvm_Version
{
    public partial class App : Application
    {
        private static UserService _userService;
        private static ContactService _contactService;
        private static NavigationService _navigationService;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Instansiera beroenden
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json");
            var exampleUserCreate = new ExampleUserCreate();
            var jsonRepository = new JsonRepository(filePath, exampleUserCreate);

            var userRepository = new UserRepository(jsonRepository);
            var userCreateFactory = new UserCreate();
            var contactRepository = new ContactRepository(jsonRepository);

            _userService = new UserService(userRepository, userCreateFactory);
            _contactService = new ContactService(contactRepository);

            // Skapa MainWindow
           var mainWindow = new MainWindow();
           mainWindow.Show();

            // Tilldela NavigationService från ContentFrame
            var frame = mainWindow.ContentFrame; // Se till att detta inte är null
            if (frame != null)
            {
                _navigationService = frame.NavigationService;
            }
            else
            {
                MessageBox.Show("ContentFrame är null! Kontrollera MainWindow.xaml.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static UserService GetUserService() => _userService;
        public static ContactService GetContactService() => _contactService;
        public static NavigationService GetNavigationService() => _navigationService;


        public static void SetNavigationService(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
