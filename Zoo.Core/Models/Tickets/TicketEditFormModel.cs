using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Tickets
{
    public class TicketEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "{0} трябва да бъде по-късо от {1} символа и по-дълго от {2}")]
        [Display(Name = "Описание на типа на билета")]
        public string Type { get; set; }
    }
}
