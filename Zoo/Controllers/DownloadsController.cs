using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Downloads;

namespace Zoo.Controllers
{
    public class DownloadsController : Controller
    {
        private readonly IDownloadService downloads;
        private readonly IUserService users;

        public DownloadsController(IDownloadService downloads, IUserService users)
        {
            this.downloads = downloads;
            this.users = users;
        }

        public IActionResult All()
        {
            var downloads = this.downloads.All();

            return View(downloads);
        }

        [Authorize]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult AddPDF()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> AddPDFAsync(Guid id, DownloadAddFormModel download)
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
                await download.FileUpload.FilePDF.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(download.FileUpload.FilePDF.FileName.ToLower());

                if (fileExt == ".pdf" || fileExt == ".PDF")
                {
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        fileArray = memoryStream.ToArray();
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Размерът на PDF файлът е твърде голям! Ограничението за размер е до 2MB!";
                        return Redirect(Request.Path);
                    }
                }
                else
                {
                    TempData[MessageConstants.ErrorMessage] = "Само .PDF/.pdf файлове са разрешени!";
                    return Redirect(Request.Path);
                }
            }

            var downloadId = this.downloads.CreateFile(
                download.FileName,
                download.Description,
                fileArray,
                userId
                );

            TempData[MessageConstants.SuccessMessage] = "Успешно добавихте PDF файл.";
            return Redirect($"../../Downloads/All");
        }

        public async Task<FileResult> Download(Guid id)
        {
            var file = await downloads.GetFile(id);
            return File(file.FilePDF, "application/pdf");
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

            var newsForm = this.downloads.EditViewData(id);

            return View(newsForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id, DownloadEditFormModel download)
        {

            var edited = this.downloads.Edit(
                id,
                download.FileName,
                download.Description
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Информацията за файла беше успешно редактирана.";
            return RedirectToAction("All");
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

            var downloadForm = this.downloads.DeleteViewData(id);

            return View(downloadForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, DownloadDeleteModel download)
        {

            var deleted = this.downloads.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Файлът беше успешно изтрит.";
            return RedirectToAction("All");
        }
    }
}
