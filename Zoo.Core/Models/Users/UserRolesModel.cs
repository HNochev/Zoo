using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Users
{
    public class UserRolesModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string[] RoleNames { get; set; }
    }
}
