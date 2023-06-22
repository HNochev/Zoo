using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Admin;

namespace Zoo.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAdminService admins;
        private readonly IUserService users;
        public AdminController(RoleManager<IdentityRole> roleManager, IAdminService admins, IUserService users)
        {
            this.roleManager = roleManager;
            this.admins = admins;
            this.users = users;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }

        //[Authorize(Roles = UserConstants.Administrator)]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Отдел билети"
        //    });

        //    return Ok();
        //}

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.PhotoModerator}")]
        public IActionResult ApprovePhotos(int p = 1, int s = 10)
        {
            var loggedUserId = this.users.IdByUser(this.User.Id());

            if (loggedUserId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var allMyPhotosForm = this.admins.AllPendingPhotos(p, s);

            return View(allMyPhotosForm);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.PhotoModerator}")]
        public IActionResult Approve(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var approveForm = this.admins.ApproveDisapproveViewData(id);

            return View(approveForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.PhotoModerator}")]
        public IActionResult Approve(Guid id, AdminApproveDisapprovePhotoModel photo)
        {

            var approved = this.admins.Approve(id, photo.AdminMessage);

            if (!approved)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно одобрихте тази снимка.";
            return RedirectToAction("ApprovePhotos");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.PhotoModerator}")]
        public IActionResult DisApprove(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var disApproveForm = this.admins.ApproveDisapproveViewData(id);

            return View(disApproveForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.PhotoModerator}")]
        public IActionResult DisApprove(Guid id, AdminApproveDisapprovePhotoModel photo)
        {

            var disApproved = this.admins.DisApprove(id, photo.AdminMessage);

            if (!disApproved)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно отхвърлихте публикуването на тази снимка.";
            return RedirectToAction("ApprovePhotos");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult TicketRequests(int p = 1, int s = 10)
        {
            var loggedUserId = this.users.IdByUser(this.User.Id());

            if (loggedUserId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var allCardForm = this.admins.AllPendingTickets(p, s);

            return View(allCardForm);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Reject(Guid id)
        {
            var deleted = this.admins.RejectTicket(this.User.Id(), id);

            if (!deleted)
            {
                TempData[MessageConstants.ErrorMessage] = "Не можете да триете билети, които все още не са изтекли или са платени!";
                return Redirect("../../Admin/TicketRequests");
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно изтрихте заявеният билет на потребителя.";
            return Redirect("../../Admin/TicketRequests");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult PayTicket(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var approveForm = this.admins.TicketPayViewData(id);

            return View(approveForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult PayTicket(Guid id, AdminPayTicketModel ticket)
        {
            var activateCardData = this.admins.TicketPayViewData(id);
            var approved = this.admins.PayTicket(id);

            if (!approved)
            {
                TempData[MessageConstants.ErrorMessage] = "Билетът е вече платен или със задна дата.";
                return Redirect("../../Admin/TicketRequests");
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно заплатен билет.";
            return Redirect($"../../Admin/PrintTicket/{id}");
        }

        [Authorize]
        public IActionResult PrintTicket(Guid id)
        {
            var ticket = this.admins.TicketPayViewData(id);

            if (!ticket.Payed)
            {
                TempData[MessageConstants.ErrorMessage] = "Не можете да принтирате не заплатен билет!";
                return Redirect("/");
            }

            return View(ticket);
        }
    }
}
