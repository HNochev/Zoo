using Zoo.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Admin
{
    public class AdminSidebarInfoModel
    {
        private readonly IAdminService admins;
        public AdminSidebarInfoModel(IAdminService admins)
        {
            this.admins = admins;
        }
        public int CountPendingPhotos => this.admins.PendingPhotosCount();
    }
}
