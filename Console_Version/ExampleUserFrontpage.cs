using Business.CoreFiles.Models.Users;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ConsoleApp
{
    public static class ExampleUserFrontPage
    {
        public static void Start(ServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetService<UserService>();

            // Kontrollera och skapa ExampleUser om den inte finns
            userService.EnsureExampleUserExists();

            // Hämta ExampleUser från UserService
            var exampleUser = userService.ReadAllUsers().FirstOrDefault(u => u.Email == "x@x.xx");

            // Visa ExampleUser-information
            DisplayExampleUserInfo(exampleUser);

            // Vänta på användarinput för att starta applikationen
            Console.WriteLine("\nTryck '1' för att starta applikationen:");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                UserMenu.Start(userService, serviceProvider);
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Avslutar programmet.");
            }
        }

        private static void DisplayExampleUserInfo(BaseUser exampleUser)
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("             EXAMPLE USER INFORMATION          ");
            Console.WriteLine("===============================================\n");

            // User Info
            Console.WriteLine("----- Användarinformation -----");
            Console.WriteLine($"Namn: {exampleUser.Fullname}");
            Console.WriteLine($"Email: {exampleUser.Email}");
            Console.WriteLine("--------------------------------\n");

            // Contacts Info
            Console.WriteLine("----- Kontakter -----");
            if (exampleUser.Contacts != null && exampleUser.Contacts.Any())
            {
                foreach (var contact in exampleUser.Contacts)
                {
                    Console.WriteLine($"Namn: {contact.Fullname}");
                    Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                    Console.WriteLine($"Adress: {contact.Address}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Anteckningar: {contact.Notes}");
                    Console.WriteLine("--------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Inga kontakter hittades.");
            }
            Console.WriteLine("\n");

            // Favorites Info
            Console.WriteLine("----- Favoriter -----");
            if (exampleUser.Favorites != null && exampleUser.Favorites.Any())
            {
                foreach (var favorite in exampleUser.Favorites)
                {
                    Console.WriteLine($"Namn: {favorite.Fullname}");
                    Console.WriteLine($"Telefonnummer: {favorite.PhoneNumber}");
                    Console.WriteLine($"Adress: {favorite.Address}");
                    Console.WriteLine($"Email: {favorite.Email}");
                    Console.WriteLine($"Anteckningar: {favorite.Notes}");
                    Console.WriteLine($"Favoritstatus: {favorite.Favorite}");
                    Console.WriteLine("--------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Inga favoriter hittades.");
            }

            Console.WriteLine("\n===============================================");
        }
    }
}
