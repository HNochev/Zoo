using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Photos
{
    public class PhotoAddFormModel
    {
        [StringLength(500, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Описание на снимката")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на снимката")]
        public DateTime DateOfPicture { get; set; }

        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Място на снимката")]
        public string? Location { get; set; }

        [Display(Name = "Аз съм автор на тази снимка")]
        public bool IsAuthor { get; set; }

        [StringLength(500, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Пояснения към администратор")]
        public string? UserMessage { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
    }
}
