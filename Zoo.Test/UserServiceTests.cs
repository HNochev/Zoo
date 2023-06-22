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
    public class UserServiceTests
    {
        [Fact]
        public void IdByUserShouldReturnUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IdByUserShouldReturnTheUser").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1" };


            dbContext.Add(user);
            dbContext.SaveChanges();

            var service = new UserService(dbContext);

            Assert.Equal(user.Id, service.IdByUser(user.Id));
        }

        [Fact]
        public void DetailsShouldReturnUserDetails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DetailsShouldReturnUserDetails").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1", Email = "aaa@abv.bg", UserName = "Gtsofia", };

            dbContext.Add(user);
            dbContext.SaveChanges();

            var service = new UserService(dbContext);
            var details = dbContext.WebsiteUsers.Where(x => x.Id == user.Id).First();

            Assert.Equal(user.Id, details.Id);
            Assert.Equal("aaa@abv.bg", details.Email);
            Assert.Equal("Gtsofia", details.UserName);
        }

        [Fact]
        public void DefaultUserPendingPhotoCountShouldBeZero()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DefaultUserPendingPhotoCountShouldBeZero").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1", Email = "aaa@abv.bg", UserName = "Gtsofia", };

            dbContext.Add(user);
            dbContext.SaveChanges();

            var service = new UserService(dbContext);

            Assert.Equal(0, service.UserPendingPhotosCount(user.Id));
        }
    }
}
