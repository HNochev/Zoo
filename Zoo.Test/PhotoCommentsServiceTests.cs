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
    public class PhotoCommentsServiceTests
    {
        [Fact]
        public void GetAllCommentsForNewsReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllCommentsForPhotoReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);

            var guidId = Guid.NewGuid();
            var photo = new Photo { Id = guidId, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.Photos.Add(photo);

            dbContext.PhotoComments.Add(new PhotoComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), User = new WebsiteUser { }, PhotoId = guidId });
            dbContext.PhotoComments.Add(new PhotoComment { Content = "absolutely different content", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), User = new WebsiteUser { }, PhotoId = guidId });

            dbContext.SaveChanges();
            var service = new PhotoService(dbContext);
            var commentsService = new PhotoCommentsService(dbContext);

            Assert.Equal(2, dbContext.PhotoComments.Where(x => x.PhotoId == guidId).Count());
        }

        [Fact]
        public void DeleteReduceCountOfComments()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfComments").Options;
            var dbContext = new ApplicationDbContext(options);

            var newsToDeleteId = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var photo = new Photo { Id = guidId, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.Photos.Add(photo);

            dbContext.PhotoComments.Add(new PhotoComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = newsToDeleteId, PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });
            dbContext.PhotoComments.Add(new PhotoComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new PhotoCommentsService(dbContext);
            var photoService = new PhotoService(dbContext);

            service.Delete(newsToDeleteId);
            Assert.Equal(1, dbContext.PhotoComments.Where(x => x.PhotoId == guidId).Count());
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var photo = new Photo { Id = guidId, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.PhotoComments.Add(new PhotoComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });
            dbContext.PhotoComments.Add(new PhotoComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new PhotoCommentsService(dbContext);

            var exception = Record.Exception(() => service.Delete(Guid.NewGuid()));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var photo = new Photo { Id = guidId, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.PhotoComments.Add(new PhotoComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = firstAddGuid, PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });
            dbContext.PhotoComments.Add(new PhotoComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), PhotoId = guidId, Photo = photo, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new PhotoCommentsService(dbContext);

            service.Edit(firstAddGuid, "New content", DateTime.Now);
            var editedData = service.EditViewData(firstAddGuid);
            Assert.Equal("New content", editedData.Content);
        }

        [Fact]
        public void CreateCommentReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateNewsReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();
            var service = new PhotoCommentsService(dbContext);

            var id = service.CreatePhotoComment("Hello everyone", DateTime.Now, "1", Guid.NewGuid());

            Assert.NotNull(id);
        }

        [Fact]
        public void IsByUserAndIdByPhotoTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IsByUserAndIdByPhotoTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var photo = new Photo { Id = guidId, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            var user = new WebsiteUser { Id = "1" };

            dbContext.PhotoComments.Add(new PhotoComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = firstAddGuid, PhotoId = guidId, Photo = photo, User = user });

            dbContext.SaveChanges();
            var service = new PhotoCommentsService(dbContext);

            Assert.True(service.IsByUser(firstAddGuid, "1"));
            Assert.Equal(service.IdOfPhoto(firstAddGuid), guidId);
        }
    }
}
