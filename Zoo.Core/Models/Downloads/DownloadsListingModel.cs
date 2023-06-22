using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Downloads
{
    public class DownloadsListingModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public string DownloadUrlFromDatabase { get; set; }
    }
}
