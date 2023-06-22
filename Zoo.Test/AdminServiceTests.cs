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
    public class AdminServiceTests
    {
        [Fact]
        public void PendingPhotosCountShouldZeroIfNoPendingPhotos()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("PendingPhotosCountShouldZeroIfNoPendingPhotos").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();

            var service = new AdminService(dbContext);

            Assert.Equal(0, service.PendingPhotosCount());
        }

        [Fact]
        public void PendingPhotosCountShouldOneIfThereIsOnePendingPhoto()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("PendingPhotosCountShouldOneIfThereIsOnePendingPhoto").Options;
            var dbContext = new ApplicationDbContext(options);

            var pendingPhoto = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1" };

            dbContext.Add(pendingPhoto);
            dbContext.SaveChanges();

            var service = new AdminService(dbContext);

            Assert.Equal(1, service.PendingPhotosCount());
        }

        [Fact]
        public void AllPendingPhotosShouldBeEqualWithPendingPhotoCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AllPendingPhotosShouldBeEqualWithPendingPhotoCount").Options;
            var dbContext = new ApplicationDbContext(options);

            var pendingPhoto1 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };
            var pendingPhoto2 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.Add(pendingPhoto1);
            dbContext.Add(pendingPhoto2);
            dbContext.SaveChanges();

            var service = new AdminService(dbContext);

            Assert.Equal(service.PendingPhotosCount(), service.AllPendingPhotos(1, 25).AllPendingPhotos.Count());
        }

        [Fact]
        public void AllPendingPhotosShouldBeExactCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AllPendingPhotosShouldBeExactCount").Options;
            var dbContext = new ApplicationDbContext(options);

            var pendingPhoto1 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };
            var pendingPhoto2 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.Add(pendingPhoto1);
            dbContext.Add(pendingPhoto2);
            dbContext.SaveChanges();

            var service = new AdminService(dbContext);

            Assert.Equal(2, service.AllPendingPhotos(1, 25).AllPendingPhotos.Count());
        }

        [Fact]
        public void ApproveDisapproveViewDataShouldWork()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ApproveDisapproveViewDataShouldWork").Options;
            var dbContext = new ApplicationDbContext(options);

            var pendingPhoto1 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { }, UserMessage = "abcdefg" };
            var pendingPhoto2 = new Photo { Id = Guid.NewGuid(), PhotoStatus = new PhotoStatus { ClassColor = "table-warning", StatusDescription = "Pending" }, PhotoFile = new byte[] { }, UserId = "1", User = new WebsiteUser { } };

            dbContext.Add(pendingPhoto1);
            dbContext.Add(pendingPhoto2);
            dbContext.SaveChanges();

            var service = new AdminService(dbContext);
            var photo = service.ApproveDisapproveViewData(pendingPhoto1.Id);

            Assert.Equal(pendingPhoto1.User.Id, photo.UserId);
            Assert.Equal("abcdefg", photo.UserMessage);
        }
    }
}
