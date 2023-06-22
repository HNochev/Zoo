using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Transports
{
    public class TransportLineTypeModel
    {
        public Guid Id { get; set; }

        public string VehicleTypeDescription { get; set; }

        public string ClassColor { get; set; }

        public int Counter { get; set; }
    }
}
