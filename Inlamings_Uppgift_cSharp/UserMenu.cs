using System;
using Business.Services;
using Business.CoreFiles.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public static class UserMenu
    {
        private static BaseUser _loggedInUser = null;

        /// <summary>
        /// Startar användarmenyn och hanterar val för olika användaroperationer.
        /// </summary>
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

        /// <summary>
        /// Uppdaterar en befintlig användare med möjlighet att välja specifika fält att ändra.
        /// </summary>
        static void UpdateUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Uppdatera en användare");

            Console.Write("Ange e-postadressen för användaren som ska uppdateras: ");
            string email = Console.ReadLine()?.ToLower();

            var user = userService.ReadUserByEmail(email);

            if (user != null)
            {
                bool updating = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Användare: {user.Fullname}");
                    Console.WriteLine("Vad vill du ändra?");
                    Console.WriteLine("1. Förnamn");
                    Console.WriteLine("2. Efternamn");
                    Console.WriteLine("3. E-postadress");
                    Console.WriteLine("4. Lösenord");
                    Console.WriteLine("5. Avsluta uppdatering");
                    Console.Write("Välj ett alternativ (1-5): ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Nytt förnamn: ");
                            user.Name = Console.ReadLine();
                            break;

                        case "2":
                            Console.Write("Nytt efternamn: ");
                            user.Lastname = Console.ReadLine();
                            break;

                        case "3":
                            Console.Write("Ny e-postadress: ");
                            user.Email = Console.ReadLine();
                            break;

                        case "4":
                            Console.Write("Nytt lösenord: ");
                            string password = Console.ReadLine();
                            user.SetPassword(password);
                            break;

                        case "5":
                            updating = false;
                            break;

                        default:
                            Console.WriteLine("Ogiltigt alternativ, försök igen.");
                            break;
                    }

                    if (updating)
                    {
                        Console.Write("Vill du ändra något mer? (ja/nej): ");
                        string response = Console.ReadLine()?.Trim().ToLower();
                        if (response != "ja")
                        {
                            updating = false;
                        }
                    }

                } while (updating);

                userService.UpdateUser(user);
                Console.WriteLine("Användare uppdaterad!");
            }
            else
            {
                Console.WriteLine("Användare med den angivna e-postadressen hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }


        /// <summary>
        /// Tar bort en befintlig användare.
        /// </summary>
        static void DeleteUser(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Ta bort en användare");

            Console.Write("Ange e-postadressen för användaren som ska tas bort: ");
            string email = Console.ReadLine()?.ToLower();

            var user = userService.ReadUserByEmail(email);

            if (user != null)
            {
                userService.DeleteUser(user);
                Console.WriteLine("Användare raderad!");
            }
            else
            {
                Console.WriteLine("Användare med den angivna e-postadressen hittades inte.");
            }
        }

        /// <summary>
        /// Hämtar och visar alla användare.
        /// </summary>
        static void ReadAllUsers(UserService userService)
        {
            Console.Clear();
            Console.WriteLine("Alla användare:");

            var users = userService.ReadAllUsers();

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
        }

        /// <summary>
        /// Skapar en ny användare med inmatad information.
        /// </summary>
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

            // E-post
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

                valid = !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
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
        }
        /// <summary>
        /// Simulerar inloggning för en användare baserat på e-postadress.
        /// </summary>
        private static void SimulateLogin(UserService userService, ServiceProvider serviceProvider)
        {
            Console.Clear();
            Console.Write("Ange din e-post för att logga in: ");
            string email = Console.ReadLine()?.ToLower();

            Console.Write("Ange ditt lösenord: ");
            string password = Console.ReadLine();

            var user = userService.ReadUserByEmail(email);
            if (user != null && user.ValidatePassword(password))
            {
                Console.WriteLine($"Inloggning lyckades! Välkommen, {user.Fullname}.");
                Console.WriteLine("Tryck på en tangent för att fortsätta...");
                Console.ReadKey();

                // Starta kontaktmenyn med den inloggade användaren
                ContactMenu.Start(user, serviceProvider);
            }
            else
            {
                Console.WriteLine("E-postadress eller lösenord är felaktigt. Försök igen.");
                Console.WriteLine("Tryck på en tangent för att fortsätta...");
                Console.ReadKey();
            }
        }


    }
}
