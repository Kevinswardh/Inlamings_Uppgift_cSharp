using System.Windows;
using Business.Services;
using Business.CoreFiles.Factory;
using Business.CoreFiles.Factory.Interfaces;
using Business.Interfaces.IUser;
using Business._2_Repositories.JsonRepository.Interface;
using Business.Logic._2_Repositories;
using System.IO;
using Business.Logic._1_Services.UserService;

namespace WPF_Version
{
    public partial class App : Application
    {
        private static UserService _userService;
        private static ContactService _contactService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Skapa instanser av nödvändiga beroenden
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json"); // Sökvägen för JSON-filen
            var exampleUserCreate = new ExampleUserCreate(); // Skapa instans av ExampleUserCreate
            var jsonRepository = new JsonRepository(filePath, exampleUserCreate); // Initiera JsonRepository

            var userRepository = new UserRepository(jsonRepository); // Initiera UserRepository
            var userCreateFactory = new UserCreate(); // Initiera UserCreate-fabriken
            var contactRepository = new ContactRepository(jsonRepository); // Initiera ContactRepository

            // Initiera UserService
            _userService = new UserService(userRepository, userCreateFactory);

            // Initiera ContactService
            _contactService = new ContactService(contactRepository);
        }

        // Tillhandahåller en global instans av UserService
        public static UserService GetUserService()
        {
            return _userService;
        }

        // Tillhandahåller en global instans av ContactService
        public static ContactService GetContactService()
        {
            return _contactService;
        }
    }
}
