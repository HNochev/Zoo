using System.ComponentModel.DataAnnotations;

namespace Zoo.Infrastructure.Data.Models
{
    public class AnimalCondition
    {
        public AnimalCondition()
        {
            this.Id = Guid.NewGuid();
            this.Animals = new HashSet<Animal>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ConditionDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassColor { get; set; }

        [Required]
        [Range(0, 50)]
        public int Counter { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}
