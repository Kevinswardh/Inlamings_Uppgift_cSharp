using Business.CoreFiles.Models.Contacts;
using System.Collections.Generic;

namespace Business.Interfaces.Repositories
{
    public interface IContactRepository
    {
        List<Contact> ReadContactsForUser(string userId);
        void WriteContactsForUser(string userId, List<Contact> contacts);

        List<FavoriteContact> ReadFavoritesForUser(string userId);
        void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites);
    }
}
