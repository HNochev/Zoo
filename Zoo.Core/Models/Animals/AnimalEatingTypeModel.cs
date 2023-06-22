using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Animals
{
    public class AnimalEatingTypeModel
    {
        public Guid Id { get; set; }

        public string EatingTypeDescription { get; set; }

        public string ClassColor { get; set; }

        public int Counter { get; set; }
    }
}
