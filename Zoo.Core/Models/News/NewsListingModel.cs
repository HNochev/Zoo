using Zoo.Infrastructure.Data.Models;

namespace Zoo.Core.Models.News
{
    public class NewsListingModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public string ImgUrl { get; set; }

        public string AuthorId { get; set; }

        public WebsiteUser Author { get; set; }

    }
}
