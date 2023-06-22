using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Models.Transports;

namespace Zoo.Core.Contracts
{
    public interface ITransportService
    {
        public List<TransportListingModel> AllWithNotActive();

        public List<TransportListingModel> AllActive();

        public IEnumerable<TransportLineTypeModel> AllLineTypes();

        public Guid CreateTransport(string lineNumber, Guid transportLineTypeId, string shortRouteDescription, string fullRouteDescription, DateTime startingHour, DateTime finishHour, int minutesDistanceFromFirstStop, int interval, byte[] photoFile, string userId);

        public TransportEditFormModel EditViewData(Guid id);

        public bool Edit(Guid id, string lineNumber, Guid transportLineTypeId, string shortRouteDescription, string fullRouteDescription, DateTime startingHour, DateTime finishHour, int minutesDistanceFromFirstStop, int interval, byte[] photoFile);

        public bool IsActive(Guid id);

        public bool ActivateDeactivate(Guid id);
    }
}
