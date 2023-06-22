using Zoo.Core.Models.AnimalFeedings;
using Zoo.Core.Models.News;

namespace Zoo.Core.Models.Home
{
    public class HomePageModel
    {
        public IEnumerable<NewsListingModel> News { get; set; }

        public IEnumerable<FeedingListingModel> Feedings { get; set; }
    }
}
