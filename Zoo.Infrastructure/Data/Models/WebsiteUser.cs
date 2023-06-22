using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class WebsiteUser : IdentityUser
    {
        public WebsiteUser()
        {
            this.NewsComments = new HashSet<NewsComment>();
            this.Photos = new HashSet<Photo>();
            this.PhotoComments = new HashSet<PhotoComment>();
            this.BuyedTickets = new HashSet<BuyedTicket>();
        }
        public DateTime RegisteredOn { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<PhotoComment> PhotoComments { get; set; }

        public ICollection<BuyedTicket> BuyedTickets { get; set; }
    }
}
