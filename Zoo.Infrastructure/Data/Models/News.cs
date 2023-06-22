using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data.Models
{
    public class News
    {
        public News()
        {
            this.Id = Guid.NewGuid();
            this.NewsComments = new HashSet<NewsComment>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public WebsiteUser Author { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }
    }
}
