using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Users
{
    public class UserDetailsModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<PhotoComment> PhotoComments { get; set; }

        public ICollection<BuyedTicket> BuyedTickets { get; set; }
    }
}
