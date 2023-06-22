using System.ComponentModel.DataAnnotations;

namespace Zoo.Infrastructure.Data.Models
{
    public class PhotoStatus
    {
        public PhotoStatus()
        {
            this.Id = Guid.NewGuid();
            this.Photos = new HashSet<Photo>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassColor { get; set; }

        [Required]
        [Range(0, 10)]
        public int Counter { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
