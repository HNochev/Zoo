using Zoo.Core.Contracts;
using Zoo.Core.Models.Animals;
using Zoo.Core.Models.Photos;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly ApplicationDbContext data;

        public AnimalService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<AnimalListingModel> All()
        {
            return this.data
                 .Animals
                 .Where(x => !x.IsDeleted)
                 .Select(x => new AnimalListingModel
                 {
                     Id = x.Id,
                     AnimalName = x.AnimalName,
                     Count = x.Count,
                     Description = x.Description,
                     InZooSince = x.InZooSince,
                     InZooAgainFrom = x.InZooAgainFrom,
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                     AnimalConditionId = x.AnimalConditionId,
                     ClassColor = this.data.AnimalConditions
                                .Where(y => y.Id == x.AnimalConditionId)
                                .Select(y => y.ClassColor)
                                .First(),
                     PhotosCount = this.data.Photos
                                .Where(y => y.AnimalId == x.Id && y.IsApproved == true)
                                .Count()

                 })
                 .OrderByDescending(x => x.InZooSince)
                 .OrderBy(x => x.AnimalName)
                 .ToList();
        }

        public IEnumerable<AnimalConditionModel> AllAnimalsConditions()
        {
            return this.data
                .AnimalConditions
                .Select(x => new AnimalConditionModel
                {
                    Id = x.Id,
                    ConditionDescription = x.ConditionDescription,
                    ClassColor = x.ClassColor,
                    Counter = x.Counter,
                })
                .OrderBy(x => x.Counter)
                .ToList();
        }

        public IEnumerable<AnimalEatingTypeModel> AllAnimalEatingTypes()
        {
            return this.data
                .AnimalEatingTypes
                .Select(x => new AnimalEatingTypeModel
                {
                    Id = x.Id,
                    EatingTypeDescription = x.EatingTypeDescription,
                    ClassColor = x.ClassColor,
                    Counter = x.Counter,
                })
                .OrderBy(x => x.Counter)
                .ToList();
        }

        public IEnumerable<AnimalKindModel> AllAnimalKinds()
        {
            return this.data
                .AnimalKinds
                .Select(x => new AnimalKindModel
                {
                    Id = x.Id,
                    AnimalsKind = x.AnimalsKind,
                    ClassColor = x.ClassColor,
                    Counter = x.Counter,
                })
                .OrderBy(x => x.Counter)
                .ToList();
        }

        public Guid CreateAnimal(string animalName, DateTime inZooSince, DateTime? inZooAgainFrom, int count, Guid animalConditionId, Guid animalEatingTypeId, Guid animalKindId, string? description, byte[] photoFile, bool isDeleted)
        {
            var newAnimal = new Animal
            {
                AnimalName = animalName,
                Count = count,
                InZooSince = inZooSince,
                InZooAgainFrom = inZooAgainFrom,
                AnimalConditionId = animalConditionId,
                AnimalEatingTypeId = animalEatingTypeId,
                AnimalKindId = animalKindId,
                Description = description,
                PhotoFile = photoFile,
                IsDeleted = isDeleted,
            };

            this.data.Animals.Add(newAnimal);
            this.data.SaveChanges();

            return newAnimal.Id;
        }

        public AnimalDetailsModel Details(Guid id)
        {
            return this.data.Animals
                .Where(x => x.Id == id)
                .Select(x => new AnimalDetailsModel
                {
                    Id = x.Id,
                    AnimalName = x.AnimalName,
                    Count = x.Count,
                    InZooSince = x.InZooSince,
                    InZooAgainFrom = x.InZooAgainFrom,
                    Description = x.Description,
                    AnimalCondition = x.AnimalCondition,
                    AnimalConditionId = x.AnimalConditionId,
                    AnimalEatingType = x.AnimalEatingType,
                    AnimalEatingTypeId = x.AnimalEatingTypeId,
                    AnimalsKind = x.AnimalKind,
                    AnimalsKindId = x.AnimalKindId,
                    AnimalFeedings = x.AnimalFeedings,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                    PhotosForYear = x.Photos
                    .Where(y => y.IsApproved == true)
                    .Select(y => new PhotosForOneYearModel
                    {
                        Year = y.DateOfPicture.Year,
                        Photos = x.Photos
                        .Where(z => z.IsApproved == true)
                        .Select(z => new PhotosListingModel
                        {
                            Id = z.Id,
                            DateOfPicture = z.DateOfPicture,
                            DateUploaded = z.DateUploaded,
                            Location = z.Location,
                            UserId = z.UserId,
                            User = z.User,
                            ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(z.PhotoFile),
                        })
                        .Where(z => z.DateOfPicture.Year == y.DateOfPicture.Year)
                        .OrderByDescending(z => z.DateOfPicture)
                        .ToList()
                    })
                    .OrderByDescending(y => y.Year)
                    .ToList()
                })
                .First();
        }

        public bool Edit(Guid id, string animalKind, DateTime inZooSince, DateTime? inZooAgainFrom, int count, Guid animalConditionId, Guid animalEatingTypeId, Guid animalKindId, string? description, byte[] photoFile)
        {
            var animalData = this.data.Animals.Find(id);

            if (animalData == null)
            {
                return false;
            }

            animalData.AnimalName = animalKind;
            animalData.InZooSince = inZooSince;
            animalData.InZooAgainFrom = inZooAgainFrom;
            animalData.Count = count;
            animalData.AnimalConditionId = animalConditionId;
            animalData.AnimalEatingTypeId = animalEatingTypeId;
            animalData.AnimalKindId = animalKindId;
            animalData.Description = description;

            animalData.AnimalFeedings = this.data.AnimalFeedings.Where(x => x.AnimalId == id).ToList();

            if (photoFile != null)
            {
                animalData.PhotoFile = photoFile;
            }

            var animalCondition = this.data.AnimalConditions.FirstOrDefault(x => x.Id == animalConditionId);
            if (animalCondition.ClassColor != "table-success")
            {
                animalData.AnimalFeedings.All(x => x.IsActive = false);
            }
            else
            {
                animalData.AnimalFeedings.All(x => x.IsActive = true);
            }

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var animalData = this.data.Animals.Find(id);

            if (animalData == null)
            {
                return false;
            }

            animalData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public AnimalDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Animals
                .Where(x => x.Id == id)
                .Select(x => new AnimalDeleteModel
                {
                    AnimalName = x.AnimalName,
                    AnimalsKind = x.AnimalKind,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                })
                .First();
        }

        public AnimalEditFormModel EditViewData(Guid id)
        {
            return this.data.Animals
                .Where(x => x.Id == id)
                .Select(x => new AnimalEditFormModel
                {
                    AnimalName = x.AnimalName,
                    Count = x.Count,
                    InZooSince = x.InZooSince,
                    InZooAgainFrom = x.InZooAgainFrom,
                    Description = x.Description,
                    AnimalConditionId = x.AnimalConditionId,
                    AnimalEatingTypeId = x.AnimalEatingTypeId,
                    AnimalsKindId = x.AnimalKindId,
                })
                .First();
        }
    }
}
