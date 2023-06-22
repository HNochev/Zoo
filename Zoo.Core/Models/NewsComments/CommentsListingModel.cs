using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.NewsComments
{
    public class CommentsListingModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public DateTime? LastEditedOn { get; set; }

        public Guid NewsId { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }
    }
}
