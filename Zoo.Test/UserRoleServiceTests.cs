using Microsoft.EntityFrameworkCore;
using Zoo.Core.Services;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Zoo.Test
{
    public class UserRoleServiceTests
    {
        [Fact]
        public void GetUserByIdShouldReturnUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IdByUserShouldReturnThUser").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1" };


            dbContext.Add(user);
            dbContext.SaveChanges();

            var service = new UserRoleService(dbContext);

            Assert.Equal(user.Id, service.GetUserById(user.Id).Result.Id);
        }

        [Fact]
        public void GetTotalCountOfUsersShouldReturnCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetTotalCountOfUsersShouldReturnCount").Options;
            var dbContext = new ApplicationDbContext(options);

            var user1 = new WebsiteUser { Id = "1" };
            var user2 = new WebsiteUser { Id = "2" };
            var user3 = new WebsiteUser { Id = "3" };


            dbContext.Add(user1);
            dbContext.Add(user2);
            dbContext.Add(user3);
            dbContext.SaveChanges();

            var service = new UserRoleService(dbContext);

            Assert.Equal(3, service.GetUsers().Result.Count());
        }

        [Fact]
        public void GetUserForEditShouldEditUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IdByUserShouldReturnUser").Options;
            var dbContext = new ApplicationDbContext(options);

            var user1 = new WebsiteUser { Id = "1" };

            dbContext.Add(user1);
            dbContext.SaveChanges();

            var service = new UserRoleService(dbContext);

            var userForEdit = service.GetUserForEdit(user1.Id);

            Assert.Null(userForEdit.Result.Username);
            Assert.Null(userForEdit.Result.Email);
            Assert.NotNull(userForEdit.Result.Id);
        }
    }
}
