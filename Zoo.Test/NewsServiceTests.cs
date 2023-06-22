using Microsoft.EntityFrameworkCore;
using Zoo.Core.Services;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using Xunit;
using Moq;
using System;

namespace Zoo.Test
{
    public class NewsServiceTests
    {
        [Fact]
        public void GetAllNewsReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AllNewsReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.News.Add(new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });
            dbContext.News.Add(new News { AuthorId = "2", Title = "2", Description = "2", ImgUrl = "2", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            Assert.Equal(2, service.All(1, 25).News.Count);
        }

        [Fact]
        public void DeleteReduceCountOfNews()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceSize").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.News.Add(new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = firstAddGuid, Author = new WebsiteUser { } });
            dbContext.News.Add(new News { AuthorId = "2", Title = "2", Description = "2", ImgUrl = "2", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            service.Delete(firstAddGuid, true);
            Assert.Single(service.All(1, 25).News);
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.News.Add(new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = firstAddGuid, Author = new WebsiteUser { } });
            dbContext.News.Add(new News { AuthorId = "2", Title = "2", Description = "2", ImgUrl = "2", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });

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

            dbContext.News.Add(new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = firstAddGuid, Author = new WebsiteUser { } });
            dbContext.News.Add(new News { AuthorId = "2", Title = "2", Description = "2", ImgUrl = "2", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            service.Edit(firstAddGuid, "newTitle", "newDescription", "newImg");
            var editedData = service.Details(firstAddGuid);
            Assert.Equal("newTitle", editedData.Title);
            Assert.Equal("newDescription", editedData.Description);
            Assert.Equal("newImg", editedData.ImgUrl);
        }

        [Fact]
        public void CreateNewsReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateNewsReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.News.Add(new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });
            dbContext.News.Add(new News { AuthorId = "2", Title = "2", Description = "2", ImgUrl = "2", IsDeleted = false, Date = DateTime.Now, Id = Guid.NewGuid(), Author = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            var id = service.CreateNews("gafasfasfsafsa", "safsaffsasfa", DateTime.Now, "sfsafasffsa", "safsaffsa", false);

            Assert.NotNull(id);
        }
    }
}