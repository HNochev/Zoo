using Zoo.Core.Models.Downloads;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Contracts
{
    public interface IDownloadService
    {
        public Guid CreateFile(string fileName, string? description, byte[] FilePdf, string userId);

        public List<DownloadsListingModel> All();

        public Task<Download> GetFile(Guid Id);

        public bool Edit(Guid id, string fileName, string? description);

        public bool Delete(Guid id);

        public DownloadEditFormModel EditViewData(Guid id);

        public DownloadDeleteModel DeleteViewData(Guid id);
    }
}
