using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Admin
{
    public class AdminApproveDisapprovePhotoModel
    {
        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string? UserMessage { get; set; }

        [StringLength(500, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Съобщение към потребителя публикувал снимката")]
        public string? AdminMessage { get; set; }

        public DateTime UploadedOn { get; set; }
    }
}
