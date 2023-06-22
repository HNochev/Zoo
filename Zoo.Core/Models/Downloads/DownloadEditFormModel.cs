using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Downloads
{
    public class DownloadEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Заглавие на качваният файл")]
        public string FileName { get; set; }

        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Описание на файлът")]
        public string? Description { get; set; }
    }
}
