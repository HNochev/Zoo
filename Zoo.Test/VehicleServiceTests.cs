using Microsoft.EntityFrameworkCore;
using Zoo.Core.Services;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Zoo.Test
{
    public class VehicleServiceTests
    {
        [Fact]
        public void GetAllVehiclesReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllVehiclesReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Animals.Add(new Animal { Id = new Guid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { Id = Guid.NewGuid(), ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Lion", PhotoFile = new byte[] { } });
            dbContext.Animals.Add(new Animal { Id = new Guid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Tiger", PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new AnimalService(dbContext);

            Assert.Equal(2, dbContext.Animals.Count());
        }

        [Fact]
        public void DeleteReduceCountOfVehicles()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceCountOfVehicles").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstAddGuid = Guid.NewGuid();

            dbContext.Animals.Add(new Animal { Id = firstAddGuid, AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { Id = Guid.NewGuid(), ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Lion", PhotoFile = new byte[] { } });
            dbContext.Animals.Add(new Animal { Id = new Guid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Tiger", PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new AnimalService(dbContext);

            service.Delete(firstAddGuid, true);
            Assert.Single(dbContext.Animals.Where(x => !x.IsDeleted));
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Animals.Add(new Animal { Id = new Guid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { Id = Guid.NewGuid(), ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Tiger", PhotoFile = new byte[] { } });
            dbContext.Animals.Add(new Animal { Id = new Guid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Lion", PhotoFile = new byte[] { } });

            dbContext.SaveChanges();
            var service = new AnimalService(dbContext);

            var exception = Record.Exception(() => service.Delete(Guid.NewGuid(), true));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);

            var exampleGuid = Guid.NewGuid();
            dbContext.AnimalConditions.Add(new AnimalCondition() { Id = exampleGuid, Counter = 1, ClassColor = "1", ConditionDescription = "1" });

            var firstAddGuid = Guid.NewGuid();

            dbContext.Animals.Add(new Animal { Id = firstAddGuid, AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { Id = Guid.NewGuid(), ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Tiger", PhotoFile = new byte[] { }, AnimalConditionId = exampleGuid});
            dbContext.Animals.Add(new Animal { Id = Guid.NewGuid(), AnimalKindId = Guid.NewGuid(), AnimalKind = new AnimalKind() { ClassColor = "aaa", Counter = 1, AnimalsKind = "sss" }, AnimalName = "Lion", PhotoFile = new byte[] { }, AnimalConditionId = exampleGuid });

            dbContext.SaveChanges();
            var service = new AnimalService(dbContext);

            service.Edit(firstAddGuid, "Siberian Tiger", new DateTime(2022, 2, 2), new DateTime(2023, 2, 2), 3, exampleGuid, Guid.NewGuid(), Guid.NewGuid(), "abcdefg", new byte[] { });

            var editedData = service.EditViewData(firstAddGuid);
            Assert.Equal("Siberian Tiger", editedData.AnimalName);
            Assert.Equal(new DateTime(2022, 2, 2), editedData.InZooSince);
            Assert.Equal(new DateTime(2023, 2, 2), editedData.InZooAgainFrom);
            Assert.Equal(3, editedData.Count);
            Assert.Equal("abcdefg", editedData.Description);
        }

        [Fact]
        public void CreateVehicleReturnNewGuid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateNewsReturnNewGuid").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.SaveChanges();
            var service = new AnimalService(dbContext);

            var id = service.CreateAnimal("Lion", new DateTime(), new DateTime(), 2, new Guid(), new Guid(), new Guid(), "new animal", new byte[] { }, false);

            Assert.NotNull(id);
        }
    }
}
