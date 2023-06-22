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
    public class NewsCommentsServiceTests
    {
        [Fact]
        public void GetAllCommentsForNewsReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllCommentsForNewsReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);

            var guidId = Guid.NewGuid();
            var news = new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = guidId, Author = new WebsiteUser { } };

            dbContext.News.Add(news);

            dbContext.NewsComments.Add(new NewsComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), NewsId = guidId, News = news, User = new WebsiteUser { } });
            dbContext.NewsComments.Add(new NewsComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), NewsId = guidId, News = news, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsService(dbContext);

            Assert.Equal(2, service.Details(guidId).NewsComments.Count);
        }

        [Fact]
        public void DeleteReduceCountOfComments()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfComments").Options;
            var dbContext = new ApplicationDbContext(options);

            var newsToDeleteId = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var news = new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = guidId, Author = new WebsiteUser { } };

            dbContext.News.Add(news);

            dbContext.NewsComments.Add(new NewsComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = newsToDeleteId, NewsId = guidId, News = news, User = new WebsiteUser { } });
            dbContext.NewsComments.Add(new NewsComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), NewsId = guidId, News = news, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsCommentsService(dbContext);
            var newsService = new NewsService(dbContext);

            service.Delete(newsToDeleteId);
            Assert.Equal(1, newsService.Details(guidId).NewsComments.Count);
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var news = new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = guidId, Author = new WebsiteUser { } };

            dbContext.NewsComments.Add(new NewsComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = firstAddGuid, NewsId = Guid.NewGuid(), News = news, User = new WebsiteUser { } });
            dbContext.NewsComments.Add(new NewsComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), NewsId = Guid.NewGuid(), News = news, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsCommentsService(dbContext);

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
            var news = new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = guidId, Author = new WebsiteUser { } };

            dbContext.NewsComments.Add(new NewsComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = firstAddGuid, NewsId = Guid.NewGuid(), News = news, User = new WebsiteUser { } });
            dbContext.NewsComments.Add(new NewsComment { Content = "second comment", Date = DateTime.Now, UserId = "1", Id = Guid.NewGuid(), NewsId = Guid.NewGuid(), News = news, User = new WebsiteUser { } });

            dbContext.SaveChanges();
            var service = new NewsCommentsService(dbContext);

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
            var service = new NewsCommentsService(dbContext);

            var id = service.CreateNewsComment("Hello everyone", DateTime.Now, "1", Guid.NewGuid());

            Assert.NotNull(id);
        }

        [Fact]
        public void IsByUserAndIdByNewsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IsByUserAndIdByNewsTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            var guidId = Guid.NewGuid();
            var news = new News { AuthorId = "1", Title = "1", Description = "1", ImgUrl = "1", IsDeleted = false, Date = DateTime.Now, Id = guidId, Author = new WebsiteUser { } };

            var user = new WebsiteUser { Id = "1" };

            dbContext.NewsComments.Add(new NewsComment { Content = "content", Date = DateTime.Now, UserId = "1", Id = firstAddGuid, NewsId = guidId, News = news, User = user });

            dbContext.SaveChanges();
            var service = new NewsCommentsService(dbContext);

            Assert.True(service.IsByUser(firstAddGuid, "1"));
            Assert.Equal(service.IdOfNews(firstAddGuid), guidId);
        }
    }
}
