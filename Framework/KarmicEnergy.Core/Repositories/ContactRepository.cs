using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class ContactRepository : KERepositoryBase<Contact>, IContactRepository
    {
        #region Constructor
        public ContactRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              

        public List<Contact> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId && x.DeletedDate == null).ToList();
        }
        
        public override IEnumerable<Contact> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Contact> contacts = new List<Contact>();
            var entities = (from co in Context.Contacts
                            join c in Context.Customers on co.CustomerId equals c.Id
                            join s in Context.Sites on c.Id equals s.CustomerId
                            where c.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select co).ToList();

            foreach (var entity in entities)
            {
                Contact contact = new Contact()
                {
                    Id = entity.Id
                };

                contact.Update(entity);
                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
