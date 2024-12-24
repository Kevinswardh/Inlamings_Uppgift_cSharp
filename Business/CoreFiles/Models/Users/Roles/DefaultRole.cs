using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users.Roles
{
    /// <summary>
    /// Representerar en standardanvändare, ärver från BaseUser.
    /// </summary>
    public class DefaultUser : BaseUser
    {
        /// <summary>
        /// Anger rollen för användaren som "Default".
        /// </summary>
        public override string Role => "Default";

        /// <summary>
        /// Anger användartypen som "Standard User".
        /// </summary>
        public override string UserType => "Standard User";
    }
}
