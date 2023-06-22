using Zoo.Core.Models.PhotosComments;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Photos
{
    public class PhotoDetailsModel : PhotoCommentAddFormModel
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime DateOfPicture { get; set; }

        public string? Location { get; set; }

        public bool IsAuthor { get; set; }

        public Guid PhotoStatusId { get; set; }

        public PhotoStatus PhotoStatus { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public Guid VehicleId { get; set; }

        public Animal Vehicle { get; set; }

        public bool IsApproved { get; set; }

        public string ImgUrlFromDatabase { get; set; }

        public ICollection<PhotoCommentsListingModel> PhotoComments { get; set; }
    }
}
