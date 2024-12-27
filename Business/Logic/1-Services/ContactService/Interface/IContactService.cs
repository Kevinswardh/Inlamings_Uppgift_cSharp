using Business.CoreFiles.Models.Contacts;
using System.Collections.Generic;

namespace Business.Logic._1_Services.UserService.Interface
{
    /// <summary>
    /// Gränssnitt för hantering av kontakter och favoritkontakter för en användare.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Skapar en ny kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contact">Den kontakt som ska skapas.</param>
        void CreateContact(string userId, Contact contact);

        /// <summary>
        /// Hämtar alla kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med alla kontakter för användaren.</returns>
        List<Contact> GetAllContacts(string userId);

        /// <summary>
        /// Uppdaterar en befintlig kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contact">Den kontakt som ska uppdateras.</param>
        void UpdateContact(string userId, Contact contact);

        /// <summary>
        /// Tar bort en kontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contactId">ID för den kontakt som ska tas bort.</param>
        void DeleteContact(string userId, string contactId);

        /// <summary>
        /// Lägger till en kontakt som favorit för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favoriteContact">Den kontakt som ska läggas till som favorit.</param>
        void AddFavorite(string userId, FavoriteContact favoriteContact);


        /// <summary>
        /// Uppdaterar en favoritkontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favoriteContact">Den favoritkontakt som ska uppdateras.</param>
        void UpdateFavorite(string userId, FavoriteContact favoriteContact);


        /// <summary>
        /// Hämtar alla favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med alla favoritkontakter för användaren.</returns>
        List<FavoriteContact> GetAllFavorites(string userId);

        /// <summary>
        /// Tar bort en favoritkontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favoriteId">ID för den favoritkontakt som ska tas bort.</param>
        void RemoveFavorite(string userId, string favoriteId);
    }
}
