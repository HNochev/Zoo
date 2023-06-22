using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Contacts
{
    public class ContactAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(60, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Име и фамилия")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Phone]
        [StringLength(10, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(25, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Длъжност")]
        public string Position { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} трябва да бъде по-късo от {1} и по-дълго от {2} символа")]
        [Display(Name = "Потребителско име в сайта")]
        public string? UsernameInWebsite { get; set; }
    }
}
