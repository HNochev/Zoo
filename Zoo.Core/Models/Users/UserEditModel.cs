using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Потребителско име")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }
    }
}
