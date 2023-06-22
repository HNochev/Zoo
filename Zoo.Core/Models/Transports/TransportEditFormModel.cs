using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Transports
{
    public class TransportEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(15, ErrorMessage = "{0} трябва да бъде по-къс от {1} символа")]
        [Display(Name = "Номер линия")]
        public string LineNumber { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Тип на превозните средства по линията")]
        public Guid TransportLineTypeId { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "{0} трябва да бъде по-късо от {1} символа и по-дълго от {2}")]
        [Display(Name = "Кратко описание на линията")]
        public string ShortRouteDescription { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "{0} трябва да бъде по-късо от {1} символа и по-дълго от {2}")]
        [Display(Name = "Пълно описание на маршрута на линията")]
        public string FullRouteDescription { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [DataType(DataType.Time)]
        [Display(Name = "Час на започване обслужването на линията")]
        public DateTime StartingHour { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [DataType(DataType.Time)]
        [Display(Name = "Час на край обслужването на линията")]
        public DateTime FinishHour { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(0, 100, ErrorMessage = "{0} може да бъде между {1} и {2} символа")]
        [Display(Name = "Минути дистанция от начална спирака до нашата")]
        public int MinutesDistanceFromFirstStop { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(1, 500, ErrorMessage = "{0} може да бъде между {1} и {2}")]
        [Display(Name = "Интервал на линията (в минути)")]
        public int Interval { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDbNotRequired FileUpload { get; set; }

        public IEnumerable<TransportLineTypeModel> TransportLineTypeModels { get; set; }
    }
}
