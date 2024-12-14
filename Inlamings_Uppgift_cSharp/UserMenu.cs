using System;
using Business.Services;
using Business.CoreFiles.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public static class UserMenu
    {
        private static BaseUser _loggedInUser = null;
        public static void Start(UserService userService, ServiceProvider serviceProvider)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Välj en operation:");
                Console.WriteLine("1. Skapa användare");
                Console.WriteLine("2. Logga in som användare");
                Console.WriteLine("3. Uppdatera användare");
                Console.WriteLine("4. Ta bort användare");
                Console.WriteLine("5. Läs alla användare");
                Console.WriteLine("6. Avsluta");
                Console.Write("Välj ett alternativ (1-6): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUser(userService);
                        break;
                    case "2":
                        SimulateLogin(userService, serviceProvider);
                        break;
                    case "3":
                        UpdateUser(userService);
                        break;
                    case "4":
                        DeleteUser(userService);
                        break;
                    case "5":
                        ReadAllUsers(userService);
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Avslutar...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt alternativ, försök igen.");
                        break;
                }

                Console.WriteLine("Tryck på en tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private static void SimulateLogin(UserService userService, ServiceProvider serviceProvider)
        {
            Console.Clear();
            Console.Write("Ange din e-post för att logga in: ");
            string email = Console.ReadLine()?.ToLower();

            var user = userService.ReadUserByEmail(email);
            if (user != null)
            {
                _loggedInUser = user;
                Console.WriteLine($"Inloggning lyckades! Välkommen, {user.Fullname}.");
                ContactMenu.Start(_loggedInUser, serviceProvider);
            }
            else
            {
                Console.WriteLine("Användare hittades inte.");
            }
        }

        // Skapa användare
        static void CreateUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Skapa en ny användare");

            string name;
            bool valid;
            // Förnamn
            do
            {
                Console.Write("Förnamn: ");
                name = Console.ReadLine();
                valid = !string.IsNullOrWhiteSpace(name) && name.Length <= 50 && Regex.IsMatch(name, @"^[\p{L}\s'-]+$");
                if (!valid)
                    Console.WriteLine("Förnamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.");
            } while (!valid);

            string lastname;

            // Efternamn
            do
            {
                Console.Write("Efternamn: ");
                lastname = Console.ReadLine();
                valid = !string.IsNullOrWhiteSpace(lastname) && lastname.Length <= 50 && Regex.IsMatch(lastname, @"^[\p{L}\s'-]+$");
                if (!valid)
                    Console.WriteLine("Efternamn är ogiltigt. Det kan inte vara tomt och måste vara mellan 2 och 50 tecken långt.");
            } while (!valid);

            string email;

            // Email
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();

                var existingUser = userService.ReadUserByEmail(email);

                if (existingUser != null)
                {
                    Console.WriteLine("E-postadressen är redan registrerad. Vänligen ange en annan e-postadress.");
                    valid = false;
                }
                else
                {
                    valid = !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");
                    if (!valid)
                        Console.WriteLine("Ogiltig e-postadress. Kontrollera att den är korrekt.");
                }
            } while (!valid);

            string password;

            // Lösenord
            do
            {
                Console.Write("Lösenord: ");
                password = Console.ReadLine();

                valid = !string.IsNullOrWhiteSpace(password) && password.Length >= 6; // Exempel på minimum längd

                if (!valid)
                    Console.WriteLine("Lösenordet måste vara minst 6 tecken långt.");
            } while (!valid);

            string role;

            // Roll (Admin/Default)
            do
            {
                Console.Write("Roll (Admin/Default): ");
                role = Console.ReadLine()?.Trim();

                role = role.Substring(0, 1).ToUpper() + role.Substring(1).ToLower();

                valid = role == "Admin" || role == "Default";

                if (!valid)
                    Console.WriteLine("Ogiltig roll. Vänligen skriv 'Admin' eller 'Default'.");
            } while (!valid);

            userService.CreateUser(name, lastname, email, password, role);

            Console.WriteLine("Användare skapad!");
            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }



        // Läs användare
        static void ReadUser(UserService userService, ServiceProvider serviceProvider)
        {
            Console.Clear();
            Console.WriteLine("Logga in som användare");

            Console.Write("Email: ");
            string email = Console.ReadLine()?.ToLower();

            var user = userService.ReadUserByEmail(email);

            if (user != null)
            {
                _loggedInUser = user;
                Console.WriteLine($"Inloggning lyckades! Välkommen, {user.Fullname}.");

                // Starta en ny meny för kontaktoperationer
                ContactMenu.Start(_loggedInUser, serviceProvider);
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
