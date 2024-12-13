using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Users.Roles;
using Business.CoreFiles.Helpers;
using Business.CoreFiles.Models.Contacts;  // Import the GuidGenerator helper

namespace Business.CoreFiles.Factory
{
    public class UserCreate
    {
        /// <summary>
        /// Skapar en ny användare och sätter lösenordet.
        /// </summary>
        /// <param name="name">Förnamn på användaren.</param>
        /// <param name="lastname">Efternamn på användaren.</param>
        /// <param name="email">E-postadress på användaren.</param>
        /// <param name="password">Lösenord för användaren.</param>
        /// <param name="role">Användarens roll (t.ex. Admin eller Default).</param>
        /// <returns>Den skapade användaren.</returns>
        public BaseUser CreateUser(string name, string lastname, string email, string password, string role)
        {
            BaseUser user;

            // Generate GUID for the new user
            var userId = GuidGenerator.GenerateGuid();  // Get a new GUID for the user

            // Check the role and create the appropriate user type
            if (role == "Admin")
            {
                user = new Admin  // Create an Admin user
                {
                    Id = userId,  // Assign the generated GUID as the ID
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),  // Initialize Contacts list
                    Favorites = new List<FavoriteContact>()  // Initialize Favorites list
                };
            }
            else
            {
                user = new DefaultUser  // Create a DefaultUser if not Admin
                {
                    Id = userId,  // Assign the generated GUID as the ID
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    Contacts = new List<Contact>(),  // Initialize Contacts list
                    Favorites = new List<FavoriteContact>()  // Initialize Favorites list
                };
            }

            // Set the password using the BaseUser method
            user.SetPassword(password);

            return user;  // Return the created user with the GUID, empty lists, and other properties
        }
    }
}
