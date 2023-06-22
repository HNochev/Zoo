using Zoo.Core.Models.Animals;

namespace Zoo.Core.Contracts
{
    public interface IAnimalService
    {
        public List<AnimalListingModel> All();

        public IEnumerable<AnimalConditionModel> AllAnimalsConditions();

        public IEnumerable<AnimalEatingTypeModel> AllAnimalEatingTypes();

        public IEnumerable<AnimalKindModel> AllAnimalKinds();

        public Guid CreateAnimal(
            string animalKind,
            DateTime inZooSince,
            DateTime? inZooAgainFrom,
            int count,
            Guid animalConditionId,
            Guid animalEatingTypeId,
            Guid animalKindId,
            string? description,
            byte[] photoFile,
            bool isDeleted);

        public AnimalDetailsModel Details(Guid id);

        public bool Edit(
            Guid id,
            string animalKind,
            DateTime inZooSince,
            DateTime? inZooAgainFrom,
            int count,
            Guid animalConditionId,
            Guid animalEatingTypeId,
            Guid animalKindId,
            string? description,
            byte[] photoFile);

        public bool Delete(Guid id, bool isDeleted);

        public AnimalDeleteModel DeleteViewData(Guid id);

        public AnimalEditFormModel EditViewData(Guid id);
    }
}
