using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Photos
{
    public class PhotoDeleteModel
    {
        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string? UserMessage { get; set; }

        public DateTime UploadedOn { get; set; }

        public bool IsApproved { get; set; }

        public string AnimalKind { get; set; }
    }
}
