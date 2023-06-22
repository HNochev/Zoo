using Zoo.Core.Models.Photos;
using Zoo.Core.Models.Users;

namespace Zoo.Core.Contracts
{
    public interface IPhotoService
    {
        public Guid CreatePhoto(
            string? description,
            DateTime dateOfPicture,
            DateTime dateUploaded,
            string? location,
            bool isAuthor,
            string? userMessage,
            byte[] photoFile,
            Guid photoStatusId,
            string userId,
            Guid vehicleId,
            bool isApproved);

        public Guid GetPendingStatusId();

        public UserMyPhotosPaginationModel AllPhotosByUser(string id, int pageNo, int pageSize);

        public bool IsByUser(Guid photoId, string userId);


        public PhotoEditFormModel EditViewData(Guid id);

        public bool Edit(
            Guid id,
            string? Description,
            DateTime DateOfPicture,
            bool IsAuthor,
            string? Location,
            string? UserMessage);

        public PhotoDetailsModel Details(Guid id);

        public PhotoDeleteModel DeleteViewData(Guid id);

        public bool Delete(Guid id);

        public Guid IdOfVehicle(Guid photoId);
    }
}
