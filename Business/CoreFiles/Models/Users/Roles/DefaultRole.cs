using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users.Roles
{
    public class DefaultUser : BaseUser
    {
        public override string Role => "Default";
        public override string UserType => "Standard User";
    }

}
