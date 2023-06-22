using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;

namespace Zoo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService users;
        private readonly IPhotoService photos;

        public UserController(IUserService users, IPhotoService photos)
        {
            this.users = users;
            this.photos = photos;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }

        public IActionResult UserProfile(string id)
        {

            var detailsForm = this.users.UserDetails(id);

            return View(detailsForm);
        }

        [Authorize]
        public IActionResult MyPhotos(int p = 1, int s = 10)
        {
            var loggedUserId = this.users.IdByUser(this.User.Id());

            if (loggedUserId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var allMyPhotosForm = this.photos.AllPhotosByUser(loggedUserId, p, s);

            return View(allMyPhotosForm);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }
    }
}
