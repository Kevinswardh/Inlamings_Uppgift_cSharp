using Business._2_Repositories.JsonRepository.Interface;
using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._2_Repositories
{
    /// <summary>
    /// Repository för att hantera användare i en JSON-fil.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly JsonRepository _jsonRepository;

        /// <summary>
        /// Konstruktor som tar emot en instans av JsonRepository via Dependency Injection.
        /// </summary>
        /// <param name="jsonRepository">Instans av IJsonRepository för att läsa och skriva användardata.</param>
        public UserRepository(IJsonRepository jsonRepository)
        {
            _jsonRepository = (JsonRepository)jsonRepository;
        }

        /// <summary>
        /// Sparar en användare i JSON-filen. Om användaren redan finns, uppdateras den.
        /// </summary>
        /// <param name="user">Användaren som ska sparas.</param>
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
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>Den användare som matchar det angivna ID:t.</returns>
        public BaseUser GetUser(string userId)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Tar bort en specifik användare från JSON-filen.
        /// </summary>
        /// <param name="user">Användaren som ska tas bort.</param>
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
        /// Hämtar en användare baserat på e-postadress från JSON-filen.
        /// </summary>
        /// <param name="email">Användarens e-postadress.</param>
        /// <returns>Den användare som matchar den angivna e-postadressen.</returns>
        public BaseUser GetUserByEmail(string email)
        {
            var users = _jsonRepository.ReadAll<BaseUser>();
            return users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Hämtar alla användare från JSON-filen.
        /// </summary>
        /// <returns>En lista med alla användare.</returns>
        public List<BaseUser> GetAllUsers()
        {
            return _jsonRepository.ReadAll<BaseUser>();
        }

        /// <summary>
        /// Säkerställer att en exempelanvändare finns i JSON-filen.
        /// Om exempelanvändaren inte finns, skapas en ny.
        /// </summary>
        public void EnsureExampleUserExists()
        {
            _jsonRepository.EnsureExampleUserExists();
        }
    }
}
