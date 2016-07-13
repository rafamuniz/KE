using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerRepository : KERepositoryBase<Customer>, ICustomerRepository
    {
        #region Constructor
        public CustomerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
        
    }
}
