using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Users
{
    public class UserMyPhotosPaginationModel
    {
        public int PageNo { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public List<UserMyPhotosModel> AllMyPhotos { get; set; }
    }
}
