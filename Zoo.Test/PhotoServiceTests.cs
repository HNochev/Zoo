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
    public class PhotoServiceTests
    {
        [Fact]
        public void GetAllPhotosByUserReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllPhotosByUserReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1" };
            dbContext.Users.Add(user);

            dbContext.Photos.Add(new Photo { Id = Guid.NewGuid(), UserId = user.Id, User = user, PhotoFile = new byte[] { } });
            dbContext.Photos.Add(new Photo { Id = Guid.NewGuid(), UserId = user.Id, User = user, PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);

            Assert.Equal(2, dbContext.Photos.Where(x => x.UserId == user.Id).Count());
        }

        [Fact]
        public void DeleteReduceCountOfPhotos()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfPhotos").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var user = new WebsiteUser { Id = "1" };
            dbContext.Users.Add(user);

            dbContext.Photos.Add(new Photo { Id = firstAddGuid, UserId = user.Id, User = user, PhotoFile = new byte[] { } });
            dbContext.Photos.Add(new Photo { Id = Guid.NewGuid(), UserId = user.Id, User = user, PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);

            service.Delete(firstAddGuid);
            Assert.Single(dbContext.Photos.Where(x => x.UserId == user.Id));
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new WebsiteUser { Id = "1" };
            dbContext.Users.Add(user);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Photos.Add(new Photo { Id = firstAddGuid, UserId = user.Id, User = user, PhotoFile = new byte[] { } });
            dbContext.Photos.Add(new Photo { Id = Guid.NewGuid(), UserId = user.Id, User = user, PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            var exception = Record.Exception(() => service.Delete(Guid.NewGuid(), true));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Photos.Add(new Photo { Id = firstAddGuid, UserId = "1", User = new WebsiteUser { }, PhotoFile = new byte[] { } });
            dbContext.Photos.Add(new Photo { Id = Guid.NewGuid(), UserId = "2", User = new WebsiteUser { }, PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);

            service.Edit(firstAddGuid, "new Desc", DateTime.Now, false, "Haskovo", null);
            var editedData = service.EditViewData(firstAddGuid);
            Assert.Equal("new Desc", editedData.Description);
            Assert.False(editedData.IsAuthor);
            Assert.Equal("Haskovo", editedData.Location);
            Assert.Null(editedData.UserMessage);
        }

        [Fact]
        public void CreatePhotoReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreatePhotoReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);

            var id = service.CreatePhoto("Desc", DateTime.Now, DateTime.UtcNow, "new loc", false, "new user message", new byte[] { }, Guid.NewGuid(), "1", Guid.NewGuid(), true);

            Assert.NotNull(id);
        }

        [Fact]
        public void IsByUserAndIdOfVehicleTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IsByUserAndIdOfVehicleTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var animal = new Animal { Id = guidId, AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { Id = Guid.NewGuid(), ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Animal", PhotoFile = new byte[] { }};

            var user = new WebsiteUser { Id = "1" };

            dbContext.Photos.Add(new Photo { UserId = "1", Id = firstAddGuid, AnimalId = guidId, Animal = animal, User = user, PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);

            Assert.True(service.IsByUser(firstAddGuid, "1"));
            Assert.Equal(service.IdOfVehicle(firstAddGuid), guidId);
        }
    }
}
