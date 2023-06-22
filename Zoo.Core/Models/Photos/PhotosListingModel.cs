using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Photos
{
    public class PhotosListingModel
    {
        public Guid Id { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime DateOfPicture { get; set; }

        public string? Location { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public string ImgUrlFormDatabase { get; set; }
    }
}
