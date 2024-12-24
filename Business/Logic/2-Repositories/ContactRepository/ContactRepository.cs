using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business._2_Repositories.JsonRepository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._2_Repositories
{
    /// <summary>
    /// Repository-klass för att hantera kontakter och favoritkontakter i JSON-datalagret.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly IJsonRepository _jsonRepository;

        /// <summary>
        /// Konstruktor som tar emot ett IJsonRepository för datalagring.
        /// </summary>
        /// <param name="jsonRepository">Instans av IJsonRepository för att hantera läs- och skrivoperationer.</param>
        public ContactRepository(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        /// <summary>
        /// Hämtar alla kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med användarens kontakter.</returns>
        public List<Contact> ReadContactsForUser(string userId)
        {
            var user = _jsonRepository.Get(userId);
            return user?.Contacts ?? new List<Contact>();
        }

        /// <summary>
        /// Sparar en lista med kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contacts">Listan med kontakter som ska sparas.</param>
        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            var user = _jsonRepository.Get(userId);
            if (user != null)
            {
                user.Contacts = contacts;
                _jsonRepository.Update(user);
            }
        }

        /// <summary>
        /// Hämtar alla favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med användarens favoritkontakter.</returns>
        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            var user = _jsonRepository.Get(userId);
            return user?.Favorites ?? new List<FavoriteContact>();
        }

        /// <summary>
        /// Sparar en lista med favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favorites">Listan med favoritkontakter som ska sparas.</param>
        public void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites)
        {
            var user = _jsonRepository.Get(userId);
            if (user != null)
            {
                user.Favorites = favorites;
                _jsonRepository.Update(user);
            }
        }
    }
}
