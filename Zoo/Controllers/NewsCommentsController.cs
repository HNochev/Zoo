using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.NewsComments;
using Zoo.Infrastructure.Data;

namespace Zoo.Controllers
{
    public class NewsCommentsController : Controller
    {
        private readonly INewsCommentsService comments;
        private readonly INewsService news;
        private readonly IUserService users;

        public NewsCommentsController(INewsCommentsService comments, INewsService news, IUserService users)
        {
            this.comments = comments;
            this.news = news;
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
        public IActionResult Edit(Guid id, CommentsEditFormModel comment)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var newsId = this.comments.IdOfNews(id);

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

            return Redirect($"../../News/Details/{newsId}");
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var newsId = this.comments.IdOfNews(id);

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
            return Redirect($"../../News/Details/{newsId}");
        }
    }
}
