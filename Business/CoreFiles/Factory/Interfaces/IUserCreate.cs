using Business.CoreFiles.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Factory.Interfaces
{
    public interface IUserCreate
    {
        public BaseUser CreateUser(string name, string lastname, string email, string password, string role);
    }
}
