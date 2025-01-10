using System;
using Business.CoreFiles.Models.Users;
using Business.Logic._1_Services.UserService.Interface;
using Microsoft.Extensions.DependencyInjection;
using Business.CoreFiles.Models.Contacts;
using Business.Services;

namespace ConsoleApp
{
    public static class ContactMenu
    {
        /// <summary>
        /// Startar menyn för att hantera kontakter.
        /// </summary>
        /// <param name="loggedInUser">Den inloggade användaren.</param>
        /// <param name="serviceProvider">Dependency Injection-tjänsteprovider.</param>
        public static void Start(BaseUser loggedInUser, ServiceProvider serviceProvider)
        {
            var contactService = serviceProvider.GetService<IContactService>();
            bool loggedIn = true;

            while (loggedIn)
            {
                Console.Clear();
                Console.WriteLine($"Inloggad som: {loggedInUser.Fullname}");
                Console.WriteLine("Välj en operation:");
                Console.WriteLine("1. Skapa kontakt");
                Console.WriteLine("2. Läs alla kontakter");
                Console.WriteLine("3. Uppdatera kontakt");
                Console.WriteLine("4. Ta bort kontakt");
                Console.WriteLine("5. Logga ut");
                Console.Write("Välj ett alternativ (1-5): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateContact(contactService, loggedInUser);
                        break;
                    case "2":
                        ReadAllContacts(contactService, loggedInUser);
                        break;
                    case "3":
                        UpdateContact(contactService, loggedInUser);
                        break;
                    case "4":
                        DeleteContact(contactService, loggedInUser);
                        break;
                    case "5":
                        loggedIn = false;
                        Console.WriteLine("Du har loggat ut.");
                        loggedInUser = null;
                        UserMenu.Start(serviceProvider.GetService<UserService>(), serviceProvider);
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
        /// Skapar en ny kontakt för den inloggade användaren.
        /// </summary>
        private static void CreateContact(IContactService contactService, BaseUser user)
        {
            Console.Clear();
            Console.WriteLine("Skapa en ny kontakt");

            Console.Write("Namn: ");
            string name = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastname = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Adress: ");
            string address = Console.ReadLine();

            var contact = new Contact
            {
                Name = name,
                Lastname = lastname,
                PhoneNumber = phoneNumber,
                Address = address
            };

            contactService.CreateContact(user.Id, contact);
            Console.WriteLine("Kontakt skapad!");
        }

        /// <summary>
        /// Visar alla kontakter för den inloggade användaren.
        /// </summary>
        private static void ReadAllContacts(IContactService contactService, BaseUser user)
        {
            Console.Clear();
            Console.WriteLine("Alla kontakter:");

            var contacts = contactService.GetAllContacts(user.Id);

            if (contacts.Count > 0)
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"Namn: {contact.Fullname}");
                    Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                    Console.WriteLine($"Adress: {contact.Address}");
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Inga kontakter hittades.");
            }
        }

        /// <summary>
        /// Uppdaterar en befintlig kontakt för den inloggade användaren.
        /// </summary>
        private static void UpdateContact(IContactService contactService, BaseUser user)
        {
            Console.Clear();
            Console.WriteLine("Uppdatera en kontakt");

            Console.Write("Ange kontaktens fullständiga namn (Förnamn Efternamn) för att uppdatera: ");
            string fullname = Console.ReadLine()?.Trim();

            var contacts = contactService.GetAllContacts(user.Id);
            var contact = contacts.FirstOrDefault(c => c.Fullname.Equals(fullname, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                bool updating = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Kontakt: {contact.Fullname}");
                    Console.WriteLine("Vad vill du ändra?");
                    Console.WriteLine("1. Namn");
                    Console.WriteLine("2. Efternamn");
                    Console.WriteLine("3. Telefonnummer");
                    Console.WriteLine("4. Adress");
                    Console.WriteLine("5. Avsluta uppdatering");
                    Console.Write("Välj ett alternativ (1-5): ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Nytt namn: ");
                            contact.Name = Console.ReadLine();
                            break;

                        case "2":
                            Console.Write("Nytt efternamn: ");
                            contact.Lastname = Console.ReadLine();
                            break;

                        case "3":
                            Console.Write("Nytt telefonnummer: ");
                            contact.PhoneNumber = Console.ReadLine();
                            break;

                        case "4":
                            Console.Write("Ny adress: ");
                            contact.Address = Console.ReadLine();
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

                contactService.UpdateContact(user.Id, contact);
                Console.WriteLine("Kontakt uppdaterad!");
            }
            else
            {
                Console.WriteLine("Kontakt med det angivna namnet hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }

        /// <summary>
        /// Tar bort en kontakt för den inloggade användaren.
        /// </summary>
        private static void DeleteContact(IContactService contactService, BaseUser user)
        {
            Console.Clear();
            Console.WriteLine("Ta bort en kontakt");

            Console.Write("Ange kontaktens fullständiga namn (Förnamn Efternamn) för att ta bort: ");
            string fullname = Console.ReadLine()?.Trim();

            var contacts = contactService.GetAllContacts(user.Id);
            var contact = contacts.FirstOrDefault(c => c.Fullname.Equals(fullname, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                contactService.DeleteContact(user.Id, contact.Id);
                Console.WriteLine("Kontakt raderad!");
            }
            else
            {
                Console.WriteLine("Kontakt med det angivna namnet hittades inte.");
            }
        }
    }
}
