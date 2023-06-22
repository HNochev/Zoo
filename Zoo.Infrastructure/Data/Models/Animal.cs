using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class Animal
    {
        public Animal()
        {
            this.Id = Guid.NewGuid();
            this.Photos = new HashSet<Photo>();
            this.AnimalFeedings = new HashSet<AnimalFeeding>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string AnimalName { get; set; }

        public DateTime InZooSince { get; set; }

        public DateTime? InZooAgainFrom { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Count { get; set; }

        [StringLength(5000)]
        public string? Description { get; set; }

        [Required]
        public Guid AnimalConditionId { get; set; }

        [ForeignKey(nameof(AnimalConditionId))]
        public AnimalCondition AnimalCondition { get; set; }

        [Required]
        public Guid AnimalEatingTypeId { get; set; }

        [ForeignKey(nameof(AnimalEatingTypeId))]
        public AnimalEatingType AnimalEatingType { get; set; }

        [Required]
        public Guid AnimalKindId { get; set; }

        [ForeignKey(nameof(AnimalKindId))]
        public AnimalKind AnimalKind { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<AnimalFeeding> AnimalFeedings { get; set; }
    }
}
