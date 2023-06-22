using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Infrastructure.Data.Models
{
    public class Transport
    {
        public Transport()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; }

        [Required]
        public string LineNumber { get; set; }

        [Required]
        public Guid TransportLineTypeId { get; set; }

        [ForeignKey(nameof(TransportLineTypeId))]
        public TransportLineType TransportLineType { get; set; }

        [Required]
        public string ShortRouteDescription { get; set; }

        [Required]
        public string FullRouteDescription { get; set; }

        [Required]
        public DateTime StartingHour { get; set; }

        [Required]
        public DateTime FinishHour { get; set; }

        [Required]
        public int MinutesDistanceFromFirstStop { get; set; }

        [Required]
        public int Interval { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }
    }
}
