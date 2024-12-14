using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business.Logic._1_Services.UserService.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._1_Services.UserService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void CreateContact(string userId, Contact contact)
        {
            var contacts = _contactRepository.ReadContactsForUser(userId);
            contacts.Add(contact);
            _contactRepository.WriteContactsForUser(userId, contacts);
        }

        public List<Contact> GetAllContacts(string userId)
        {
            return _contactRepository.ReadContactsForUser(userId);
        }

        public void UpdateContact(string userId, Contact contact)
        {
            var contacts = _contactRepository.ReadContactsForUser(userId);
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);

            if (existingContact != null)
            {
                contacts.Remove(existingContact);
                contacts.Add(contact);
                _contactRepository.WriteContactsForUser(userId, contacts);
            }
        }

        public void DeleteContact(string userId, int contactId)
        {
            var contacts = _contactRepository.ReadContactsForUser(userId);
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == contactId);

            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                _contactRepository.WriteContactsForUser(userId, contacts);
            }
        }

        public void AddFavorite(string userId, FavoriteContact favoriteContact)
        {
            var favorites = _contactRepository.ReadFavoritesForUser(userId);
            favorites.Add(favoriteContact);
            _contactRepository.WriteFavoritesForUser(userId, favorites);
        }

        public List<FavoriteContact> GetAllFavorites(string userId)
        {
            return _contactRepository.ReadFavoritesForUser(userId);
        }

        public void RemoveFavorite(string userId, int favoriteId)
        {
            var favorites = _contactRepository.ReadFavoritesForUser(userId);
            var favoriteToRemove = favorites.FirstOrDefault(f => f.Id == favoriteId);

            if (favoriteToRemove != null)
            {
                favorites.Remove(favoriteToRemove);
                _contactRepository.WriteFavoritesForUser(userId, favorites);
            }
        }
    }
}
