using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Photos
{
    public class PhotosForOneYearModel
    {
        public int Year { get; set; }

        public ICollection<PhotosListingModel> Photos { get; set; }
    }
}
