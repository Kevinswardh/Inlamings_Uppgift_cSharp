using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Contacts;
using System.Collections.Generic;

namespace Business._2_Repositories.JsonRepository.Interface
{
    /// <summary>
    /// Interface för att hantera CRUD-operationer och kontaktbaserade funktioner i JSON-datalagret.
    /// </summary>
    public interface IJsonRepository
    {
        /// <summary>
        /// Skapar en ny användare i datalagret.
        /// </summary>
        /// <param name="user">Den nya användaren som ska skapas.</param>
        void Create(BaseUser user);

        /// <summary>
        /// Hämtar en användare från datalagret baserat på användarens ID.
        /// </summary>
        /// <param name="id">Den unika identifieraren för användaren.</param>
        /// <returns>Användaren som matchar det angivna ID:t, eller null om ingen användare hittas.</returns>
        BaseUser Get(string id);

        /// <summary>
        /// Hämtar alla användare från datalagret.
        /// </summary>
        /// <returns>En lista med alla användare.</returns>
        List<BaseUser> GetAll();

        /// <summary>
        /// Uppdaterar en befintlig användares information i datalagret.
        /// </summary>
        /// <param name="user">Användaren med uppdaterad information.</param>
        void Update(BaseUser user);

        /// <summary>
        /// Tar bort en användare från datalagret baserat på användarens ID.
        /// </summary>
        /// <param name="id">Den unika identifieraren för användaren som ska tas bort.</param>
        void Delete(string id);

        /// <summary>
        /// Hämtar alla kontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Den unika identifieraren för användaren.</param>
        /// <returns>En lista med kontakter kopplade till användaren.</returns>
        List<Contact> ReadContactsForUser(string userId);

        /// <summary>
        /// Uppdaterar kontaktlistan för en specifik användare.
        /// </summary>
        /// <param name="userId">Den unika identifieraren för användaren.</param>
        /// <param name="contacts">Listan med uppdaterade kontakter.</param>
        void WriteContactsForUser(string userId, List<Contact> contacts);

        /// <summary>
        /// Hämtar alla favoritkontakter för en specifik användare.
        /// </summary>
        /// <param name="userId">Den unika identifieraren för användaren.</param>
        /// <returns>En lista med favoritkontakter kopplade till användaren.</returns>
        List<FavoriteContact> ReadFavoritesForUser(string userId);

        /// <summary>
        /// Uppdaterar favoritlistan för en specifik användare.
        /// </summary>
        /// <param name="userId">Den unika identifieraren för användaren.</param>
        /// <param name="favorites">Listan med uppdaterade favoritkontakter.</param>
        void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites);

        /// <summary>
        /// Uppdaterar en enskild favoritkontakt för en specifik användare.
        /// </summary>
        /// <param name="userId">Den unika identifieraren för användaren.</param>
        /// <param name="favoriteContact">Den favoritkontakt som ska uppdateras.</param>
        void UpdateFavorite(string userId, FavoriteContact favoriteContact);

        /// <summary>
        /// Säkerställer att en exempelanvändare finns i JSON-filen.
        /// </summary>
        void EnsureExampleUserExists();
    }
}
