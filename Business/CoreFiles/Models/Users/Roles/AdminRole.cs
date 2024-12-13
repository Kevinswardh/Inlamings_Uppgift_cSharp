using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users.Roles
{
    public class Admin : BaseUser
    {
        public override string Role => "Admin";
        public override string UserType => "Administrator";
    }
}
