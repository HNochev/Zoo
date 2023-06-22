using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Zoo.Core.Contracts;
using Zoo.Core.Models.News;
using Zoo.Core.Models.NewsComments;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext data;

        public NewsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public NewsPaginationModel All(int pageNo, int pageSize)
        {
            NewsPaginationModel result = new NewsPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.News.Count();
            result.News = this.data
                 .News
                 .Where(x => !x.IsDeleted)
                 .Select(x => new NewsListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Description = Truncate(Regex.Replace(x.Description, "<[^>]*(>|$)", ""), 200),
                     ImgUrl = x.ImgUrl,
                     Date = x.Date,
                     IsDeleted = x.IsDeleted,
                     AuthorId = x.AuthorId,
                     Author = x.Author,
                 })
                 .OrderByDescending(x => x.Date)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public Guid CreateNews(string title, string description, DateTime date, string authorId, string imgUrl, bool isDeleted)
        {
            var newNews = new News
            {
                Title = title,
                Description = description,
                Date = date,
                AuthorId = authorId,
                ImgUrl = imgUrl,
                IsDeleted = isDeleted,
            };

            this.data.News.Add(newNews);
            this.data.SaveChanges();

            return newNews.Id;
        }

        public NewsCommentsModel Details(Guid id)
        {
            return this.data.News
                .Include(x => x.Author)
                .Where(x => x.Id == id)
                .Select(x => new NewsCommentsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date,
                    AuthorId = x.AuthorId,
                    ImgUrl = x.ImgUrl,
                    IsDeleted = x.IsDeleted,
                    Author = x.Author,
                    NewsComments = x.NewsComments
                    .Select(x => new CommentsListingModel
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Date = x.Date,
                        LastEditedOn = x.LastEditedOn,
                        NewsId = x.NewsId,
                        User = x.User,
                        UserId = x.UserId,
                    })
                    .OrderByDescending(x => x.Date)
                    .ToList(),
                })
                .First();
        }

        public async Task<News> GetNewsByIdAsync(Guid id) => await this.data.News
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        private static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length) + "...";
            }
            return source;
        }

        public bool Edit(Guid id, string title, string description, string imgUrl)
        {
            var newsData = this.data.News.Find(id);

            if (newsData == null)
            {
                return false;
            }

            newsData.Title = title;
            newsData.Description = description;
            newsData.ImgUrl = imgUrl;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var newsData = this.data.News.Find(id);

            if (newsData == null)
            {
                return false;
            }

            newsData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public Guid GetNewsId(Guid newsId)
        {
            return this.data.News
                .Where(x => x.Id == newsId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public NewsAddFormModel EditViewData(Guid id)
        {
            return this.data.News
                .Where(x => x.Id == id)
                .Select(x => new NewsAddFormModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    ImgUrl = x.ImgUrl,
                })
                .First();
        }

        public NewsDeleteModel DeleteViewData(Guid id)
        {
            return this.data.News
                .Where(x => x.Id == id)
                .Select(x => new NewsDeleteModel
                {
                    Title = x.Title,
                    Date = x.Date,
                    ImgUrl = x.ImgUrl,
                })
                .First();
        }

        public List<NewsListingModel> GetTopThreeNews()
        {
            return this.data
                 .News
                 .Where(x => !x.IsDeleted)
                 .Select(x => new NewsListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Description = Truncate(Regex.Replace(x.Description, "<[^>]*(>|$)", ""), 150),
                     ImgUrl = x.ImgUrl,
                     Date = x.Date,
                     IsDeleted = x.IsDeleted,
                     AuthorId = x.AuthorId,
                     Author = x.Author,
                 })
                 .OrderByDescending(x => x.Date)
                 .Take(3)
                 .ToList();
        }
    }
}
