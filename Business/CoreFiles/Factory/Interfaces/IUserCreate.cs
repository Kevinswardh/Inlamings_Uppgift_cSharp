using Business.CoreFiles.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Factory.Interfaces
{
    /// <summary>
    /// Ett interface för att skapa användare med angivna egenskaper.
    /// </summary>
    public interface IUserCreate
    {
        /// <summary>
        /// Skapar en ny användare med förnamn, efternamn, e-post, lösenord och roll.
        /// </summary>
        /// <param name="name">Förnamn på användaren.</param>
        /// <param name="lastname">Efternamn på användaren.</param>
        /// <param name="email">E-postadress för användaren.</param>
        /// <param name="password">Lösenord för användaren.</param>
        /// <param name="role">Användarens roll, t.ex. "Admin" eller "Default".</param>
        /// <returns>En instans av <see cref="BaseUser"/> som representerar den skapade användaren.</returns>
        public BaseUser CreateUser(string name, string lastname, string email, string password, string role);
    }
}
