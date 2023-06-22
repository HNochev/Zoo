using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Animals;
using Zoo.Core.Models.Photos;

namespace Zoo.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalService vehicles;
        private readonly IUserService users;
        private readonly IPhotoService photos;

        public AnimalsController(IAnimalService vehicles, IUserService users, IPhotoService photos)
        {
            this.vehicles = vehicles;
            this.users = users;
            this.photos = photos;
        }

        public IActionResult All()
        {
            var vehicles = this.vehicles.All();

            return View(vehicles);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Add()
        {
            return View(new AnimalAddFormModel
            {
                AnimalConditions = this.vehicles.AllAnimalsConditions(),
                AnimalEatingTypes = this.vehicles.AllAnimalEatingTypes(),
                AnimalKinds = this.vehicles.AllAnimalKinds(),
            });
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> AddAsync(AnimalAddFormModel animal)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            byte[] fileArray = null;

            using (var memoryStream = new MemoryStream())
            {
                await animal.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(animal.FileUpload.PhotoFile.FileName.ToLower());

                if (fileExt == ".jpeg" || fileExt == ".jpg")
                {
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        fileArray = memoryStream.ToArray();
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Размерът на снимката е твърде голям! Моля качете снимка до 2MB!";
                        return Redirect(Request.Path);
                    }
                }
                else
                {
                    TempData[MessageConstants.ErrorMessage] = "Само .jpeg/.jpg файлове са разрешени!";
                    return Redirect(Request.Path);
                }
            }

            var newsId = this.vehicles.CreateAnimal(
                animal.AnimalName,
                animal.InZooSince,
                animal.InZooAgainFrom,
                animal.Count,
                animal.AnimalConditionId,
                animal.AnimalEatingTypeId,
                animal.AnimalsKindId,
                animal.Description,
                fileArray,
                false);

            TempData[MessageConstants.SuccessMessage] = "Животното беше успешно добавено.";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var vehicles = this.vehicles.Details(id);

            if (vehicles == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(vehicles);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var vehicles = this.vehicles.Details(id);

            var vehiclesForm = this.vehicles.EditViewData(vehicles.Id);

            vehiclesForm.AnimalConditions = this.vehicles.AllAnimalsConditions();
            vehiclesForm.AnimalEatingTypes = this.vehicles.AllAnimalEatingTypes();
            vehiclesForm.AnimalKinds = this.vehicles.AllAnimalKinds();

            return View(vehiclesForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> EditAsync(Guid id, AnimalEditFormModel animal)
        {
            byte[] fileArray = null;

            if (animal.FileUpload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await animal.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                    string fileExt = Path.GetExtension(animal.FileUpload.PhotoFile.FileName.ToLower());

                    if (fileExt == ".jpeg" || fileExt == ".jpg")
                    {
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            fileArray = memoryStream.ToArray();
                        }
                        else
                        {
                            TempData[MessageConstants.ErrorMessage] = "Размерът на снимката е твърде голям! Моля качете снимка до 2MB!";
                            return Redirect(Request.Path);
                        }
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Само .jpeg/.jpg файлове са разрешени!";
                        return Redirect(Request.Path);
                    }
                }
            }

            if (animal.InZooAgainFrom < DateTime.Today)
            {
                TempData[MessageConstants.ErrorMessage] = "Датата на завръщане на животното не може да бъде преди днес!";
                return RedirectToAction(nameof(Edit), "Animals", new { id = id });
            }

            var edited = this.vehicles.Edit(
                id,
                animal.AnimalName,
                animal.InZooSince,
                animal.InZooAgainFrom,
                animal.Count,
                animal.AnimalConditionId,
                animal.AnimalEatingTypeId,
                animal.AnimalsKindId,
                animal.Description,
                fileArray
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Животното беше успешно редактирано.";
            return Redirect($"../../Animals/Details/{id}");
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var vehicle = this.vehicles.Details(id);

            var vehicleForm = this.vehicles.DeleteViewData(vehicle.Id);

            return View(vehicleForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, AnimalDeleteModel vehicle)
        {

            var deleted = this.vehicles.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Животното беше успешно изтрито.";
            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult AddPhoto(Guid id)
        {
            var pendingPhotosCount = this.users.UserPendingPhotosCount(this.User.Id());

            if (!User.IsInRole(UserConstants.Administrator))
            {
                if (pendingPhotosCount >= 3)
                {
                    TempData[MessageConstants.ErrorMessage] = "Вие имате поне 3 снимки изчакващи одобрение.";
                    return Redirect($"../../Animals/Details/{id}");
                }
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPhotoAsync(Guid id, PhotoAddFormModel photo)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var pendingStatusId = this.photos.GetPendingStatusId();

            byte[] fileArray = null;

            using (var memoryStream = new MemoryStream())
            {
                await photo.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(photo.FileUpload.PhotoFile.FileName.ToLower());

                if (fileExt == ".jpeg" || fileExt == ".jpg")
                {
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        fileArray = memoryStream.ToArray();
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Размерът на снимката е твърде голям! Моля качете снимка до 2MB!";
                        return Redirect(Request.Path);
                    }
                }
                else
                {
                    TempData[MessageConstants.ErrorMessage] = "Само .jpeg/.jpg файлове са разрешени!";
                    return Redirect(Request.Path);
                }
            }

            var photoId = this.photos.CreatePhoto(
                photo.Description,
                photo.DateOfPicture,
                DateTime.Now,
                photo.Location,
                photo.IsAuthor,
                photo.UserMessage,
                fileArray,
                pendingStatusId,
                userId,
                id,
                false
                );

            TempData[MessageConstants.SuccessMessage] = "Успешно добавихте снимка. Сега тя трябва да бъде одобрена, за да я видите в сайта.";
            return Redirect(Request.Path);
        }
    }
}
