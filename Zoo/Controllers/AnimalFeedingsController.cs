using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.AnimalFeedings;
using Zoo.Core.Models.Contacts;

namespace Zoo.Controllers
{
    public class AnimalFeedingsController : Controller
    {
        private readonly IAnimalFeedingService feedings;
        private readonly IUserService users;

        public AnimalFeedingsController(IAnimalFeedingService feedings, IUserService users)
        {
            this.feedings = feedings;
            this.users = users;
        }

        public IActionResult All()
        {
            var feedings = this.feedings.All();

            return View(feedings);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Add()
        {
            return View(new FeedingAddFormModel
            {
                Animals = this.feedings.AllAnimals(),
            });
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Add(FeedingAddFormModel feeding)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var feedingId = this.feedings.CreateFeeding(
                feeding.Title,
                feeding.HourOfEating,
                feeding.Duration,
                feeding.Description,
                feeding.AnimalId
                );

            TempData[MessageConstants.SuccessMessage] = "Храненето беше успешно добавено.";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var feeding = this.feedings.Details(id);

            if (feeding == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(feeding);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var feedingForm = this.feedings.EditViewData(id);

            feedingForm.Animals = this.feedings.AllAnimals();

            return View(feedingForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Edit(Guid id, FeedingAddFormModel feeding)
        {

            var edited = this.feedings.EditFeeding(
                id,
                feeding.Title,
                feeding.HourOfEating,
                feeding.Duration,
                feeding.Description,
                feeding.AnimalId
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Храненето беше успешно редактиранo.";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var contactForm = this.feedings.DeleteViewData(id);

            return View(contactForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Delete(Guid id, FeedingDeleteModel feeding)
        {

            var deleted = this.feedings.DeleteFeeding(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Храненето беше успешно изтритo.";
            return RedirectToAction("All");
        }
    }
}
