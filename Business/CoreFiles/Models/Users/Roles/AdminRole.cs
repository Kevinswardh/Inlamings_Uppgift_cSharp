using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users.Roles
{
    /// <summary>
    /// Representerar en administratörsanvändare, ärver från BaseUser.
    /// </summary>
    public class Admin : BaseUser
    {
        /// <summary>
        /// Anger rollen för användaren som "Admin".
        /// </summary>
        public override string Role => "Admin";

        /// <summary>
        /// Anger användartypen som "Administrator".
        /// </summary>
        public override string UserType => "Administrator";
    }
}
