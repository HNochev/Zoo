using Zoo.Core.Contracts;
using Zoo.Core.Models.Users;
using Zoo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Zoo.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string IdByUser(string userId)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .First();
        }

        public UserDetailsModel UserDetails(string id)
        {
            return this.data.WebsiteUsers
                .Include(x => x.BuyedTickets)
                .Where(x => x.Id == id)
                .Select(x => new UserDetailsModel
                {
                    Id = id,
                    Email = x.Email,
                    Username = x.UserName,
                    Role = this.data.Roles.First(x => x.Id == this.data.UserRoles.First(x => x.UserId == id).RoleId).Name,
                    Photos = x.Photos,
                    PhotoComments = x.PhotoComments,
                    NewsComments = x.NewsComments,
                    BuyedTickets = this.data.BuyedTickets
                    .Include(x => x.Ticket)
                    .Where(x => x.UserId == id)
                    .OrderBy(x => x.Date < DateTime.Today)
                    .ThenBy(x => x.Date)
                    .ToList(),
                })
                .First();
        }

        public int UserPendingPhotosCount(string id)
        {
            return this.data.Photos
                .Where(x => x.PhotoStatus.ClassColor == "table-warning" && x.UserId == id)
                .Count();
        }
    }
}
