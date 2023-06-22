using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class NewsComment
    {
        public NewsComment()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(700)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public DateTime? LastEditedOn { get; set; }

        [Required]
        public Guid NewsId { get; set; }
        
        [ForeignKey(nameof(NewsId))]
        public News News { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }
    }
}
