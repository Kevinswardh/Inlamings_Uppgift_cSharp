using Business.CoreFiles.Models.Contacts;
using System.Collections.Generic;

namespace Business.Interfaces.Repositories
{
    /// <summary>
    /// Interface för att hantera kontakter och favoritkontakter i datalagringslagret.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Läser alla kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med användarens kontakter.</returns>
        List<Contact> ReadContactsForUser(string userId);

        /// <summary>
        /// Skriver uppdaterade kontakter för en specifik användare till datalagret.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="contacts">Listan med kontakter som ska sparas.</param>
        void WriteContactsForUser(string userId, List<Contact> contacts);

        /// <summary>
        /// Läser alla favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>En lista med användarens favoritkontakter.</returns>
        List<FavoriteContact> ReadFavoritesForUser(string userId);

        /// <summary>
        /// Skriver uppdaterade favoritkontakter för en specifik användare till datalagret.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <param name="favorites">Listan med favoritkontakter som ska sparas.</param>
        void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites);
    }
}
