using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business._2_Repositories.JsonRepository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._2_Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IJsonRepository _jsonRepository;

        public ContactRepository(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        public List<Contact> ReadContactsForUser(string userId)
        {
            var user = _jsonRepository.Get(userId);
            return user?.Contacts ?? new List<Contact>();
        }

        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            var user = _jsonRepository.Get(userId);
            if (user != null)
            {
                user.Contacts = contacts;
                _jsonRepository.Update(user);
            }
        }

        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            var user = _jsonRepository.Get(userId);
            return user?.Favorites ?? new List<FavoriteContact>();
        }

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
