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

            // Debugging output to check what role value is being passed
            Console.WriteLine($"Role passed: '{role}' (Length: {role.Length})");

            // Trim everything except the last word after the dot
            int lastDotIndex = role.LastIndexOf(' ');
            if (lastDotIndex != -1)
            {
                role = role.Substring(lastDotIndex + 1).Trim();  // Keep only the part after the last dot
            }

            // Debug the role after trimming
            Console.WriteLine($"Trimmed role: '{role}' (Length: {role.Length})");

            // Generate a unique GUID for the new user
            var userId = GuidGenerator.GenerateGuid();  // Retrieve the GUID for the new user

            // Check the role and create the corresponding user
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                user = new Admin
                {
                    Id = userId,
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),
                    Favorites = new List<FavoriteContact>()
                };
            }
            else
            {
                user = new DefaultUser
                {
                    Id = userId,
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),
                    Favorites = new List<FavoriteContact>()
                };
            }

            // Set the password using the BaseUser method
            user.SetPassword(password);

            // Return the created user with GUID, empty lists, and other properties
            return user;
        }




    }
}
