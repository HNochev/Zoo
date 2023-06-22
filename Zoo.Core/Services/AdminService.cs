using Zoo.Core.Contracts;
using Zoo.Core.Models.Admin;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext data;

        public AdminService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int PendingPhotosCount()
        {
            return this.data.Photos.Where(x => x.PhotoStatus.ClassColor == "table-warning").Count();
        }

        public AdminPendingPhotosPaginationModel AllPendingPhotos(int pageNo, int pageSize)
        {
            AdminPendingPhotosPaginationModel result = new AdminPendingPhotosPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.Photos.Where(x => x.PhotoStatus.ClassColor == "table-warning").Count();
            result.AllPendingPhotos = this.data.Photos
                .Where(x => x.PhotoStatus.ClassColor == "table-warning")
                .Select(x => new AdminAllPendingPhotosModel
                {
                    Id = x.Id,
                    UserMessage = x.UserMessage,
                    UserId = x.UserId,
                    User = x.User,
                    DateOfPicture = x.DateOfPicture,
                    DateUploaded = x.DateUploaded,
                    Location = x.Location,
                    Description = x.Description,
                    IsAuthor = x.IsAuthor,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                })
                 .OrderBy(x => x.DateUploaded)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public AdminApproveDisapprovePhotoModel ApproveDisapproveViewData(Guid id)
        {
            return this.data.Photos
                .Where(x => x.Id == id)
                .Select(x => new AdminApproveDisapprovePhotoModel
                {
                    AdminMessage = x.AdminMessage,
                    UploadedOn = x.DateUploaded,
                    UserId = x.UserId,
                    User = x.User,
                    UserMessage = x.UserMessage,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                })
                .First();
        }

        public bool Approve(Guid id, string? adminMessage)
        {
            var photoData = this.data.Photos.Find(id);

            if (photoData == null)
            {
                return false;
            }

            photoData.AdminMessage = adminMessage;
            photoData.IsApproved = true;
            photoData.PhotoStatusId = this.data.PhotoStatuses.First(x => x.ClassColor == "table-success").Id;
            photoData.PhotoStatus = this.data.PhotoStatuses.First(x => x.ClassColor == "table-success");

            this.data.SaveChanges();

            return true;
        }

        public bool DisApprove(Guid id, string? adminMessage)
        {
            var photoData = this.data.Photos.Find(id);

            if (photoData == null)
            {
                return false;
            }

            photoData.AdminMessage = adminMessage;
            photoData.IsApproved = false;
            photoData.PhotoStatusId = this.data.PhotoStatuses.First(x => x.ClassColor == "table-danger").Id;
            photoData.PhotoStatus = this.data.PhotoStatuses.First(x => x.ClassColor == "table-danger");

            this.data.SaveChanges();

            return true;
        }

        public AdminPendingTicketsPaginationModel AllPendingTickets(int pageNo, int pageSize)
        {
            AdminPendingTicketsPaginationModel result = new AdminPendingTicketsPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.BuyedTickets.Count();
            result.AllPendingTickets = this.data.BuyedTickets
                .Select(x => new AdminAllPendingTicketsModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Count = x.Count,
                    Date = x.Date,
                    UniqueCode = x.UniqueCode,
                    Payed = x.Payed,
                    TicketId = x.TicketId,
                    UserId = x.UserId,
                    Ticket = x.Ticket,
                    User = x.User,
                })
                 .OrderBy(x => x.Date < DateTime.Today)
                 .ThenBy(x => x.Date)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            result.AllPendingTicketsWithoutPagination = this.data.BuyedTickets
                .Select(x => new AdminAllPendingTicketsModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Count = x.Count,
                    Date = x.Date,
                    UniqueCode = x.UniqueCode,
                    Payed = x.Payed,
                    TicketId = x.TicketId,
                    UserId = x.UserId,
                    Ticket = x.Ticket,
                    User = x.User,
                })
                 .OrderBy(x => x.Date < DateTime.Today)
                 .ThenBy(x => x.Date)
                 .ToList();

            return result;
        }

        public bool RejectTicket(string userId, Guid ticketId)
        {
            if (userId == null)
            {
                return false;
            }

            var ticket = this.data.BuyedTickets.FirstOrDefault(t => t.Id == ticketId);

            if (ticket == null || ticket.Date >= DateTime.Today || ticket.Payed == true)
            {
                return false;
            }

            this.data.BuyedTickets.Remove(ticket);
            this.data.SaveChanges();

            return true;
        }

        public AdminPayTicketModel TicketPayViewData(Guid id)
        {
            return this.data.BuyedTickets
                .Where(x => x.Id == id)
                .Select(x => new AdminPayTicketModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Count = x.Count,
                    Date = x.Date,
                    Payed = x.Payed,
                    PayedOn = x.PayedOn,
                    UniqueCode = x.UniqueCode,
                    TicketId = x.TicketId,
                    UserId = x.UserId,
                    Ticket = x.Ticket,
                    User = x.User,
                })
                .First();
        }

        public bool PayTicket(Guid id)
        {
            var ticket = this.data.BuyedTickets.Find(id);

            if (ticket == null || ticket.Date < DateTime.Today || ticket.Payed == true)
            {
                return false;
            }

            ticket.Payed = true;
            ticket.PayedOn = DateTime.Now;

            this.data.SaveChanges();

            return true;
        }
    }
}
