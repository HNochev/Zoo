using System.ComponentModel.DataAnnotations;

namespace Zoo.Infrastructure.Data.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Id = Guid.NewGuid();
            this.BuyedTickets = new HashSet<BuyedTicket>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        [Required]
        [Range(0, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public ICollection<BuyedTicket> BuyedTickets { get; set; }
    }
}
