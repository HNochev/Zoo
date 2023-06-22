using Zoo.Core.Models.AnimalFeedings;

namespace Zoo.Core.Contracts
{
    public interface IAnimalFeedingService
    {
        public List<FeedingListingModel> All();

        public List<FeedingListingModel> GetTopSevenFeedings();

        public IEnumerable<AllAnimalsModel> AllAnimals();

        public Guid CreateFeeding(
            string title,
            DateTime hourOfEating,
            int duration,
            string? description,
            Guid animalId);

        public FeedingDetailsModel Details(Guid id);

        public FeedingAddFormModel EditViewData(Guid id);

        public bool EditFeeding(Guid id, string title, DateTime hourOfEating, int duration, string? description, Guid animalId);

        public FeedingDeleteModel DeleteViewData(Guid id);

        public bool DeleteFeeding(Guid id);
    }
}
