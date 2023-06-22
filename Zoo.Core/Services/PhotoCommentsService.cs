using Zoo.Core.Contracts;
using Zoo.Core.Models.PhotosComments;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Services
{
    public class PhotoCommentsService : IPhotoCommentsService
    {
        private readonly ApplicationDbContext data;

        public PhotoCommentsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Guid CreatePhotoComment(string content, DateTime date, string userId, Guid photoId)
        {
            var newComment = new PhotoComment
            {
                Content = content,
                Date = date,
                UserId = userId,
                PhotoId = photoId,
            };

            this.data.PhotoComments.Add(newComment);
            this.data.SaveChanges();

            return newComment.Id;
        }

        public bool Edit(Guid id, string content, DateTime lastEditedOn)
        {
            var commentData = this.data.PhotoComments.Find(id);

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
                .PhotoComments
                .Any(c => c.Id == commentId && c.UserId == userId);

        public Guid IdOfPhoto(Guid commentId)
            => this.data
                .PhotoComments
                .Where(x => x.Id == commentId)
                .Select(x => x.PhotoId)
                .FirstOrDefault();

        public bool Delete(Guid id)
        {
            var comment = this.data.PhotoComments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.data.Remove(comment);

            this.data.SaveChanges();

            return true;
        }

        public PhotoCommentEditFormModel EditViewData(Guid id)
        {
            return this.data.PhotoComments
                    .Where(x => x.Id == id)
                    .Select(x => new PhotoCommentEditFormModel
                    {
                        Content = x.Content
                    })
                    .First();
        }
    }
}
