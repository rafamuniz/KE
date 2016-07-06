using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserRepository : KERepositoryBase<CustomerUser>, ICustomerUserRepository
    {
        #region Constructor
        public CustomerUserRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<CustomerUser> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId).ToList();
        }
    }
}
