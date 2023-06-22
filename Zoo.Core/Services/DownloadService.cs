using Microsoft.EntityFrameworkCore;
using Zoo.Core.Contracts;
using Zoo.Core.Models.Downloads;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly ApplicationDbContext data;

        public DownloadService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Guid CreateFile(string fileName, string? description, byte[] FilePdf, string userId)
        {
            var newDownload = new Download
            {
                FileName = fileName,
                Description = description,
                FilePDF = FilePdf,
                UserId = userId,
            };

            this.data.Downloads.Add(newDownload);
            this.data.SaveChanges();

            return newDownload.Id;
        }

        public List<DownloadsListingModel> All()
        {
            return this.data
                 .Downloads
                 .Select(x => new DownloadsListingModel
                 {
                     Id = x.Id,
                     FileName = x.FileName,
                     Description = x.Description,
                     UserId = x.UserId,
                     User = x.User,
                     DownloadUrlFromDatabase = "data:application/pdf;base64," + Convert.ToBase64String(x.FilePDF),
                 })
                 .ToList();
        }

        public async Task<Download> GetFile(Guid Id)
        {
            return await this.data.Downloads
                          .Where(x => x.Id == Id)
                          .Select(x => x)
                          .FirstAsync();
        }

        public bool Edit(Guid id, string fileName, string? description)
        {
            var downloadData = this.data.Downloads.Find(id);

            if (downloadData == null)
            {
                return false;
            }

            downloadData.FileName = fileName;
            downloadData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            var download = this.data.Downloads.Find(id);

            if (download == null)
            {
                return false;
            }

            this.data.Remove(download);

            this.data.SaveChanges();

            return true;
        }

        public DownloadEditFormModel EditViewData(Guid id)
        {
            return this.data.Downloads
                .Where(x => x.Id == id)
                .Select(x => new DownloadEditFormModel
                {
                    FileName = x.FileName,
                    Description = x.Description,
                })
                .First();
        }

        public DownloadDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Downloads
                .Where(x => x.Id == id)
                .Select(x => new DownloadDeleteModel
                {
                    FileName = x.FileName,
                })
                .First();
        }
    }
}
