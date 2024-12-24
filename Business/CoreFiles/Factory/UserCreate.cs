using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Users.Roles;
using Business.CoreFiles.Helpers;
using Business.CoreFiles.Models.Contacts;
using Business.CoreFiles.Factory.Interfaces;  // Importera GuidGenerator-hjälpen

namespace Business.CoreFiles.Factory
{
    /// <summary>
    /// En fabriksklass för att skapa användare med specifika roller och egenskaper.
    /// </summary>
    public class UserCreate : IUserCreate
    {
        /// <summary>
        /// Skapar en ny användare och sätter lösenordet.
        /// </summary>
        /// <param name="name">Förnamn på användaren.</param>
        /// <param name="lastname">Efternamn på användaren.</param>
        /// <param name="email">E-postadress för användaren.</param>
        /// <param name="password">Lösenord för användaren.</param>
        /// <param name="role">Användarens roll (t.ex. "Admin" eller "Default").</param>
        /// <returns>Den skapade användaren som en instans av <see cref="BaseUser"/>.</returns>
        public BaseUser CreateUser(string name, string lastname, string email, string password, string role)
        {
            BaseUser user;

            // Genererar ett unikt GUID för den nya användaren
            var userId = GuidGenerator.GenerateGuid();  // Hämtar ett nytt GUID för användaren

            // Kontrollerar rollen och skapar rätt typ av användare
            if (role == "Admin")
            {
                user = new Admin  // Skapar en Admin-användare
                {
                    Id = userId,  // Tilldelar det genererade GUID som ID
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),  // Initierar en tom lista för kontakter
                    Favorites = new List<FavoriteContact>()  // Initierar en tom lista för favoriter
                };
            }
            else
            {
                user = new DefaultUser  // Skapar en DefaultUser om rollen inte är Admin
                {
                    Id = userId,  // Tilldelar det genererade GUID som ID
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),  // Initierar en tom lista för kontakter
                    Favorites = new List<FavoriteContact>()  // Initierar en tom lista för favoriter
                };
            }

            // Sätter lösenordet med hjälp av BaseUser-metoden
            user.SetPassword(password);

            // Returnerar den skapade användaren med GUID, tomma listor och övriga egenskaper
            return user;
        }
    }
}
