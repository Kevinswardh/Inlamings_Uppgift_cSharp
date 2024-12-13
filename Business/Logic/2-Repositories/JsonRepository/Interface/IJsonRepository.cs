using Business.CoreFiles.Models.Users;
using System.Collections.Generic;

namespace Business._2_Repositories.JsonRepository.Interface
{
    public interface IJsonRepository
    {
        // Method to create a new user
        void Create(BaseUser user);

        // Method to read a user by ID
        BaseUser Get(string id);

        // Method to get all users
        List<BaseUser> GetAll();

        // Method to update an existing user
        void Update(BaseUser user);

        // Method to delete a user by ID
        void Delete(string id);
    }
}
