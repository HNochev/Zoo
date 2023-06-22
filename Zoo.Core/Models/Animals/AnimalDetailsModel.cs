using Zoo.Core.Models.Photos;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Animals
{
    public class AnimalDetailsModel
    {
        public Guid Id { get; set; }

        public string AnimalName { get; set; }

        public DateTime InZooSince { get; set; }

        public DateTime? InZooAgainFrom { get; set; }

        public int Count { get; set; }

        public string? Description { get; set; }

        public Guid AnimalConditionId { get; set; }

        public AnimalCondition AnimalCondition { get; set; }

        public Guid AnimalEatingTypeId { get; set; }

        public AnimalEatingType AnimalEatingType { get; set; }

        public Guid AnimalsKindId { get; set; }

        public AnimalKind AnimalsKind { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public ICollection<PhotosForOneYearModel> PhotosForYear { get; set; }

        public ICollection<AnimalFeeding> AnimalFeedings { get; set; }

    }
}
