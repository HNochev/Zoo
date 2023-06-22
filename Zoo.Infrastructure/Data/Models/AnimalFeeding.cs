using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class AnimalFeeding
    {
        public AnimalFeeding()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public DateTime HourOfEating { get; set; }

        [Required]
        [Range(1, 100)]
        public int Duration { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(5000)]
        public string? Description { get; set; }

        [Required]
        public Guid AnimalId { get; set; }

        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; }

    }
}
