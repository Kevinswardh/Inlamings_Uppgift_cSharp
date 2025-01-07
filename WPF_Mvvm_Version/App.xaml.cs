using System.Windows;
using System.IO;
using System.Windows.Navigation;
using Business.Services;
using Business.Logic._2_Repositories;
using Business.CoreFiles.Factory;
using Business.Logic._1_Services.UserService;

namespace WPF_Mvvm_Version
{
    /// <summary>
    /// Huvudklassen för applikationen som hanterar startup och globala tjänster.
    /// </summary>
    public partial class App : Application
    {
        // Globala instanser av UserService, ContactService och NavigationService.
        private static UserService _userService;
        private static ContactService _contactService;
        private static NavigationService _navigationService;

        /// <summary>
        /// Körs när applikationen startar och hanterar initialisering av tjänster och huvudfönster.
        /// </summary>
        /// <param name="e">Argument för startup-händelsen.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Instansiera beroenden som krävs för applikationen.
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json");
            var exampleUserCreate = new ExampleUserCreate();
            var jsonRepository = new JsonRepository(filePath, exampleUserCreate);

            var userRepository = new UserRepository(jsonRepository);
            var userCreateFactory = new UserCreate();
            var contactRepository = new ContactRepository(jsonRepository);

            // Initialisera UserService och ContactService.
            _userService = new UserService(userRepository, userCreateFactory);
            _contactService = new ContactService(contactRepository);

            // Skapa och visa huvudfönstret.
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Hämta NavigationService från ContentFrame i MainWindow.
            var frame = mainWindow.ContentFrame;
            if (frame != null)
            {
                _navigationService = frame.NavigationService;
            }
            else
            {
                // Visa ett felmeddelande om ContentFrame inte hittas.
                MessageBox.Show("ContentFrame är null! Kontrollera MainWindow.xaml.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Hämtar instansen av UserService.
        /// </summary>
        /// <returns>Den globala instansen av UserService.</returns>
        public static UserService GetUserService() => _userService;

        /// <summary>
        /// Hämtar instansen av ContactService.
        /// </summary>
        /// <returns>Den globala instansen av ContactService.</returns>
        public static ContactService GetContactService() => _contactService;

        /// <summary>
        /// Hämtar instansen av NavigationService.
        /// </summary>
        /// <returns>Den globala instansen av NavigationService.</returns>
        public static NavigationService GetNavigationService() => _navigationService;

        /// <summary>
        /// Ställer in NavigationService för applikationen.
        /// </summary>
        /// <param name="navigationService">Instansen av NavigationService som ska tilldelas.</param>
        public static void SetNavigationService(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
