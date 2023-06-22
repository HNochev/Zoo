using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Tickets
{
    public class TicketAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "{0} трябва да бъде по-късо от {1} символа и по-дълго от {2}")]
        [Display(Name = "Описание на типа на билета")]
        public string Type { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(0, 1000.00, ErrorMessage = "{0} трябва да бъде между от {1} и {2}")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
