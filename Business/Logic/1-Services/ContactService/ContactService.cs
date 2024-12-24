using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business.Logic._1_Services.UserService.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Business.Logic._1_Services.UserService
{
    /// <summary>
    /// Tjänst för att hantera kontakter och favoritkontakter för användare.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        /// <summary>
        /// Konstruktor som injicerar kontakt-repository.
        /// </summary>
        /// <param name="contactRepository">Repository för att hantera kontakter.</param>
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// Skapar en ny kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contact">Den kontakt som ska skapas.</param>
        public void CreateContact(string userId, Contact contact)
        {
            var contacts = _contactRepository.ReadContactsForUser(userId);
            contacts.Add(contact);
            _contactRepository.WriteContactsForUser(userId, contacts);
        }

        /// <summary>
        /// Hämtar alla kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med alla kontakter för användaren.</returns>
        public List<Contact> GetAllContacts(string userId)
        {
            return _contactRepository.ReadContactsForUser(userId);
        }

        /// <summary>
        /// Uppdaterar en befintlig kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contact">Den kontakt som ska uppdateras.</param>
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

        /// <summary>
        /// Tar bort en kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contactId">ID för den kontakt som ska tas bort.</param>
        public void DeleteContact(string userId, string contactId)
        {
            var contacts = _contactRepository.ReadContactsForUser(userId);
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == contactId);

            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                _contactRepository.WriteContactsForUser(userId, contacts);
            }
        }

        /// <summary>
        /// Lägger till en kontakt som favorit för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favoriteContact">Den kontakt som ska läggas till som favorit.</param>
        public void AddFavorite(string userId, FavoriteContact favoriteContact)
        {
            var favorites = _contactRepository.ReadFavoritesForUser(userId);
            favorites.Add(favoriteContact);
            _contactRepository.WriteFavoritesForUser(userId, favorites);
        }

        /// <summary>
        /// Hämtar alla favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med alla favoritkontakter för användaren.</returns>
        public List<FavoriteContact> GetAllFavorites(string userId)
        {
            return _contactRepository.ReadFavoritesForUser(userId);
        }

        /// <summary>
        /// Tar bort en favoritkontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favoriteId">ID för den favoritkontakt som ska tas bort.</param>
        public void RemoveFavorite(string userId, string favoriteId)
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
