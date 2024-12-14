using Business._2_Repositories.JsonRepository.Interface;
using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._2_Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JsonRepository _jsonRepository;

        // Konstruktor som tar emot JsonRepository via Dependency Injection
        public UserRepository(IJsonRepository jsonRepository)
        {
            _jsonRepository = (JsonRepository)jsonRepository;
        }

        /// <summary>
        /// Sparar användaren i JSON-filen.
        /// </summary>
        public void SaveUser(BaseUser user)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                // Uppdatera användaren om den redan finns
                users.Remove(existingUser);
            }

            users.Add(user);
            _jsonRepository.WriteAll(users);
        }

        /// <summary>
        /// Hämtar en användare baserat på användar-ID från JSON-filen.
        /// </summary>
        public BaseUser GetUser(string userId)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Tar bort en användare från JSON-filen.
        /// </summary>
        public void DeleteUser(BaseUser user)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                users.Remove(existingUser);
                _jsonRepository.WriteAll(users);
            }
        }

        /// <summary>
        /// Hämtar en användare baserat på email från JSON-filen.
        /// </summary>
        public BaseUser GetUserByEmail(string email)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Email == email);
        }
        public List<BaseUser> GetAllUsers()
        {
            return _jsonRepository.ReadAll<BaseUser>();  // Använd ReadAll för att hämta alla användare
        }

        // Checkar Exmaple user
        public void EnsureExampleUserExists()
        {
            _jsonRepository.EnsureExampleUserExists();
        }
    }
}
