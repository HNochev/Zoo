using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Transports;

namespace Zoo.Controllers
{
    public class TransportsController : Controller
    {
        private readonly ITransportService transports;
        private readonly IUserService users;

        public TransportsController(ITransportService transports, IUserService users)
        {
            this.transports = transports;
            this.users = users;
        }

        public IActionResult All()
        {
            if (this.User.IsInRole(UserConstants.Administrator) || this.User.IsInRole(UserConstants.Moderator))
            {
                var lines = this.transports.AllWithNotActive();
                return View(lines);
            }
            else
            {
                var lines = this.transports.AllActive();
                return View(lines);
            }
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Add()
        {
            return View(new TransportAddFormModel
            {
                TransportLineTypeModels = this.transports.AllLineTypes(),
            });
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public async Task<IActionResult> AddAsync(TransportAddFormModel transport)
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
                await transport.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(transport.FileUpload.PhotoFile.FileName.ToLower());

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

            var transportId = this.transports.CreateTransport(
                transport.LineNumber,
                transport.TransportLineTypeId,
                transport.ShortRouteDescription,
                transport.FullRouteDescription,
                transport.StartingHour,
                transport.FinishHour,
                transport.MinutesDistanceFromFirstStop,
                transport.Interval,
                fileArray,
                this.User.Id()
                );

            TempData[MessageConstants.SuccessMessage] = "Линията беше успешно добавена.";
            return RedirectToAction("All");
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

            var transportForm = this.transports.EditViewData(id);

            transportForm.TransportLineTypeModels = this.transports.AllLineTypes();

            return View(transportForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public async Task<IActionResult> EditAsync(Guid id, TransportEditFormModel transport)
        {
            byte[] fileArray = null;

            if (transport.FileUpload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await transport.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                    string fileExt = Path.GetExtension(transport.FileUpload.PhotoFile.FileName.ToLower());

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

            var edited = this.transports.Edit(
                id,
                transport.LineNumber,
                transport.TransportLineTypeId,
                transport.ShortRouteDescription,
                transport.FullRouteDescription,
                transport.StartingHour,
                transport.FinishHour,
                transport.MinutesDistanceFromFirstStop,
                transport.Interval,
                fileArray
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Линията беше успешно редактирана.";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult ActivateDisactivateLine(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var isActive = this.transports.IsActive(id);

            var activated = this.transports.ActivateDeactivate(id);

            if (!activated)
            {
                return BadRequest();
            }
            if (!isActive)
            {
                TempData[MessageConstants.SuccessMessage] = "Успешно активирахте линията.";
            }
            else
            {
                TempData[MessageConstants.SuccessMessage] = "Успешно деактивирахте линията.";
            }
            return Redirect($"../../Transports/All");
        }
    }
}
