using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Animals
{
    public class AnimalConditionModel
    {
        public Guid Id { get; set; }

        public string ConditionDescription { get; set; }

        public string ClassColor { get; set; }

        public int Counter { get; set; }
    }
}
