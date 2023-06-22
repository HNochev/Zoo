using Zoo.Core.Contracts;
using Zoo.Core.Models.Contacts;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext data;

        public ContactService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<ContactsListingModel> All()
        {
            return this.data
                 .Contacts
                 .Where(x => !x.IsDeleted)
                 .Select(x => new ContactsListingModel
                 {
                     Id = x.Id,
                     Email = x.Email,
                     FullName = x.FullName,
                     IsDeleted = x.IsDeleted,
                     PhoneNumber = x.PhoneNumber,
                     Position = x.Position,
                     UsernameInWebsite = x.UsernameInWebsite,
                 })
                 .ToList();
        }

        public Guid CreateContact(string fullName, string email, string phoneNumber, string position, string? usernameInWebsite, string addedByUserId, bool isDeleted)
        {
            var newContact = new Contact
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Position = position,
                UsernameInWebsite = usernameInWebsite,
                IsDeleted = isDeleted,
                UserId = addedByUserId,
            };

            this.data.Contacts.Add(newContact);
            this.data.SaveChanges();

            return newContact.Id;
        }

        public ContactAddFormModel EditViewData(Guid id)
        {
            return this.data.Contacts
                .Where(x => x.Id == id)
                .Select(x => new ContactAddFormModel
                {
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Position = x.Position,
                    UsernameInWebsite = x.UsernameInWebsite,
                })
                .First();
        }

        public bool Edit(Guid id, string fullName, string email, string phoneNumber, string position, string? usernameInWebsite)
        {
            var contactData = this.data.Contacts.Find(id);

            if (contactData == null)
            {
                return false;
            }

            contactData.FullName = fullName;
            contactData.Email = email;
            contactData.PhoneNumber = phoneNumber;
            contactData.Position = position;
            contactData.UsernameInWebsite = usernameInWebsite;

            this.data.SaveChanges();

            return true;
        }

        public ContactDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Contacts
                .Where(x => x.Id == id)
                .Select(x => new ContactDeleteModel
                {
                    FullName = x.FullName,
                    Position = x.Position,
                })
                .First();
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var contactData = this.data.Contacts.Find(id);

            if (contactData == null)
            {
                return false;
            }

            contactData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }
    }
}
