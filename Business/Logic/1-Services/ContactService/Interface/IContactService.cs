using Business.CoreFiles.Models.Contacts;
using System.Collections.Generic;

namespace Business.Logic._1_Services.UserService.Interface
{
    public interface IContactService
    {
        void CreateContact(string userId, Contact contact);
        List<Contact> GetAllContacts(string userId);
        void UpdateContact(string userId, Contact contact);
        void DeleteContact(string userId, int contactId);

        void AddFavorite(string userId, FavoriteContact favoriteContact);
        List<FavoriteContact> GetAllFavorites(string userId);
        void RemoveFavorite(string userId, int favoriteId);
    }
}
