using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Transports
{
    public class BufferedSingleFileUploadDb
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Качете снимка (.jpg/.jpeg до 2MB)")]
        public IFormFile PhotoFile { get; set; }
    }
}
