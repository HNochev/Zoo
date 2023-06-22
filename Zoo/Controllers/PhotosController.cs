using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Photos;
using Zoo.Core.Models.PhotosComments;

namespace Zoo.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoService photos;
        private readonly IUserService users;
        private readonly IAnimalService vehicles;
        private readonly IPhotoCommentsService comments;

        public PhotosController(IPhotoService photos, IUserService users, IAnimalService vehicles, IPhotoCommentsService comments)
        {
            this.photos = photos;
            this.users = users;
            this.vehicles = vehicles;
            this.comments = comments;
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

            if (!this.photos.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.PhotoModerator))
            {
                return BadRequest();
            }

            var photoForm = this.photos.EditViewData(id);

            if (photoForm.IsApproved)
            {
                TempData[MessageConstants.ErrorMessage] = "Снимката няма как да бъде променяна, понеже е одобрена!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(photoForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, PhotoEditFormModel photo)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (!this.photos.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.PhotoModerator))
            {
                return BadRequest();
            }

            var edited = this.photos.Edit(
                id,
                photo.Description,
                photo.DateOfPicture,
                photo.IsAuthor,
                photo.Location,
                photo.UserMessage
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Информацията за снимката беше успешно редактирана.";

            return Redirect($"../../Photos/Details/{id}");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var vehicles = this.photos.Details(id);

            if (vehicles == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(vehicles);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (!this.photos.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.PhotoModerator))
            {
                return BadRequest();
            }

            var photoForm = this.photos.DeleteViewData(id);

            if (photoForm.IsApproved)
            {
                TempData[MessageConstants.ErrorMessage] = "Снимката няма как да бъде изтрита, понеже е одобрена!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(photoForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(Guid id, PhotoDeleteModel photo)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (!this.photos.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.PhotoModerator))
            {
                return BadRequest();
            }

            var vehicleId = this.photos.IdOfVehicle(id);

            var deleted = this.photos.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }


            TempData[MessageConstants.SuccessMessage] = "Снимката беше успешно изтрита.";
            return Redirect($"../../Vehicles/Details/{vehicleId}");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(Guid id, PhotoCommentAddFormModel comment)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var edited = this.comments.CreatePhotoComment(
                comment.Content,
                DateTime.Now,
                userId,
                id);

            TempData[MessageConstants.SuccessMessage] = "Успешно публикувахте коментар.";
            return Redirect(Request.Path);
        }
    }
}
