using Zoo.Core.Contracts;
using Zoo.Core.Models.Tickets;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext data;

        public TicketService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<TicketsListingModel> All()
        {
            return this.data
                 .Tickets
                 .Where(x => !x.IsDeleted)
                 .Select(x => new TicketsListingModel
                 {
                     Id = x.Id,
                     Type = x.Type,
                     Price = x.Price,
                 })
                 .ToList();
        }

        public List<BuyedTicket> AllActiveBuyedTicketsByUser(string userId)
        {
            return this.data
                 .BuyedTickets
                 .Where(x => x.UserId == userId && x.Date >= DateTime.Today && x.Payed == false)
                 .Select(x => new BuyedTicket
                 {
                     Id = x.Id,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     Date = x.Date,
                     Count = x.Count,
                     Ticket = x.Ticket,
                     UniqueCode = x.UniqueCode,
                     UserId = userId,
                 })
                 .OrderByDescending(x => x.Date)
                 .ToList();
        }

        public Guid CreateCard(string type, decimal price, bool isDeleted)
        {
            var newTicket = new Ticket
            {
                Type = type,
                IsDeleted = isDeleted,
                Price = price,
            };

            this.data.Tickets.Add(newTicket);
            this.data.SaveChanges();

            return newTicket.Id;
        }

        public TicketEditFormModel EditViewData(Guid id)
        {
            return this.data.Tickets
                .Where(x => x.Id == id)
                .Select(x => new TicketEditFormModel
                {
                    Type = x.Type,
                })
                .First();
        }

        public bool Edit(Guid id, string type)
        {
            var cardData = this.data.Tickets.Find(id);

            if (cardData == null)
            {
                return false;
            }

            cardData.Type = type;

            this.data.SaveChanges();

            return true;
        }

        public TicketDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Tickets
                .Where(x => x.Id == id)
                .Select(x => new TicketDeleteModel
                {
                    Type = x.Type,
                    Price = x.Price,
                })
                .First();
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var cardData = this.data.Tickets.Find(id);

            if (cardData == null)
            {
                return false;
            }

            cardData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public Ticket GetTicket(Guid id)
        {
            return this.data.Tickets
                .Where(x => x.Id == id)
                .First();
        } 
        
        public BuyedTicket GetBuyedTicket(Guid id)
        {
            return this.data.BuyedTickets
                .Where(x => x.Id == id)
                .First();
        }

        public bool Order(string userId, Guid ticketId, string firstName, string lastName, DateTime date, int count)
        {
            if ((date - DateTime.Today).Days < 0)
            {
                return false;
            }

            if (this.data.BuyedTickets.Where(x => x.UserId == userId && x.Date >= DateTime.Today).Count() > 5)
            {
                return false;
            }

            var newBuyedTicket = new BuyedTicket
            {
                FirstName = firstName,
                LastName = lastName,
                Date = date,
                Count = count,
                TicketId = ticketId,
                UserId = userId,
                Payed = false,
                UniqueCode = GenerateUniqueCode(),
            };

            this.data.BuyedTickets.Add(newBuyedTicket);
            this.data.SaveChanges();

            return true;
        }

        public bool RejectTicket(string userId, Guid ticketId)
        {
            if (userId == null)
            {
                return false;
            }

            var ticket = this.data.BuyedTickets.FirstOrDefault(t => t.UserId == userId && t.Id == ticketId);

            if (ticket == null || ticket.Payed)
            {
                return false;
            }

            this.data.BuyedTickets.Remove(ticket);
            this.data.SaveChanges();

            return true;
        }

        private int GenerateUniqueCode()
        {
            var random = new Random();
            int uniqueCode = random.Next(1000, 9999);

            while (IsCodeUsed(uniqueCode))
            {
                uniqueCode = random.Next(1000, 9999);
            }

            return uniqueCode;
        }

        private bool IsCodeUsed(int code)
        {
            return this.data.BuyedTickets.Where(x => x.Date >= DateTime.Today).Any(x => x.UniqueCode == code);
        }
    }
}
