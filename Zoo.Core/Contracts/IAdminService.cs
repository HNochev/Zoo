using Zoo.Core.Models.Admin;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Contracts
{
    public interface IAdminService
    {
        public int PendingPhotosCount();

        public AdminPendingPhotosPaginationModel AllPendingPhotos(int pageNo, int pageSize);

        public AdminApproveDisapprovePhotoModel ApproveDisapproveViewData(Guid id);

        public bool Approve(Guid id, string? adminMessage);

        public bool DisApprove(Guid id, string? adminMessage);

        public AdminPendingTicketsPaginationModel AllPendingTickets(int pageNo, int pageSize);

        public bool RejectTicket(string userId, Guid ticketId);

        public AdminPayTicketModel TicketPayViewData(Guid id);

        public bool PayTicket(Guid id);
    }
}
