using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.Tickets
{
    public class TicketPageModel
    {
        public IEnumerable<TicketsListingModel> Tickets { get; set; }

        public IEnumerable<BuyedTicket> BuyedTickets { get; set; }
    }
}
