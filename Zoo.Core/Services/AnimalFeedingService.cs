using Zoo.Core.Contracts;
using Zoo.Core.Models.AnimalFeedings;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Services
{
    public class AnimalFeedingService : IAnimalFeedingService
    {
        private readonly ApplicationDbContext data;

        public AnimalFeedingService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<FeedingListingModel> All()
        {
            return this.data
                 .AnimalFeedings
                 .Select(x => new FeedingListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Duration = x.Duration,
                     HourOfEating = x.HourOfEating,
                     IsActive = x.IsActive,
                 })
                 .OrderBy(c => c.HourOfEating.TimeOfDay)
                 .ToList();
        }

        public List<FeedingListingModel> GetTopSevenFeedings()
        {
            return this.data
                 .AnimalFeedings
                 .Where(x => x.IsActive == true)
                 .Select(x => new FeedingListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Duration = x.Duration,
                     HourOfEating = x.HourOfEating,
                     IsActive = x.IsActive,
                 })
                 .OrderBy(x => x.HourOfEating.TimeOfDay)
                 .Take(7)
                 .ToList();
        }

        public IEnumerable<AllAnimalsModel> AllAnimals()
        {
            return this.data
                .Animals
                .Where(x => !x.IsDeleted)
                .Select(x => new AllAnimalsModel
                {
                    Id = x.Id,
                    AnimalName = x.AnimalName,
                    AnimalCondition = x.AnimalCondition,
                    AnimalConditionId = x.AnimalConditionId,
                })
                .OrderBy(x => x.AnimalName)
                .ToList();
        }

        public Guid CreateFeeding(string title, DateTime hourOfEating, int duration, string? description, Guid animalId)
        {
            var animal = this.data.Animals.FirstOrDefault(x => x.Id == animalId);
            var animalCondition = this.data.AnimalConditions.FirstOrDefault(x => x.Id == animal.AnimalConditionId);

            var newFeeding = new AnimalFeeding
            {
                Title = title,
                Description = description,
                Duration = duration,
                HourOfEating = hourOfEating,
                AnimalId = animalId,
                Animal = animal,
            };

            newFeeding.IsActive = animalCondition.ClassColor != "table-success" ? false : true;

            this.data.AnimalFeedings.Add(newFeeding);
            this.data.SaveChanges();

            return newFeeding.Id;
        }

        public FeedingDetailsModel Details(Guid id)
        {
            return this.data.AnimalFeedings
                .Where(x => x.Id == id)
                .Select(x => new FeedingDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    HourOfEating = x.HourOfEating,
                    Duration = x.Duration,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    AnimalId = x.AnimalId,
                    Animal = x.Animal,
                })
                .First();
        }

        public FeedingAddFormModel EditViewData(Guid id)
        {
            return this.data.AnimalFeedings
                .Where(x => x.Id == id)
                .Select(x => new FeedingAddFormModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    Duration = x.Duration,
                    HourOfEating = x.HourOfEating,
                    AnimalId = x.AnimalId,
                })
                .First();
        }

        public bool EditFeeding(Guid id, string title, DateTime hourOfEating, int duration, string? description, Guid animalId)
        {
            var feedingData = this.data.AnimalFeedings.Find(id);

            var animal = this.data.Animals.FirstOrDefault(x => x.Id == animalId);
            var animalCondition = this.data.AnimalConditions.FirstOrDefault(x => x.Id == animal.AnimalConditionId);

            if (feedingData == null)
            {
                return false;
            }

            feedingData.Title = title;
            feedingData.HourOfEating = hourOfEating;
            feedingData.Duration = duration;
            feedingData.Description = description;
            feedingData.AnimalId = animalId;
            feedingData.Animal = animal;

            feedingData.IsActive = animalCondition.ClassColor != "table-success" ? false : true;

            this.data.SaveChanges();

            return true;
        }

        public FeedingDeleteModel DeleteViewData(Guid id)
        {
            return this.data.AnimalFeedings
                .Where(x => x.Id == id)
                .Select(x => new FeedingDeleteModel
                {
                    Title = x.Title,
                    HourOfEating = x.HourOfEating,
                    Duration = x.Duration,
                })
                .First();
        }

        public bool DeleteFeeding(Guid id)
        {
            var feeding = this.data.AnimalFeedings.Find(id);

            if (feeding == null)
            {
                return false;
            }

            this.data.Remove(feeding);
            this.data.SaveChanges();

            return true;
        }

    }
}
