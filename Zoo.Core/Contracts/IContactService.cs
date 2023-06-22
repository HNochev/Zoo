using Zoo.Core.Models.Contacts;

namespace Zoo.Core.Contracts
{
    public interface IContactService
    {
        public List<ContactsListingModel> All();

        public Guid CreateContact(
            string fullName,
            string email,
            string phoneNumber,
            string position,
            string? usernameInWebsite,
            string addedByUserId,
            bool isDeleted);

        public ContactAddFormModel EditViewData(Guid id);

        public bool Edit(
            Guid id,
            string fullName,
            string email,
            string phoneNumber,
            string position,
            string? usernameInWebsite);

        public ContactDeleteModel DeleteViewData(Guid id);

        public bool Delete(Guid id, bool isDeleted);
    }
}
