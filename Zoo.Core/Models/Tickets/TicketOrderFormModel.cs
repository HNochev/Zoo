using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.Tickets
{
    public class TicketOrderFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        public Guid? TicketId { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [ForeignKey(nameof(TicketId))]
        public Ticket? Ticket { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-късо от {1} символа")]
        [Display(Name = "Име на закупуващия билет/и")]
        public string? TicketOwnerFirstName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-къса от {1} символа")]
        [Display(Name = "Фамилия на закупуващия билет/и")]
        public string? TicketOwnerLastName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(1, 100, ErrorMessage = "{0} трябва да бъде между {1} и {2}")]
        [Display(Name = "Брой билети")]
        public int Count { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата за посещение")]
        public DateTime Date { get; set; }
    }
}
