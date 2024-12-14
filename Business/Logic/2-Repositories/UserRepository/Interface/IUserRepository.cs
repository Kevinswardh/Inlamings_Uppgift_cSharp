using Business.CoreFiles.Models.Users;

namespace Business.Interfaces.IUser
{
    public interface IUserRepository
    {
        void SaveUser(BaseUser user);
        BaseUser GetUser(string userId);
        void DeleteUser(BaseUser user);
        BaseUser GetUserByEmail(string email);  // Ny metod för att hämta användare via email

        List<BaseUser> GetAllUsers();

        //Example user
        void EnsureExampleUserExists();
    }
}
