using Zoo.Core.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Contracts
{
    public interface INewsCommentsService
    {
        Guid CreateNewsComment(
            string content,
            DateTime date,
            string userId,
            Guid newsId);

        public bool Edit(
            Guid id,
            string content,
            DateTime lastEditedOn
            );

        public bool IsByUser(Guid commentId, string userId);

        public Guid IdOfNews(Guid commentId);

        public bool Delete(Guid id);

        public CommentsEditFormModel EditViewData(Guid id);
    }
}
