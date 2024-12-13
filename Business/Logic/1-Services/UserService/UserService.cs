using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Business.CoreFiles.Factory;  // Import the UserCreate factory
using System;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserCreate _userCreateFactory;  // Inject the factory

        // Constructor to inject the repository and factory
        public UserService(IUserRepository userRepository, UserCreate userCreateFactory)
        {
            _userRepository = userRepository;
            _userCreateFactory = userCreateFactory;
        }

        /// <summary>
        /// Skapar en användare och hashar lösenordet innan användaren sparas.
        /// </summary>
        public void CreateUser(string name, string lastname, string email, string password, string role)
        {
            // Use the factory to create a new user
            var user = _userCreateFactory.CreateUser(name, lastname, email, password, role);

            // Save the user using the repository's SaveUser method
            _userRepository.SaveUser(user);  // Correct method to save user to the repository
    
        }

        /// <summary>
        /// Hämtar en användare från datalagret baserat på användar-ID.
        /// </summary>
        public BaseUser ReadUser(string userId)
        {
            return _userRepository.GetUser(userId);  // Fetch the user by ID
        }

        /// <summary>
        /// Uppdaterar användarens information i repositoryt.
        /// </summary>
        public void UpdateUser(BaseUser user)
        {
            _userRepository.SaveUser(user);  // Update the user in the repository
            Console.WriteLine($"User {user.Fullname} updated.");
        }

        /// <summary>
        /// Tar bort en användare från datalagret.
        /// </summary>
        public void DeleteUser(BaseUser user)
        {
            _userRepository.DeleteUser(user);  // Delete the user from the repository
            Console.WriteLine($"User {user.Fullname} deleted.");
        }
        public BaseUser ReadUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);  // Hämta användare via UserRepository
        }
        public List<BaseUser> ReadAllUsers()
        {
            return _userRepository.GetAllUsers();  // Anropa metod i UserRepository för att hämta alla användare
        }

    }
}
