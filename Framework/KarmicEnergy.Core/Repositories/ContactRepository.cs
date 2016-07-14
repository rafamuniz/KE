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
    }
}
