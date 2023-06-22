using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.AnimalFeedings
{
    public class FeedingDetailsModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime HourOfEating { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; }

        public string? Description { get; set; }

        public Guid AnimalId { get; set; }

        public Animal Animal { get; set; }
    }
}
