using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Admin
{
    public class AdminPayTicketModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Count { get; set; }

        public DateTime Date { get; set; }

        public int UniqueCode { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public bool Payed { get; set; }

        public DateTime? PayedOn { get; set; }
    }
}
