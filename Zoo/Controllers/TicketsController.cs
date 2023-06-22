using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;
using Zoo.Core.Contracts;
using Zoo.Core.Extensions;
using Zoo.Core.Models.Tickets;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService tickets;
        private readonly IUserService users;

        public TicketsController(ITicketService tickets, IUserService users)
        {
            this.tickets = tickets;
            this.users = users;
        }

        public IActionResult All()
        {
            IEnumerable<BuyedTicket> buyedTickets = null;

            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                buyedTickets = this.tickets.AllActiveBuyedTicketsByUser(this.User.Id());
            }

            return View(new TicketPageModel
            {
                Tickets = this.tickets.All(),
                BuyedTickets = buyedTickets,
            });
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Add(TicketAddFormModel ticket)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var contactId = this.tickets.CreateCard(
                ticket.Type,
                ticket.Price,
                false
                );

            TempData[MessageConstants.SuccessMessage] = "Картата беше успешно добавена.";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var contactForm = this.tickets.EditViewData(id);

            return View(contactForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Edit(Guid id, TicketEditFormModel ticket)
        {
            var edited = this.tickets.Edit(
                id,
                ticket.Type
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Името на абонаментната карта беше успешно редактирано";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var contactForm = this.tickets.DeleteViewData(id);

            return View(contactForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Tickets}")]
        public IActionResult Delete(Guid id, TicketDeleteModel card)
        {
            var deleted = this.tickets.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Вида билет беше успешно изтрит.";
            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Order(Guid id)
        {
            if (this.User.IsInRole(UserConstants.Administrator) || this.User.IsInRole(UserConstants.Moderator) || this.User.IsInRole(UserConstants.PhotoModerator) || this.User.IsInRole(UserConstants.Tickets))
            {
                TempData[MessageConstants.ErrorMessage] = "Вие сте част от екипа на Зоологическа градина - гр. Хасково и имате служебен абонамент";
                return RedirectToAction("All");
            }

            return View(new TicketOrderFormModel
            {
                TicketId = id,
                Ticket = tickets.GetTicket(id),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Order(Guid id, TicketOrderFormModel ticket)
        {
            if (this.User.IsInRole(UserConstants.Administrator) || this.User.IsInRole(UserConstants.Moderator) || this.User.IsInRole(UserConstants.PhotoModerator) || this.User.IsInRole(UserConstants.Tickets))
            {
                TempData[MessageConstants.ErrorMessage] = "Вие сте част от екипа на Зоологическа градина - гр. Хасково и имате служебен абонамент";
                return RedirectToAction("All");
            }

            if ((ticket.Date - DateTime.Today).Days < 0)
            {
                TempData[MessageConstants.ErrorMessage] = "Датата за посещение не може да бъде преди днес!";
                return RedirectToAction(nameof(Order), "Tickets", new { id = id });
            }

            if (ticket.Date >= DateTime.Today.AddYears(1))
            {
                TempData[MessageConstants.ErrorMessage] = "Датата на посещение може да бъде максимум до една година след днешна дата!";
                return RedirectToAction(nameof(Order), "Tickets", new { id = id });
            }

            var ordered = this.tickets.Order(
                this.User.Id(),
                id,
                ticket.TicketOwnerFirstName,
                ticket.TicketOwnerLastName,
                ticket.Date,
                ticket.Count
                );

            if (!ordered)
            {
                TempData[MessageConstants.ErrorMessage] = "Достигнали сте ЛИМИТА от до 6 активни покупки на билети.";
                return Redirect($"../../Tickets/All");
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно заявихте своите билети. За история на билетите може да разгледате профила си.";
            return Redirect($"../../Tickets/All");
        }

        [Authorize]
        public IActionResult RejectTicket(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            var ticket = this.tickets.GetBuyedTicket(id);

            if (ticket.Payed || ticket.Date < DateTime.Today)
            {
                TempData[MessageConstants.ErrorMessage] = "Не можете да изтривате платени или изтекли билети.";
                return RedirectToAction("All");
            }

            var deleted = this.tickets.RejectTicket(userId, id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно отказахте заявеният/те билет/и.";
            return Redirect($"../../Tickets/All");
        }
    }
}
