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
    public class DownloadServiceTests
    {
        [Fact]
        public void GetAllDownloadsReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllDownloadsReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile", Description = "New desc", UserId = "1", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile2", Description = "New desc new", UserId = "2", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            Assert.Equal(2, service.All().Count);
        }

        [Fact]
        public void DeleteReduceCountOfDownloads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfDownloads").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Downloads.Add(new Download { Id = firstAddGuid, FileName = "newFile", Description = "New desc", UserId = "1", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile2", Description = "New desc new", UserId = "2", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            service.Delete(firstAddGuid);
            Assert.Single(service.All());
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Downloads.Add(new Download { Id = firstAddGuid, FileName = "newFile", Description = "New desc", UserId = "1", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile2", Description = "New desc new", UserId = "2", FilePDF = new byte[] { }, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            var exception = Record.Exception(() => service.Delete(Guid.NewGuid()));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Downloads.Add(new Download { Id = firstAddGuid, FileName = "newFile", Description = "New desc", UserId = "1", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile2", Description = "New desc new", UserId = "2", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.SaveChanges();
            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            service.Edit(firstAddGuid, "New File Name", "New File Description");
            var editedData = service.EditViewData(firstAddGuid);
            Assert.Equal("New File Name", editedData.FileName);
            Assert.Equal("New File Description", editedData.Description);
        }

        [Fact]
        public void CreateDownloadReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateNewsReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            var id = service.CreateFile("forDownload", null, new byte[] { }, "1");

            Assert.NotNull(id);
        }

        [Fact]
        public void GetFileShouldReturnFile()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetFileShouldReturnFile").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Downloads.Add(new Download { Id = firstAddGuid, FileName = "newFile", Description = "New desc", UserId = "1", FilePDF = new byte[] { }, User = new WebsiteUser { } });
            dbContext.Downloads.Add(new Download { Id = Guid.NewGuid(), FileName = "newFile2", Description = "New desc new", UserId = "2", FilePDF = new byte[] { }, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new DownloadService(dbContext);

            var file = service.GetFile(firstAddGuid);

            Assert.NotNull(file);
        }
    }
}
