using Zoo.Core.Models.Tickets;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Contracts
{
    public interface ITicketService
    {
        public List<TicketsListingModel> All();

        public Guid CreateCard(string type, decimal price, bool isDeleted);

        public TicketEditFormModel EditViewData(Guid id);

        public bool Edit(Guid id, string type);

        public TicketDeleteModel DeleteViewData(Guid id);

        public bool Delete(Guid id, bool isDeleted);

        public Ticket GetTicket(Guid id);

        public BuyedTicket GetBuyedTicket(Guid id);

        public bool Order(string userId, Guid ticketId, string firstName, string lastName, DateTime date, int count);

        public bool RejectTicket(string userId, Guid ticketId);

        public List<BuyedTicket> AllActiveBuyedTicketsByUser(string userId);
    }
}
