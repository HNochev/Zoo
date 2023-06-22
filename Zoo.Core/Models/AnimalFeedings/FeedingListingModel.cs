namespace Zoo.Core.Models.AnimalFeedings
{
    public class FeedingListingModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime HourOfEating { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; }
    }
}
