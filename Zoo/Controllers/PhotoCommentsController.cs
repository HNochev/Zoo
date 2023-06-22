using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.PhotosComments;

namespace Zoo.Controllers
{
    public class PhotoCommentsController : Controller
    {
        private readonly IPhotoCommentsService comments;
        private readonly IPhotoService photos;
        private readonly IUserService users;

        public PhotoCommentsController(IPhotoCommentsService comments, IPhotoService photos, IUserService users)
        {
            this.comments = comments;
            this.photos = photos;
            this.users = users;
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var commentForm = this.comments.EditViewData(id);

            return View(commentForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, PhotoCommentEditFormModel comment)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var photoId = this.comments.IdOfPhoto(id);

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var edited = this.comments.Edit(
                id,
                comment.Content.Trim(),
                lastEditedOn: DateTime.Now
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Коментарът беше успешно редактиран.";

            return Redirect($"../../Photos/Details/{photoId}");
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var photoId = this.comments.IdOfPhoto(id);

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var deleted = this.comments.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Коментарът беше успешно изтрит.";
            return Redirect($"../../Photos/Details/{photoId}");
        }
    }
}
