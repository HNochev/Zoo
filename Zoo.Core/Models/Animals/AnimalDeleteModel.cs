using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.Animals
{
    public class AnimalDeleteModel
    {
        public string AnimalName { get; set; }

        public AnimalKind AnimalsKind { get; set; }

        public string ImgUrlFormDatabase { get; set; }
    }
}
