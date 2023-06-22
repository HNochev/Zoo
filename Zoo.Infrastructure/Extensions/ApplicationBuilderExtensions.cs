using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            SeedAnimalConditions(services);
            SeedAnimalEatingTypes(services);
            SeedAnimalKinds(services);
            SeedPhotoStatuses(services);
            SeedLineTypes(services);

            return app;
        }

        private static void SeedAnimalConditions(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.AnimalConditions.Any())
            {
                return;
            }

            data.AnimalConditions.AddRange(new[]
            {
                new AnimalCondition { ConditionDescription = "Може да бъде видяно в зоопарка", ClassColor = "table-success", Counter = 1},
                new AnimalCondition { ConditionDescription = "Зимуващо животно", ClassColor = "table-primary", Counter = 2},
                new AnimalCondition { ConditionDescription = "На визита в друг град", ClassColor = "table-info" , Counter = 3 },
                new AnimalCondition { ConditionDescription = "Отсъства поради друга причина", ClassColor = "table-warning" , Counter = 4},
            });

            data.SaveChanges();
        }

        private static void SeedAnimalEatingTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.AnimalEatingTypes.Any())
            {
                return;
            }

            data.AnimalEatingTypes.AddRange(new[]
            {
                new AnimalEatingType { EatingTypeDescription = "Растителноядни", ClassColor = "table-success", Counter = 1},
                new AnimalEatingType { EatingTypeDescription = "Месоядни", ClassColor = "table-danger", Counter = 2},
                new AnimalEatingType { EatingTypeDescription = "Всеядни", ClassColor = "table-warning" , Counter = 3 },
            });

            data.SaveChanges();
        }

        private static void SeedAnimalKinds(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.AnimalKinds.Any())
            {
                return;
            }

            data.AnimalKinds.AddRange(new[]
            {
                new AnimalKind { AnimalsKind = "Бозайници", ClassColor = "table-warning", Counter = 1},
                new AnimalKind { AnimalsKind = "Птици", ClassColor = "table-info", Counter = 2},
                new AnimalKind { AnimalsKind = "Влечуги", ClassColor = "table-success" , Counter = 3 },
                new AnimalKind { AnimalsKind = "Земноводни", ClassColor = "table-secondary" , Counter = 4 },
                new AnimalKind { AnimalsKind = "Риби", ClassColor = "table-primary" , Counter = 5 },
                new AnimalKind { AnimalsKind = "Насекоми", ClassColor = "table-danger" , Counter = 6 },
            });

            data.SaveChanges();
        }

        private static void SeedPhotoStatuses(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.PhotoStatuses.Any())
            {
                return;
            }

            data.PhotoStatuses.AddRange(new[]
            {
                new PhotoStatus { StatusDescription = "Одобрена фотография", ClassColor="table-success", Counter = 1},
                new PhotoStatus { StatusDescription = "Изчакваща одобрение фотография", ClassColor="table-warning", Counter = 2},
                new PhotoStatus { StatusDescription = "Отхвърлена фотография", ClassColor="table-danger", Counter = 3},

            });

            data.SaveChanges();
        }

        private static void SeedLineTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.TransportLineTypes.Any())
            {
                return;
            }

            data.TransportLineTypes.AddRange(new[]
            {
                new TransportLineType { VehicleTypeDescription = "Електрически автобуси", ClassColor="table-warning", Counter = 1},
                new TransportLineType { VehicleTypeDescription = "Тролейбуси", ClassColor="table-primary", Counter = 2},
                new TransportLineType { VehicleTypeDescription = "Дизелови автобуси", ClassColor="table-secondary", Counter = 3},
                new TransportLineType { VehicleTypeDescription = "Метанови автобуси", ClassColor="table-success", Counter = 4},
                new TransportLineType { VehicleTypeDescription = "Газови автобуси", ClassColor="table-info", Counter = 5},
                new TransportLineType { VehicleTypeDescription = "Трамваи", ClassColor="table-danger", Counter = 6},
            });

            data.SaveChanges();
        }
    }
}
