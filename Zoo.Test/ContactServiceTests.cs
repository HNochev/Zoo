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
    public class ContactServiceTests
    {
        [Fact]
        public void GetAllContactsReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllContactsReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Contacts.Add(new Contact { FullName = "New User", IsDeleted = false, Email = "Example@abv.bg", PhoneNumber = "089934242", Position = "Director", UsernameInWebsite = "NewUser", UserId = "1" });
            dbContext.Contacts.Add(new Contact { FullName = "New User 1", IsDeleted = false, Email = "example2@abv.bg", PhoneNumber = "0899342242", Position = "Tech expert", UsernameInWebsite = null, UserId = "2" });
            dbContext.SaveChanges();
            var service = new ContactService(dbContext);

            Assert.Equal(2, service.All().Count);
        }

        [Fact]
        public void DeleteReduceCountOfContacts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfContacts").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Contacts.Add(new Contact { Id = firstAddGuid, FullName = "New User", IsDeleted = false, Email = "Example@abv.bg", PhoneNumber = "089934242", Position = "Director", UsernameInWebsite = "NewUser", UserId = "1", User = new WebsiteUser { } });
            dbContext.Contacts.Add(new Contact { Id = Guid.NewGuid(), FullName = "New User 1", IsDeleted = false, Email = "example2@abv.bg", PhoneNumber = "0899342242", Position = "Tech expert", UsernameInWebsite = null, UserId = "2", User = new WebsiteUser { } });
            dbContext.SaveChanges();
            var service = new ContactService(dbContext);

            service.Delete(firstAddGuid, true);
            Assert.Single(service.All());
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Contacts.Add(new Contact { Id = firstAddGuid, FullName = "New User", IsDeleted = false, Email = "Example@abv.bg", PhoneNumber = "089934242", Position = "Director", UsernameInWebsite = "NewUser", UserId = "1", User = new WebsiteUser { } });
            dbContext.Contacts.Add(new Contact { Id = Guid.NewGuid(), FullName = "New User 1", IsDeleted = false, Email = "example2@abv.bg", PhoneNumber = "0899342242", Position = "Tech expert", UsernameInWebsite = null, UserId = "2", User = new WebsiteUser { } });
            dbContext.SaveChanges();
            var service = new ContactService(dbContext);

            var exception = Record.Exception(() => service.Delete(Guid.NewGuid(), true));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Contacts.Add(new Contact { Id = firstAddGuid, FullName = "New User", IsDeleted = false, Email = "Example@abv.bg", PhoneNumber = "089934242", Position = "Director", UsernameInWebsite = "NewUser", UserId = "1", User = new WebsiteUser { } });
            dbContext.Contacts.Add(new Contact { Id = Guid.NewGuid(), FullName = "New User 1", IsDeleted = false, Email = "example2@abv.bg", PhoneNumber = "0899342242", Position = "Tech expert", UsernameInWebsite = null, UserId = "2", User = new WebsiteUser { } });
            dbContext.SaveChanges();
            var service = new ContactService(dbContext);

            service.Edit(firstAddGuid, "newFName", "newEmail", "newPNumber", "newPos", null);
            var editedData = service.EditViewData(firstAddGuid);
            Assert.Equal("newFName", editedData.FullName);
            Assert.Equal("newEmail", editedData.Email);
            Assert.Equal("newPNumber", editedData.PhoneNumber);
            Assert.Equal("newPos", editedData.Position);
            Assert.Null(editedData.UsernameInWebsite);
        }

        [Fact]
        public void CreateContactReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateNewsReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();
            var service = new ContactService(dbContext);

            var id = service.CreateContact("Ädam Newman", "aaa@abv.bg", "000000000", "Direct", "Eleven", "1", false);

            Assert.NotNull(id);
        }
    }
}
