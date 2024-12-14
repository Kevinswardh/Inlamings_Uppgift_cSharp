using Business.CoreFiles.Models.Users;
using Business.Logic._2_Repositories;

namespace Business.Interfaces.IUser
{
    public interface IUserService
    {
        /// <summary>
        /// Skapar en användare och hashar lösenordet innan användaren sparas.
        /// </summary>
        void CreateUser(string name, string lastname, string email, string password, string role);

        /// <summary>
        /// Skapar en användare baserat på hela BaseUser objektet, används till example user, Overload.
        /// </summary>
        void CreateUser(BaseUser user);

        /// <summary>
        /// Hämtar en användare från datalagret baserat på användar-ID.
        /// </summary>
        BaseUser ReadUser(string userId);

        /// <summary>
        /// Uppdaterar användarens information i repositoryt.
        /// </summary>
        void UpdateUser(BaseUser user);

        /// <summary>
        /// Tar bort en användare från datalagret.
        /// </summary>
        void DeleteUser(BaseUser user);

        /// <summary>
        /// Hämtar en användare från datalagret baserat på e-postadress.
        /// </summary>
        BaseUser ReadUserByEmail(string email);

        /// <summary>
        /// Hämtar alla användare.
        /// </summary>
        List<BaseUser> ReadAllUsers();

        /// <summary>
        /// Skapar Exempel användare.
        /// </summary>
        void EnsureExampleUserExists();
      
    }
}
