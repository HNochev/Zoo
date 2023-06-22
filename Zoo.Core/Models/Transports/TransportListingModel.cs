using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.Transports
{
    public class TransportListingModel
    {
        public Guid Id { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string LineNumber { get; set; }

        public Guid TransportLineTypeId { get; set; }

        public TransportLineType TransportLineType { get; set; }

        public string ShortRouteDescription { get; set; }

        public string FullRouteDescription { get; set; }

        public DateTime StartingHour { get; set; }

        public DateTime FinishHour { get; set; }

        public int MinutesDistanceFromFirstStop { get; set; }

        public int Interval { get; set; }

        public bool IsActive { get; set; }

    }
}
