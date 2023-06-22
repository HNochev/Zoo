using Zoo.Core.Models.PhotosComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Contracts
{
    public interface IPhotoCommentsService
    {
        Guid CreatePhotoComment(
            string content,
            DateTime date,
            string userId,
            Guid photoId);

        public bool Edit(
            Guid id,
            string content,
            DateTime lastEditedOn
            );

        public bool IsByUser(Guid commentId, string userId);

        public Guid IdOfPhoto(Guid commentId);

        public bool Delete(Guid id);

        public PhotoCommentEditFormModel EditViewData(Guid id);
    }
}
