using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.Tickets
{
    public class TicketsListingModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }
    }
}
