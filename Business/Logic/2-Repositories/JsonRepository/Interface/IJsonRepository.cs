using Business.CoreFiles.Models.Users;
using System.Collections.Generic;

namespace Business._2_Repositories.JsonRepository.Interface
{
    /// <summary>
    /// Interface för att hantera CRUD-operationer för användare i JSON-datalagret.
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
    }
}
