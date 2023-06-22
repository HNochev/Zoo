using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class Download
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }

        [Required]
        public byte[] FilePDF { get; set; }
    }
}
