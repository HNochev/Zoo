using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Animals
{
    public class AnimalListingModel
    {
        public Guid Id { get; set; }

        public string AnimalName { get; set; }

        public DateTime InZooSince { get; set; }

        public DateTime? InZooAgainFrom { get; set; }

        public int Count { get; set; }

        public string? Description { get; set; }

        public Guid AnimalConditionId { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string ClassColor { get; set; }

        public int PhotosCount { get; set; }
    }
}
