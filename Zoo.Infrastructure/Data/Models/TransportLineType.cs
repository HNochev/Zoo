using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Infrastructure.Data.Models
{
    public class TransportLineType
    {
        public TransportLineType()
        {
            this.Id = Guid.NewGuid();
            this.Transports = new HashSet<Transport>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string VehicleTypeDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassColor { get; set; }

        [Required]
        [Range(0, 50)]
        public int Counter { get; set; }

        public ICollection<Transport> Transports { get; set; }
    }
}
