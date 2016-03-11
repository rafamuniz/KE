using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserRepository : Repository<CustomerUser, KEContext>, ICustomerUserRepository
    {
        #region Constructor
        public CustomerUserRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<CustomerUser> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId).ToList();
        }
    }
}
