using Zoo.Core.Contracts;
using Zoo.Core.Models.NewsComments;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Services
{
    public class NewsCommentsService : INewsCommentsService
    {
        private readonly ApplicationDbContext data;

        public NewsCommentsService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public Guid CreateNewsComment(string content, DateTime date, string userId, Guid newsId)
        {
            var newComment = new NewsComment
            {
                Content = content,
                Date = date,
                UserId = userId,
                NewsId = newsId
            };

            this.data.NewsComments.Add(newComment);
            this.data.SaveChanges();

            return newComment.Id;
        }

        public bool Edit(Guid id, string content, DateTime lastEditedOn)
        {
            var commentData = this.data.NewsComments.Find(id);

            if (commentData == null)
            {
                return false;
            }

            commentData.Content = content;
            commentData.LastEditedOn = lastEditedOn;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByUser(Guid commentId, string userId)
            => this.data
                .NewsComments
                .Any(c => c.Id == commentId && c.UserId == userId);

        public Guid IdOfNews(Guid commentId)
            => this.data
                .NewsComments
                .Where(x => x.Id == commentId)
                .Select(x => x.NewsId)
                .FirstOrDefault();

        public bool Delete(Guid id)
        {
            var comment = this.data.NewsComments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.data.Remove(comment);

            this.data.SaveChanges();

            return true;
        }

        public CommentsEditFormModel EditViewData(Guid id)
        {
            return this.data.NewsComments
                    .Where(x => x.Id == id)
                    .Select(x => new CommentsEditFormModel
                    {
                        Content = x.Content
                    })
                    .First();
        }
    }
}
