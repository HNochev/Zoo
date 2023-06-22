using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Contracts;
using Zoo.Core.Models.Transports;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Services
{
    public class TransportService : ITransportService
    {
        private readonly ApplicationDbContext data;

        public TransportService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<TransportListingModel> AllWithNotActive()
        {
            return this.data
                 .Transports
                 .Select(x => new TransportListingModel
                 {
                     Id = x.Id,
                     LineNumber = x.LineNumber,
                     ShortRouteDescription = x.ShortRouteDescription,
                     FullRouteDescription = x.FullRouteDescription,
                     StartingHour = x.StartingHour,
                     FinishHour = x.FinishHour,
                     Interval = x.Interval,
                     IsActive = x.IsActive,
                     MinutesDistanceFromFirstStop = x.MinutesDistanceFromFirstStop,
                     TransportLineTypeId = x.TransportLineTypeId,
                     TransportLineType = x.TransportLineType,
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),

                 })
                 .OrderBy(x => x.LineNumber)
                 .ToList();
        }

        public List<TransportListingModel> AllActive()
        {
            return this.data
                 .Transports
                 .Where(x => x.IsActive)
                 .Select(x => new TransportListingModel
                 {
                     Id = x.Id,
                     LineNumber = x.LineNumber,
                     ShortRouteDescription = x.ShortRouteDescription,
                     FullRouteDescription = x.FullRouteDescription,
                     StartingHour = x.StartingHour,
                     FinishHour = x.FinishHour,
                     Interval = x.Interval,
                     IsActive = x.IsActive,
                     MinutesDistanceFromFirstStop = x.MinutesDistanceFromFirstStop,
                     TransportLineTypeId = x.TransportLineTypeId,
                     TransportLineType = x.TransportLineType,
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),

                 })
                 .OrderBy(x => x.LineNumber)
                 .ToList();
        }

        public IEnumerable<TransportLineTypeModel> AllLineTypes()
        {
            return this.data
                .TransportLineTypes
                .Select(x => new TransportLineTypeModel
                {
                    Id = x.Id,
                    VehicleTypeDescription = x.VehicleTypeDescription,
                    ClassColor = x.ClassColor,
                    Counter = x.Counter,
                })
                .OrderBy(x => x.Counter)
                .ToList();
        }

        public Guid CreateTransport(string lineNumber, Guid transportLineTypeId, string shortRouteDescription, string fullRouteDescription, DateTime startingHour, DateTime finishHour, int minutesDistanceFromFirstStop, int interval, byte[] photoFile, string userId)
        {
            var newTransport = new Transport
            {
                LineNumber = lineNumber,
                ShortRouteDescription = shortRouteDescription,
                FullRouteDescription = fullRouteDescription,
                StartingHour = startingHour,
                FinishHour = finishHour,
                Interval = interval,
                MinutesDistanceFromFirstStop = minutesDistanceFromFirstStop,
                PhotoFile = photoFile,
                TransportLineTypeId = transportLineTypeId,
                IsActive = true,
                UserId = userId,
            };

            this.data.Transports.Add(newTransport);
            this.data.SaveChanges();

            return newTransport.Id;
        }

        public TransportEditFormModel EditViewData(Guid id)
        {
            return this.data.Transports
                .Where(x => x.Id == id)
                .Select(x => new TransportEditFormModel
                {
                    LineNumber = x.LineNumber,
                    ShortRouteDescription = x.ShortRouteDescription,
                    FullRouteDescription = x.FullRouteDescription,
                    StartingHour = x.StartingHour,
                    FinishHour = x.FinishHour,
                    Interval = x.Interval,
                    MinutesDistanceFromFirstStop = x.MinutesDistanceFromFirstStop,
                    TransportLineTypeId = x.TransportLineTypeId,
                })
                .First();
        }

        public bool Edit(Guid id, string lineNumber, Guid transportLineTypeId, string shortRouteDescription, string fullRouteDescription, DateTime startingHour, DateTime finishHour, int minutesDistanceFromFirstStop, int interval, byte[] photoFile)
        {
            var transportData = this.data.Transports.Find(id);

            if (transportData == null)
            {
                return false;
            }

            transportData.LineNumber = lineNumber;
            transportData.ShortRouteDescription = shortRouteDescription;
            transportData.FullRouteDescription = fullRouteDescription;
            transportData.StartingHour = startingHour;
            transportData.FinishHour = finishHour;
            transportData.MinutesDistanceFromFirstStop = minutesDistanceFromFirstStop;
            transportData.Interval = interval;
            transportData.TransportLineTypeId = transportLineTypeId;

            if (photoFile != null)
            {
                transportData.PhotoFile = photoFile;
            }

            this.data.SaveChanges();

            return true;
        }

        public bool IsActive(Guid id)
        {
            return this.data
                .Transports
                .Where(x => x.Id == id)
                .Select(x => x.IsActive)
                .First();
        }

        public bool ActivateDeactivate(Guid id)
        {
            var lineData = this.data.Transports.Find(id);

            if (lineData == null)
            {
                return false;
            }
            if (lineData.IsActive)
            {
                lineData.IsActive = false;
            }
            else
            {
                lineData.IsActive = true;
            }

            this.data.SaveChanges();
            return true;
        }
    }
}
