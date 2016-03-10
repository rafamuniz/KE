using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerRepository : Repository<Customer, KEContext>, ICustomerRepository
    {
        #region Constructor
        public CustomerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
