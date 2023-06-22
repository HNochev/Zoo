using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Models.Contacts
{
    public class ContactsListingModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public string UsernameInWebsite { get; set; }

        public bool IsDeleted { get; set; }
    }
}
