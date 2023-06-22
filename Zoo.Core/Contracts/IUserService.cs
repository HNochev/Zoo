using Zoo.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Contracts
{
    public interface IUserService
    {
        public string IdByUser(string userId);

        public UserDetailsModel UserDetails(string id);

        public int UserPendingPhotosCount(string id);
    }
}
