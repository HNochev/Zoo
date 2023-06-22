using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Animals
{
    public class AnimalAddFormModel
    {
        public static readonly string todayDate = DateTime.Today.ToString("dd/MM/yyyy");

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-къс от {1} символа")]
        [Display(Name = "Какво животно добавяте")]
        public string AnimalName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "В зоопарк Хасково от")]
        [DataType(DataType.Date)]
        public DateTime InZooSince { get; set; }

        [Display(Name = "Може да бъде видяно в зоопарк Хасково на")]
        [DataType(DataType.Date)]
        public DateTime? InZooAgainFrom { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(1, 1000, ErrorMessage = "{0} могат да бъдат между {1} и {2}")]
        [Display(Name = "Брой животни от вида")]
        public int Count { get; set; }

        [StringLength(5000, ErrorMessage = "{0} трябва да бъде по-късa от {1} символа")]
        [Display(Name = "Информация")]
        public string? Description { get; set; }

        [Display(Name = "Състояние")]
        public Guid AnimalConditionId { get; set; }

        [Display(Name = "Начин на хранене")]
        public Guid AnimalEatingTypeId { get; set; }

        [Display(Name = "Вид на животното")]
        public Guid AnimalsKindId { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

        public IEnumerable<AnimalConditionModel> AnimalConditions { get; set; }

        public IEnumerable<AnimalEatingTypeModel> AnimalEatingTypes { get; set; }

        public IEnumerable<AnimalKindModel> AnimalKinds { get; set; }
    }
}
