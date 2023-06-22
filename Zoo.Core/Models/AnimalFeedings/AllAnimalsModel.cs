using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.AnimalFeedings
{
    public class AllAnimalsModel
    {
        public Guid Id { get; set; }

        public string AnimalName { get; set; }

        public Guid AnimalConditionId { get; set; }

        public AnimalCondition AnimalCondition { get; set; }
    }
}
