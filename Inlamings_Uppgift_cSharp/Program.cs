using System;
using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Business.CoreFiles.Factory;
using Business.Logic._2_Repositories;
using Business.Services;

namespace ConsoleApp
{
    class Program
    {
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json");
        static void Main(string[] args)
        {
            // Här kan du ange sökvägen till din JSON-fil
            var jsonRepository = new JsonRepository(_filePath);
            var userRepository = new UserRepository(jsonRepository);
            var userCreateFactory = new UserCreate();
            var userService = new UserService(userRepository, userCreateFactory);

            // Huvudmeny
            MainMenu(userService);
        }

        // Huvudmenyn där användaren kan välja operationer
        static void MainMenu(UserService userService)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Välj en operation:");
                Console.WriteLine("1. Skapa användare");
                Console.WriteLine("2. Läs användare");
                Console.WriteLine("3. Uppdatera användare");
                Console.WriteLine("4. Ta bort användare");
                Console.WriteLine("5. Läs alla användare");  // Nytt alternativ för att läsa alla användare
                Console.WriteLine("6. Avsluta");
                Console.Write("Välj ett alternativ (1-6): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUser(userService);
                        break;
                    case "2":
                        ReadUser(userService);
                        break;
                    case "3":
                        UpdateUser(userService);
                        break;
                    case "4":
                        DeleteUser(userService);
                        break;
                    case "5":
                        ReadAllUsers(userService);  // Anropa ReadAllUsers
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Avslutar...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt alternativ, försök igen.");
                        break;
                }
            }
        }
        // Skapa användare
        static void CreateUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Skapa en ny användare");

            Console.Write("Förnamn: ");
            string name = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastname = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Lösenord: ");
            string password = Console.ReadLine();

            Console.Write("Roll (Admin/Default): ");
            string role = Console.ReadLine();

            userService.CreateUser(name, lastname, email, password, role);

            Console.WriteLine("Användare skapad!");
            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }

        // Läs användare
        static void ReadUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Sök efter en användare");

            Console.Write("Email: ");
            string email = Console.ReadLine()?.ToLower();  // Gör inmatad email till lowercase

            var user = userService.ReadUserByEmail(email);  // Anropa en metod för att läsa användare efter e-postadress

            if (user != null)
            {
                Console.WriteLine($"Användare: {user.Fullname}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Roll: {user.Role}");
            }
            else
            {
                Console.WriteLine("Användare hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }

        // Uppdatera användare
        static void UpdateUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Uppdatera en användare");

            Console.Write("Email: ");
            string email = Console.ReadLine()?.ToLower();  // Gör inmatad email till lowercase

            var user = userService.ReadUserByEmail(email);  // Anropa en metod för att läsa användare efter e-postadress

            if (user != null)
            {
                Console.Write("Förnamn (ny): ");
                user.Name = Console.ReadLine();

                Console.Write("Efternamn (ny): ");
                user.Lastname = Console.ReadLine();

                Console.Write("Email (ny): ");
                user.Email = Console.ReadLine();

                Console.Write("Lösenord (ny): ");
                string password = Console.ReadLine();
                user.SetPassword(password);

                userService.UpdateUser(user);
                Console.WriteLine("Användare uppdaterad!");
            }
            else
            {
                Console.WriteLine("Användare hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }

        // Ta bort användare
        static void DeleteUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Ta bort en användare");

            Console.Write("Email: ");
            string email = Console.ReadLine()?.ToLower();  // Gör inmatad email till lowercase

            var user = userService.ReadUserByEmail(email);  // Anropa en metod för att läsa användare efter e-postadress

            if (user != null)
            {
                userService.DeleteUser(user);
                Console.WriteLine("Användare raderad!");
            }
            else
            {
                Console.WriteLine("Användare hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }
        static void ReadAllUsers(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Alla användare:");

            var users = userService.ReadAllUsers();  // Hämta alla användare från UserService

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"Användare: {user.Fullname}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Roll: {user.Role}");
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Inga användare hittades.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }

    }
}
