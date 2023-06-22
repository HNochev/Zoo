using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Admin
{
    public class AdminPendingPhotosPaginationModel
    {
        public int PageNo { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public List<AdminAllPendingPhotosModel> AllPendingPhotos { get; set; }
    }
}
