using System.ComponentModel.DataAnnotations;

namespace Zoo.Core.Models.AnimalFeedings
{
    public class FeedingAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0}, то трябва да бъде по-късo от {1} и по-дългo от {2} символа")]
        [Display(Name = "Кратко описание (Заглавие)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Час на хранене")]
        [DataType(DataType.Time)]
        public DateTime HourOfEating { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Range(1, 100, ErrorMessage = "{0}та трябва да бъде по-къса от {1} и по-дълга от {2} минути")]
        [Display(Name = "Продължителност (в минути)")]
        public int Duration { get; set; }

        [StringLength(5000, ErrorMessage = "{0}, то трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Подробно описание")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Животно за което се отнася")]
        public Guid AnimalId { get; set; }

        public IEnumerable<AllAnimalsModel> Animals { get; set; }
    }
}
