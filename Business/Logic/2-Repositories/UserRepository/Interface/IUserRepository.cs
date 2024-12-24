using Business.CoreFiles.Models.Users;

namespace Business.Interfaces.IUser
{
    /// <summary>
    /// Gränssnitt för att hantera CRUD-operationer och specifika funktioner för användare.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Sparar en användare i databasen eller uppdaterar en befintlig användare.
        /// </summary>
        /// <param name="user">Användaren som ska sparas.</param>
        void SaveUser(BaseUser user);

        /// <summary>
        /// Hämtar en användare baserat på användar-ID.
        /// </summary>
        /// <param name="userId">Användarens unika ID.</param>
        /// <returns>Den användare som matchar det angivna ID:t.</returns>
        BaseUser GetUser(string userId);

        /// <summary>
        /// Tar bort en specifik användare från databasen.
        /// </summary>
        /// <param name="user">Användaren som ska tas bort.</param>
        void DeleteUser(BaseUser user);

        /// <summary>
        /// Hämtar en användare baserat på e-postadress.
        /// </summary>
        /// <param name="email">Användarens e-postadress.</param>
        /// <returns>Den användare som matchar den angivna e-postadressen.</returns>
        BaseUser GetUserByEmail(string email);

        /// <summary>
        /// Hämtar alla användare från databasen.
        /// </summary>
        /// <returns>En lista med alla användare.</returns>
        List<BaseUser> GetAllUsers();

        /// <summary>
        /// Säkerställer att en exempelanvändare finns i databasen.
        /// Om exempelanvändaren inte finns, skapas en ny.
        /// </summary>
        void EnsureExampleUserExists();
    }
}
